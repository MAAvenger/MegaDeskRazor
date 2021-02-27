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

        [Display(Name = "Customer Name")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CustomerName { get; set; }

        [Display(Name = "Quote Date")]
        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }

        [Display(Name = "Quote Price")]
        public decimal QuotePrice { get; set; }

        //Foreign key
        public int ShippingId { get; set; }

        public int DeskId { get; set; }
        //Navigation
        public Shipping Shipping { get; set; }

        public Desk Desk { get; set; }

        public decimal GetAreaPrice()
        {
            decimal price = 0;
            int limit = 1000;
            decimal area = Desk.Width * Desk.Depth;
            if (area > limit)
            {
                price = area - limit;
            }
            return price;
        }

        public decimal GetDrawerPrice()
        {
            decimal drawePrice = 50;
            return drawePrice * Desk.NumberofDrawer;
        }

        public decimal GetMaterialPrice()
        {
            return Desk.SurfaceMaterial.SurfaceCost;
        }

        public decimal GetShipping()
        {
            decimal area = Desk.Width * Desk.Depth;

            if (area < 1000)
            {
                return Shipping.ShippingUnder1000;
            }
            else if (area < 2000)
            {
                return Shipping.ShippingBtwn10002000;
            }
            else if (area > 2000)
            {
                return Shipping.ShippingOver2000;
            }
            else
            {
                return 0;
            }
        }

        public decimal GetQuotePrice()
        {
            decimal Base = 200;
            this.QuotePrice = Base + GetAreaPrice() + GetDrawerPrice() + GetMaterialPrice() + GetShipping();
            return this.QuotePrice;
        }
    }
}

