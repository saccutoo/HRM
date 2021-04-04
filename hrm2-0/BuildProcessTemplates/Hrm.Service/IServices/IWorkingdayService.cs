using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IWorkingdayService : IBaseService
    {
        string GetWorkingdayByStaffAndMonth(long staffId, int month, int year);
        string GetWorkingdaySummaryByStaffAndDate(long staffId, DateTime date);
        string GetWorkingdayMachineByStaffAndMonth(long staffId, int month, int year);
        string GetWorkingdaySupplementByStaffAndMonth(long staffId, int month, int year, string filterString);
        string GetWorkingdaySupplementById(long id);
        string GetWorkingdaySupplementByStaffAndDate(long staffId, long roleId, long userId, DateTime date);
        string SaveWorkingdaySupplenment(WorkingdaySupplementEntity data, BasicParamType param);
        string GetWorkingdayFurlough(BasicParamType param, long staffId, long organizationId, out int totalRecord);
        string GetWorkingdayAllStaff(BasicParamType param, long staffId, int month, int year, long organizationId, out int totalRecord);
        string GetTableWorkingdaySupplementByStaffAndMonth(BasicParamType param, long staffId, int month, int year, out int totalRecord);
        string SaveSubmitApproval(List<WorkingdaySupplementApprovalType> type, long approvedBy, long roleApprovedBy, bool isApproval);
        string GetNeedApproveWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType);
        string SearchWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType, int filterTypeSupplement, string searchKey);
        string GetWorkingdaySupplementApprovalById(long requestId);
        string DeleteWorkingDaySupplement(long id);
        string DeleteListWorkingDaySupplement(string listStringId, long staffId);
        string SaveListWorkingDaySupplement(List<WorkingdaySupplementType> listDatal, long StaffId, bool isApprova);
        string GetTableNeedApproveWorkingdaySupplement(BasicParamType param, long staffId, int month, int year, out int totalRecord);
        string GetSummaryFinal(BasicParamType param, long staffId, int month, int year, out string outTotalJson, out int totalRecord);
        string CheckSaveWorkingdaySupplement(List<WorkingdaySupplementType> listData, DateTime date, long staffId);
        #region
        string GetWorkingDayHoliday(BasicParamType param, out int totalRecord);
        string SaveWorkingdayHoliday(WorkingdayHolidayEntity entity, List<ListDataSelectedIdType> mapList);
        string GetWorkingDayHolidayById(long id);
        string GetWorkingDayHolidayMapperByHolidayId(long holidayId);
        string DeleteWorkingdayHoliday(long id);
        string GetWorkingdayHolidayShiftId(long holidayId);
        string GetListDropdownWorkingdayShift();
        #endregion


        #region Workingday Shift
        string GetWorkingdayShift(BasicParamType param, out int totalRecord);
        string SaveWorkingdayShift(WorkingdayShiftEntity entity, List<WorkingdayShiftDetailType> mapList);
        string GetWorkingdayShiftById(long id);
        string GetWorkingdayShiftDetailByShiftId(long shiftId);
        string DeleteWorkingdayShift(long id);
        string GetWorkingdayShiftMapperByStaffId(long staffId);
        #endregion
        #region Workingday Period
        string  GetWorkingdayPeriod(BasicParamType param, out int totalRecord);
        string SaveWorkingdayCalculationPeriod(WorkingdayPeriodEntity entity, List<WorkDayType> mapList);
        string DeleteWorkingdayCalculationPeroid(long id);
        string GetWorkingDayCalculationPeriodById(long id);
        string GetWorkDayByPeroidId(long id);
        string GetCalculateWorkingDayPeriodByIsDefault();
        #endregion
        #region Workingday Annual Leave 
        string GetWorkingdayAnnualLeave(BasicParamType param, out int totalRecord);
        string SaveAnnualLeave(AnnualLeaveEntity entity, int type);
        string SaveWorkingdayAnnualLeave(WorkingdayAnnualLeaveEntity entity, List<WorkingdayAnnualLeaveStaffMapperType> mapList);
        string GetWorkingdayAnnualLeaveById(long id);
        string GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(long id);
        string DeleteWorkingdayAnnualLeaveById(long id);
        #endregion


        #region Data_Workingday_CheckInOut
        string GetWorkingCheckInOutByDate(DateTime date, long staffId);
        string SaveCheckinCheckoutFromMachine(WorkingdayCheckInOutEntity data);
        string GetCustomerByTimeKeeper(long timeKeeperId, long machineId);
        #endregion
    }
}
