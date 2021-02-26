using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class SurfaceMaterial
    {
        public int SurfaceMaterialId { get; set; }
        public string SurfaceMaterialName { get; set; }
        public decimal SurfaceCost { get; set; }
    }
}
