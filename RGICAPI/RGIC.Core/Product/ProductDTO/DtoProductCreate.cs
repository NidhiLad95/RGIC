using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product.ProductDTO
{
    public class DtoProductCreate
    {

        public int? ProductSubclassID { get; set; }
        public string? NEPCalcType { get; set; }
        public int? ProductClassID { get; set; }
        public int? ProductCategoryID { get; set; }
        public string? ProductSubCategory { get; set; }
        public int? ProductGroupID { get; set; }
        public string? ODTPFilter { get; set; }
        public bool? RenewalMotor { get; set; }
        public bool? RenewalHealth { get; set; }
        public string? FuelType { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
