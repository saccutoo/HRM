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
    public partial class WorkingdayService : IWorkingdayService
    {
        private string _dbName;
        private long _languageId;
        IWorkingdayRepository _workingdayRepository;
        public WorkingdayService(IWorkingdayRepository workingdayRepository)
        {
            this._workingdayRepository = workingdayRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
            _languageId = CurrentUser.LanguageId;
        }
        public string GetWorkingdayByStaffAndMonth(long staffId, int month, int year)
        {
            var response = this._workingdayRepository.GetWorkingdayByStaffAndMonth(staffId, month, year, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayMachineByStaffAndMonth(long staffId, int month, int year)
        {
            var response = this._workingdayRepository.GetWorkingdayMachineByStaffAndMonth(staffId, month, year, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdaySummaryByStaffAndDate(long staffId, DateTime date)
        {
            var response = this._workingdayRepository.GetWorkingdaySummaryByStaffAndDate(staffId, date, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdaySupplementByStaffAndMonth(long staffId, int month, int year, string filterString)
        {
            var response = this._workingdayRepository.GetWorkingdaySupplementByStaffAndMonth(staffId, month, year, filterString, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdaySupplementById(long id)
        {
            var response = this._workingdayRepository.GetWorkingdaySupplementById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string GetWorkingdaySupplementByStaffAndDate(long staffId, long roleId, long userId, DateTime date)
        {
            var response = this._workingdayRepository.GetWorkingdaySupplementByStaffAndDate(staffId, roleId, userId,date, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveWorkingdaySupplenment(WorkingdaySupplementEntity data, BasicParamType param)
        {
            var staffResponse = this._workingdayRepository.SaveWorkingdaySupplenment(data, param);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetWorkingdayFurlough(BasicParamType param, long staffId, long organizationId, out int totalRecord)
        {
            var staffResponse = this._workingdayRepository.GetWorkingdayFurlough(param, staffId, organizationId, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string GetWorkingdayAllStaff(BasicParamType param, long staffId, int month, int year, long organizationId, out int totalRecord)
        {
            var response = this._workingdayRepository.GetWorkingdayAllStaff(param, staffId, month, year, organizationId, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string GetTableWorkingdaySupplementByStaffAndMonth(BasicParamType param, long staffId, int month, int year, out int totalRecord)
        {
            var response = this._workingdayRepository.GetTableWorkingdaySupplementByStaffAndMonth(param, staffId, month , year,out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveSubmitApproval(List<WorkingdaySupplementApprovalType> type, long approvedBy, long roleApprovedBy, bool isApproval)
        {
            var response = this._workingdayRepository.SaveSubmitApproval(type, approvedBy, roleApprovedBy, isApproval, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetNeedApproveWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType)
        {
            var response = this._workingdayRepository.GetNeedApproveWorkingdaySupplement(staffId, userId, roleId, date, filterType, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SearchWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType, int filterTypeSupplement, string searchKey)
        {
            var response = this._workingdayRepository.SearchWorkingdaySupplement(_languageId, staffId, userId, roleId, date, filterType, filterTypeSupplement, searchKey, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdaySupplementApprovalById(long requestId)
        {
            var response = this._workingdayRepository.GetWorkingdaySupplementApprovalById(requestId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteWorkingDaySupplement(long id)
        {
            var response = this._workingdayRepository.DeleteWorkingDaySupplement(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteListWorkingDaySupplement(string listStringId, long staffId)
        {
            var response = this._workingdayRepository.DeleteListWorkingDaySupplement(listStringId, staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveListWorkingDaySupplement(List<WorkingdaySupplementType> listData, long StaffId, bool isApproval)
        {
            var response = this._workingdayRepository.SaveListWorkingDaySupplement(listData, StaffId, isApproval, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetTableNeedApproveWorkingdaySupplement(BasicParamType param, long staffId, int month, int year, out int totalRecord)
        {
            var response = this._workingdayRepository.GetTableNeedApproveWorkingdaySupplement(param, staffId,month,year, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string GetSummaryFinal(BasicParamType param, long staffId, int year, int month, out string outTotalJson, out int totalRecord)
        {
            var staffResponse = this._workingdayRepository.GetSummaryFinal(param, staffId, month, year, out outTotalJson, out totalRecord);
            return JsonConvert.SerializeObject(staffResponse);
        }
        public string CheckSaveWorkingdaySupplement(List<WorkingdaySupplementType> listData, DateTime date, long staffId)
        {
            var response = this._workingdayRepository.CheckSaveWorkingdaySupplement(listData, date, staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        #region holiday
        public string GetWorkingDayHoliday(BasicParamType param, out int totalRecord)
        {
            var response = this._workingdayRepository.GetWorkingDayHoliday(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }

        public string SaveWorkingdayHoliday(WorkingdayHolidayEntity entity, List<ListDataSelectedIdType> mapList)
        {
            var response = this._workingdayRepository.SaveWorkingdayHoliday(entity, mapList, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingDayHolidayById(long id)
        {
            var response = this._workingdayRepository.GetWorkingDayHolidayById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingDayHolidayMapperByHolidayId(long holidayId)
        {
            var response = this._workingdayRepository.GetWorkingDayHolidayMapperByHolidayId(holidayId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteWorkingdayHoliday(long id)
        {
            var response = this._workingdayRepository.DeleteWorkingdayHoliday(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayHolidayShiftId(long holidayId)
        {
            var response = this._workingdayRepository.GetWorkingdayHolidayShiftId(holidayId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        #endregion

        #region Workingday Shift
        public string GetWorkingdayShift(BasicParamType param, out int totalRecord)
        {
            var response = this._workingdayRepository.GetWorkingdayShift(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveWorkingdayShift(WorkingdayShiftEntity entity, List<WorkingdayShiftDetailType> mapList)
        {
            var response = this._workingdayRepository.SaveWorkingdayShift(entity, mapList, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayShiftById(long id)
        {
            var response = this._workingdayRepository.GetWorkingdayShiftById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayShiftDetailByShiftId(long shiftId)
        {
            var response = this._workingdayRepository.GetWorkingdayShiftDetailByShiftId(shiftId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteWorkingdayShift(long id)
        {
            var response = this._workingdayRepository.DeleteWorkingdayShift(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayShiftMapperByStaffId(long staffId)
        {
            var response = this._workingdayRepository.GetWorkingdayShiftMapperByStaffId(staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetListDropdownWorkingdayShift()
        {
            var response = this._workingdayRepository.GetListDropdownWorkingdayShift(_dbName);
            return JsonConvert.SerializeObject(response);
        }

        #endregion
        #region Workingday Period
        public string GetWorkingdayPeriod(BasicParamType param, out int totalRecord)
        {
            var response = this._workingdayRepository.GetWorkingdayPeriod(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveWorkingdayCalculationPeriod(WorkingdayPeriodEntity entity, List<WorkDayType> mapList)
        {
            var response = this._workingdayRepository.SaveWorkingdayCalculationPeriod(entity, mapList, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteWorkingdayCalculationPeroid(long id)
        {
            var response = this._workingdayRepository.DeleteWorkingdayCalculationPeroid(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingDayCalculationPeriodById(long id)
        {
            var response = this._workingdayRepository.GetWorkingDayCalculationPeriodById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkDayByPeroidId(long id)
        {
            var response = this._workingdayRepository.GetWorkDayByPeroidId(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetCalculateWorkingDayPeriodByIsDefault()
        {
            var response = this._workingdayRepository.GetCalculateWorkingDayPeriodByIsDefault(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        #endregion

        #region Workingday Annual Leave 
        public string GetWorkingdayAnnualLeave(BasicParamType param, out int totalRecord)
        {
            var response = this._workingdayRepository.GetWorkingdayAnnualLeave(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveAnnualLeave(AnnualLeaveEntity entity, int type)
        {
            var response = this._workingdayRepository.SaveAnnualLeave(entity,type, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string  SaveWorkingdayAnnualLeave(WorkingdayAnnualLeaveEntity entity, List<WorkingdayAnnualLeaveStaffMapperType> mapList)
        {
            var response = this._workingdayRepository.SaveWorkingdayAnnualLeave(entity, mapList, _dbName);
            return JsonConvert.SerializeObject(response);

        }
        public string GetWorkingdayAnnualLeaveById(long id)
        {
            var response = this._workingdayRepository.GetWorkingdayAnnualLeaveById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(long id)
        {
            var response = this._workingdayRepository.GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteWorkingdayAnnualLeaveById(long id)
        {
            var response = this._workingdayRepository.DeleteWorkingdayAnnualLeaveById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        #endregion


        #region Data_Workingday_CheckInOut

        public string GetWorkingCheckInOutByDate(DateTime date, long staffId)
        {
            var response = this._workingdayRepository.GetWorkingCheckInOutByDate(date, staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveCheckinCheckoutFromMachine(WorkingdayCheckInOutEntity data)
        { 
            var response = this._workingdayRepository.SaveCheckinCheckoutFromMachine(data, data.DBName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetCustomerByTimeKeeper(long timeKeeperId, long machineId)
        {
            var response = this._workingdayRepository.GetCustomerByTimeKeeper(timeKeeperId, machineId);
            return JsonConvert.SerializeObject(response);
        }
    #endregion
    }
}
