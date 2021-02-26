using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class Desk
    {
        public int DeskId { get; set; }
        public decimal Width { get; set; }

        public decimal Depth { get; set; }

        public int NumberofDrawer { get; set; }
        //Foreign Keys
        public int SurfaceMaterialId { get; set; }
        //Navigation
        public SurfaceMaterial SurfaceMaterial { get; set; }

    }
}
