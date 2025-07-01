using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Common.CommonDTOs
{
    public class DTOActivityFilterResponse
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }

        public string? FileDownloadName { get; set; }
    }
}
