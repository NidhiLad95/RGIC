using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Product.ProductDTO
{
    public class DtoProductDelete
    {
        public int? productID { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
