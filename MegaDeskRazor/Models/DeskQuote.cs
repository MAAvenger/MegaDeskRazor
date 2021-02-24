using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace MegaDesk_Tsao
{
    //for surface material dropdown in AddQuote.cs
    public enum SurfaceMaterial
    {
        Pine = 50,
        Laminate = 100,
        Veneer = 125,
        Oak = 200,
        Rosewood = 300
    }

    //for shipping drop down in AddQuote.cs
    public enum Shipping
    {
        [Description("Rush 3 days")]
        Rush3Day,
        [Description("Rush 5 days")]
        Rush5Day,
        [Description("Rush 7 days")]
        Rush7Day,
        [Description("No Rush")]
        NoRush
    }

    public class DeskQuote
    {
        //create and fill 2d array ShippingPrice with values from GetRushOrder()
        private decimal[,] ShippingPrice = GetRushOrder();
        //this value is always added to the price at the end
        private decimal BasePrice = 200;

        public string CustomerName { get; set; }

        public DateTime QuoteDate { get; set; }

        public decimal QuotePrice { get; set; }

        public Shipping Shipping { get; set; }

        public Desk Desk { get; set; }

        public static decimal[,] GetRushOrder()
        {
            int size = 3;
            decimal[,] rushOrderValue = new decimal[size, size];
            string[] readLine;

            string file = @"\rushOrderPrices.txt";

            readLine = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + file);

            int i = 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    rushOrderValue[y, x] = decimal.Parse(readLine[i]);
                    i++;
                }
            }
            Console.WriteLine("2D created");
            return rushOrderValue;
        }

        public decimal GetBasePrice()
        {
            return BasePrice;
        }

        public decimal GetQuotePrice(SurfaceMaterial material, int shippingIndex)
        {
            decimal Base = 200;
            this.QuotePrice = Base + GetAreaPrice() + GetDrawerPrice() + GetMaterialPrice(material) + GetShipping(shippingIndex);
            return this.QuotePrice;
        }

        public decimal GetAreaPrice()
        {
            decimal price = 0;
            int limit = 1000;
            if(Desk.GetArea() > limit)
            {
                price = Desk.GetArea() - limit;
            }
            return price;
        }

        public decimal GetDrawerPrice()
        {
            decimal drawePrice = 50;
            return drawePrice * Desk.NumberofDrawer;
        }

        public decimal GetMaterialPrice(SurfaceMaterial material)
        {
            return (decimal)material;
        }

        public decimal GetShipping(int shippingIndex)
        {
            if(Desk.GetArea() < 1000 && shippingIndex != 3)
            {
                return ShippingPrice[shippingIndex, 0];
            }
            else if(Desk.GetArea() < 2000 && shippingIndex != 3)
            {
                return ShippingPrice[shippingIndex, 1];
            }
            else if(Desk.GetArea() > 2000 && shippingIndex != 3)
            {
                return ShippingPrice[shippingIndex, 2];
            }
            else
            {
                return 0;
            }
        }
        
        //for construction of shipping drop down in AddQuote
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
        ///////////////////////////////////////////////////////////
    }
}
