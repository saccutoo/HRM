using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Repository
{
    public class WorkingdayRepository : CommonRepository, IWorkingdayRepository
    {
        public HrmResultEntity<WorkingdayEntity> GetWorkingdayByStaffAndMonth(long staffId, int month, int year, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdayEntity>("WorkingdaySummary_Get_GetWorkingdayByStaffAndMonth", par, dbName: dbName);
        }

        public HrmResultEntity<WorkingdayEntity> GetWorkingdaySummaryByStaffAndDate(long staffId,DateTime date,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Date", date);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdayEntity>("WorkingdaySummary_Get_GetWorkingdaySummaryByStaffAndDate", par, dbName: dbName);
        }

        public HrmResultEntity<WorkingdayCheckInOutEntity> GetWorkingdayMachineByStaffAndMonth(long staffId, int month, int year, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdayCheckInOutEntity>("WorkingdaySummary_Get_GetWorkingdayByStaffAndMonth", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementByStaffAndMonth(long staffId, int month, int year, string filterString, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@FilterString", filterString);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetWorkingdaySupplementByStaffAndMonth", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetWorkingdaySupplementById", par, dbName: dbName);
        }

        public HrmResultEntity<WorkingdaySupplementEntity> GetWorkingdaySupplementByStaffAndDate(long staffId,long roleId,long userId, DateTime date, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@roleId", roleId);
            par.Add("@userId", userId);
            par.Add("@Date", date);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetWorkingdaySupplementByStaffAndDate", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdaySupplementEntity> SaveWorkingdaySupplenment(WorkingdaySupplementEntity data, BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", data.StaffId);
            par.Add("@SupplementTypeId", data.SupplementTypeId);
            par.Add("@Date", data.Date);
            par.Add("@StartTime", data.StartTime);
            par.Add("@EndTime", data.EndTime);
            par.Add("@MissingTimeDuration", data.MissingTimeDuration);
            par.Add("@OvertimeRate", data.OvertimeRate);
            par.Add("@ResonTypeId", data.ReasonTypeId);
            par.Add("@Note", data.Note);
            par.Add("@RequestStatusId", data.RequestStatusId);
            par.Add("CreatedBy", param.UserId);
            par.Add("UpdatedBy", param.UserId);
            par.Add("@DbName", param.DbName);
            par.Add("@Id", data.Id, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Update_SaveWorkingdaySupplenment", par);
            return result;
        }
        public HrmResultEntity<WorkingdayFurlough> GetWorkingdayFurlough(BasicParamType param, long starffId, long organizationId, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", starffId);
            par.Add("@OrganizationId", organizationId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayFurlough>("WorkingdayAnnualLeave_Get_GetWorkingdayFurlough", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                var parCache = HttpRuntime.Cache.Get(param.DbName + "-WorkingdayAnnualLeave_Get_GetWorkingdayFurlough-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                if (parCache != null)
                {
                    par = parCache;
                }
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingdayAllStaffEntity> GetWorkingdayAllStaff(BasicParamType param,long staffId,int month,int year,long organizationId, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@OrganizationId", organizationId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayAllStaffEntity>("WorkingdaySummary_Get_GetWorkingdayAllStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-WorkingdaySummary_Get_GetWorkingdayAllStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingdaySupplementEntity> GetTableWorkingdaySupplementByStaffAndMonth(BasicParamType param, long staffId, int month, int year, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetTableWorkingdaySupplementByStaffAndMonth", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingdaySupplementEntity> SaveSubmitApproval(List<WorkingdaySupplementApprovalType> type, long approvedBy, long roleApprovedBy, bool isApproval, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@WorkingdaySupplementApprovalType", type.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@ApprovedBy", approvedBy);
            par.Add("@roleApprovedBy", roleApprovedBy);
            par.Add("@IsApproval", isApproval);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplementApproval_Update_SaveSubmitApproval", par);
            return result;
        }
        public HrmResultEntity<WorkingdaySupplementEntity> GetNeedApproveWorkingdaySupplement(long staffId, long userId, long roleId, DateTime date, int filterType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@UserId", userId);
            par.Add("@RoleId", roleId);
            par.Add("@Date", date);
            par.Add("@FilterType", filterType);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetNeedApproveWorkingdaySupplement", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdaySupplementEntity> SearchWorkingdaySupplement(long languageId, long staffId, long userId, long roleId, DateTime date, int filterType, int filterTypeSupplement, string searchKey, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@StaffId", staffId);
            par.Add("@UserId", userId);
            par.Add("@RoleId", roleId);
            par.Add("@Date", date);
            par.Add("@FilterType", filterType);
            par.Add("@FilterTypeSupplement", filterTypeSupplement);
            par.Add("@SearchKey", searchKey);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_SearchWorkingdaySupplement", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdaySupplementApprovalEntity> GetWorkingdaySupplementApprovalById(long requestId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@RequestId", requestId);
            par.Add("@DbName", dbName);
            return ListProcedure<WorkingdaySupplementApprovalEntity>("WorkingdaySupplementApproval_Get_GetWorkingdaySupplementApprovalById", par, dbName: dbName);
        }
        public HrmResultEntity<bool> DeleteWorkingDaySupplement(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = Procedure("WorkingDay_Del_DeleteWorkingDaySupplement", par);
            return result;
        }
        public HrmResultEntity<WorkingdaySupplementEntity> DeleteListWorkingDaySupplement(string listStringId, long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@ListStringId", listStringId);
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Del_DeleteListWorkingDaySupplement", par);
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementEntity> SaveListWorkingDaySupplement(List<WorkingdaySupplementType> listData, long StaffId,bool isApproval, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@WorkingdaySupplementType", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", StaffId);
            par.Add("@IsApproval", isApproval);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Update_SaveListWorkingdaySupplenment", par);
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementEntity> GetTableNeedApproveWorkingdaySupplement(BasicParamType param, long staffId,int month,int year, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_GetTableNeedApproveWorkingdaySupplement", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingdayEntity> GetSummaryFinal(BasicParamType param,long staffId, int month,int year,out string outTotalJson, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@Month", month);
            par.Add("@Year", year);
            par.Add("@OutTotalJson", "", DbType.String, ParameterDirection.InputOutput);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayEntity>("WorkingdaySummary_Get_GetWorkingdaySummaryFinal", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                outTotalJson= par.GetDataOutput<string>("@OutTotalJson");
                totalRecord = par.GetDataOutput<int>("@TotalRecord");

            }

            else
            {
                totalRecord = 0;
                outTotalJson = string.Empty;
            }
            return result;
        }

        public HrmResultEntity<WorkingdaySupplementEntity> CheckSaveWorkingdaySupplement(List<WorkingdaySupplementType> listData, DateTime date, long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@WorkingdaySupplementType", listData.ConvertToUserDefinedDataTable(), DbType.Object);
            par.Add("@Date", date);
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdaySupplementEntity>("WorkingdaySupplement_Get_CheckSaveWorkingdaySupplement", par, dbName: dbName);
            return result;
        }

        #region Workingdya-holiday
        public HrmResultEntity<WorkingdayHolidayEntity> GetWorkingDayHoliday(BasicParamType param, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayHoliday_Get_GetWorkingdayHoliday", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }

        public HrmResultEntity<WorkingdayHolidayEntity> SaveWorkingdayHoliday(WorkingdayHolidayEntity entity, List<ListDataSelectedIdType> mapList, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.Id);
            par.Add("@Name", entity.Name);
            par.Add("@ClassifyId", entity.ClassifyId);
            par.Add("@IsAnnualLeave", entity.IsAnnualLeave);
            par.Add("@SalaryRegimeId", entity.SalaryRegimeId);
            par.Add("@DesistRegimeId", entity.DesistRegimeId);
            par.Add("@OvertimeRate", entity.OvertimeRate);
            par.Add("@FromDate", entity.FromDate);
            par.Add("@ToDate", entity.ToDate);
            par.Add("@Note", entity.Note);
            par.Add("@CreatedBy", entity.CreatedBy);
            par.Add("@DbName", dbName);
            par.Add("@ListData", mapList.ConvertToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayHoliday_Update_SaveWorkingdayHoliday", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdayHolidayEntity> GetWorkingDayHolidayById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayHoliday_Get_GetWorkingDayHolidayById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayHolidayMapperEntity> GetWorkingDayHolidayMapperByHolidayId(long holidayId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@HolidayId", holidayId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayHolidayMapperEntity>("WorkingdayHoliday_Get_GetWorkingDayHolidayMapperByHolidayId", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdayHolidayEntity> DeleteWorkingdayHoliday(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayHoliday_Del_DeleteWorkingdayHoliday", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdayHolidayShiftEntity> GetWorkingdayHolidayShiftId(long holidayId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@HolidayId", holidayId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayHolidayShiftEntity>("WorkingdayHoliday_Get_GetWorkingDayHolidayMapperByHolidayId", par, dbName: dbName);
            return result;
        }

        #endregion 

        #region Workingday Shift
        public HrmResultEntity<WorkingdayShiftEntity> GetWorkingdayShift(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayShiftEntity>("WorkingdayShift_Get_GetWorkingdayShift", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-WorkingdayShift_Get_GetWorkingdayShift-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<WorkingdayHolidayEntity> SaveWorkingdayShift(WorkingdayShiftEntity entity, List<WorkingdayShiftDetailType> mapList, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.ShiftId);
            par.Add("@Name", entity.Name);
            par.Add("@PeriodId", entity.PeriodId);
            par.Add("@DatabaseName", entity.DatabaseName);
            par.Add("@IsOverNight", entity.IsOverNight);
            par.Add("@LateAllowed", entity.LateAllowed);
            par.Add("@EarlyAllowed", entity.EarlyAllowed);
            par.Add("@WorkId", entity.WorkId);
            par.Add("@Status", entity.Status);
            par.Add("@IsDefault", entity.IsDefault);
            par.Add("@ToShift", entity.ToShift.ToString(),DbType.Time);
            par.Add("@FromShift", entity.FromShift.ToString(), DbType.Time);
            par.Add("@CreatedBy", entity.CreatedBy);
            par.Add("@DbName", dbName);
            par.Add("@WorkingdayShiftDetailType", mapList.ConvertToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayShift_Update_SaveWorkingdayShift", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayHolidayEntity> DeleteWorkingdayShift(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayHolidayEntity>("WorkingdayShift_Del_DeleteWorkingdayShift", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayShiftEntity> GetWorkingdayShiftById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayShiftEntity>("WorkingdayShift_Get_GetWorkingdayShiftById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayShiftDetailEntity> GetWorkingdayShiftDetailByShiftId(long shiftId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@shiftId", shiftId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayShiftDetailEntity>("WorkingdayShift_Get_GetWorkingdayShiftDetailByShiftId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayShiftMapperEntity> GetWorkingdayShiftMapperByStaffId(long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayShiftMapperEntity>("WorkingdayShift_Get_GetWorkingdayShiftMapperByStaffId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayShiftEntity> GetListDropdownWorkingdayShift(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayShiftEntity>("WorkingdayShift_Get_GetListDropdownWorkingdayShift", par, dbName: dbName);
            return result;
        }

        #endregion

        #region Workingday Period
        public HrmResultEntity<WorkingdayPeriodEntity> GetWorkingdayPeriod(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayPeriodEntity>("WorkingDayCalculationPeriod_Get_GetPeriod", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }
        public HrmResultEntity<WorkingdayPeriodEntity> SaveWorkingdayCalculationPeriod(WorkingdayPeriodEntity entity, List<WorkDayType> mapList, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.AutoId);
            par.Add("@Name", entity.Name);
            par.Add("@FromDate", entity.FromDate);
            par.Add("@ToDate", entity.ToDate);
            par.Add("@Status", entity.Status);
            par.Add("@Note", entity.Note);
            par.Add("@IsDefault", entity.IsDefault);
            par.Add("@UserID", entity.CreatedBy);
            par.Add("@MaximumEdition", entity.MaximumEdition);
            par.Add("@DbName", dbName);
            par.Add("@WorkDayPeriodType", mapList.ConvertToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<WorkingdayPeriodEntity>("WorkingDayCalculationPeriod_Update_SaveCalculateWorkingDayPeriod", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayPeriodEntity> DeleteWorkingdayCalculationPeroid(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayPeriodEntity>("WorkingDayCalculationPeriod_Del_DeleteCalculateWorkingDayPeriod", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayPeriodEntity> GetWorkingDayCalculationPeriodById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayPeriodEntity>("WorkingDayCalculationPeriod_Get_GetCalculateWorkingDayPeriodById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkDayPeriodEntity> GetWorkDayByPeroidId(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@PeriodID", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkDayPeriodEntity>("WorkingDayCalculationPeriod_Get_GetWorkDayByPeroidId", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayPeriodEntity> GetCalculateWorkingDayPeriodByIsDefault(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayPeriodEntity>("WorkingDayCalculationPeriod_Get_GetCalculateWorkingDayPeriodByIsDefault", par, dbName: dbName);
            return result;
        }

        #endregion


        #region Workingday Annual Leave 
        public HrmResultEntity<WorkingdayAnnualLeaveEntity> GetWorkingdayAnnualLeave(BasicParamType param, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<WorkingdayAnnualLeaveEntity>("WorkingdayAnnualLeave_Get_GetWorkingdayAnnualLeave", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-WorkingdayAnnualLeave_Get_GetWorkingdayAnnualLeave-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<AnnualLeaveEntity> SaveAnnualLeave(AnnualLeaveEntity entity,int type, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.Id);
            par.Add("@AnnualLeave", entity.AnnualLeave);
            par.Add("@AnnualLeaveSeniority", entity.AnnualLeaveSeniority);
            par.Add("@NumberOfLeaveGranted", entity.NumberOfLeaveGranted);
            par.Add("@HandlingAnnualLeaveBacklog", entity.HandlingAnnualLeaveBacklog);
            par.Add("@DateRemoveSurplusAnnualLeave", entity.DateRemoveSurplusAnnualLeave);
            par.Add("@Type", type);
            par.Add("@DbName", dbName);
            return ListProcedure<AnnualLeaveEntity>("WorkingdayAnnualLeave_Update_SaveAnnualLeave", par, dbName: dbName);
        }
        public HrmResultEntity<WorkingdayAnnualLeaveEntity> SaveWorkingdayAnnualLeave(WorkingdayAnnualLeaveEntity entity, List<WorkingdayAnnualLeaveStaffMapperType> mapList, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", entity.Id);
            par.Add("@Name", entity.Name);
            par.Add("@AnnualLeave", entity.AnnualLeave);
            par.Add("@TypeId", entity.TypeId);
            par.Add("@RegimeId", entity.RegimeId);
            par.Add("@PeriodApplyId", entity.PeriodApplyId);
            par.Add("@Status", entity.Status);
            par.Add("@CreatedBy", entity.CreatedBy);
            par.Add("@Note", entity.Note);
            par.Add("@DbName", dbName);
            par.Add("@WorkingdayAnnualLeaveStaffMapperType", mapList.ConvertToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<WorkingdayAnnualLeaveEntity>("WorkingdayAnnualLeave_Update_SaveWorkingdayAnnualLeave", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayAnnualLeaveEntity> GetWorkingdayAnnualLeaveById(long id,string dbName)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayAnnualLeaveEntity>("WorkingdayAnnualLeave_Get_GetWorkingdayAnnualLeaveById", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayAnnualLeaveStaffMapperEntity> GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(long annualLeaveId, string dbName)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@AnnualLeaveId", annualLeaveId);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayAnnualLeaveStaffMapperEntity>("WorkingdayAnnualLeave_Get_GetWorkingdayAnnualLeaveStaffMapperAnnualLeaveId", par, dbName: dbName);
            return result;
        }

        public HrmResultEntity<WorkingdayAnnualLeaveEntity> DeleteWorkingdayAnnualLeaveById(long id, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayAnnualLeaveEntity>("WorkingdayAnnualLeave_Del_DeleteWorkingdayAnnualLeave", par, dbName: dbName);
            return result;
        }

        #endregion

        #region //Data_Workingday_CheckInOut
        public HrmResultEntity<WorkingdayCheckInOutEntity> GetWorkingCheckInOutByDate(DateTime date ,long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@StaffId", staffId);
            par.Add("@Date", date);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayCheckInOutEntity>("WorkingdaySummary_Get_GetWorkingCheckInOutByDate", par, dbName: dbName);
            return result;
        }
        public HrmResultEntity<WorkingdayCheckInOutEntity> SaveCheckinCheckoutFromMachine(WorkingdayCheckInOutEntity data, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@SSN", data.SSN);
            par.Add("@WorkingDayMachineId", data.WorkingDayMachineId);
            par.Add("@CheckTime", data.CheckTime);
            par.Add("@CheckType", data.CheckType);
            par.Add("@TypeHolidayId", data.TypeHolidayId);
            par.Add("@Emotion", data.Emotion);
            par.Add("@ImageUrl", data.ImageUrl);
            par.Add("@DbName", dbName);
            var result = ListProcedure<WorkingdayCheckInOutEntity>("System_Update_SaveCheckinCheckoutFromMachine", par, dbName: dbName);
            return result;
        }
        // for system: ON DB HRM2.0
        public HrmResultEntity<WorkingdayCheckInOutEntity> GetCustomerByTimeKeeper(long timeKeeperId, long machineId)
        {
            var par = new DynamicParameters();
            par.Add("@TimeKeeperId", timeKeeperId);
            par.Add("@MachineId", machineId);
            var result = ListProcedure<WorkingdayCheckInOutEntity>("System_Get_GetCustomerByTimeKeeper", par);
            return result;
        }
        #endregion
    }
}
