using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ERP.Framework.DataBusiness.Common;
using HRM.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.DAL;
using Newtonsoft.Json;
using static HRM.Constants.Constant;

namespace HRM.Controllers
{
    public class CheckSalaryErpController : Controller
    {
        [Permission(TableID = (int)ETable.CheckSalaryErp, TypeAction = (int)EAction.Index)]
        // GET: CheckSalaryErp
        public ActionResult Index()
        {
            ViewBag.url = "/CheckSalaryErp/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.CheckSalaryErp, TypeAction = (int)EAction.Get)]
        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new CheckSalaryErpDAL();
            int? total = 0;
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
            var result = db.GetSalaryErpData(baseListParam, out total);
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                roleid = baseListParam.UserType
            }));
        }


        [Permission(TableID = (int)ETable.CheckSalaryErp, TypeAction = (int)EAction.Excel)]
        public ActionResult CheckSalaryErpExportExcel(string filterString, int pageIndex, int pageSize)
        {
            filterString = filterString.Replace("!!", "%");
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[86]
            {
                new DataColumn(AppRes.StaffID), //0
                new DataColumn(AppRes.FullName),
                new DataColumn(AppRes.OrganazationUnitCode),//2
                new DataColumn(AppRes.Status),
                new DataColumn(AppRes.Department),
                new DataColumn(AppRes.qtctht_StartDate),
                new DataColumn(AppRes.qtctht_EndDate),
                new DataColumn(AppRes.qtctht_BasicPay),//7
                new DataColumn(AppRes.qtctht_EfficiencyBonus),//8
                new DataColumn("Chính sách (QTCT-Hiện tại)"),
                new DataColumn("Trạng thái hợp đồng (QTCT-Hiện tại)"),
                new DataColumn("Cấp bậc (QTCT-Hiện tại)"),
                new DataColumn(AppRes.qtctht_WPID),
                new DataColumn(AppRes.qtcttd_StartDate),
                new DataColumn(AppRes.qtcttd_EndDate),
                new DataColumn(AppRes.qtcttd_BasicPay),//15
                new DataColumn(AppRes.qtcttd_EfficiencyBonus),//16
                new DataColumn("Chính sách (QTCT-Trước đó)"),
                new DataColumn("Trạng thái hợp đồng (QTCT-Trước đó)"),
                new DataColumn("Cấp bậc (QTCT-Trước đó)"),
                new DataColumn(AppRes.qtcttd_WPID),
                new DataColumn(AppRes.bhht_BasicSalary),//21
                new DataColumn(AppRes.bhht_FromMonth),
                new DataColumn(AppRes.bhht_ToMonth),
                new DataColumn(AppRes.bhht_InsuranceStatus),
                new DataColumn(AppRes.bhtd_BasicSalary),//25
                new DataColumn(AppRes.bhtd_FromMonth),
                new DataColumn(AppRes.bhtd_ToMonth),
                new DataColumn(AppRes.bhtd_InsuranceStatus),
                new DataColumn(AppRes.pclht_Amount),//29
                new DataColumn(AppRes.pclht_StartDate),
                new DataColumn(AppRes.pclht_EndDate),
                new DataColumn(AppRes.pcltd_Amount),//32
                new DataColumn(AppRes.pcltd_StartDate),
                new DataColumn(AppRes.pcltd_EndDate),
                new DataColumn(AppRes.pctnht_Amount),//35
                new DataColumn(AppRes.pctnht_StartDate),
                new DataColumn(AppRes.pctnht_EndDate),
                new DataColumn(AppRes.pctntd_Amount),//38
                new DataColumn(AppRes.pctntd_StartDate),
                new DataColumn(AppRes.pctntd_EndDate),

                new DataColumn(AppRes.pcgxht_Amount),//41
                new DataColumn(AppRes.pcgxht_StartDate),
                new DataColumn(AppRes.pcgxht_EndDate),

                new DataColumn(AppRes.pcgxtd_Amount),//44
                new DataColumn(AppRes.pcgxtd_StartDate),
                new DataColumn(AppRes.pcgxtd_EndDate),

                new DataColumn(AppRes.pcadminht_Amount),//47
                new DataColumn(AppRes.pcadminht_StartDate),
                new DataColumn(AppRes.pcadminht_EndDate),

                new DataColumn(AppRes.pcadmintd_Amount),//50
                new DataColumn(AppRes.pcadmintd_StartDate),
                new DataColumn(AppRes.pcadmintd_EndDate),

                new DataColumn(AppRes.pcrrht_Amount),//53
                new DataColumn(AppRes.pcrrht_StartDate),
                new DataColumn(AppRes.pcrrht_EndDate),

                new DataColumn(AppRes.pcrrtd_Amount),//56
                new DataColumn(AppRes.pcrrtd_StartDate),
                new DataColumn(AppRes.pcrrtd_EndDate),

                new DataColumn(AppRes.pcnhht_Amount),//59
                new DataColumn(AppRes.pcnhht_StartDate),
                new DataColumn(AppRes.pcnhht_EndDate),

                new DataColumn(AppRes.pcnhtd_Amount),//62
                new DataColumn(AppRes.pcnhtd_StartDate),
                new DataColumn(AppRes.pcnhtd_EndDate),

                new DataColumn(AppRes.pccvht_Amount),//65
                new DataColumn(AppRes.pccvht_StartDate),
                new DataColumn(AppRes.pccvht_EndDate),

                new DataColumn(AppRes.pccvtd_Amount),//68
                new DataColumn(AppRes.pccvtd_StartDate),
                new DataColumn(AppRes.pccvtd_EndDate),


                new DataColumn(AppRes.pcbdht_Amount),//71
                new DataColumn(AppRes.pcbdht_StartDate),
                new DataColumn(AppRes.pcbdht_EndDate),

                new DataColumn(AppRes.pcbdtd_Amount),//74
                new DataColumn(AppRes.pcbdtd_StartDate),
                new DataColumn(AppRes.pcbdtd_EndDate),
                new DataColumn(AppRes.IDWorkingdayMachine),
                new DataColumn(AppRes.WorkingDayMachineName),
                new DataColumn(AppRes.IDManagerDirect),
                new DataColumn(AppRes.Manager),
                new DataColumn(AppRes.IDHRManager),
                new DataColumn(AppRes.ManagementHR),
                //new DataColumn(AppRes.StatusContract),
                new DataColumn(AppRes.Office),
                //new DataColumn(AppRes.Position)
                //,new DataColumn("CompanyName")
                new DataColumn(AppRes.BankAccount),
                new DataColumn(AppRes.HoldSaraly),
            });

            dt.Columns[0].DataType = typeof(double);
            dt.Columns[2].DataType = typeof(double);
            dt.Columns[7].DataType = typeof(double);
            dt.Columns[8].DataType = typeof(double);
            dt.Columns[15].DataType = typeof(double);
            dt.Columns[16].DataType = typeof(double);
            dt.Columns[21].DataType = typeof(double);
            dt.Columns[25].DataType = typeof(double);
            dt.Columns[29].DataType = typeof(double);
            dt.Columns[32].DataType = typeof(double);

            dt.Columns[35].DataType = typeof(double);
            dt.Columns[38].DataType = typeof(double);
            dt.Columns[41].DataType = typeof(double);
            dt.Columns[44].DataType = typeof(double);
            dt.Columns[47].DataType = typeof(double);
            dt.Columns[50].DataType = typeof(double);

            dt.Columns[53].DataType = typeof(double);
            dt.Columns[56].DataType = typeof(double);
            dt.Columns[59].DataType = typeof(double);
            dt.Columns[62].DataType = typeof(double);
            dt.Columns[65].DataType = typeof(double);
            dt.Columns[68].DataType = typeof(double);
            dt.Columns[71].DataType = typeof(double);
            dt.Columns[74].DataType = typeof(double);
            var db = new CheckSalaryErpDAL();
            int? total = 0;
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
            var lstData = db.ExportExcelSalaryFull(baseListParam, out total);
            foreach (var item in lstData)
            {
                dt.Rows.Add(
                    item.staffid,
                    item.StaffName,
                    item.OrganizationUnitID,
                    item.StatusName,
                    item.OrganizationUnit,
                    item.qtctStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.qtctStartDate),
                    item.qtctEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.qtctEndDate),
                    item.qtctBasicPay,
                    item.qtctEfficiencyBonus,
                    item.qtctPolicy,
                    item.qtctContractType,
                    item.qtctStaffLevel,
                    item.qtctWPID,
                    item.qtct1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.qtct1StartDate),
                    item.qtct1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.qtct1EndDate),
                    item.qtct1BasicPay,
                    item.qtct1EfficiencyBonus,
                    item.qtct1Policy,
                    item.qtct1ContractType,
                    item.qtct1StaffLevel,
                    item.qtct1WPID,
                    item.bhBasicSalary,
                    item.bhFromMonth == null ? null : String.Format("{0:dd/MM/yyyy}", item.bhFromMonth),
                    item.bhToMonth == null ? null : String.Format("{0:dd/MM/yyyy}", item.bhToMonth),
                    item.trangthaibh,
                    item.bh1BasicSalary,
                    item.bh1FromMonth == null ? null : String.Format("{0:dd/MM/yyyy}", item.bh1FromMonth),
                    item.bh1ToMonth == null ? null : String.Format("{0:dd/MM/yyyy}", item.bh1ToMonth),
                    item.trangthaibh1,
                    item.pclAmount,
                    item.pclStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pclStartDate),
                    item.pclEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pclEndDate),
                    item.pcl1Amount,
                    item.pcl1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcl1StartDate),
                    item.pcl1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcl1EndDate),
                    item.pctnAmount,
                    item.pctnStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pctnStartDate),
                    item.pctnEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pctnEndDate),
                    item.pctn1Amount,
                    item.pctn1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pctn1StartDate),
                    item.pctn1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pctn1EndDate),
                    item.pcgxAmount,
                    item.pcgxStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcgxStartDate),
                    item.pcgxEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcgxEndDate),
                    item.pcgx1Amount,
                    item.pcgx1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcgx1StartDate),
                    item.pcgx1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcgx1EndDate),
                    item.pcadminAmount,
                    item.pcadminStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcadminStartDate),
                    item.pcadminEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcadminEndDate),
                    item.pcadmin1Amount,
                    item.pcadmin1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcadmin1StartDate),
                    item.pcadmin1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcadmin1EndDate),
                    item.pcrrAmount,
                    item.pcrrStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcrrStartDate),
                    item.pcrrEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcrrEndDate),
                    item.pcrr1Amount,
                    item.pcrr1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcrr1StartDate),
                    item.pcrr1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcrr1EndDate),
                    item.pcnhAmount,
                    item.pcnhStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcnhStartDate),
                    item.pcnhEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcnhEndDate),
                    item.pcnh1Amount,
                    item.pcnh1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcnh1StartDate),
                    item.pcnh1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcnh1EndDate),
                    item.pccvAmount,
                    item.pccvStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pccvStartDate),
                    item.pccvEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pccvEndDate),
                    item.pccv1Amount,
                    item.pccv1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pccv1StartDate),
                    item.pccv1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pccv1EndDate),

                    item.pcbdAmount,
                    item.pcbdStartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcbdStartDate),
                    item.pcbdEndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcbdEndDate),
                    item.pcbd1Amount,
                    item.pcbd1StartDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcbd1StartDate),
                    item.pcbd1EndDate == null ? null : String.Format("{0:dd/MM/yyyy}", item.pcbd1EndDate),
                    item.WorkingDayMachineID,
                    item.WorkingDayMachinename,
                    item.WorkingManagerID,
                    item.WorkingManagerName,
                    item.WorkingHRID,
                    item.WorkingHRManagerName,
                    item.OfficeName,
                   //item.ContractType,
                   item.BankNumber,
                   item.HoldSaraly
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
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CheckSalaryErp.xlsx");
        }
    }
}