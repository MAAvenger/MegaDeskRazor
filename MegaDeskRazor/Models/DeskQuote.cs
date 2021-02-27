using MegaDeskRazor.Data;
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

        public decimal GetQuotePrice(MegaDeskRazorContext context)
        {
            decimal Base = 200;

            //get material price from db

            decimal materialPrice = 0;

            var materialPrices = context.SurfaceMaterial
                                    .Where(d => d.SurfaceMaterialId == this.Desk.SurfaceMaterialId).FirstOrDefault();

            materialPrice = materialPrices.SurfaceCost;

            //get shipping price from db
            decimal shippingCost = 0;

            var shippingCosts = context.Shipping
                                .Where(s => s.ShippingId == this.ShippingId).FirstOrDefault();
            
            decimal area = this.Desk.Width * this.Desk.Depth;

            if (area < 1000)
            {
                shippingCost = shippingCosts.ShippingUnder1000;
            }
            else if (area < 2000)
            {
                shippingCost = shippingCosts.ShippingBtwn10002000;
            }
            else if (area > 2000)
            {
                shippingCost = shippingCosts.ShippingOver2000;
            }
            else
            {
                shippingCost = 0;
            }
            this.QuotePrice = Base + GetAreaPrice() + GetDrawerPrice() + materialPrice + (decimal)shippingCost;

            return this.QuotePrice;
        }
    }
}

