using CRUDOperations;
using RGIC.Core.Common.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.Common
{
    public interface ICommonRepo
    {
        Task<ResponseList<DtoFetchDropdownList>> GetListAsync();
    }
}
