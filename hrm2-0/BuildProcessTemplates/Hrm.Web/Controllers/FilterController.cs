using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using Hrm.Service;
using Hrm.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrm.Web.Controllers
{
    public class FilterController : Hrm.Framework.Controllers.FilterController
    {
        private IMasterDataService _masterDataService;
        private IOrganizationService _organizationService;
        private ITableConfigService _tableConfigService;
        private IStaffService _staffService;
        private ITableColumnService _tableColumnService;
        private ILocalizationService _localizationService;
        public FilterController(
            IMasterDataService masterDataService,
            IOrganizationService organizationService,
            ITableConfigService tableConfigService,
            IStaffService staffService,
            ITableColumnService tableColumnService,
            ILocalizationService localizationService
            )
        {
            _masterDataService = masterDataService;
            _organizationService = organizationService;
            _tableConfigService = tableConfigService;
            _staffService = staffService;
            _localizationService = localizationService;
            _tableColumnService = tableColumnService;
        }
        public ActionResult ShowFilter(string tableName, string tableUrl, string isFilter, string groupId)
        {
            var dataTableConfig = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                if (tableConfigDetail.Results.FirstOrDefault() != null)
                {
                    dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                }
            }
            return ShowDialog(_tableColumnService, _masterDataService, tableName, dataTableConfig, tableConfigDetail.Results.FirstOrDefault().FilterData, tableUrl, isFilter, groupId);
        }
        public ActionResult FilterSelector(TableColumnModel column)
        {
            return FilterValueSelector(_tableColumnService, column);
        }
        public ActionResult SaveUserTableConfig(string tableName, string queryData, string filterData, string isFilter, List<ShowHideColumnModel> lstColumnVisibled)
        {
            if (isFilter == "1")
            {
                filterData = filterData.Replace("\\\\", "\\");
                var respone = this._tableConfigService.SaveUserTableConfig(tableName, queryData, filterData, "", isFilter);
                var responeResult = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(respone);
                return Json(new { TableId = responeResult.Results.FirstOrDefault().TableId }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableName);
                var tableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
                var tableViewModel = JsonConvert.DeserializeObject<TableViewModel>(tableConfig.Results.FirstOrDefault().ConfigData);
                var model = tableViewModel;
                foreach (var col in lstColumnVisibled)
                {
                    if (col.ColumnName != null)
                    {
                        model.Fields.Where(x => x.FieldName == col.ColumnName).ToList().ForEach(x =>
                        {
                            x.Visible = col.Visible;
                        });
                    }
                }

                var respone = this._tableConfigService.SaveUserTableConfig(tableName, queryData, filterData, JsonConvert.SerializeObject(model), isFilter);
                var responeResult = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(respone);
                return Json(new { TableId = responeResult.Results.FirstOrDefault().TableId }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RowInlineEditor(TableColumnModel column, int index, string tableName)
        {
            return ShowInlineEditing(_tableColumnService, _localizationService, column, index, tableName);
        }
        public ActionResult InlineEditor(List<TableColumnModel> columns, List<FieldModel> fields, int index, string tableName, bool isEdit = true)
        {
            var vm = new InlineEditingViewModel()
            {
                Columns = columns,
                Fields = fields,
                EditType = isEdit,
                Index = index,
                TableName = tableName
            };
            return PartialView(UrlHelpers.View("~/Views/Shared/Template/_InlineEditingRow.cshtml"), vm);
        }
    }
}