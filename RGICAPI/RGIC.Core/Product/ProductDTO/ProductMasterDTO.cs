using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product.ProductDTO
{
    public class ProductMasterDTO
    {
        public string? Product_Subclass { get; set; }

        public string? NEP_Clac_Type { get; set; }

        public string? Product_Class { get; set; }

        public string? Product_Category { get; set; }

        public string? Product_sub_Category { get; set; }

        public string? Product_Group { get; set; }

        public string? ODTP_Filter { get; set; }

        public bool Renewal_Motor { get; set; }

        public bool Renewal_Health { get; set; }

        public string? Fuel_Type { get; set; }

    }
}



