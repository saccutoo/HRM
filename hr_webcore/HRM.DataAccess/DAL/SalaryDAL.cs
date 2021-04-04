using Dapper;
using ERP.Framework.Common;
using ERP.Framework.Data;
using ERP.Framework.DataBusiness.Common;
using HRM.DataAccess.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HRM.DataAccess.DAL
{
    public class SalaryDAL : BaseDal<ADOProvider>
    {
        public List<Salary> GetSalary(BaseListParam listParam, int type, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@type", type);
                param.Add("@isExcel", false);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total5", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total6", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total7", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total8", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total9", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total10", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total11", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total12", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total13", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total19", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total20", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total21", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total22", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total23", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total24", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total25", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total26", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total27", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total28", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total29", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total30", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total31", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total32", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<Salary>("Salary_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("Salary_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();
                totalColumns.Total7 = param.GetDataOutput<double>("@Total7").ToString();
                totalColumns.Total8 = param.GetDataOutput<double>("@Total8").ToString();
                totalColumns.Total9 = param.GetDataOutput<double>("@Total9").ToString();
                totalColumns.Total10 = param.GetDataOutput<double>("@Total10").ToString();
                totalColumns.Total11 = param.GetDataOutput<double>("@Total11").ToString();
                totalColumns.Total12 = param.GetDataOutput<double>("@Total12").ToString();
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                totalColumns.Total19 = param.GetDataOutput<double>("@Total19").ToString();
                totalColumns.Total20 = param.GetDataOutput<double>("@Total20").ToString();
                totalColumns.Total21 = param.GetDataOutput<double>("@Total21").ToString();
                totalColumns.Total22 = param.GetDataOutput<double>("@Total22").ToString();
                totalColumns.Total23 = param.GetDataOutput<double>("@Total23").ToString();
                totalColumns.Total24 = param.GetDataOutput<double>("@Total24").ToString();
                totalColumns.Total25 = param.GetDataOutput<double>("@Total25").ToString();
                totalColumns.Total26 = param.GetDataOutput<double>("@Total26").ToString();
                totalColumns.Total27 = param.GetDataOutput<double>("@Total27").ToString();
                totalColumns.Total28 = param.GetDataOutput<double>("@Total28").ToString();
                totalColumns.Total29 = param.GetDataOutput<double>("@Total29").ToString();
                totalColumns.Total30 = param.GetDataOutput<double>("@Total30").ToString();
                totalColumns.Total31 = param.GetDataOutput<double>("@Total31").ToString();
                totalColumns.Total32 = param.GetDataOutput<double>("@Total32").ToString();
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<Salary>();
            }
        }


        public List<Salary> GetSalaryWhereListstaffid(bool isCheckAll, string strListStaffId, int LanguageID, int month, int year, int period, int userid, int roleid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ListStaffid", strListStaffId);
                param.Add("@isCheckAll ", isCheckAll);
                param.Add("@LanguageID", LanguageID);
                param.Add("@month", month);
                param.Add("@year", year);
                param.Add("@UserId", userid);
                param.Add("@RoleId", roleid);
                param.Add("@period", period);
                var list = UnitOfWork.Procedure<Salary>("Salary_GetsWhereListID", param).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Salary>();
            }


        }
        public List<Template2> GetTemplate(BaseListParam listParam, out int? totalRecord)


        {
            try
            {
                var param = new DynamicParameters();
                //param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var list = UnitOfWork.Procedure<Template2>("Template_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("Template_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                return new List<Template2>();
            }


        }
        public Salary GetSalaryById(int roleId, int idTable, int id, int languageID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SalaryID", id);
                param.Add("@LanguageID", languageID);
                param.Add("@Roleid", roleId);
                return UnitOfWork.Procedure<Salary>("Salary_GetInfo", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public Salary GetSalaryGetInfo(BaseListParam listParam, int roleId, int idTable, int id, int month, int year)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@StaffID", id);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                //param.Add("@DeptId", listParam.DeptId);
                param.Add("@LanguageId", int.Parse(listParam.LanguageCode));
                param.Add("@month", month);
                param.Add("@year", year);
                return UnitOfWork.Procedure<Salary>("Salary_GetInfoWhereMonthYear", param).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SystemMessage SaveSalary(int roleId, int idTable, Salary obj)
        {
            SystemMessage systemMessage = new SystemMessage();

            try
            {
                var param = new DynamicParameters();
                param.Add("@WPID", obj.WPID);
                param.Add("@OtherAllowances", obj.OtherAllowances);
                param.Add("@AllowancesNote", obj.AllowancesNote);
                param.Add("@OtherBonus", obj.OtherBonus);
                param.Add("@BonusNote", obj.BonusNote);
                param.Add("@Nontaxableincome", obj.Nontaxableincome);
                param.Add("@NontaxableincomeNote", obj.NontaxableincomeNote);
                param.Add("@OtherReduction", obj.OtherReduction);
                param.Add("@ReductionNote", obj.ReductionNote);
                param.Add("@Margincompensation", obj.Margincompensation);
                param.Add("@MargincompensationNote", obj.MargincompensationNote);
                param.Add("@AdvancePayment", obj.AdvancePayment);
                param.Add("@SalarylastMonth", obj.SalarylastMonth);
                param.Add("@BDOAllowances", obj.BDOAllowances);
                param.Add("@Commission", obj.Commission);
                param.Add("@Decemberbonus", obj.Decemberbonus);
                param.Add("@Commission", obj.Commission);
                param.Add("@StaffID1", obj.StaffID);
                param.Add("@SysBonus", obj.SysBonus);
                param.Add("@BonusKPIYear", obj.BonusKPIYear);
                param.Add("@OtherKPIYear", obj.OtherKPIYear);
                param.Add("@SalaryID", obj.SalaryID, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("Salary_Save", param);
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

        public SystemMessage LatchesWorkDay(BaseListParam listParam, string ListID, bool isCheckAll, int month, int year, int Period, out int result, out string ListHoldSalary, out string ListNetsalary, out string ListBankNumber)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {

                var param = new DynamicParameters();
                param.Add("@ListStaff", ListID);
                param.Add("@isCheckAll ", isCheckAll);
                param.Add("@UserID", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@month", month);
                param.Add("@year", year);
                param.Add("@Period", Period);
                param.Add("@Result", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@ListHoldSalary", "", DbType.String, ParameterDirection.InputOutput);
                param.Add("@ListNetsalary", "", DbType.String, ParameterDirection.InputOutput);
                param.Add("@ListBankNumber", "", DbType.String, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("PublishPaymentSalary", param);
                result = param.GetDataOutput<int>("@Result");
                ListHoldSalary = param.GetDataOutput<string>("@ListHoldSalary");
                ListNetsalary = param.GetDataOutput<string>("@ListNetsalary");
                ListBankNumber = param.GetDataOutput<string>("@ListBankNumber");
                systemMessage.IsSuccess = true;
                return systemMessage;
            }
            catch (Exception e)
            {
                result = 0;
                ListHoldSalary = "";
                ListNetsalary = "";
                ListBankNumber = "";
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage RemoveLatchesWorkDay(BaseListParam listParam, string ListID, bool isCheckAll, int month, int year, int Period, out int result)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                var param = new DynamicParameters();
                param.Add("@ListStaff", ListID);
                param.Add("@isCheckAll ", isCheckAll);
                param.Add("@UserID", listParam.UserId);
                param.Add("@RoleID", listParam.UserType);
                param.Add("@month", month);
                param.Add("@year", year);
                param.Add("@Period", Period);
                param.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                UnitOfWork.ProcedureExecute("PublishPaymentSalaryBack", param);
                result = param.GetDataOutput<int>("@result");
                systemMessage.IsSuccess = true;
                return systemMessage;
            }
            catch (Exception e)
            {
                result = 0;
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }
        }

        public SystemMessage CreatePayslip(BaseListParam listParam, List<string> ListStaffID, List<string> ListWPID, bool isCheckAll, int month, int year, out int result)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                if (isCheckAll == false)
                {
                    result = 0;
                    foreach (var item in ListStaffID.Zip(ListWPID, (a, b) => new { A = a, B = b }))
                    {
                        var param2 = new DynamicParameters();
                        param2.Add("@ListStaff", item.A);
                        param2.Add("@ListWPID", item.B);
                        param2.Add("@isCheckAll", isCheckAll);
                        param2.Add("@UserID", listParam.UserId);
                        param2.Add("@RoleID", listParam.UserType);
                        param2.Add("@month", month);
                        param2.Add("@year", year);
                        param2.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                        UnitOfWork.ProcedureExecute("PublishCaculatorSalary", param2);
                        result = param2.GetDataOutput<int>("@result");
                    }
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
                else
                {
                    var param = new DynamicParameters();
                    param.Add("@isCheckAll", isCheckAll);
                    param.Add("@ListStaff", "");
                    param.Add("@UserID", listParam.UserId);
                    param.Add("@RoleID", listParam.UserType);
                    param.Add("@ListWPID", "");
                    param.Add("@month", month);
                    param.Add("@year", year);
                    param.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("PublishCaculatorSalary", param);
                    result = param.GetDataOutput<int>("@result");
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
            }
            catch (Exception e)
            {
                result = 0;
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }
        public List<Salary> ExportExcelSalary(BaseListParam listParam, int type, out int? totalRecord, out TableColumnsTotal totalColumns)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@FilterField", listParam.FilterField);
                param.Add("@OrderByField", listParam.OrderByField);
                param.Add("@PageIndex", listParam.PageIndex);
                param.Add("@PageSize", listParam.PageSize);
                param.Add("@UserId", listParam.UserId);
                param.Add("@RoleId", listParam.UserType);
                param.Add("@DeptId", listParam.DeptId);
                param.Add("@isExcel", false);
                param.Add("@type", type);
                param.Add("@LanguageID", listParam.LanguageCode);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                param.Add("@Total1", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total2", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total3", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total4", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total5", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total6", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total7", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total8", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total9", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total10", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total11", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total12", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total13", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total14", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total15", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total16", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total17", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total18", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total19", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total20", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total21", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total22", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total23", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total24", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total25", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total26", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total27", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total28", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total29", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total30", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total31", dbType: DbType.Double, direction: ParameterDirection.Output);
                param.Add("@Total32", dbType: DbType.Double, direction: ParameterDirection.Output);
                var list = UnitOfWork.Procedure<Salary>("Salary_Gets", param).ToList();
                param = HttpRuntime.Cache.Get("Salary_Gets-" + ERP.Framework.Common.Utils.GetKeyFromParam(param as object) + "-" + HRM.Common.Global.CurrentUser.UserID + "-output") as DynamicParameters;
                totalColumns = new TableColumnsTotal();
                totalColumns.Total1 = param.GetDataOutput<double>("@Total1").ToString();
                totalColumns.Total2 = param.GetDataOutput<double>("@Total2").ToString();
                totalColumns.Total3 = param.GetDataOutput<double>("@Total3").ToString();
                totalColumns.Total4 = param.GetDataOutput<double>("@Total4").ToString();
                totalColumns.Total5 = param.GetDataOutput<double>("@Total5").ToString();
                totalColumns.Total6 = param.GetDataOutput<double>("@Total6").ToString();
                totalColumns.Total7 = param.GetDataOutput<double>("@Total7").ToString();
                totalColumns.Total8 = param.GetDataOutput<double>("@Total8").ToString();
                totalColumns.Total9 = param.GetDataOutput<double>("@Total9").ToString();
                totalColumns.Total10 = param.GetDataOutput<double>("@Total10").ToString();
                totalColumns.Total11 = param.GetDataOutput<double>("@Total11").ToString();
                totalColumns.Total12 = param.GetDataOutput<double>("@Total12").ToString();
                totalColumns.Total13 = param.GetDataOutput<double>("@Total13").ToString();
                totalColumns.Total14 = param.GetDataOutput<double>("@Total14").ToString();
                totalColumns.Total15 = param.GetDataOutput<double>("@Total15").ToString();
                totalColumns.Total16 = param.GetDataOutput<double>("@Total16").ToString();
                totalColumns.Total17 = param.GetDataOutput<double>("@Total17").ToString();
                totalColumns.Total18 = param.GetDataOutput<double>("@Total18").ToString();
                totalColumns.Total19 = param.GetDataOutput<double>("@Total19").ToString();
                totalColumns.Total20 = param.GetDataOutput<double>("@Total20").ToString();
                totalColumns.Total21 = param.GetDataOutput<double>("@Total21").ToString();
                totalColumns.Total22 = param.GetDataOutput<double>("@Total22").ToString();
                totalColumns.Total23 = param.GetDataOutput<double>("@Total23").ToString();
                totalColumns.Total24 = param.GetDataOutput<double>("@Total24").ToString();
                totalColumns.Total25 = param.GetDataOutput<double>("@Total25").ToString();
                totalColumns.Total26 = param.GetDataOutput<double>("@Total26").ToString();
                totalColumns.Total27 = param.GetDataOutput<double>("@Total27").ToString();
                totalColumns.Total28 = param.GetDataOutput<double>("@Total28").ToString();
                totalColumns.Total29 = param.GetDataOutput<double>("@Total29").ToString();
                totalColumns.Total30 = param.GetDataOutput<double>("@Total30").ToString();
                totalColumns.Total31 = param.GetDataOutput<double>("@Total31").ToString();
                totalColumns.Total32 = param.GetDataOutput<double>("@Total31").ToString();
                totalRecord = param.GetDataOutput<int>("@TotalRecord");
                return list;
            }
            catch (Exception e)
            {
                totalRecord = 0;
                totalColumns = new TableColumnsTotal();
                return new List<Salary>();
            }
        }

        public SystemMessage ImportExcelSalary(List<Salary> listSalary)
        {

            SystemMessage systemMessage = new SystemMessage();

            try
            {
                if (listSalary != null)
                {
                    foreach (var item in listSalary)
                    {
                        var stringMonth = Regex.Split(item.MonthYear, "/")[0];
                        var Month = Convert.ToInt32(stringMonth.Substring(1));
                        var stringYear = Regex.Split(item.MonthYear, "/")[1];
                        var Year = Convert.ToInt32(stringYear);
                        var param = new DynamicParameters();
                        param.Add("@OtherBonus", item.OtherBonus);
                        param.Add("@BonusNote", item.BonusNote);
                        param.Add("@BDOAllowances", item.BDOAllowances);
                        param.Add("@Nontaxableincome", item.Nontaxableincome);
                        param.Add("@NontaxableincomeNote", item.NontaxableincomeNote);
                        param.Add("@OtherReduction", item.OtherReduction);
                        param.Add("@ReductionNote", item.ReductionNote);
                        param.Add("@OtherAllowances", item.OtherAllowances);
                        param.Add("@AllowancesNote", item.AllowancesNote);
                        param.Add("@StaffCode", item.StaffCode);
                        param.Add("@Margincompensation", item.Margincompensation);
                        param.Add("@MargincompensationNote", item.MargincompensationNote);
                        param.Add("@AdvancePayment", item.AdvancePayment);
                        param.Add("@SalarylastMonth", item.SalarylastMonth);
                        param.Add("@Decemberbonus", item.Decemberbonus);
                        param.Add("@Commission", item.Commission);
                        param.Add("@month", Month);
                        param.Add("@year", Year);
                        UnitOfWork.ProcedureExecute("ImportExcelSalary", param);
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
        public SystemMessage UpdateBonus(BaseListParam listParam, List<string> ListStaffID, List<string> ListWPID, bool isCheckAll, int month, int year,int policyBonusIDRun, out int result)
        {
            SystemMessage systemMessage = new SystemMessage();
            try
            {
                if (isCheckAll == false)
                {
                    result = 0;
                    foreach (var item in ListStaffID.Zip(ListWPID, (a, b) => new { A = a, B = b }))
                    {
                        var param2 = new DynamicParameters();
                        param2.Add("@ListStaff", item.A);
                        param2.Add("@ListWPID", item.B);
                        param2.Add("@isCheckAll", isCheckAll);
                        param2.Add("@UserID", listParam.UserId);
                        param2.Add("@RoleID", listParam.UserType);
                        param2.Add("@month", month);
                        param2.Add("@year", year);
                        param2.Add("@PolicyBonusIDRun", policyBonusIDRun);
                        param2.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                        UnitOfWork.ProcedureExecute("PublishBonus", param2);
                        result = param2.GetDataOutput<int>("@result");
                    }
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
                else
                {
                    var param = new DynamicParameters();
                    param.Add("@isCheckAll", isCheckAll);
                    param.Add("@ListStaff", "");
                    param.Add("@UserID", listParam.UserId);
                    param.Add("@RoleID", listParam.UserType);
                    param.Add("@ListWPID", "");
                    param.Add("@month", month);
                    param.Add("@year", year);
                    param.Add("@PolicyBonusIDRun", policyBonusIDRun);
                    param.Add("@result", 0, DbType.Int32, ParameterDirection.InputOutput);
                    UnitOfWork.ProcedureExecute("PublishBonus", param);
                    result = param.GetDataOutput<int>("@result");
                    systemMessage.IsSuccess = true;
                    return systemMessage;
                }
            }
            catch (Exception e)
            {
                result = 0;
                systemMessage.IsSuccess = false;
                systemMessage.Message = e.ToString();
                return systemMessage;
            }


        }
    }
}
