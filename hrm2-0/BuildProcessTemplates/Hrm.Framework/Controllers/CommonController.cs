using Hrm.Common;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Helper;
using Hrm.Framework.Models;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using ClosedXML.Excel;
using System.IO;
using System;

namespace Hrm.Framework.Controllers
{
    public class CommonController : BaseController
    {
        public ActionResult LanguageSelector()
        {
            var languageSelectorModel = new LanguageSelectorModel()
            {
                Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(GetLanguage())),
                CurrentLanguageId = CurrentUser.LanguageId,
                UseImages = true
            };
            return View(UrlHelpers.View("~/Views/Shared/_LanguageSelector.cshtml"), languageSelectorModel);
        }
        public ActionResult SetLanguage(long languageId)
        {
            CurrentUser.LanguageId = languageId;
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchSelector()
        {
            var _menuService = EngineContext.Current.Resolve<IMenuService>();
            var model = new List<dynamic>();
            var response = _menuService.GetMenuForSearch();
            var result = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                model = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
            }
            return PartialView(UrlHelpers.View("~/Views/Shared/_SearchSelector.cshtml"), model);
        }
        public JsonResult SetStorage(List<ExportCellModel> cells, string tableName)
        {
            HttpContext.Session[CurrentUser.UserId + "-" + tableName] = cells;
            return null;
        }
        public ActionResult Export(string tableName)
        {
            var cells = HttpContext.Session[CurrentUser.UserId + "-" + tableName] as List<ExportCellModel>;
            var wb = new XLWorkbook();
            wb.AddWorksheet(tableName);
            var ws = wb.Worksheet(tableName);
            var rowIndex = cells.Max(x => x.X);
            var columnIndex = cells.Max(x => x.Y);
            for (var i = 0; i <= rowIndex; i++)
            {
                for (var j = 1; j <= columnIndex; j++)
                {
                    var cell = cells.FirstOrDefault(x => x.X == i && x.Y == j);
                    var cellVal = string.Empty;
                    if (cell.Value != null && !string.IsNullOrEmpty(cell.Value))
                    {
                        var vals = cell.Value.Split('\n');
                        foreach (var val in vals)
                        {
                            if (!string.IsNullOrEmpty(val.Trim()))
                            {
                                if (string.IsNullOrEmpty(cellVal))
                                {
                                    cellVal += val.Trim();
                                }
                                else
                                    cellVal += Environment.NewLine + val.Trim();
                            }
                        }
                    }
                    ws.Cell(i + 1, j).Value = cellVal;
                }
            }

            ws.Rows(1, 1).Style.Font.Bold = true;
            for (var j = 1; j <= columnIndex; j++)
            {
                ws.Column(j).AdjustToContents();
            }
            using (MemoryStream stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                HttpContext.Session[CurrentUser.UserId + "-" + tableName] = null;
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", tableName + ".xlsx");
            }
        }
    }
}