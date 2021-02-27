using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaDeskRazor.Models
{
    public class Desk
    {
        public int DeskId { get; set; }

        [Range(24, 96)]
        [Required]
        public decimal Width { get; set; }

        [Range(12, 48)]
        [Required]
        public decimal Depth { get; set; }

        [Display(Name = "Number of Drawers")]
        [Required]
        public int NumberofDrawer { get; set; }

        //Foreign Keys
        public int SurfaceMaterialId { get; set; }
        //Navigation
        public SurfaceMaterial SurfaceMaterial { get; set; }

    }
}
