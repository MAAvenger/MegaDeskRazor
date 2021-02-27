using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazor.Data;
using MegaDeskRazor.Models;

namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDeskRazor.Data.MegaDeskRazorContext _context;

        public EditModel(MegaDeskRazor.Data.MegaDeskRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Desk Desk { get; set; }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeskQuote = await _context.DeskQuote
                .Include(d => d.Desk)
                .Include(d => d.Shipping).FirstOrDefaultAsync(m => m.DeskQuoteId == id);

            if (DeskQuote == null)
            {
                return NotFound();
            }
            ViewData["SurfaceMaterialId"] = new SelectList(_context.Set<SurfaceMaterial>(), "SurfaceMaterialId", "SurfaceMaterialName");
            ViewData["ShippingId"] = new SelectList(_context.Set<Shipping>(), "ShippingId", "ShippingDays");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DeskQuote).State = EntityState.Modified;

            try
            {
                //add desk
                _context.Desk.Add(Desk);

                //set desk id
                DeskQuote.DeskId = Desk.DeskId;

                //set desk
                DeskQuote.Desk = Desk;

                //set quote date
                DeskQuote.QuoteDate = DateTime.Today;

                //set quote price
                DeskQuote.QuotePrice = DeskQuote.GetQuotePrice(_context);

                //set deskqoute
                _context.DeskQuote.Add(DeskQuote);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DeskQuoteExists(int id)
        {
            return _context.DeskQuote.Any(e => e.DeskQuoteId == id);
        }
    }
}
