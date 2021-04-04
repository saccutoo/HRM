using ClosedXML.Excel;
using HRM.App_LocalResources;
using HRM.DataAccess.DAL;
using HRM.DataAccess.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRM.Common;
using WebGrease.Configuration;
using System.Text;
using HRM.Models;
using HRM.Security;
using static HRM.Constants.Constant;
using HRM.Logger;

namespace HRM.Controllers
{
    [HRMAuthorize]
    public class SecMenuController : Controller
    {
        // GET: SecMenu

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Index)]
        public ActionResult Index()
        {
            ViewBag.url = "/SecMenu/TableServerSideGetData";
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Get)]
        [WriteLog(Action = EAction.Get,LogStoreProcedure = "sec_Menu_List")]

        public ActionResult TableServerSideGetData(int pageIndex, int pageSize, string filter = "")
        {
            var db = new Sec_MenuDal();
            int total = 0;
            int Language = Global.CurrentLanguage;
            var result = db.GetSecMenu(pageIndex, pageSize, filter, out total, Language);
            var lstTotal = new TableColumnsTotalModel();
            lstTotal.Total1 = "15";
            lstTotal.Total2 = "25";
            lstTotal.Total3 = "35";
            return Content(JsonConvert.SerializeObject(new
            {
                employees = result,
                totalCount = total,
                lstTotal = lstTotal
            }));
        }
        public ActionResult SaveSecMenu()
        {
            return PartialView();
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Get)]
        public ActionResult GetEditItemById(int id, int idTable)
        {
            var db = new Sec_MenuDal();
            var result = db.GetSecMenuById(1, idTable, id);
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Edit)]
        [WriteLog(Action = EAction.Edit, LogStoreProcedure = "Sec_Menu_Save")]

        public ActionResult _SaveSecMenu(Sec_Menu obj)
        {
            var db = new Sec_MenuDal();
            var result = db.SaveSecMenu(1, 1, obj);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Update_success;
            else
                result.Message = AppRes.MS_Update_error;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Delete)]
        [WriteLog(Action = EAction.Delete, LogStoreProcedure = "sec_Menu_Delete")]

        public ActionResult _DeleteSecMenu(int id, int idTable)
        {
            var db = new Sec_MenuDal();
            var result = db.DeleteSecMenu(1, idTable, id);
            if (result.IsSuccess == true)
                result.Message = AppRes.MS_Delete_success;
            else
                result.Message = AppRes.NotFound;
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }

        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Get)]
        public ActionResult GetListParent()
        {
            var db = new Sec_MenuDal();
            var result = db.ListParent();
            return Content(JsonConvert.SerializeObject(new
            {
                result
            }));
        }



        [Permission(TableID = (int)ETable.Sec_Menu, TypeAction = (int)EAction.Excel)]
        [WriteLog(Action = EAction.Excel, LogStoreProcedure = "sec_Menu_List")]

        public ActionResult SecMenuExportExcel(string filterString)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn("MenuID"),
                new DataColumn(AppRes.NameVi),
                new DataColumn(AppRes.NameEn),
                new DataColumn("ActionName"),
                new DataColumn("IncludeMenu"),
                new DataColumn("MenuPositionID"),
                new DataColumn("OrderNo"),
                new DataColumn("ParentID"),
                new DataColumn("IsActive"),
                new DataColumn("Url")
            });
            dt.Columns[0].DataType = typeof(int);
            dt.Columns[1].DataType = typeof(string);
            var db = new Sec_MenuDal();
            int total = 0;
            var lstData = db.ExportExcelSecMenu(filterString);
            foreach (var item in lstData)
            {
                dt.Rows.Add(item.MenuID, item.Name,item.NameEN,item.ActionName,item.IncludeMenu,item.MenuPositionID,item.OrderNo,item.ParentID,item.IsActive,item.Url);
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt);
            byte[] data = null;
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                data = stream.ToArray();
            }
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SecMenu.xlsx");
        }


        public ActionResult LoadMenu()
        {

            var str = GetHtmlMenu();
            @ViewBag.Html = str;
            return PartialView("_Menu");
        }
        private string GetHtmlMenu()
        {
            var db = new Sec_MenuDal();

            var currentUser = Global.CurrentUser;
            var lstMenu = db.GetMenuSystem(currentUser.UserID, currentUser.UserType);

            var html = new StringBuilder();
            html.AppendLine("<ul>");
            html.AppendLine(GetHtml(lstMenu));
            html.AppendLine("</ul>");

            return html.ToString();
        }

        private string GetHtml(List<Sec_Menu> lstMenu, int menuLevel = 0)
        {
            var html = new StringBuilder();
            string menu = string.Empty;
            int checkSubMenu = 0;
            lstMenu = lstMenu.OrderBy(t => t.OrderNo).ToList();
             foreach (var m in lstMenu)
            {
                var hasChild = m.SubMenu.Count > 0;
                //html.AppendLine("<li class='menu-item' >");
                if (m.MenuID == 242 || m.MenuID == 243 || m.MenuID == 244 || m.MenuID == 241 || m.MenuID == 237 || m.MenuID == 175
                    || m.MenuID == 263 || m.MenuID == 264 || m.MenuID == 265 || m.MenuID == 266 || m.MenuID == 267 || m.MenuID == 277 ||m.MenuID==425 || m.MenuID == 426
                    )
                {
                    html.AppendLine("<li class='menu-item" + m.MenuID + "' id='menu-item" + menuLevel + "' >");
                }
                else
                {
                    html.AppendLine("<li class='menu-item' >");
                }
                if (hasChild)
                {
                    html.AppendFormat(
                                                           "<a class='menu-itema" + m.MenuID + "' href='{0}' title='{1}'>" +
                                                           "<i class='{2}'></i>" +
                                                           " <span class='menu-item-parent'>{1}</span>" +
                                                          "<b class='collapse-sign'><em id=" + m.MenuID + " class='fa fa-plus-square-o'></em></b>" +
                                                           "</a>", m.Url,
                                                           m.MenuNameByLanguage, m.CssIconClass);
                    menuLevel++;
                }
                else
                {
                    //string submenu = string.Format("<a class='menu-itema' href='{0}' title='{1}'>" +
                    //                                                          "<i class='{2}'></i>" +
                    //                                                          " <span class='menu-item-parent'>{1}</span>" +
                    //                                                          "</a>", m.Url,
                    //                                                          m.MenuNameByLanguage, m.CssIconClass);
                    html.AppendFormat(
                                                                              "<a class='menu-itema" + m.MenuID + "' href='{0}' title='{1}'>" +
                                                                              "<i class='{2}'></i>" +
                                                                              " <span class='menu-item-parent'>{1}</span>" +
                                                                              "</a>", m.Url,
                                                                              m.MenuNameByLanguage, m.CssIconClass);
                    checkSubMenu = 1;
                }

                if (hasChild)
                {
                    menuLevel++;
                    html.AppendLine("<ul style='display: none;' class=" + m.MenuID + ">");
                    m.SubMenu = m.SubMenu.OrderBy(t => t.OrderNo).ToList();
                    html.Append(GetHtml(m.SubMenu, menuLevel));
                    // html.Append(GetHtmlChild(m.SubMenu));
                    html.AppendLine("</ul>");
                    //GetHtml(m.SubMenu);
                }
                else
                {
                    if (checkSubMenu != 1)
                    {
                        html.AppendLine("<ul style='display: none;' class=" + m.MenuID + ">");
                        m.SubMenu = m.SubMenu.OrderBy(t => t.OrderNo).ToList();
                        html.AppendFormat(
                                   "<a class='menu-itema" + m.MenuID + "' href='{0}' title='{1}'><i class='{2}'></i> <span class='menu-item-parent'>{1}</span></a>", m.Url,
                                   m.MenuNameByLanguage, m.CssIconClass);
                        // html.Append(GetHtmlChild(m.SubMenu));
                        html.AppendLine("</ul>");
                    }
                    //html.AppendLine("<ul style='display: none;'>");
                    //m.SubMenu = m.SubMenu.OrderBy(t => t.OrderNo).ToList();
                    //html.AppendFormat(
                    //           "<a class='menu-itema' href='{0}' title='{1}'><i class='{2}'></i> <span class='menu-item-parent'>{1}</span></a>", m.Url,
                    //           m.MenuNameByLanguage, m.CssIconClass);
                    //// html.Append(GetHtmlChild(m.SubMenu));
                    //html.AppendLine("</ul>");

                }

                //if (hasChild)
                //{
                //    html.AppendLine("<ul style='display: none;'>");
                //    GetHtml(m.SubMenu);
                //    m.SubMenu = m.SubMenu.OrderBy(t => t.OrderNo).ToList();
                //    html.Append(GetHtmlChild(m.SubMenu));
                //    html.AppendLine("</ul>");
                //}
                html.AppendLine("</li>");

            }

            return html.ToString();
        }
        private string GetHtmlChild(List<Sec_MenuModel> lstMenu)
        {
            var html = new StringBuilder();
            lstMenu = lstMenu.OrderBy(t => t.OrderNo).ToList();
            foreach (var m in lstMenu)
            {
                var hasChild = m.SubMenu.Count > 0;
                html.AppendLine("<li>");
                html.AppendFormat(
                    "<a href='{0}' title='{1}'><i class='{2}'></i> <span class='menu-item-parent'>{1}</span></a>", m.Url,
                    m.MenuNameByLanguage, m.CssIconClass);

                if (hasChild)
                {
                    html.AppendLine("<ul style='display: none;'>");
                    m.SubMenu = m.SubMenu.OrderBy(t => t.OrderNo).ToList();
                    html.Append(GetHtmlChild(m.SubMenu));
                    html.AppendLine("</ul>");
                }

                html.AppendLine("</li>");
            }

            return html.ToString();
        }

    }
}