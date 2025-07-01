using CRUDOperations;
using RGIC.Core.Common;
using RGIC.Core.Common.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Infrastructure
{
    public class CommonRepo(ICrudOperationService crudOperationService) : ICommonRepo
    {
        private readonly ICrudOperationService _crudOperationService = crudOperationService;
        public  async Task<ResponseList<DtoFetchDropdownList>> GetListAsync()
        {
            var response = await _crudOperationService.GetList<DtoFetchDropdownList>("[Common].[UspGetAllMasterDropdownData]",new DtoGetDropDown());
            return response;
        }
    }
}
