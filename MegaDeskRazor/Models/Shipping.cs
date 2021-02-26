using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }

        public string ShippingDays { get; set; }

        public int ShippingUnder1000 { get; set; }
        public int ShippingBtwn10002000 { get; set; }
        public int ShippingOver2000 { get; set; }

    }
}

