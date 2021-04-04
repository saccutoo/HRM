using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IDashboardRepository
    {
        HrmResultEntity<DashboardSummaryEntity> GetSummary(DateTime startDate, DateTime endDate, DateTime startPeriod, DateTime endPeriod, string dbName);
        HrmResultEntity<DashboardTurnoverRateEntity> GetTurnoverrate(DateTime startDate, DateTime endDate, int viewType, string dbName);
        HrmResultEntity<DashboardImplementationEntity> GetImplementation(DateTime startDate, DateTime endDate, int viewType, string dbName);
        HrmResultEntity<DashboardWorkingDayChart> GetWorkingDayChart(long staffId, int type, string year, string dbName);
        HrmResultEntity<DashboardEventEnity> GetStaffOnboardOrBirthdayByParentId(long staffId, string dbName);

    }
}
