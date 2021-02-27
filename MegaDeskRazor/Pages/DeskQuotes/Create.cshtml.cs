using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskRazor.Data;
using MegaDeskRazor.Models;

namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskRazor.Data.MegaDeskRazorContext _context;

        public CreateModel(MegaDeskRazor.Data.MegaDeskRazorContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SurfaceMaterialId"] = new SelectList(_context.Set<SurfaceMaterial>(), "SurfaceMaterialId", "SurfaceMaterialName");
        ViewData["ShippingId"] = new SelectList(_context.Set<Shipping>(), "ShippingId", "ShippingDays");
            return Page();
        }

        [BindProperty]
        public Desk Desk { get; set; }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //add desk
            _context.Desk.Add(Desk);
            await _context.SaveChangesAsync();

            //set desk id
            DeskQuote.DeskId = Desk.DeskId;

            //set desk
            DeskQuote.Desk = Desk;

            //set quote date
            DeskQuote.QuoteDate = DateTime.Today;

            DeskQuote.Shipping = DeskQuote.Shipping;
            DeskQuote.Desk.SurfaceMaterial = Desk.SurfaceMaterial;
            //set quote price
            DeskQuote.QuotePrice = DeskQuote.GetQuotePrice(_context);

            //set deskqoute
            _context.DeskQuote.Add(DeskQuote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
