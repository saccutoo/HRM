using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using HRM.DataAccess;
using HRM.DataAccess.DAL;
using Newtonsoft.Json;
using HRM.DataAccess.Entity;
using HRM.Security;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class DemoTableController : Controller
    {
        // GET: DemoTable
        public ActionResult Index()

        {
            ViewBag.url = "/DemoTable/TableServerSideGetData";
            return PartialView();
        }
        //public ActionResult TableServerSideGetData(int pageIndex, int pageSize,string filter="")
        //{
        //    var db = new TableDal();
        //    int total = 0;
        //    var result = db.GetEmloyeeDemo(pageIndex, pageSize, filter, out total);
        //    var lstTotal = new TableColumnsTotalViewModel();
        //    lstTotal.Total1 = "15";
        //    lstTotal.Total2 = "25";
        //    lstTotal.Total3 = "35";
        //    return Content(JsonConvert.SerializeObject(new
        //    {
        //        employees = result,
        //        totalCount = total,
        //        lstTotal = lstTotal
        //    }));
        //}
        public ActionResult TableServerSide()
        {
            return PartialView();
        }

        public ActionResult AddEmp()
        {
            return PartialView();
        }

        public ActionResult EditEmp()
        {
            return PartialView();
        }
        public ActionResult GetEditItemById(int id,int idTable)
        {
            var db = new TableDal();
            var result = db.GetEmpById(1, idTable,id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }
        //public ActionResult _EditEmp(int id,string name, string DOB, string Gender, string Email, string Mobile, string Address)
        //{
        //    var db = new TableDal();
        //    Employee emp = new Employee();
        //    emp.name = name;
        //    emp.DOB = DateTime.Parse(DOB);
        //    emp.Gender = Gender;
        //    emp.Address = Address;
        //    emp.Email = Email;
        //    emp.Mobile = Mobile;
        //    var result = db.UpdateEmpById(1,1,id, emp);
        //    return Content(JsonConvert.SerializeObject(new
        //    {
        //        result
        //    }));
        //}
        //public ActionResult _AddEmp(string name, string DOB,string Gender,string Email,string Mobile,string Address)
        //{
        //    var db = new TableDal();
        //    Employee emp = new Employee();
        //    emp.name = name;
        //    emp.DOB = DateTime.Parse(DOB);
        //    emp.Gender = Gender;
        //    emp.Address = Address;
        //    emp.Email = Email;
        //    emp.Mobile = Mobile;
        //    emp.JoiningDate = DateTime.Now.ToString("yyyy/MM/dd");
        //    emp.DepartmentID = 1;
        //    emp.DesignationID = 1;
        //    var result = db.AddEmp(1, 1, emp);
        //    return Content(JsonConvert.SerializeObject(new
        //    {
        //        result
        //    }));
        //}
        public ActionResult DeleteEmp(int id, int idTable)
        {
            var db = new TableDal();
            var result = db.DeleteEmp(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

    
        //public ActionResult DemoExportExcel(string filterString)
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[5]
        //    {
        //        new DataColumn("ID"),
        //        new DataColumn("Tên"),
        //        new DataColumn("Giới tính"),
        //        new DataColumn("Email"),
        //        new DataColumn("Ngày sinh")
        //    });
        //    dt.Columns[0].DataType = typeof(int);
        //    dt.Columns[4].DataType = typeof(DateTime);
        //    var db = new TableDal();
        //    int total = 0;
        //    var lstData = db.ExportExcelDemo(filterString);
        //    foreach (var item in lstData)
        //    {
        //        dt.Rows.Add(item.Id,item.name,item.Gender,item.Email,item.DOB);
        //    }

        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(dt);
        //    byte[] data = null;
        //    using (var stream = new MemoryStream())
        //    {
        //        wb.SaveAs(stream);
        //        data = stream.ToArray();
        //    }
        //    return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Demo.xlsx");
        //}

    }
}