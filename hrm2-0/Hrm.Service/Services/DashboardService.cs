using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Hrm.Repository.Type;



namespace Hrm.Service
{
    public partial class DashboardService : IDashboardService
    {
        IDashboardRepository _dashboardRepository;
        private string _dbName;
        private long _userId;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
                this._userId = CurrentUser.UserId;
            }
        }
        public string GetSummary(DateTime startDate, DateTime endDate, DateTime startPeriod, DateTime endPeriod)
        {
            var response = this._dashboardRepository.GetSummary(startDate, endDate, startPeriod, endPeriod, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetTurnoverrate(DateTime startDate, DateTime endDate, int viewType)
        {
            var response = this._dashboardRepository.GetTurnoverrate(startDate, endDate, viewType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetImplementation(DateTime startDate, DateTime endDate, int viewType)
        {
            var response = this._dashboardRepository.GetImplementation(startDate, endDate, viewType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingDayChart(int type,string year, long staffId )
        {
            var response = this._dashboardRepository.GetWorkingDayChart(staffId, type, year, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetStaffOnboardOrBirthdayByParentId()
        {
            var response = this._dashboardRepository.GetStaffOnboardOrBirthdayByParentId(_userId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}

