using RGIC.Core.ActivityLogger.AcitvityLoggerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGIC.Core.ActivityLogger
{
    public interface ILogUserActivityRepo
    {
        void SaveLog(LogUserActivity logUserActivity);
    }
}
