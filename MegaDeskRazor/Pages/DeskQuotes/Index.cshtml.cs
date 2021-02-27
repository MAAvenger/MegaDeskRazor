using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazor.Data;
using MegaDeskRazor.Models;

namespace MegaDeskRazor.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskRazor.Data.MegaDeskRazorContext _context;

        public IndexModel(MegaDeskRazor.Data.MegaDeskRazorContext context)
        {
            _context = context;
        }

        public IList<DeskQuote> DeskQuote { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var name = from n in _context.DeskQuote
                          select n;
            if (!string.IsNullOrEmpty(SearchString))
            {
                name = name.Where(s => s.CustomerName.Contains(SearchString));
            }
            DeskQuote = await _context.DeskQuote
                .Include(d => d.Desk)
                .Include(d => d.Desk.SurfaceMaterial)
                .Include(d => d.Shipping)
                .ToListAsync();
        }
    }
}
