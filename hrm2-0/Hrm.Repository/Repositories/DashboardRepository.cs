using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Hrm.Repository
{
    public partial class DashboardRepository : CommonRepository, IDashboardRepository
    {
        public HrmResultEntity<DashboardSummaryEntity> GetSummary(DateTime startDate, DateTime endDate,DateTime startPeriod, DateTime endPeriod, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StartDate", startDate);
            par.Add("@EndDate", endDate);
            par.Add("@StartPeriod", startPeriod);
            par.Add("@EndPeriod", endPeriod);
            par.Add("@DbName", dbName);
            return ListProcedure<DashboardSummaryEntity>("Dashboard_Get_GetSummary", par, dbName: dbName);
        }
        public HrmResultEntity<DashboardTurnoverRateEntity> GetTurnoverrate(DateTime startDate, DateTime endDate, int viewType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StartDate", startDate);
            par.Add("@EndDate", endDate);
            par.Add("@TypeView", viewType);
            par.Add("@DbName", dbName);
            return ListProcedure<DashboardTurnoverRateEntity>("Dashboard_Get_GetTurnoverrate", par, dbName: dbName);
        }
        public HrmResultEntity<DashboardImplementationEntity> GetImplementation(DateTime startDate, DateTime endDate, int viewType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StartDate", startDate);
            par.Add("@EndDate", endDate);
            par.Add("@TypeView", viewType);
            par.Add("@DbName", dbName);
            return ListProcedure<DashboardImplementationEntity>("Dashboard_Get_GetImplementation", par, dbName: dbName);
        }
        public HrmResultEntity<DashboardWorkingDayChart> GetWorkingDayChart(long staffId, int type, string year, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Type", type);
            par.Add("@Year", year);
            par.Add("@DbName", dbName);
            return ListProcedure<DashboardWorkingDayChart>("Dashboard_Get_GetWorkingDayChart", par, dbName: dbName);
        }

        public HrmResultEntity<DashboardEventEnity> GetStaffOnboardOrBirthdayByParentId(long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ParentId", staffId);
            par.Add("@DbName", dbName);
            return ListProcedure<DashboardEventEnity>("Dashboard_Get_GetStaffOnboardOrBirthdayByParentId", par, dbName: dbName);
        }
    }
}

