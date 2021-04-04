using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IDashboardService : IBaseService
    {
        string GetSummary(DateTime startDate, DateTime endDate, DateTime startPeriod, DateTime endPeriod);
        string GetTurnoverrate(DateTime startDate, DateTime endDate, int viewType);
        string GetImplementation(DateTime startDate, DateTime endDate, int viewType);
        string GetWorkingDayChart(int type, string year, long staffId);
        string GetStaffOnboardOrBirthdayByParentId();
    }
}
