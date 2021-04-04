using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IWorkingdayRepository
    {
        HrmResultEntity<WorkingdayEntity> GetWorkingdayByStaffAndMonth(long staffId, int month, int year, string dbName);
        HrmResultEntity<WorkingdayEntity> GetWorkingdaySummaryByStaffAndDate(long staffId, DateTime date, string dbName);
        HrmResultEntity<WorkingdayCheckInOutEntity> GetWorkingdayMachineByStaffAndMonth(long staffId, int month, int year, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementByStaffAndMonth(long staffId, int month, int year, string filterString, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementById(long id, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementByStaffAndDate(long staffId, long roleId, long userId, DateTime date, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> SaveWorkingdaySupplenment(WorkingdaySupplementEntity data, BasicParamType param);
        HrmResultEntity<WorkingdayFurlough> GetWorkingdayFurlough(BasicParamType param, long starffId, long organizationId, out int totalRecord);
        HrmResultEntity<WorkingdayAllStaffEntity> GetWorkingdayAllStaff(BasicParamType param, long staffId, int month, int year, long organizationId, out int totalRecord);
        HrmResultEntity<WorkingdaySupplementEntity> GetTableWorkingdaySupplementByStaffAndMonth(BasicParamType param, long staffId, int month, int year, out int totalRecord);
        HrmResultEntity<WorkingdaySupplementEntity> SaveSubmitApproval(List<WorkingdaySupplementApprovalType> type, long approvedBy, long roleApprovedBy, bool isApproval, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> GetNeedApproveWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> SearchWorkingdaySupplement(long languageId, long staffId, long userId, long roleId, DateTime date, int filterType, int filterTypeSupplement, string searchKey, string dbName);
        HrmResultEntity<WorkingdaySupplementApprovalEntity> GetWorkingdaySupplementApprovalById(long requestId, string dbName);
        HrmResultEntity<bool> DeleteWorkingDaySupplement(long id, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> DeleteListWorkingDaySupplement(string listStringId, long staffId, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> SaveListWorkingDaySupplement(List<WorkingdaySupplementType> listData, long StaffId,bool isApproval, string dbName);
        HrmResultEntity<WorkingdaySupplementEntity> GetTableNeedApproveWorkingdaySupplement(BasicParamType param, long staffId, int month, int year, out int totalRecord);
        HrmResultEntity<WorkingdayEntity> GetSummaryFinal(BasicParamType param, long staffId, int month, int year, out string outTotalJson, out int totalRecord);

        HrmResultEntity<WorkingdaySupplementEntity> CheckSaveWorkingdaySupplement(List<WorkingdaySupplementType> listData, DateTime date, long staffId, string dbName);
        #region Workingday-holiday
        HrmResultEntity<WorkingdayHolidayEntity> GetWorkingDayHoliday(BasicParamType param, out int totalRecord);
        HrmResultEntity<WorkingdayHolidayEntity> SaveWorkingdayHoliday(WorkingdayHolidayEntity entity, List<ListDataSelectedIdType> mapList, string dbName);
        HrmResultEntity<WorkingdayHolidayEntity> GetWorkingDayHolidayById(long id, string dbName);
        HrmResultEntity<WorkingdayHolidayMapperEntity> GetWorkingDayHolidayMapperByHolidayId(long holidayId, string dbName);
        HrmResultEntity<WorkingdayHolidayEntity> DeleteWorkingdayHoliday(long id, string dbName);
        HrmResultEntity<WorkingdayHolidayShiftEntity> GetWorkingdayHolidayShiftId(long holidayId, string dbName);
        #endregion
      
        #region Workingday Shift
        HrmResultEntity<WorkingdayShiftEntity> GetWorkingdayShift(BasicParamType param, out int totalRecord);
        HrmResultEntity<WorkingdayHolidayEntity> SaveWorkingdayShift(WorkingdayShiftEntity entity, List<WorkingdayShiftDetailType> mapList, string dbName);
        HrmResultEntity<WorkingdayShiftEntity> GetWorkingdayShiftById(long id, string dbName);
        HrmResultEntity<WorkingdayShiftDetailEntity> GetWorkingdayShiftDetailByShiftId(long shiftId, string dbName);
        HrmResultEntity<WorkingdayHolidayEntity> DeleteWorkingdayShift(long id, string dbName);
        HrmResultEntity<WorkingdayShiftMapperEntity> GetWorkingdayShiftMapperByStaffId(long staffId, string dbName);
        HrmResultEntity<WorkingdayShiftEntity> GetListDropdownWorkingdayShift(string dbName);
        #endregion
        #region Workingday Period
        HrmResultEntity<WorkingdayPeriodEntity> GetWorkingdayPeriod(BasicParamType param, out int totalRecord);
        HrmResultEntity<WorkingdayPeriodEntity> SaveWorkingdayCalculationPeriod(WorkingdayPeriodEntity entity, List<WorkDayType> mapList, string dbName);
        HrmResultEntity<WorkingdayPeriodEntity> DeleteWorkingdayCalculationPeroid(long id, string dbName);
        HrmResultEntity<WorkingdayPeriodEntity> GetWorkingDayCalculationPeriodById(long id, string dbName);
        HrmResultEntity<WorkDayPeriodEntity> GetWorkDayByPeroidId(long id, string dbName);
        HrmResultEntity<WorkingdayPeriodEntity> GetCalculateWorkingDayPeriodByIsDefault(string dbName);
        #endregion
        #region Workingday Annual Leave 
        HrmResultEntity<WorkingdayAnnualLeaveEntity> GetWorkingdayAnnualLeave(BasicParamType param, out int totalRecord);
        HrmResultEntity<AnnualLeaveEntity> SaveAnnualLeave(AnnualLeaveEntity entity, int type, string dbName);
        HrmResultEntity<WorkingdayAnnualLeaveEntity> SaveWorkingdayAnnualLeave(WorkingdayAnnualLeaveEntity entity, List<WorkingdayAnnualLeaveStaffMapperType> mapList, string dbName);
        HrmResultEntity<WorkingdayAnnualLeaveEntity> GetWorkingdayAnnualLeaveById(long id, string dbName);
        HrmResultEntity<WorkingdayAnnualLeaveStaffMapperEntity> GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(long annualLeaveId, string dbName);
        HrmResultEntity<WorkingdayAnnualLeaveEntity> DeleteWorkingdayAnnualLeaveById(long id, string dbName);
        #endregion

        #region Data_Workingday_CheckInOut
        HrmResultEntity<WorkingdayCheckInOutEntity> GetWorkingCheckInOutByDate(DateTime date, long staffId, string dbName);
        HrmResultEntity<WorkingdayCheckInOutEntity> SaveCheckinCheckoutFromMachine(WorkingdayCheckInOutEntity data, string dbName);
        HrmResultEntity<WorkingdayCheckInOutEntity> GetCustomerByTimeKeeper(long timeKeeperId, long machineId);
        #endregion
    }
}
