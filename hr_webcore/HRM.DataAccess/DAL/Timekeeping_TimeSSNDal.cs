using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.DataAccess.Common;
using ERP.Framework.DataBusiness.Common;
using HRM.Common;
using HRM.DataAccess.Entity.UserDefinedType;
using HRM.DataAccess.Helpers;

namespace HRM.DataAccess.DAL
{
    public class Timekeeping_TimeSSNDal : BaseDal<ADOProvider>
    {
        public List<TimeKeeping_TimeSSN> GetTimeKeepingMachine(BaseListParam listParam, int month, int year, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                if (listParam.FilterField != "" && listParam.FilterField != null)
                {
                    param.Add("@UserId", listParam.FilterField);
                }
                else
                {
                    param.Add("@UserId", listParam.UserId);
                }
                param.Add("@Month", month);
                param.Add("@Year", year);
                param.Add("@TotalDay", 0, dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@TotalHourLate", "", dbType: DbType.String, direction: ParameterDirection.Output);
                param.Add("@TotalHourEarly", "", dbType: DbType.String, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<TimeKeeping_TimeSSN>("Get_TimeKeepingMachine", param).ToList();
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@TotalDay").ToString();
                totalColumns.Total2 = param.GetDataOutput<string>("@TotalHourLate");
                totalColumns.Total3 = param.GetDataOutput<string>("@TotalHourEarly");
                return list;
            }
            catch (Exception ex)
            {
                totalColumns = new TableColumnsTotal();
                return new List<TimeKeeping_TimeSSN>();
            }
        }

        public int HR_CheckFurlough(HR_WorkingDaySupplement entity, int langId)
        {
            var param = new DynamicParameters();
            int year = Convert.ToInt32(entity.MonthVacation.Substring(3, 4));
            int month = Convert.ToInt32(entity.MonthVacation.Substring(0, 2));
            param.Add("@UserId", entity.StaffID);
            param.Add("@Year", year);
            param.Add("@Month", month);
            param.Add("@LanguageId", langId);
            param.Add("@Furlough", entity.DayOff);
            param.Add("@CheckFurlough", 0, DbType.Int32, ParameterDirection.InputOutput);
            UnitOfWork.ProcedureExecute("HR_CheckFurlough", param);
            var statusId = param.GetDataOutput<int>("@CheckFurlough");
            return statusId;
        }

        public int HR_WorkingDaySupplement_CheckExists(HR_WorkingDaySupplement entity)
        {
            var param = new DynamicParameters();
            int TypeCheck = 0;
            var date = entity.Date.ToString("yyyy-MM-dd");
            param.Add("@Date", date);
            param.Add("@FromTime", entity.FromTime);
            param.Add("@ToTime", entity.ToTime);
            if (entity.Type == 4 || entity.Type == 2 || entity.Type == 3)
            {
                TypeCheck = 1;
                param.Add("@FromTime", entity.FromTime);
            }
            else if (entity.Type == 5)
            {
                TypeCheck = 2;
                param.Add("@FromTime", entity.FromTime);
            }
            else
            {
                TypeCheck = 4;
            }
            param.Add("@TypeCheck", TypeCheck);
            param.Add("@AutoID", entity.AutoID);
            param.Add("@UserID", entity.StaffID);
            param.Add("@TimeOfActual", entity.HourOff);
            param.Add("@ReasonType", entity.ReasonType);
            param.Add("@MonthVacation", entity.MonthVacation);
            param.Add("@Type", entity.Type);
            param.Add("@CheckExists", 0, DbType.Int32, ParameterDirection.InputOutput);
            UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_CheckExists", param);
            var check = param.GetDataOutput<int>("@CheckExists");
            if (check == 1)
            {
                if (entity.Type == 4 || entity.Type == 2)
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@Date", date);
                    param1.Add("@FromTime", entity.FromTime);
                    param1.Add("@ToTime", entity.ToTime);
                    param1.Add("@AutoID", entity.AutoID);
                    param1.Add("@UserID", entity.StaffID);
                    param1.Add("@CheckExists", 0, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_CheckBoSung", param1);
                    check = param1.GetDataOutput<int>("@CheckExists");
                }
            }
            return check;
        }

        public List<Customer> GetCustomers(int userid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userid);
                var list = UnitOfWork.Procedure<Customer>("Customer_Gets_ByUserID", param).ToList();
                return list;
            }
            catch (Exception ex)
            {

                return new List<Customer>();
            }
        }

