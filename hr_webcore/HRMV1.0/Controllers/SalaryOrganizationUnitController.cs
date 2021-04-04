using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.Constants;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using HRM.Logger;
using Newtonsoft.Json;

namespace HRM.Controllers
{
    public class SalaryOrganizationUnitController : Controller
    {
        // GET: SalaryOrganizationUnit
        public ActionResult Index()
        {
            return PartialView();
        }



        [Permission(TableID = (int)Constant.ETable.Salary, TypeAction = (int)Constant.EAction.Get)]
        [WriteLog(Action = Constant.EAction.Get, LogStoreProcedure = "Salary_Gets")]
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
            int type = 2; //lương theo phòng ban
            var result = db.GetSalary(baseListParam,type, out total, out totalColumns);

            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = totalColumns,
                staffID = baseListParam.UserId
            }));
        }

        [Permission(TableID = (int)Constant.ETable.Salary, TypeAction = (int)Constant.EAction.Excel)]
        [WriteLog(Action = Constant.EAction.Excel, LogStoreProcedure = "Salary_Gets")]
        public ActionResult SalaryExportExcel(string filterString, int pageIndex, int pageSize)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[79]
            {
                new DataColumn(AppRes.MonthYear),
                new DataColumn(AppRes.StaffCode),
                new DataColumn(AppRes.FullName),
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

            });

            dt.Columns[0].DataType = typeof(string);
            dt.Columns[1].DataType = typeof(string);
            dt.Columns[2].DataType = typeof(string);
            dt.Columns[3].DataType = typeof(string);
            dt.Columns[4].DataType = typeof(string);
            dt.Columns[5].DataType = typeof(string);
            dt.Columns[6].DataType = typeof(string);
            dt.Columns[7].DataType = typeof(string);
            dt.Columns[8].DataType = typeof(double);
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
            dt.Columns[20].DataType = typeof(string);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[22].DataType = typeof(double);
            dt.Columns[23].DataType = typeof(double);
            dt.Columns[24].DataType = typeof(double);
            dt.Columns[25].DataType = typeof(double);
            dt.Columns[26].DataType = typeof(double);
            dt.Columns[27].DataType = typeof(double);
            dt.Columns[28].DataType = typeof(string);
            dt.Columns[29].DataType = typeof(double);
            dt.Columns[30].DataType = typeof(double);
            dt.Columns[31].DataType = typeof(double);
            dt.Columns[32].DataType = typeof(double);
            dt.Columns[33].DataType = typeof(double);
            dt.Columns[34].DataType = typeof(double);
            dt.Columns[35].DataType = typeof(double);
            dt.Columns[36].DataType = typeof(double);
            dt.Columns[37].DataType = typeof(double);
            dt.Columns[38].DataType = typeof(string);
            dt.Columns[39].DataType = typeof(string);
            dt.Columns[40].DataType = typeof(string);
            dt.Columns[41].DataType = typeof(double);
            dt.Columns[42].DataType = typeof(string);
            dt.Columns[43].DataType = typeof(string);
            dt.Columns[44].DataType = typeof(double);
            dt.Columns[45].DataType = typeof(string);
            dt.Columns[46].DataType = typeof(double);
            dt.Columns[47].DataType = typeof(double);
            dt.Columns[48].DataType = typeof(int);
            dt.Columns[49].DataType = typeof(string);
            dt.Columns[50].DataType = typeof(string);
            dt.Columns[51].DataType = typeof(double);
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
            dt.Columns[72].DataType = typeof(string);
            dt.Columns[73].DataType = typeof(string);
            dt.Columns[74].DataType = typeof(string);
            dt.Columns[75].DataType = typeof(string);
            dt.Columns[76].DataType = typeof(string);
            dt.Columns[77].DataType = typeof(double);
            dt.Columns[78].DataType = typeof(double);
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
            int type = 2;
            var lstData = db.ExportExcelSalary(baseListParam,type, out total, out totalColumns);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.MonthYear == null ? "" : item.MonthYear,
                    item.StaffCode == null ? "" : item.StaffCode,
                    item.FullName == null ? "" : item.FullName,
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
                    item.SysBonus
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

    }
}