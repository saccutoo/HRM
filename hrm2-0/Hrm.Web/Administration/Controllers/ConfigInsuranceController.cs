
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
using System.Web;
using System.Linq;
using System.Web.Mvc;
namespace Hrm.Admin.Controllers
{
    public partial class ConfigInsuranceController : Hrm.Framework.Controllers.ConfigInsuranceController
    {
        #region Fields
        private IPersonalIncomeTaxService _personalIncomeTaxService;
        private IConfigInsuranceService _configInsuranceService;
        private IMasterDataService _masterDataService;
        private ITableConfigService _tableConfigService;
        private ITableColumnService _tableColumnService;
        private IConfigInsuranceDetailService _configInsuranceDetailService;
        private long _languageId, _userId, _roleId;
        private int _totalRecord = 0;

        #endregion Fields
        #region Constructors
        public ConfigInsuranceController(IConfigInsuranceService configInsuranceService, IConfigInsuranceDetailService configInsuranceDetailService, ITableConfigService tableConfigService, ITableColumnService tableColumnService, IMasterDataService masterDataService, IPersonalIncomeTaxService personalIncomeTaxService)
        {
            _configInsuranceService = configInsuranceService;
            _configInsuranceDetailService = configInsuranceDetailService;
            _tableConfigService = tableConfigService;
            _tableColumnService = tableColumnService;
            _masterDataService = masterDataService;
            _personalIncomeTaxService = personalIncomeTaxService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = CurrentUser.RoleId;
        }
        #endregion

        public ActionResult ConfigInsurance()
        {
            ConfigInsuranceViewModel configInsurance_vm = new ConfigInsuranceViewModel();
            var param = new BasicParamModel() {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var result = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseConfigInsurance = _configInsuranceService.GetCurrentConfigInsurance(result, out _totalRecord);
            if (responseConfigInsurance != null)
            {
                var resultConfigInsurance = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceModel>>(responseConfigInsurance);
                if (!CheckPermission(resultConfigInsurance))
                {
                    //return to Access Denied
                }
                else
                {
                    configInsurance_vm.ConfigInsurance = resultConfigInsurance.Results.FirstOrDefault();

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
                            dataTableConfig.ShowFooter = false;
                            param = new BasicParamModel()
                            {
                                FilterField = "",
                                PageNumber = 1,
                                ReferenceId = configInsurance_vm.ConfigInsurance.Id,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                DbName = CurrentUser.DbName
                            };
                            dataTableConfig.TableConfigName = TableConfig.ConfigInsuranceDetail;
                            dataTableConfig.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                            configInsurance_vm.Table = RenderTable(dataTableConfig, param, TableName.TableConfigInsuranceDetail);
                        }
                    }
                }
            }
            
            return View(configInsurance_vm);
        }
        public ActionResult _ShowFromAddInsurance()
        {

            ConfigInsuranceViewModel model = new ConfigInsuranceViewModel();
            var listGroup = new List<LongTypeModel>();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);

            //get dropdown value
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownStatus = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
            }
            // tax 
            var responseTax = this._configInsuranceService.GetCurrentConfigInsurance(paramEntity, out _totalRecord);
            if (responseTax != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceModel>>(responseTax);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.ConfigInsurance = result.Results.FirstOrDefault();
                    }
                }
            }

            var resultTableTaxDetailConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.ConfigInsuranceDetailPopup);
            var resultConfigTaxDetailDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableTaxDetailConfig);
            if (!CheckPermission(resultConfigTaxDetailDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableTaxDetailConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigTaxDetailDetail.Results.FirstOrDefault().ConfigData);
                dataTableTaxDetailConfig.ShowFooter = false;
                dataTableTaxDetailConfig.TableName = TableName.TableConfigInsuranceDetailPopup;
                dataTableTaxDetailConfig.TableConfigName = TableConfig.ConfigInsuranceDetailPopup;
                var param3 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableTaxDetailConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.ConfigInsurance.Id,
                };
                model.Table = RenderTable(dataTableTaxDetailConfig, param3, TableName.TableConfigInsuranceDetailPopup);
            }
            return PartialView(UrlHelpers.TemplateAdmin("ConfigInsurance", "_ShowFromAddInsurance.cshtml"),model);     
        }
        public ActionResult _History()
        {
            var param = new BasicParamModel();
            var model = new List<ConfigInsuranceViewModel>();
            var result = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            var list = new List<ConfigInsuranceModel>();
            var responseListConfigInsurance = _configInsuranceService.GetAllConfigInsurance(result, out _totalRecord);
            if (responseListConfigInsurance != null)
            {
                var resultConfigInsurance = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceModel>>(responseListConfigInsurance);
                if (!CheckPermission(resultConfigInsurance))
                {
                    //return to Access Denied
                }
                else
                {
                    list = resultConfigInsurance.Results;

                    var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.ConfigInsuranceDetail);
                    var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
                    

                    foreach (var item in list)
                    {
                        var tmp = new ConfigInsuranceViewModel();
                        tmp.ConfigInsurance = item;
                        var dataTableConfig = new TableViewModel();
                        if (tableConfigDetail.Results.FirstOrDefault() != null)
                        {
                            dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                            param = new BasicParamModel()
                            {
                                FilterField = "",
                                PageNumber = 1,
                                ReferenceId = item.Id,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                DbName = CurrentUser.DbName
                            };
                            dataTableConfig.TableConfigName = TableConfig.ConfigInsuranceDetail;
                            dataTableConfig.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                            tmp.Table = RenderTable(dataTableConfig, param, TableName.TableConfigInsuranceDetail);
                        }
                        model.Add(tmp);
                    }
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("ConfigInsurance", "_History.cshtml"), model);
        }


        #region RenderTable
        //public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        //{
        //    tableData = RenderTable(tableData, param, tableData.TableName);
        //    return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        //}
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
                if (!String.IsNullOrEmpty(response))
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceDetailModel>>(response);
                    if (result != null && !CheckPermission(result))
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
            if (type == TableName.TableConfigInsuranceDetailPopup)
            {
                if (!String.IsNullOrEmpty(response))
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceDetailModel>>(response);
                    if (result != null && !CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableConfigInsuranceDetailPopup;
                tableData.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                tableData.TableConfigName = TableConfig.ConfigInsuranceDetailPopup;

            } 
            if (type == TableName.TableConfigInsuranceDetailHistory)
            {
                if (!String.IsNullOrEmpty(response))
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<ConfigInsuranceDetailModel>>(response);
                    if (result != null && !CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableConfigInsuranceDetailHistory;
                tableData.TableDataUrl = TableUrl.ConfigInsuranceDetailUrl;
                tableData.TableConfigName = TableConfig.ConfigInsuranceDetaiHistory;

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
                if (CheckPermission(resultMasterDataSelectList))
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
            if (type == TableName.TableConfigInsuranceDetail || type == TableName.TableConfigInsuranceDetailPopup || type == TableName.TableConfigInsuranceDetailHistory)
            {
                return this._configInsuranceDetailService.GetCurrentConfigInsuranceDetail(paramEntity, param.ReferenceId, out totalRecord);
            }


            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}