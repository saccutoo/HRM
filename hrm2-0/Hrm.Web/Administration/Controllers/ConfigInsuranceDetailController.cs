using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Hrm.Admin.Controllers
{
    public partial class ConfigInsuranceDetailController : Hrm.Framework.Controllers.ConfigInsuranceController
    {
        #region Fields
        private IConfigInsuranceDetailService _configInsuranceDetailService;
        private IMasterDataService _masterDataService;
        private ITableColumnService _tableColumnService;
        private ITableConfigService _tableConfigService;
        private ILocalizationService _localizationService;
        private IOrganizationService _organizationService;

        private long _languageId;
        private int _totalRecord = 0;

        #endregion Fields
        #region Constructors
        public ConfigInsuranceDetailController(IConfigInsuranceDetailService configInsuranceDetailService, IMasterDataService masterDataService, ITableColumnService tableColumnService, ITableConfigService tableConfigService, ILocalizationService localizationService, IOrganizationService organizationService)
        {
            _tableConfigService = tableConfigService;
            _configInsuranceDetailService = configInsuranceDetailService ;
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _localizationService = localizationService;
            _organizationService = organizationService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion

        public ActionResult ConfigInsuranceDetail()
        {
            ConfigInsuranceViewModel configInsuranceDetail_vm = new ConfigInsuranceViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.ConfigInsuranceDetail);
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
                    dataTableConfig.TableConfigName = TableConfig.ConfigInsuranceDetail;
                    dataTableConfig.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                    configInsuranceDetail_vm.Table = RenderTable(dataTableConfig, param, TableName.TableConfigInsuranceDetail);
                }
            }
            return View(configInsuranceDetail_vm);
        }

 
        #region RenderTable
        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        private TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            //model param
            int totalRecord = 0;
            param.LanguageId = param.LanguageId;
            param.UserId = CurrentUser.RoleId;
            param.RoleId = CurrentUser.UserId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, out totalRecord);

            tableData.CurrentPage = param.PageNumber;
            tableData.ItemsPerPage = param.PageSize;
            tableData.TotalRecord = totalRecord;

            if (type == TableName.TableConfigInsuranceDetail)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceDetailModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableConfigInsuranceDetail;
                tableData.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                tableData.TableConfigName = TableConfig.ConfigInsuranceDetail;

            }
           
            var responseColumn = this._tableColumnService.GetTableColumn(tableData.TableConfigName);
            if (responseColumn != null)
            {
                var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                if (!CheckPermission(resultColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    tableData.ListTableColumns = resultColumn.Results;
                }
            }
            List<MasterDataModel> masterData = new List<MasterDataModel>();
            var responeseMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, _languageId);
            if (responeseMasterDataSelectList != null)
            {
                var resultMasterDataSelectList = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responeseMasterDataSelectList);
                if (!CheckPermission(resultMasterDataSelectList))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData = resultMasterDataSelectList.Results;
                }
            }
            var dataDropdownList = MapperHelper.MapList<MasterDataModel, DropdownListContentModel>(masterData);

            foreach (var item in dataDropdownList)
            {
                if (Convert.ToInt32(item.Value) == param.PageSize)
                {
                    item.IsSelected = true;
                    break;
                }
            }
            List<dynamic> dataDropDownListDynamic = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(dataDropdownList));
            tableData.lstItemsPerPage = dataDropDownListDynamic;
            return tableData;
        }
        private string GetData(string type, BasicParamModel param, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TableConfigInsuranceDetail)
            {
                return this._configInsuranceDetailService.GetCurrentConfigInsuranceDetail(paramEntity, param.ReferenceId, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}