using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Hrm.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hrm.Web.Models;

namespace Hrm.Web.Controllers
{
    public class SalaryController : BaseController
    {
        private long _languageId;
        private long _userId;
        private long _roleId;
        private ITableColumnService _tableColumnService;
        private IMasterDataService _masterDataService;
        private ITableConfigService _tableConfigService;
        private ISalaryService _salaryService;

        public SalaryController(ITableColumnService tableColumnService, IMasterDataService masterDataService, ITableConfigService tableConfigService, ISalaryService salaryService)
        {
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _tableConfigService = tableConfigService;
            _salaryService = salaryService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = CurrentUser.RoleId;        
        }
      
        public ActionResult GeneralnformationAndDetailSalary(int tabActive= 0)
        {
            GeneralnformationAndSalaryViewModel generalnformationAndDetailSalary_vm = new GeneralnformationAndSalaryViewModel();
            generalnformationAndDetailSalary_vm.ActiveTab = 1;

            switch (generalnformationAndDetailSalary_vm.ActiveTab)
            {
                #region 

                case 0:
                    {
                        
                        break;
                    }
                #endregion
                case 1:
                    {

                        var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Salary);
                        var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
                        if (!CheckPermission(tableConfigDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableConfig = new TableViewModel();
                            if (tableConfigDetail.Results.FirstOrDefault() != null)
                            {
                                dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                                var param = new BasicParamModel()
                                {
                                    FilterField = "",
                                    PageNumber = 1,
                                    PageSize = dataTableConfig.ItemsPerPage,
                                    LanguageId = _languageId,
                                    DbName = CurrentUser.DbName
                                };
                                dataTableConfig.TableConfigName = TableConfig.Salary;
                                dataTableConfig.TableDataUrl = TableUrl.TableSalary;
                                generalnformationAndDetailSalary_vm.Salary.Table = RenderTable(dataTableConfig, param, TableName.TableSalary);
                            }
                        }
                        break;
                    }


            }

            
            return View(generalnformationAndDetailSalary_vm);
        }


        #region RenderTable
        //public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        //{
        //    tableData = RenderTable(tableData, param, tableData.TableName);
        //    return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        //}
        public TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            var result = new TableViewModel();
            result = tableData;
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.UserId = _userId;
            param.RoleId = _roleId;
            param.DbName = CurrentUser.DbName;
            var outTotalJson = string.Empty;
            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, out outTotalJson, out totalRecord);
            result.CurrentPage = param.PageNumber;
            result.ItemsPerPage = param.PageSize;
            result.TotalRecord = totalRecord;
            result.TableDataUrl = TableUrl.WorkingdayGetDataUrl;
            if (outTotalJson != string.Empty && outTotalJson != null && outTotalJson != "")
            {
                result.Total = JsonConvert.DeserializeObject<dynamic>(outTotalJson);
            }
            if (type == TableName.TableSalary)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<SalaryModel>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.TableSalary;
                result.TableConfigName = TableConfig.Salary;

            }

            var responseColumn = this._tableColumnService.GetTableColumn(result.TableConfigName);
            if (responseColumn != null)
            {
                var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                if (!CheckPermission(resultColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    result.ListTableColumns = resultColumn.Results;
                }
            }

            var resultMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, _languageId);
            var resultSelectList = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultMasterDataSelectList);
            if (!CheckPermission(resultSelectList))
            {
                //return to Access Denied
            }
            else
            {
                var dataSelectList = resultSelectList.Results;
                var dataDropdownList = MapperHelper.MapList<MasterDataModel, DropdownListContentModel>(dataSelectList);
                foreach (var item in dataDropdownList)
                {
                    if (Convert.ToInt32(item.Value) == param.PageSize)
                    {
                        item.IsSelected = true;
                        break;
                    }
                }
                List<dynamic> dataDropDownListDynamic = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(dataDropdownList));
                result.lstItemsPerPage = dataDropDownListDynamic;
            }

            return result;
        }
        private string GetData(string tableName, BasicParamModel param, out string outTotalJson, out int totalRecord)
        {
            int year = 2;
            int month = 2020;
            long staffId = CurrentUser.UserId;
            //if (!string.IsNullOrEmpty(param.StringJson) && param.StringJson != null)
            //{
            //    var dynamic = JsonConvert.DeserializeObject<dynamic>(param.StringJson);
            //    month = Convert.ToInt32(dynamic["Month"]);
            //    year = Convert.ToInt32(dynamic["Year"]);
            //    staffId = Convert.ToInt32(dynamic["StaffId"]);
            //    if (staffId == 0)
            //    {
            //        staffId = CurrentUser.UserId;
            //    }
            //}
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            if (tableName == TableName.TableSalary)
            {
                if (staffId == 0)
                {
                    staffId = CurrentUser.UserId;
                }
                return _salaryService.GetSalary(paramEntity, month,year,staffId, out outTotalJson, out totalRecord);
            }
            totalRecord = 0;
            outTotalJson = string.Empty;
            return string.Empty;
        }
        #endregion
    }
}