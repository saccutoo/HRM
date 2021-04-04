using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using System.Collections;
using System.Web;

//using ERP.DataAccess.DBO;
//using ERP.DataAccess.DBO.EntityPartial;

namespace ERP.DataAccess.DAL
{
    public class TimekeepingDal : BaseDal<ADOProvider>
    {
        public List<HR_WorkingDaySummary> GetWorkingDaySupplementManager(BaseListParam listParam,int month,int year, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try {
            var param = new DynamicParameters();
            param.Add("@FilterField", listParam.FilterField?.Replace("undefined", ""));
            param.Add("@OrderByField", listParam.OrderByField);
            param.Add("@PageIndex", listParam.PageIndex);
            param.Add("@PageSize", listParam.PageSize);
            param.Add("@UserId", listParam.UserId);
            param.Add("@RoleId", listParam.UserType);
            param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
            param.Add("@Month", month);
            param.Add("@year", year);
            param.Add("@OrganizationUnitID", listParam.DeptId);
            param.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Total1",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total2",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total3",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total4",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total5",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total6",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total7",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            param.Add("@Total8",0,dbType: DbType.Double, direction: ParameterDirection.Output);
            var list = UnitOfWork.Procedure<HR_WorkingDaySummary>("HR_WorkingDaySummary_GetManager", param).ToList();
            param = HttpRuntime.Cache.Get("HR_WorkingDaySummary_GetManager-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
            totalColumns = new TableColumnsTotal();
            totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
            //totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
            totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
            totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
            totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
            totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();
            totalColumns.Total7 = param.GetDataOutput<double>("@Total7").ToString();
            //totalColumns.Total8 = param.GetDataOutput<double>("@Total8").ToString();
            return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<HR_WorkingDaySummary>();
            }
        }
        public SystemMessage LatchesWorkDay(string ListID,bool isCheckAll,int month,int year,int UserID,int RoleID)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
                var param = new DynamicParameters();
                param.Add("@ListStaffID", ListID);
                param.Add("@isCheckAll", isCheckAll);
                param.Add("@UserID", UserID);
                param.Add("@RoleID", RoleID);
                param.Add("@month", month);
                param.Add("@year", year);
                var checkExisted = UnitOfWork.Procedure<SalaryWorkingday>("SalaryWorkingday_checkExisted", param).ToList();
                var listStaffIDExisted = "";
                foreach (var item in checkExisted)
                {
                    listStaffIDExisted += item.Fullname + " , ";
                }
                if (checkExisted.Count > 0)
                {
                    systemMessage.IsSuccess = false;
                    systemMessage.checkExisted = true;
                    systemMessage.ListStaffCodeExisted = listStaffIDExisted;
                    return systemMessage;
                }
                else
                {
                    var param1 = new DynamicParameters();
                    param1.Add("@ListStaff", ListID);
                    param1.Add("@isCheckAll", isCheckAll);
                    param1.Add("@UserID", UserID);
                    param1.Add("@RoleID", RoleID);
                    param1.Add("@month", month);
                    param1.Add("@year", year);
                    UnitOfWork.ProcedureExecute("PublishWorkingday", param1);
                    if (isCheckAll == false) { 
                        var param2 = new DynamicParameters();
                        param2.Add("@ListStaff", ListID);
                        param2.Add("@month", month);
                        param2.Add("@year", year);
                        var listOrganizationUnit= UnitOfWork.Procedure<SalaryWorkingday>("SalaryWorkingday_ListOrganizationUnitWhereListStaff", param2).ToList();
                        foreach (var item in listOrganizationUnit)
                        {
                            var param3 = new DynamicParameters();
                            param3.Add("@ListStaff", item.StaffID);
                            param3.Add("@ListWPID", item.WPID);
                            param3.Add("@isCheckAll", isCheckAll);
                            param3.Add("@UserID", UserID);
                            param3.Add("@RoleID", RoleID);
                            param3.Add("@month", month);
                            param3.Add("@year", year);
                            param3.Add("@result", null, DbType.Int32, ParameterDirection.InputOutput);
                            UnitOfWork.ProcedureExecute("PublishCaculatorSalary", param3);
                        }    
                    }
                    else
                    {
                        var param4 = new DynamicParameters();
                        param4.Add("@ListStaff","");
                        param4.Add("@ListWPID", "");
                        param4.Add("@isCheckAll", isCheckAll);
                        param4.Add("@UserID", UserID);
                        param4.Add("@RoleID", RoleID);
                        param4.Add("@month", month);
                        param4.Add("@year", year);
                        param4.Add("@result", null, DbType.Int32, ParameterDirection.InputOutput);
                        UnitOfWork.ProcedureExecute("PublishCaculatorSalary", param4);
                    }
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
            }
            catch (Exception e)
            {
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }

        public SystemMessage LatchesWorkDayBack(string ListID, bool isCheckAll, int month, int year, int UserID, int RoleID)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
             
                    var param1 = new DynamicParameters();
                    param1.Add("@ListStaff", ListID);
                    param1.Add("@isCheckAll", isCheckAll);
                    param1.Add("@UserID", UserID);
                    param1.Add("@RoleID", RoleID);
                    param1.Add("@month", month);
                    param1.Add("@year", year);
                    param1.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("PublishWorkingdayBack", param1);
                    systemMessage.existedResult = param1.GetDataOutput<int>("@result");
                    if (systemMessage.existedResult > 0)
                    {
                        systemMessage.IsSuccess = true;
                    }
                    else
                    {
                        systemMessage.IsSuccess = false;
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
        public HR_WorkingDaySummary GetHRWorkingDayById(int roleId,int tableId,int id,int month,int year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID",id);
                param.Add("@month", month);
                param.Add("@year", year);
                param.Add("@Roleid", roleId);
                param.Add("@LanguageID");
                return UnitOfWork.Procedure<HR_WorkingDaySummary>("HR_WorkingDaySummary_GetByID", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<HR_WorkingDaySummary> ExportExcelTimeKeeping(BaseListParam listParam,int month,int year, out int? totalRecord)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField?.Replace("undefined", ""));
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                param.Add("@Month", month);
                param.Add("@year", year);
                param.Add("@OrganizationUnitID", listParam.DeptId);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<HR_WorkingDaySummary>("HR_WorkingDaySummary_GetManager", param).ToList();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                return new List<HR_WorkingDaySummary>();
            }
        }
        public SystemMessage SaveTimeKeeping(SalaryWorkingday obj)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
             
                var param = new DynamicParameters();
                param.Add("@SalaryID", obj.AutoID);
                param.Add("@StaffID", obj.StaffID);
                param.Add("@month", obj.Month);
                param.Add("@year", obj.Year);
                param.Add("@WorkingAdjusted", obj.WorkingAdjusted);
                param.Add("@NoteAdjusted", obj.NoteAdjusted);
                UnitOfWork.ProcedureExecute("TimeKeeping_Save", param);
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

        public SystemMessage ImportExcelSalaryWorkingday(List<SalaryWorkingday> listSalaryWorkingday)
        {

            SystemMessage systemMessage = new SystemMessage();

            try
            {
                if (listSalaryWorkingday != null)
                {
                    foreach (var item in listSalaryWorkingday)
                    {
                        var param = new DynamicParameters();
                        param.Add("@month", item.Month);
                        param.Add("@year", item.Year);
                        param.Add("@WorkingAdjusted", item.WorkingAdjusted);
                        param.Add("@NoteAdjusted", item.NoteAdjusted);
                        param.Add("@StaffCode", item.StaffCode);
                        UnitOfWork.ProcedureExecute("ImportExcelSalaryWorkingday", param);
                    }
                }
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
        public List<Customer> GetCustomerContractByUser(int UserID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                var list = UnitOfWork.Procedure<Customer>("Customer_Gets_ByUserID", param).ToList();
                return list;
            }
            catch (Exception e)
            {

                return null;
            }
           
        }
        public List<CustomerContact> HR_CustomerContactGetsByCustomerID(int customerID, int RoleId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerID", customerID);
                param.Add("@RoleID", RoleId);
                var list = UnitOfWork.Procedure<CustomerContact>("HR_CustomerContactGetsByCustomerID", param).ToList();
                return list;
            }
            catch (Exception e)
            {
                return new List<CustomerContact> ();

            }
        }
        public List<HR_WorkingDaySupplement> HR_WorkingDaySupplement_GetListId(int AutoID, int langId)
        {
            var param = new DynamicParameters();
            param.Add("@ListAutoID", AutoID);
            param.Add("@LanguageID", langId);
            var list = UnitOfWork.Procedure<HR_WorkingDaySupplement>("HR_WorkingDaySupplement_GetListId", param).ToList();
            return list;
        }

       


    }
}
