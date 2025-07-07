using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product.ProductDTO
{
    public class DtoProductMaster
    {
        public int? productID { get; set; }
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
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}