        public List<DateApproval> GetDateApproval(int userid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", userid);
                var list = UnitOfWork.Procedure<DateApproval>("GetDateApproval", param).ToList();
                return list;
            }
            catch (Exception ex)
            {

                return new List<DateApproval>();
            }
        }

        public int HR_CheckMissDayApproval(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@ShowTabApproval", 0, DbType.Int32, ParameterDirection.InputOutput);
            UnitOfWork.ProcedureExecute("HR_CheckMissDayApproval", param);
            var ShowTabApproval = param.GetDataOutput<int>("@ShowTabApproval");
            return ShowTabApproval;
        }

        public List<HR_WorkingDayMarchineDetail> HR_WorkingDay_GetHour(int userId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID", userId);
                var list = UnitOfWork.Procedure<HR_WorkingDayMarchineDetail>("HR_WorkingDay_GetHour", param,useCache:true).ToList();
                return list;
            }
            catch (Exception ex)
            {

                return new List<HR_WorkingDayMarchineDetail>();
            }
        }

        public List<CustomerContact> GetCustomerContacts(int customerid, int roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerID", customerid);
                param.Add("@RoleID", roleid);
                var list = UnitOfWork.Procedure<CustomerContact>("HR_CustomerContactGetsByCustomerID", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<CustomerContact>();
            }
        }

        public SystemMessage HR_WorkingDaySupplement_Send(int userId)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", userId);
                UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_Send", param);
                systemMessage.IsSuccess = true;
                if (Global.CurrentLanguage==5)
                {
                    systemMessage.Message = "Gửi duyệt thành công";
                }
                else
                {
                    systemMessage.Message = "Submit successful approval";
                }
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage SaveBoSungCong(HR_WorkingDaySupplement entity)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@AutoID", entity.AutoID);
                param.Add("@StaffID", entity.StaffID);
                param.Add("@Type", entity.Type);
                param.Add("@Date", entity.Date);
                param.Add("@FromTime", entity.FromTime);
                param.Add("@ToTime", entity.ToTime);
                param.Add("@HourOff", entity.HourOff);
                param.Add("@TimeOfActual", entity.TimeOfActual);
                param.Add("@DayOff", entity.DayOff);
                param.Add("@MonthVacation", entity.MonthVacation);
                param.Add("@CreatedBy", entity.CreatedBy);
                param.Add("@CreatedDate", entity.CreatedDate);
                param.Add("@SuperiorBy", entity.SuperiorBy);
                param.Add("@SuperiorDate", entity.SuperiorDate);
                param.Add("@ConfirmBy", entity.ConfirmBy);
                param.Add("@ConfirmDate", entity.ConfirmDate);
                param.Add("@Status", entity.Status);
                param.Add("@Note", entity.Note);
                param.Add("@ManagerNote", entity.ManagerNote);
                param.Add("@HRNote", entity.HRNote);
                param.Add("@ReasonType", entity.ReasonType);
                param.Add("@CustomerID", entity.CustomerID);             
                param.Add("@PercentPayrollID", entity.PercentPayrollID);
                param.Add("@CustomerContactID", entity.CustomerContactID);
                param.Add("@CustomerReasonType", entity.CustomerReasonType);
                UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_Add", param);
                systemMessage.IsSuccess = true;

                if (Global.CurrentLanguage == 5)
                {
                    systemMessage.Message = "Thêm mới thành công";
                }
                else
                {
                    systemMessage.Message = "Add successfully";
                }
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage Merge(int WorkingDayMachineIDOld, int WorkingDayMachineIDNew, string ListUserId, DateTime FromDate)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@WorkingDayMachineIDOld", WorkingDayMachineIDOld);
                param.Add("@WorkingDayMachineIDNew", WorkingDayMachineIDNew);
                param.Add("@ListUserId", ListUserId);
                param.Add("@FromDate", FromDate);
                UnitOfWork.ProcedureExecute("Merge", param);
                systemMessage.IsSuccess = true;
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }


        public SystemMessage SaveListWorkingday(List<HR_WorkingDaySupplementSaveListType> data,out int OutPut)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@HR_WorkingDaySupplementSaveListType", data.ToUserDefinedDataTable(), DbType.Object);
                param.Add("@OutPut", dbType: DbType.Int32, direction: ParameterDirection.Output);
                UnitOfWork.ProcedureExecute("HR_WorkingDaySupplement_SaveList", param);
                systemMessage.IsSuccess = true;
                OutPut = param.GetDataOutput<int>("@OutPut");
                return systemMessage;
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                OutPut = 0;
                return systemMessage;
            }
        }

    }
}
