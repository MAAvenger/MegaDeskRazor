using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class DeskQuote
    {
        public int DeskQuoteId { get; set; }

        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }

        public decimal QuotePrice { get; set; }

        //Foreign key
        public int ShippingId { get; set; }

        public int DeskId { get; set; }
        //Navigation
        public Shipping Shipping { get; set; }

        public Desk Desk { get; set; }
    }
}

