
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using ERP.Framework.Utilities;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HRM.Logger;
using HRM.Security;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SalaryController : BaseController
    {
        // GET: Salary

        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/Salary/TableServerSideGetData";
            ViewBag.url2 = "/Salary/TableServerSideGetData2";
            ViewBag.url3 = "/SalaryOrganizationUnit/TableServerSideGetData";
            return PartialView();
        }
        /// <summary>
        /// Lấy dữ liệu ra table Tổng hợp phiếu lương
        /// </summary>
        /// <param name="pageIndex"> số trang</param>
        /// <param name="pageSize"> số bản ghi/trang</param>
        /// <param name="month"> tháng</param>
        /// <param name="year"> năm</param>
        /// <param name="filter"> bộ lọc</param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Salary_Gets")]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new SalaryDAL();

            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            int type = 1; //tổng hợp lương
            var result = db.GetSalary(baseListParam, type, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                staffID = baseListParam.UserId
            }));
        }

        // <summary>
        // Lấy dữ liệu ra table phiếu lương
        // </summary>
        // <param name = "pageIndex" > số trang</param>
        // <param name = "pageSize" > số bản ghi/trang</param>
        // <param name = "month" > tháng </ param >
        // < param name="year"> năm</param>
        // <param name = "filter" > bộ lọc</param>
        // <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Salary_GetInfoWhereMonthYear")]
        public ActionResult TableServerSideGetData2(int pageIndex, int pageSize, int month, int year, string filter = "")
        {
            var db = new SalaryDAL();
            int? total = 0;
            var baseListParam = new BaseListParam()
            {
                FilterField = filter,
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.UserID,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString(),
            };
            var result = db.GetTemplate(baseListParam, out total);
            int resultstaffID = Global.CurrentUser.LoginUserId;
            if (filter != "")
            {
                string staffID = Regex.Split(filter, "=")[1];
                resultstaffID = Convert.ToInt32(staffID);
            }

            var objSalary = db.GetSalaryGetInfo(baseListParam, Global.CurrentUser.RoleId, 24, resultstaffID, month, year);
            if (objSalary != null)
            {
                foreach (var item in result)
                {
                    if (item.Value != null)
                    {
                        switch (item.DisplayType)
                        {
                            case 0: // giá trị string .
                                if (objSalary.GetType().GetProperty(item.Value).GetValue(objSalary, null) != null)
                                {
                                    item.DisplayValue = (string)objSalary.GetType().GetProperty(item.Value).GetValue(objSalary, null);
                                }
                                else
                                {
                                    item.DisplayValue = "";
                                }
                                break;
                            case 1: // giá trị float
                                if (objSalary.GetType().GetProperty(item.Value).GetValue(objSalary, null) != null)
                                {
                                    item.DisplayValueFloat = (double)objSalary.GetType().GetProperty(item.Value).GetValue(objSalary, null);
                                }
                                else
                                {
                                    item.DisplayValueFloat = 0;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal,
                staffID = baseListParam.UserId
            }));
        }

        /// <summary>
        /// PartialView Save phiếu lương 
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSalary()
        {
            return PartialView();
        }

        /// <summary>
        /// Lấy dữ liệu phiếu lương theo id
        /// </summary>
        /// <param name="id"> id</param>
        /// <param name="idTable"> id bảng tổng hợp phiếu lương</param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Salary_GetInfo")]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new SalaryDAL();
            var languageID = Global.CurrentUser.CurrentLanguageID;
            var result = db.GetSalaryById(Global.CurrentUser.RoleId, idTable, id, languageID);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        /// <summary>
        /// Save thông tin cần thiết cho phiếu lương
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Salary_Save")]
        public ActionResult SaveSalary(Salary obj)
        {

            var db = new SalaryDAL();
            var result = db.SaveSalary(Global.CurrentUser.RoleId, 1, obj);
            if (result.IsSuccess == true && obj.StaffID == 0)
            {
                result.Message = AppRes.MSG_INSERT_SUCCESSFUL;
            }
            else if (result.IsSuccess == true && obj.StaffID != 0)
            {
                result.Message = AppRes.MS_Update_success;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        /// <summary>
        /// Chốt bảng lương
        /// </summary>
        /// <param name="listID"></param>
        /// <param name="isCheckAll"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="Period"></param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PublishPaymentSalary")]
        public ActionResult LatchesWorkDay(string listID, bool isCheckAll, int month, int year, int Period)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
            };

            var db = new SalaryDAL();

            int iResult = 0;
            string ListHoldSalary = "";
            string ListNetsalary = "";
            string ListBankNumber = "";
            var result = db.LatchesWorkDay(baseListParam, listID, isCheckAll, month, year, Period, out iResult, out ListHoldSalary, out ListNetsalary, out ListBankNumber);
            if (iResult == -1)
            {
                result.IsSuccess = false;
                result.Message = AppRes.ErrorPeriodPayment;
            }
            else if (iResult == -2)
            {
                result.IsSuccess = false;
                result.Message = "Nhân viên bị giữ lương: " + ListHoldSalary + "<br /><br />Nhân viên lương ít hơn 50k: " + ListNetsalary + "<br /><br />Số tài khoản nhỏ hơn 4 kí tự: " + ListBankNumber;
            }
            else if (isCheckAll == true && result.IsSuccess == true)
                result.Message = AppRes.LatchesCouponSalaryAll;
            else if (isCheckAll == false && result.IsSuccess == true)
                result.Message = AppRes.LatchesCouponSalary + " " + Period + " " + AppRes.Success;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "PublishPaymentSalaryBack")]
        public ActionResult RemoveLatchesWorkDay(string listID, bool isCheckAll, int month, int year, int Period)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
            };
            var db = new SalaryDAL();
            int iResult = 0;
            var result = db.RemoveLatchesWorkDay(baseListParam, listID, isCheckAll, month, year, Period, out iResult);
            if (iResult < 0)
            {
                result.IsSuccess = false;
                result.Message = AppRes.ErrorPeriodPayment;
            }
            else if (isCheckAll == true && result.IsSuccess == true)
                result.Message = "Gỡ chốt thành công";
            else if (isCheckAll == false && result.IsSuccess == true)
                result.Message = AppRes.LatchesCouponSalary + " " + Period + " " + AppRes.Success;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        /// <summary>
        /// Xuất excel tổng hợp phiếu lương
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //[Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Excel)]
        //public ActionResult SalaryExportExcel(string filterString, int pageIndex, int pageSize)
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[51]
        //    {
        //        new DataColumn(AppRes.MonthYear),
        //        new DataColumn(AppRes.StaffCode),
        //        new DataColumn(AppRes.FullName),
        //        new DataColumn(AppRes.Department),
        //        new DataColumn(AppRes.Position),
        //        new DataColumn(AppRes.Rank),
        //        new DataColumn(AppRes.EmployeeStatus),
        //        new DataColumn(AppRes.Email),
        //        new DataColumn(AppRes.Workingday),
        //        new DataColumn(AppRes.Standardworkingday),
        //        new DataColumn(AppRes.BasicSalary),
        //        new DataColumn(AppRes.Bonus),
        //        new DataColumn(AppRes.SeniorityPay),
        //        new DataColumn(AppRes.AllowancesLaptop),
        //        new DataColumn(AppRes.OtherAllowances),
        //        new DataColumn(AppRes.BDOAllowances),
        //        new DataColumn(AppRes.IncomePerDay),
        //        new DataColumn(AppRes.SalarylastMonth),
        //        new DataColumn(AppRes.Commission),
        //        new DataColumn(AppRes.OtherBonus),
        //        new DataColumn(AppRes.BonusNote),
        //        new DataColumn(AppRes.PersonalIncomeTax),
        //        new DataColumn(AppRes.AmountInsurance),
        //        new DataColumn(AppRes.UnionFund),
        //        new DataColumn(AppRes.TardinessReduction),
        //        new DataColumn(AppRes.QCBonus),
        //        new DataColumn(AppRes.AdvancePayment),
        //        new DataColumn(AppRes.OtherReduction),
        //        new DataColumn(AppRes.ReductionNote),
        //        new DataColumn(AppRes.TotalIncome),
        //        new DataColumn(AppRes.TotalReduction),
        //        new DataColumn(AppRes.Netsalary),
        //        new DataColumn(AppRes.PaymentPeriod1),
        //        new DataColumn(AppRes.PaymentPeriod2),
        //        new DataColumn(AppRes.PaymentPeriod3),
        //        new DataColumn(AppRes.PaymentPeriod4),
        //        new DataColumn(AppRes.PaymentPeriod5),
        //        new DataColumn(AppRes.PaymentNextMonth),
        //        new DataColumn(AppRes.SalaryAccountNumber),
        //        new DataColumn(AppRes.SalaryAccountName),
        //        new DataColumn(AppRes.ReceipientBankName),
        //        new DataColumn(AppRes.Nontaxableincome),
        //        new DataColumn(AppRes.NontaxableincomeNote),
        //        new DataColumn(AppRes.OtherAllowanceDetails),
        //        new DataColumn(AppRes.Margincompensation),
        //        new DataColumn(AppRes.MargincompensationNote),
        //         new DataColumn(AppRes.DecemberBonus),
        //        new DataColumn(AppRes.TotalSalary),
        //        new DataColumn(AppRes.HoldSaraly),
        //        new DataColumn(AppRes.StatusOfLaborContract),
        //        new DataColumn(AppRes.Policy)

        //    });
        //    dt.Columns[0].DataType = typeof(string);
        //    dt.Columns[1].DataType = typeof(string);
        //    dt.Columns[2].DataType = typeof(string);
        //    dt.Columns[3].DataType = typeof(string);
        //    dt.Columns[4].DataType = typeof(string);
        //    dt.Columns[5].DataType = typeof(string);
        //    dt.Columns[6].DataType = typeof(string);
        //    dt.Columns[7].DataType = typeof(string);
        //    dt.Columns[8].DataType = typeof(double);
        //    dt.Columns[9].DataType = typeof(double);
        //    dt.Columns[10].DataType = typeof(double);
        //    dt.Columns[11].DataType = typeof(double);
        //    dt.Columns[12].DataType = typeof(double);
        //    dt.Columns[13].DataType = typeof(double);
        //    dt.Columns[14].DataType = typeof(double);
        //    dt.Columns[15].DataType = typeof(double);
        //    dt.Columns[16].DataType = typeof(double);
        //    dt.Columns[17].DataType = typeof(double);
        //    dt.Columns[18].DataType = typeof(double);
        //    dt.Columns[19].DataType = typeof(double);
        //    dt.Columns[20].DataType = typeof(string);
        //    dt.Columns[21].DataType = typeof(double);
        //    dt.Columns[22].DataType = typeof(double);
        //    dt.Columns[23].DataType = typeof(double);
        //    dt.Columns[24].DataType = typeof(double);
        //    dt.Columns[25].DataType = typeof(double);
        //    dt.Columns[26].DataType = typeof(double);
        //    dt.Columns[27].DataType = typeof(double);
        //    dt.Columns[28].DataType = typeof(string);
        //    dt.Columns[29].DataType = typeof(double);
        //    dt.Columns[30].DataType = typeof(double);
        //    dt.Columns[31].DataType = typeof(double);
        //    dt.Columns[32].DataType = typeof(double);
        //    dt.Columns[33].DataType = typeof(double);
        //    dt.Columns[34].DataType = typeof(double);
        //    dt.Columns[35].DataType = typeof(double);
        //    dt.Columns[36].DataType = typeof(double);
        //    dt.Columns[37].DataType = typeof(double);
        //    dt.Columns[38].DataType = typeof(string);
        //    dt.Columns[39].DataType = typeof(string);
        //    dt.Columns[40].DataType = typeof(string);
        //    dt.Columns[41].DataType = typeof(double);
        //    dt.Columns[42].DataType = typeof(string);
        //    dt.Columns[43].DataType = typeof(string);
        //    dt.Columns[44].DataType = typeof(double);
        //    dt.Columns[45].DataType = typeof(string);
        //    dt.Columns[46].DataType = typeof(double);
        //    dt.Columns[47].DataType = typeof(double);
        //    dt.Columns[48].DataType = typeof(int);
        //    dt.Columns[49].DataType = typeof(string);
        //    dt.Columns[50].DataType = typeof(string);
        //    var db = new SalaryDAL();
        //    int? total = 0;
        //    var baseListParam = new BaseListParam()
        //    {
        //        FilterField = filterString,
        //        OrderByField = "",
        //        PageIndex = pageIndex,
        //        PageSize = int.MaxValue,
        //        UserType = Global.CurrentUser.RoleId,
        //        UserId = Global.CurrentUser.LoginUserId,
        //        DeptId = Global.CurrentUser.OrganizationUnitID,
        //        LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
        //    };
        //    var lstData = db.ExportExcelSalary(baseListParam, out total);
        //    foreach (var item in lstData)
        //    {
        //        dt.Rows.Add(
        //            item.MonthYear == null ? "" : item.MonthYear,
        //            item.StaffCode == null ? "" : item.StaffCode,
        //            item.FullName == null ? "" : item.FullName,
        //            item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
        //            item.OfficePositionName == null ? "" : item.OfficePositionName,
        //            item.StaffLevelName == null ? "" : item.StaffLevelName,
        //            item.EmployeeStatus == null ? "" : item.EmployeeStatus,
        //            item.EmailPersonal == null ? "" : item.EmailPersonal,
        //            item.Workingday == null ? 0 : item.Workingday,
        //            item.Standardworkingday == null ? 0 : item.Standardworkingday,
        //            item.BasicSalary == null ? 0 : item.BasicSalary,
        //            item.Bonus == null ? 0 : item.Bonus,
        //            item.SeniorityPay == null ? 0 : item.SeniorityPay,
        //            item.AllowancesLaptop == null ? 0 : item.AllowancesLaptop,
        //            item.TotalOtherAllowances == null ? 0 : item.TotalOtherAllowances,//15
        //            item.BDOAllowances == null ? 0 : item.BDOAllowances,//16
        //            item.IncomePerDay == null ? 0 : item.IncomePerDay,
        //            item.SalarylastMonth == null ? 0 : item.SalarylastMonth,
        //            item.Commission == null ? 0 : item.Commission,
        //            item.OtherBonus == null ? 0 : item.OtherBonus,//20
        //            item.BonusNote == null ? "" : item.BonusNote,//21
        //            item.PersonalIncomeTax == null ? 0 : item.PersonalIncomeTax,
        //            item.AmountInsurance == null ? 0 : item.AmountInsurance,
        //            item.UnionFund == null ? 0 : item.UnionFund,
        //            item.TardinessReduction == null ? 0 : item.TardinessReduction,
        //            item.QCBonus == null ? 0 : item.QCBonus,
        //            item.AdvancePayment == null ? 0 : item.AdvancePayment,//27
        //            item.OtherReduction == null ? 0 : item.OtherReduction,//28
        //            item.ReductionNote == null ? "" : item.ReductionNote,//29
        //            item.TotalIncome == null ? 0 : item.TotalIncome,
        //            item.TotalReduction == null ? 0 : item.TotalReduction,
        //            item.Netsalary == null ? 0 : item.Netsalary,
        //            item.PaymentPeriod1 == null ? 0 : item.PaymentPeriod1,
        //            item.PaymentPeriod2 == null ? 0 : item.PaymentPeriod2,
        //            item.PaymentPeriod3 == null ? 0 : item.PaymentPeriod3,
        //            item.PaymentPeriod4 == null ? 0 : item.PaymentPeriod4,
        //            item.PaymentPeriod5 == null ? 0 : item.PaymentPeriod5,
        //            item.Remain == null ? 0 : item.Remain,
        //            item.AccountNumber == null ? "" : item.AccountNumber,
        //            item.AccountName == null ? "" : item.AccountName,
        //            item.BankName == null ? "" : item.BankName,
        //            item.Nontaxableincome == null ? 0 : item.Nontaxableincome,//42
        //            item.NontaxableincomeNote == null ? "" : item.NontaxableincomeNote,//43
        //            item.AllowancesNote == null ? "" : item.AllowancesNote,//44
        //            item.Margincompensation == null ? 0 : item.Margincompensation,//45
        //            item.MargincompensationNote == null ? "" : item.MargincompensationNote,//46
        //            item.Decemberbonus == null ? 0 : item.Decemberbonus,//47
        //            item.TotalSalary == null ? 0 : item.TotalSalary,
        //            item.HoldSalary == null ? 0 : item.HoldSalary,
        //            item.StatusContractName == null ? "" : item.StatusContractName,
        //            item.PolicyName == null ? "" : item.PolicyName
        //            );
        //    }

        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt);
        //    byte[] data = null;
        //    using (var stream = new MemoryStream())
        //    {
        //        wb.SaveAs(stream);
        //        data = stream.ToArray();
        //    }
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Salary.xlsx");
        //}

        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "Salary_Gets")]
        public ActionResult SalaryExportExcel(string filterString, int pageIndex, int pageSize)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[82]
            {
                new DataColumn(AppRes.MonthYear),
                new DataColumn(AppRes.StaffCode),
                new DataColumn(AppRes.FullName),
                new DataColumn(AppRes.OrganazationUnitCode),
                new DataColumn(AppRes.Department),
                new DataColumn(AppRes.Position),
                new DataColumn(AppRes.Rank),
                new DataColumn(AppRes.EmployeeStatus),
                new DataColumn(AppRes.Email),
                new DataColumn(AppRes.Workingday),
                new DataColumn(AppRes.Standardworkingday),
                new DataColumn(AppRes.BasicSalary),
                new DataColumn(AppRes.Bonus),
                new DataColumn(AppRes.SeniorityPay),
                new DataColumn(AppRes.AllowancesLaptop),
                new DataColumn(AppRes.TotalOtherAllowances),
                new DataColumn(AppRes.BDOAllowances),
                new DataColumn(AppRes.IncomePerDay),
                new DataColumn(AppRes.SalarylastMonth),
                new DataColumn(AppRes.Commission),
                new DataColumn(AppRes.OtherBonus),
                new DataColumn(AppRes.BonusNote),
                new DataColumn(AppRes.PersonalIncomeTax),
                new DataColumn(AppRes.AmountInsurance),
                new DataColumn(AppRes.UnionFund),
                new DataColumn(AppRes.TardinessReduction),
                new DataColumn(AppRes.QCBonus),
                new DataColumn(AppRes.AdvancePayment),
                new DataColumn(AppRes.OtherReduction),
                new DataColumn(AppRes.ReductionNote),
                new DataColumn(AppRes.TotalIncome),
                new DataColumn(AppRes.TotalReduction),
                new DataColumn(AppRes.Netsalary),
                new DataColumn(AppRes.PaymentPeriod1),
                new DataColumn(AppRes.PaymentPeriod2),
                new DataColumn(AppRes.PaymentPeriod3),
                new DataColumn(AppRes.PaymentPeriod4),
                new DataColumn(AppRes.PaymentPeriod5),
                new DataColumn(AppRes.Remain),
                new DataColumn(AppRes.SalaryAccountNumber),
                new DataColumn(AppRes.SalaryAccountName),
                new DataColumn(AppRes.ReceipientBankName),
                new DataColumn(AppRes.Nontaxableincome),
                new DataColumn(AppRes.NontaxableincomeNote),
                new DataColumn(AppRes.OtherAllowanceDetails),
                new DataColumn(AppRes.Margincompensation),
                new DataColumn(AppRes.MargincompensationNote),
                new DataColumn(AppRes.DecemberBonus),
                new DataColumn(AppRes.TotalSalary),
                new DataColumn(AppRes.HoldSaraly),
                new DataColumn(AppRes.StatusOfLaborContract),
                new DataColumn(AppRes.Policy),
                new DataColumn(AppRes.TimeLate),
                new DataColumn(AppRes.OverTime),
                new DataColumn(AppRes.AllowancesPosition),
                new DataColumn(AppRes.Allowancesparkingfee),
                new DataColumn(AppRes.AllowancesPhone),
                new DataColumn(AppRes.OtherAllowances),
                new DataColumn(AppRes.C_Socialinsurance),
                new DataColumn(AppRes.C_Healthinsurance),
                new DataColumn(AppRes.C_Unemploymentinsurance),
                new DataColumn(AppRes.C_KPCD),
                new DataColumn(AppRes.P_Socialinsurance),
                new DataColumn(AppRes.P_Healthinsurance),
                new DataColumn(AppRes.P_Unemploymentinsurance),
                new DataColumn(AppRes.P_KPCD),
                new DataColumn(AppRes.Deductionitself),
                new DataColumn(AppRes.NumRelatedperson),
                new DataColumn(AppRes.Deduct),
                new DataColumn(AppRes.Foodexpenses),
                new DataColumn(AppRes.Taxableincome),
                new DataColumn(AppRes.PaymentNextMonth),
                new DataColumn(AppRes.WorkingAdjusted),
                new DataColumn(AppRes.NoteAdjusted),
                new DataColumn(AppRes.Locked),
                new DataColumn(AppRes.Currency),
                new DataColumn(AppRes.Company),
                new DataColumn(AppRes.Manager),
                new DataColumn(AppRes.Period),
                new DataColumn(AppRes.RewardsEnterHands),
                new DataColumn(AppRes.BonusKPIYear),
                new DataColumn(AppRes.OtherKPIYear),
            });

            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(string);
            dt.Columns[9].DataType = typeof(double);
            dt.Columns[10].DataType = typeof(double);
            dt.Columns[11].DataType = typeof(double);
            dt.Columns[12].DataType = typeof(double);
            dt.Columns[13].DataType = typeof(double);
            dt.Columns[14].DataType = typeof(double);
            dt.Columns[15].DataType = typeof(double);
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[17].DataType = typeof(double);
            dt.Columns[18].DataType = typeof(double);
            dt.Columns[19].DataType = typeof(double);
            dt.Columns[20].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(string);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[24].DataType = typeof(double);
            dt.Columns[25].DataType = typeof(double);
            dt.Columns[26].DataType = typeof(double);
            dt.Columns[27].DataType = typeof(double);
            dt.Columns[28].DataType = typeof(double);
            dt.Columns[29].DataType = typeof(string);
            dt.Columns[30].DataType = typeof(double);
            dt.Columns[31].DataType = typeof(double);
            dt.Columns[32].DataType = typeof(double);
            dt.Columns[33].DataType = typeof(double);
            dt.Columns[34].DataType = typeof(double);
            dt.Columns[35].DataType = typeof(double);
            dt.Columns[36].DataType = typeof(double);
            dt.Columns[37].DataType = typeof(double);
            dt.Columns[38].DataType = typeof(double);
            dt.Columns[39].DataType = typeof(string);
            dt.Columns[40].DataType = typeof(string);
            dt.Columns[41].DataType = typeof(string);
            dt.Columns[42].DataType = typeof(double);
            dt.Columns[43].DataType = typeof(string);
            dt.Columns[44].DataType = typeof(string);
            dt.Columns[45].DataType = typeof(double);
            dt.Columns[46].DataType = typeof(string);
            dt.Columns[47].DataType = typeof(double);
            dt.Columns[48].DataType = typeof(double);
            dt.Columns[49].DataType = typeof(int);
            dt.Columns[50].DataType = typeof(string);
            dt.Columns[51].DataType = typeof(string);
            dt.Columns[52].DataType = typeof(double);
            dt.Columns[53].DataType = typeof(double);
            dt.Columns[54].DataType = typeof(double);
            dt.Columns[55].DataType = typeof(double);
            dt.Columns[56].DataType = typeof(double);
            dt.Columns[57].DataType = typeof(double);
            dt.Columns[58].DataType = typeof(double);
            dt.Columns[59].DataType = typeof(double);
            dt.Columns[60].DataType = typeof(double);
            dt.Columns[61].DataType = typeof(double);
            dt.Columns[62].DataType = typeof(double);
            dt.Columns[63].DataType = typeof(double);
            dt.Columns[64].DataType = typeof(double);
            dt.Columns[65].DataType = typeof(double);
            dt.Columns[66].DataType = typeof(double);
            dt.Columns[67].DataType = typeof(double);
            dt.Columns[68].DataType = typeof(double);
            dt.Columns[69].DataType = typeof(double);
            dt.Columns[70].DataType = typeof(double);
            dt.Columns[71].DataType = typeof(double);
            dt.Columns[72].DataType = typeof(double);
            dt.Columns[73].DataType = typeof(string);
            dt.Columns[74].DataType = typeof(string);
            dt.Columns[75].DataType = typeof(string);
            dt.Columns[76].DataType = typeof(string);
            dt.Columns[77].DataType = typeof(string);
            dt.Columns[78].DataType = typeof(double);
            dt.Columns[79].DataType = typeof(double);
            dt.Columns[80].DataType = typeof(double);
            dt.Columns[81].DataType = typeof(double);
            var db = new SalaryDAL();
            int? total = 0;
            TableColumnsTotal totalColumns = new TableColumnsTotal();
            var baseListParam = new BaseListParam()
            {
                FilterField = filterString,
                OrderByField = "",
                PageIndex = pageIndex,
                PageSize = int.MaxValue,
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
            };
            int type = 1;
            var lstData = db.ExportExcelSalary(baseListParam, type, out total, out totalColumns);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.MonthYear == null ? "" : item.MonthYear,
                    item.StaffCode == null ? "" : item.StaffCode,
                    item.FullName == null ? "" : item.FullName,
                     item.OrganizationUnitCode == null ? "" : item.OrganizationUnitCode,
                    item.OrganizationUnitName == null ? "" : item.OrganizationUnitName,
                    item.OfficePositionName == null ? "" : item.OfficePositionName,
                    item.StaffLevelName == null ? "" : item.StaffLevelName,
                    item.EmployeeStatus == null ? "" : item.EmployeeStatus,
                    item.EmailPersonal == null ? "" : item.EmailPersonal,
                    item.Workingday == null ? 0 : item.Workingday,
                    item.Standardworkingday == null ? 0 : item.Standardworkingday,
                    item.BasicSalary == null ? 0 : item.BasicSalary,
                    item.Bonus == null ? 0 : item.Bonus,
                    item.SeniorityPay == null ? 0 : item.SeniorityPay,
                    item.AllowancesLaptop == null ? 0 : item.AllowancesLaptop,
                    item.TotalOtherAllowances == null ? 0 : item.TotalOtherAllowances,//15
                    item.BDOAllowances == null ? 0 : item.BDOAllowances,//16
                    item.IncomePerDay == null ? 0 : item.IncomePerDay,
                    item.SalarylastMonth == null ? 0 : item.SalarylastMonth,
                    item.Commission == null ? 0 : item.Commission,
                    item.OtherBonus == null ? 0 : item.OtherBonus,//20
                    item.BonusNote == null ? "" : item.BonusNote,//21
                    item.PersonalIncomeTax == null ? 0 : item.PersonalIncomeTax,
                    item.AmountInsurance == null ? 0 : item.AmountInsurance,
                    item.UnionFund == null ? 0 : item.UnionFund,
                    item.TardinessReduction == null ? 0 : item.TardinessReduction,
                    item.QCBonus == null ? 0 : item.QCBonus,
                    item.AdvancePayment == null ? 0 : item.AdvancePayment,//27
                    item.OtherReduction == null ? 0 : item.OtherReduction,//28
                    item.ReductionNote == null ? "" : item.ReductionNote,//29
                    item.TotalIncome == null ? 0 : item.TotalIncome,
                    item.TotalReduction == null ? 0 : item.TotalReduction,
                    item.Netsalary == null ? 0 : item.Netsalary,
                    item.PaymentPeriod1 == null ? 0 : item.PaymentPeriod1,
                    item.PaymentPeriod2 == null ? 0 : item.PaymentPeriod2,
                    item.PaymentPeriod3 == null ? 0 : item.PaymentPeriod3,
                    item.PaymentPeriod4 == null ? 0 : item.PaymentPeriod4,
                    item.PaymentPeriod5 == null ? 0 : item.PaymentPeriod5,
                    item.Remain == null ? 0 : item.Remain,
                    item.AccountNumber == null ? "" : item.AccountNumber,
                    item.AccountName == null ? "" : item.AccountName,
                    item.BankName == null ? "" : item.BankName,
                    item.Nontaxableincome == null ? 0 : item.Nontaxableincome,//42
                    item.NontaxableincomeNote == null ? "" : item.NontaxableincomeNote,//43
                    item.AllowancesNote == null ? "" : item.AllowancesNote,//44
                    item.Margincompensation == null ? 0 : item.Margincompensation,//45
                    item.MargincompensationNote == null ? "" : item.MargincompensationNote,//46
                    item.Decemberbonus == null ? 0 : item.Decemberbonus,//47
                    item.TotalSalary == null ? 0 : item.TotalSalary,
                    item.HoldSalary == null ? 0 : item.HoldSalary,
                    item.StatusContractName == null ? "" : item.StatusContractName,
                    item.PolicyName == null ? "" : item.PolicyName,
                    item.TimeLate == null ? 0 : item.TimeLate,
                    item.OverTime == null ? 0 : item.OverTime,
                    item.AllowancesPosition == null ? 0 : item.AllowancesPosition,
                    item.Allowancesparkingfee == null ? 0 : item.Allowancesparkingfee,
                    item.AllowancesPhone == null ? 0 : item.AllowancesPhone,
                    item.OtherAllowances == null ? 0 : item.OtherAllowances,
                    item.C_Socialinsurance == null ? 0 : item.C_Socialinsurance,
                    item.C_Healthinsurance == null ? 0 : item.C_Healthinsurance,
                    item.C_Unemploymentinsurance == null ? 0 : item.C_Unemploymentinsurance,
                    item.C_KPCD == null ? 0 : item.C_KPCD,
                    item.P_Socialinsurance == null ? 0 : item.P_Socialinsurance,
                    item.P_Healthinsurance == null ? 0 : item.P_Healthinsurance,
                    item.P_Unemploymentinsurance == null ? 0 : item.P_Unemploymentinsurance,
                    item.P_KPCD == null ? 0 : item.P_KPCD,
                    item.Deductionitself == null ? 0 : item.Deductionitself,
                    item.NumRelatedperson,
                    item.Deduction == null ? 0 : item.Deduction,
                    item.Foodexpenses == null ? 0 : item.Foodexpenses,
                    item.Taxableincome == null ? 0 : item.Taxableincome,
                    item.PaymentNextMonth == null ? 0 : item.PaymentNextMonth,
                    item.WorkingAdjusted == null ? 0 : item.WorkingAdjusted,
                    item.NoteAdjusted == null ? "" : item.NoteAdjusted,
                    item.Locked,
                    item.Currency == null ? "" : item.Currency,
                    item.CompanyName == null ? "" : item.CompanyName,
                    item.ParentName == null ? "" : item.ParentName,
                    item.Period,
                    item.SysBonus,
                    item.BonusKPIYear,
                    item.OtherKPIYear
                    );
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryFull.xlsx");
        }

        /// <summary>
        /// Import Excel tổng hợp phiếu lương
        /// </summary>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "ImportExcelSalary")]
        public ActionResult Upload()
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file-0"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var salaryList = new List<Salary>();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var salary = new Salary();
                            salary.MonthYear = workSheet.Cells[rowIterator, 1].Value == null ? null : workSheet.Cells[rowIterator, 1].Value.ToString();
                            salary.StaffCode = workSheet.Cells[rowIterator, 2].Value == null ? null : workSheet.Cells[rowIterator, 2].Value.ToString();
                            //salary.BDOAllowances = workSheet.Cells[rowIterator, 16].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 16].Value.ToString());
                            //salary.Commission = workSheet.Cells[rowIterator, 19].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 19].Value.ToString());
                            salary.OtherBonus = workSheet.Cells[rowIterator, 20].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 20].Value.ToString());
                            salary.BonusNote = workSheet.Cells[rowIterator, 21].Value == null ? null : workSheet.Cells[rowIterator, 21].Value.ToString();
                            salary.Nontaxableincome = workSheet.Cells[rowIterator, 42].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 42].Value.ToString());
                            salary.NontaxableincomeNote = workSheet.Cells[rowIterator, 43].Value == null ? null : workSheet.Cells[rowIterator, 43].Value.ToString();
                            salary.OtherReduction = workSheet.Cells[rowIterator, 28].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 28].Value.ToString());
                            salary.ReductionNote = workSheet.Cells[rowIterator, 29].Value == null ? null : workSheet.Cells[rowIterator, 29].Value.ToString();
                            salary.OtherAllowances = workSheet.Cells[rowIterator, 15].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 15].Value.ToString());
                            salary.AllowancesNote = workSheet.Cells[rowIterator, 44].Value == null ? null : workSheet.Cells[rowIterator, 44].Value.ToString();
                            salary.Margincompensation = workSheet.Cells[rowIterator, 45].Value == null ? 0: Convert.ToDouble(workSheet.Cells[rowIterator, 45].Value.ToString());
                            salary.MargincompensationNote = workSheet.Cells[rowIterator, 46].Value == null ? null : workSheet.Cells[rowIterator, 46].Value.ToString();
                            salary.AdvancePayment = workSheet.Cells[rowIterator, 27].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 27].Value.ToString());
                            salary.SalarylastMonth = workSheet.Cells[rowIterator, 18].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 18].Value.ToString());
                            salary.Decemberbonus = workSheet.Cells[rowIterator, 47].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 47].Value.ToString());
                            salary.SysBonus = workSheet.Cells[rowIterator, 48].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 48].Value);
                            salary.BonusKPIYear = workSheet.Cells[rowIterator, 49].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 49].Value);
                            salary.OtherKPIYear = workSheet.Cells[rowIterator, 50].Value == null ? 0 : Convert.ToDouble(workSheet.Cells[rowIterator, 50].Value);
                            salaryList.Add(salary);
                        }
                    }
                    var db = new SalaryDAL();
                    var result = db.ImportExcelSalary(salaryList);
                    if (result.IsSuccess == true)
                    {
                        result.Message = AppRes.ImportExcelSuccess;
                    }
                    else
                    {
                        result.Message = AppRes.ImportExcelFailed;
                    }
                    return Content(JsonConvert.SerializeObject(new
                    {
                        result
                    }));
                }
            }
            return View("Index");
        }

        /// <summary>
        /// Cập nhật phiếu lương
        /// </summary>
        /// <param name="isCheckAll"></param>
        /// <param name="listID"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "PublishCaculatorSalary")]
        public ActionResult CreatePayslip(bool isCheckAll, List<string> listID, List<string> listWPID, int month, int year)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
            };
            var db = new SalaryDAL();
            int iResult = 0;
            var result = db.CreatePayslip(baseListParam, listID, listWPID, isCheckAll, month, year, out iResult);
            if (iResult < 0)
            {
                result.Message = AppRes.ErrorUpdateSalary;
                result.IsSuccess = false;
            }
            else
            {

                result.Message = AppRes.CreatePayslipSuccess;
                result.IsSuccess = true;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }


        /// <summary>
        /// Gửi mail phiếu lương
        /// </summary>
        /// <param name="listID"></param>
        /// <param name="TableID"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="Period"></param>
        /// <returns></returns>
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Get, LogStoreProcedure = "Salary_GetsWhereListID")]
        public ActionResult SendMail(bool isCheckAll, string listID, int TableID, int month, int year, int Period)

        {
            var db = new SalaryDAL();
            var ListSalary = db.GetSalaryWhereListstaffid(isCheckAll, listID, Global.CurrentUser.CurrentLanguageID, month, year, Period, Global.CurrentUser.UserID, Global.CurrentUser.RoleId);
            var baseListParam = new BaseListParam()
            {
                FilterField = "",
                OrderByField = "",
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
                DeptId = Global.CurrentUser.OrganizationUnitID,
                PageIndex = 1,
                PageSize = int.MaxValue,
                LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString(),
            };
            int? total = 0;
            string Body = "";
            var ListTemplate = db.GetTemplate(baseListParam, out total);
            foreach (var itemSalary in ListSalary)
            {
                Body = "";
                string cc = itemSalary.cc;
                string[] ccList = null;
                if (cc != null)
                {
                    ccList = cc.Split(',').Select(str => str.Trim()).ToArray();
                }
                string subject = AppRes.PayslipSubject + " " + itemSalary.MonthYear + " - " + AppRes.SalaryPeriod.ToUpper() + " " + Period + " " + itemSalary.FullName.ToUpper();
                string to = itemSalary.EmailPersonal;
                Body += "Dear " + itemSalary.FullName + ",<br/><br/>";
                Body += AppRes.BodyEmail1 + " " + itemSalary.MonthYear + ",<br/><br/>";
                Body += "<table style='border-collapse: collapse;' border=1>";
                Body += "<tr><td align='center' colspan='2' style='font-weight:bold; font-size:16px'>" + AppRes.MonthlyPayslip + " " + itemSalary.MonthYear + "</td></tr>";
                Body += "<tr><td align='center' colspan='2'>" + itemSalary.FullName + " - " + itemSalary.EmailPersonal + "</td></tr>";
                Body += "<tr><td>" + AppRes.StaffCode + " : " + itemSalary.StaffCode + "</td><td>" + AppRes.OrganizationUnit + " : " + itemSalary.OrganizationUnitName + "</td></tr>";
                Body += "<tr><td>" + AppRes.Workingday + " : " + itemSalary.Workingday + "</td><td>" + AppRes.Standardworkingday + " : " + itemSalary.Standardworkingday + "</td></tr>";
                Body += "<tr><td align='center' style='font-weight:bold'>" + AppRes.Body + "</td><td align='center' style='font-weight:bold'>" + AppRes.AmountOfMoney + "</td></tr>";
                foreach (var itemTemplate in ListTemplate.Skip(8).OrderBy(x => x.OrderNo))
                {
                    if (itemTemplate.Value != null)
                    {
                        switch (itemTemplate.DisplayType)
                        {
                            case 0: // giá trị string .
                                if (itemSalary.GetType().GetProperty(itemTemplate.Value).GetValue(itemSalary, null) != null)
                                {
                                    itemTemplate.DisplayValue = (string)itemSalary.GetType().GetProperty(itemTemplate.Value).GetValue(itemSalary, null);
                                }
                                else
                                {
                                    itemTemplate.DisplayValue = "";
                                }

                                break;
                            case 1: // giá trị float
                                if (itemSalary.GetType().GetProperty(itemTemplate.Value).GetValue(itemSalary, null) != null)
                                {
                                    itemTemplate.DisplayValueFloat = (double)itemSalary.GetType().GetProperty(itemTemplate.Value).GetValue(itemSalary, null);

                                }
                                else
                                {
                                    itemTemplate.DisplayValueFloat = 0;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (itemTemplate.DisplayType != 0)
                    {
                        Body += "<tr>" +
                                    "<td style='" + itemTemplate.Align + "' colspan='" + itemTemplate.Colspan + "'>"
                                        + (baseListParam.LanguageCode == "4" ? itemTemplate.NameEN : itemTemplate.Name) +
                                    "</td>";
                        if (itemTemplate.Value == null) { }
                        else
                        {
                            Body += "<td style='" + itemTemplate.Css + "'>"
                                       + FormatUtil.FormatNumber(itemTemplate.DisplayValueFloat) +
                                   "</td>" +
                              "</tr>";
                        }
                    }
                    else
                    {
                        Body += "<tr>" +
                                    "<td style='" + itemTemplate.Align + "' colspan='" + itemTemplate.Colspan + "'>"
                                        + (baseListParam.LanguageCode == "4" ? itemTemplate.NameEN : itemTemplate.Name) +
                                    "</td>";
                        if (itemTemplate.Value == null) { }
                        else
                        {
                            Body += "<td  style='" + itemTemplate.Css + "'>"
                                       + itemTemplate.DisplayValue +
                                   "</td>" +
                              "</tr>";
                        }
                    }
                }
                Body += "</table><br/><br/>";
                Body += AppRes.FeedbackPayslipEmail;
                var Result = EmailUtils.Send(new[] { to }, ccList, subject, HttpUtility.HtmlDecode(Body));
                string Message = "";
                if (Result == 1)
                {
                    Message = AppRes.SendPayslipSusscess + " " + Period + " " + AppRes.Success;
                }
                else
                {
                    Message = AppRes.SendPayslipFailed + " " + Period + " " + AppRes.Unsuccessful;
                }
                ViewBag.SendMessage = Message;
                ViewBag.Result = Result;
                ViewBag.ListNameStaff += ", " + itemSalary.FullName;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                sendMessage = ViewBag.SendMessage,
                result = ViewBag.Result,
                listNameStaff = ViewBag.ListNameStaff
            }));
        }

        //[Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Excel)]
        //public ActionResult SalaryExportExcel(string filterString, int pageIndex, int pageSize)
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[78]
        //    {
        //        new DataColumn(AppRes.StaffID), //0
        //        new DataColumn(AppRes.FullName),
        //        new DataColumn("OrganizationUnitID"),//2
        //        new DataColumn(AppRes.Status),
        //        new DataColumn(AppRes.Department),
        //        new DataColumn("qtctht-StartDate"),
        //        new DataColumn("qtctht-EndDate"),
        //        new DataColumn("qtctht-BasicPay"),//7
        //        new DataColumn("qtctht-EfficiencyBonus"),//8
        //        new DataColumn("qtctht-WPID"),//9
        //        new DataColumn("qtcttd-StartDate"),
        //        new DataColumn("qtcttd-EndDate"),
        //        new DataColumn("qtcttd-BasicPay"),//12
        //        new DataColumn("qtcttd-EfficiencyBonus"),//13
        //        new DataColumn("qtcttd-WPID"),//14
        //        new DataColumn("bhht-BasicSalary"),//15
        //        new DataColumn("bhht-FromMonth"),
        //        new DataColumn("bhht-ToMonth"),
        //        new DataColumn("bhht-InsuranceStatus"),
        //        new DataColumn("bhtd-BasicSalary"),//19
        //        new DataColumn("bhtd-FromMonth"),
        //        new DataColumn("bhtd-ToMonth"),
        //        new DataColumn("bhtd-InsuranceStatus"),
        //        new DataColumn("pclht-Amount"),//23
        //        new DataColumn("pclht-StartDate"),
        //        new DataColumn("pclht-EndDate"),
        //        new DataColumn("pcltd-Amount"),//26
        //        new DataColumn("pcltd-StartDate"),
        //        new DataColumn("pcltd-EndDate"),
        //        new DataColumn("pctnht-Amount"),//29
        //        new DataColumn("pctnht-StartDate"),
        //        new DataColumn("pctnht-EndDate"),
        //        new DataColumn("pctntd-Amount"),//32
        //        new DataColumn("pctntd-StartDate"),
        //        new DataColumn("pctntd-EndDate"),

        //        new DataColumn("pcgxht-Amount"),//35
        //        new DataColumn("pcgxht-StartDate"),
        //        new DataColumn("pcgxht-EndDate"),

        //        new DataColumn("pcgxtd-Amount"),//38
        //        new DataColumn("pcgxtd-StartDate"),
        //        new DataColumn("pcgxtd-EndDate"),

        //        new DataColumn("pcadminht-Amount"),//41
        //        new DataColumn("pcadminht-StartDate"),
        //        new DataColumn("pcadminht-EndDate"),

        //        new DataColumn("pcadmintd-Amount"),//44
        //        new DataColumn("pcadmintd-StartDate"),
        //        new DataColumn("pcadmintd-EndDate"),

        //        new DataColumn("pcrrht-Amount"),//47
        //        new DataColumn("pcrrht-StartDate"),
        //        new DataColumn("pcrrht-EndDate"),

        //        new DataColumn("pcrrtd-Amount"),//50
        //        new DataColumn("pcrrtd-StartDate"),
        //        new DataColumn("pcrrtd-EndDate"),

        //        new DataColumn("pcnhht-Amount"),//53
        //        new DataColumn("pcnhht-StartDate"),
        //        new DataColumn("pcnhht-EndDate"),

        //        new DataColumn("pcnhtd-Amount"),//56
        //        new DataColumn("pcnhtd-StartDate"),
        //        new DataColumn("pcnhtd-EndDate"),

        //        new DataColumn("pccvht-Amount"),
        //        new DataColumn("pccvht-StartDate"),
        //        new DataColumn("pccvht-EndDate"),

        //        new DataColumn("pccvtd-Amount"),
        //        new DataColumn("pccvtd-StartDate"),
        //        new DataColumn("pccvtd-EndDate"), //60


        //        new DataColumn("pcbdht-Amount"),
        //        new DataColumn("pcbdht-StartDate"),
        //        new DataColumn("pcbdht-EndDate"),

        //        new DataColumn("pcbdtd-Amount"),
        //        new DataColumn("pcbdtd-StartDate"),
        //        new DataColumn("pcbdtd-EndDate"), //66
        //        new DataColumn("ID máy chấm công"),
        //        new DataColumn("Tên máy chấm công"),
        //        new DataColumn("ID người quản lý tt"),
        //        new DataColumn("Người quản lý trực tiếp"),
        //        new DataColumn("ID HR quản lý"),
        //        new DataColumn("HR quản lý trực tiếp"),
        //        //new DataColumn(AppRes.StatusContract),
        //        new DataColumn("Văn phòng"),
        //        //new DataColumn(AppRes.Position)
        //        //,new DataColumn("CompanyName")
        //    });

        //    dt.Columns[0].DataType = typeof(double);
        //    dt.Columns[2].DataType = typeof(double);
        //    dt.Columns[7].DataType = typeof(double);
        //    dt.Columns[8].DataType = typeof(double);
        //    dt.Columns[9].DataType = typeof(double);
        //    dt.Columns[12].DataType = typeof(double);
        //    dt.Columns[13].DataType = typeof(double);
        //    dt.Columns[14].DataType = typeof(double);
        //    dt.Columns[15].DataType = typeof(double);
        //    dt.Columns[19].DataType = typeof(double);

        //    dt.Columns[23].DataType = typeof(double);
        //    dt.Columns[26].DataType = typeof(double);
        //    dt.Columns[29].DataType = typeof(double);
        //    dt.Columns[32].DataType = typeof(double);
        //    dt.Columns[35].DataType = typeof(double);
        //    dt.Columns[38].DataType = typeof(double);
        //    dt.Columns[41].DataType = typeof(double);
        //    dt.Columns[44].DataType = typeof(double);
        //    dt.Columns[47].DataType = typeof(double);
        //    dt.Columns[50].DataType = typeof(double);
        //    dt.Columns[53].DataType = typeof(double);
        //    var db = new SalaryDAL();
        //    int? total = 0;
        //    var baseListParam = new BaseListParam()
        //    {
        //        FilterField = filterString,
        //        OrderByField = "",
        //        PageIndex = pageIndex,
        //        PageSize = int.MaxValue,
        //        UserType = Global.CurrentUser.RoleId,
        //        UserId = Global.CurrentUser.LoginUserId,
        //        DeptId = Global.CurrentUser.OrganizationUnitID,
        //        LanguageCode = Global.CurrentUser.CurrentLanguageID.ToString()
        //    };
        //    var lstData = db.ExportExcelSalary(baseListParam, out total);
        //    foreach (var item in lstData)
        //    {
        //        dt.Rows.Add(
        //            item.staffid,
        //            item.StaffName,
        //            item.OrganizationUnitID,
        //            item.StatusName,
        //            item.OrganizationUnit,
        //            item.qtctStartDate == null ? null : item.qtctStartDate,
        //            item.qtctEndDate == null ? null : item.qtctEndDate,
        //            item.qtctBasicPay,
        //            item.qtctEfficiencyBonus,
        //            item.qtctWPID,
        //            item.qtct1StartDate == null ? null : item.qtct1StartDate,
        //            item.qtct1EndDate == null ? null : item.qtct1EndDate,
        //            item.qtct1BasicPay,
        //            item.qtct1EfficiencyBonus,
        //            item.qtct1WPID,
        //            item.bhBasicSalary,
        //            item.bhFromMonth == null ? null : item.bhFromMonth,
        //            item.bhToMonth == null ? null : item.bhToMonth,
        //            item.trangthaibh,
        //            item.bh1BasicSalary,
        //            item.bh1FromMonth == null ? null : item.bh1FromMonth,
        //            item.bh1ToMonth == null ? null : item.bh1ToMonth,
        //            item.trangthaibh1,
        //            item.pcl1Amount,
        //            item.pclStartDate == null ? null : item.pclStartDate,
        //            item.pclEndDate == null ? null : item.pclEndDate,
        //            item.pcl1Amount,
        //            item.pcl1StartDate == null ? null : item.pcl1StartDate,
        //            item.pcl1EndDate == null ? null : item.pcl1EndDate,
        //            item.pctnAmount,
        //            item.pctnStartDate == null ? null : item.pctnStartDate,
        //            item.pctnEndDate == null ? null : item.pctnEndDate,
        //            item.pctn1Amount,
        //            item.pctn1StartDate == null ? null : item.pctn1StartDate,
        //            item.pctn1EndDate == null ? null : item.pctn1EndDate,
        //            item.pcgxAmount,
        //            item.pcgxStartDate == null ? null : item.pcgxStartDate,
        //            item.pcgxEndDate == null ? null : item.pcgxEndDate,
        //            item.pcgx1Amount,
        //            item.pcgx1StartDate == null ? null : item.pcgx1StartDate,
        //            item.pcgx1EndDate == null ? null : item.pcgx1EndDate,
        //            item.pcadminAmount,
        //            item.pcadminStartDate == null ? null : item.pcadminAmount,
        //            item.pcadminEndDate == null ? null : item.pcadminEndDate,
        //            item.pcadmin1Amount,
        //            item.pcadmin1StartDate == null ? null : item.pcadmin1StartDate,
        //            item.pcadmin1EndDate == null ? null : item.pcadmin1EndDate,
        //            item.pcrrAmount,
        //            item.pcrrStartDate == null ? null : item.pcrrStartDate,
        //            item.pcrrEndDate == null ? null : item.pcrrEndDate,
        //            item.pcrr1Amount,
        //            item.pcrr1StartDate == null ? null : item.pcrr1StartDate,
        //            item.pcrr1EndDate == null ? null : item.pcrr1EndDate,
        //            item.pcnhAmount,
        //            item.pcnhStartDate == null ? null : item.pcnhStartDate,
        //            item.pcnhEndDate == null ? null : item.pcnhEndDate,
        //            item.pcnh1Amount,
        //            item.pcnh1StartDate == null ? null : item.pcnh1StartDate,
        //            item.pcnh1EndDate == null ? null : item.pcnh1EndDate,
        //            item.pccvAmount,
        //            item.pccvStartDate == null ? null : item.pccvStartDate,
        //            item.pccvEndDate == null ? null : item.pccvEndDate,
        //            item.pccv1Amount,
        //            item.pccv1StartDate == null ? null : item.pccv1StartDate,
        //            item.pccv1EndDate == null ? null : item.pccv1EndDate,

        //            item.pcbdAmount,
        //            item.pcbdStartDate == null ? null : item.pcbdStartDate,
        //            item.pcbdEndDate == null ? null : item.pcbdEndDate,
        //            item.pcbd1Amount,
        //            item.pcbd1StartDate == null ? null : item.pcbd1StartDate,
        //            item.pcbd1EndDate == null ? null : item.pcbd1EndDate,
        //            item.WorkingDayMachineID,
        //            item.WorkingDayMachinename,
        //            item.WorkingManagerID,
        //            item.WorkingManagerName,
        //            item.WorkingHRID,
        //            item.WorkingHRManagerName,
        //            item.OfficeName
        //           //item.ContractType
        //           );
        //    }
        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt);
        //    byte[] data = null;
        //    using (var stream = new MemoryStream())
        //    {
        //        wb.SaveAs(stream);
        //        data = stream.ToArray();
        //    }
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryFull.xlsx");
        //}
 
        [Permission(TableID = (int)ETable.Salary, TypeAction = (int)EAction.Submit)]
        [WriteLog(Action = EAction.Submit, LogStoreProcedure = "PublishBonus")]
        public ActionResult UpdateBonus(bool isCheckAll, List<string> listID, List<string> listWPID, int month, int year, int policyBonusIDRun)
        {
            var baseListParam = new BaseListParam()
            {
                UserType = Global.CurrentUser.RoleId,
                UserId = Global.CurrentUser.LoginUserId,
            };
            var db = new SalaryDAL();
            int iResult = 0;
            var result = db.UpdateBonus(baseListParam, listID, listWPID, isCheckAll, month, year, policyBonusIDRun, out iResult);
            if (iResult < 0)
            {
                result.Message = AppRes.ErrorUpdateBonus;
                result.IsSuccess = false;
            }
            else
            {

                result.Message = AppRes.UpdateBonusSuccess;
                result.IsSuccess = true;
            }
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
    }
}