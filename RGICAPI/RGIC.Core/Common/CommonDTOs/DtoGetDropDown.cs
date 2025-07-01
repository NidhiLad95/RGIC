using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CommonDTOs
{
    public class DtoGetDropDown
    {
        [Required]
        public string? Type
        {
            get; set;
        }
    }
}
