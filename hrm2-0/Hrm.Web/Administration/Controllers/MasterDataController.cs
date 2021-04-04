using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Hrm.Framework.Models;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Core.Infrastructure;
using System.Linq;
using Hrm.Framework.Helper;
using Hrm.Framework.Controllers;
using Hrm.Repository.Type;
using Hrm.Admin.ViewModels;
using Hrm.Framework.Helpers;

namespace Hrm.Admin.Controllers
{
    public partial class MasterDataController : BaseController
    {
        #region Fields
        #endregion Fields
        #region Constructors

        private IMasterDataService _masterDataService;
        private ITableService _tableService;
        private ITableConfigService _tableConfigService;
        private ILocalizationService _localizationService;
        private ITableColumnService _tableColumnService;

        private long _languageId;
        private long _userId;
        private long _roleId;
        public MasterDataController(IMasterDataService masterDataService, ITableService tableService, ITableConfigService tableConfigService, ILocalizationService localizationService, ITableColumnService tableColumnService)
        {
            this._masterDataService = masterDataService;
            this._tableService = tableService;
            this._tableConfigService = tableConfigService;
            _tableColumnService = tableColumnService;
            this._languageId = CurrentUser.LanguageId;
            _localizationService = localizationService;
            _userId = CurrentUser.UserId;
            this._roleId = 1;

        }
        #endregion        
        public ActionResult Index()
        {
            MasterDataViewModel masterData = new MasterDataViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.MasterData);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

            List<MasterDataModel> list = new List<MasterDataModel>();

            var resultMasterDataGroupId = this._masterDataService.GetAllMasterDataByGroupId(0);
            if (resultMasterDataGroupId != null)
            {
                var response = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultMasterDataGroupId);
                if (!CheckPermission(response))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.MasterDatas = response.Results;
                }
            }
            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = dataTableConfig.ItemsPerPage,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            dataTableConfig.TableConfigName = TableConfig.MasterData; //for reload and filter case
            dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TableMasterDataReloadUrl;  // for show hide column case
            masterData.Table = RenderTable(dataTableConfig, param, TableName.TableMasterData, Convert.ToInt32(masterData.MasterDatas[0].Id));
            masterData.GroupId = masterData.MasterDatas[0].Id;
            return View(masterData);
        }
        public ActionResult GetAllMasterData(BasicParamModel param, int groupId)
        {
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.MasterData);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

            TableViewModel tableData = new TableViewModel();
            dataTableConfig.TableConfigName = TableConfig.MasterData;
            tableData = RenderTable(dataTableConfig, param, TableName.TableMasterData, groupId);

            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);

        }
        public ActionResult SaveMasterData(MasterDataModel data)
        {
            var result = new List<MasterDataModel>();

            data.LanguageId = CurrentUser.LanguageId;
            var Entity = MapperHelper.Map<MasterDataModel, MasterDataEntity>(data);
            var response = this._masterDataService.SaveMasterData(Entity);
            if (response != null)
            {
                var resultMasterData = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(response);
                if (!CheckPermission(resultMasterData))
                {
                    //return to Access Denied
                }
                else
                {
                    result = resultMasterData.Results;
                }
            }

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReloadCategory()
        {
            var masterData = new List<MasterDataModel>();
            var responseMasterDataGroupId = this._masterDataService.GetAllMasterDataByGroupId(0);
            if (responseMasterDataGroupId != null)
            {
                var resultMasterData = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataGroupId);
                if (!CheckPermission(resultMasterData))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData = resultMasterData.Results;
                }
            }
            return View("~/Administration/Views/MasterData/_CategoryMasterData.cshtml", masterData);
        }
        public ActionResult ShowFormAddMasterData(long groupId = 0)
        {
            MasterDataViewModel MasterData = new MasterDataViewModel();
            List<MasterDataModel> ListMasterData = new List<MasterDataModel>();
            MasterData.GroupId = groupId;

            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    MasterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            ListMasterData.Add(new MasterDataModel()
            {
                Name = string.Empty,
                Description = string.Empty,
                Abbreviations = string.Empty,
                Value = string.Empty
            });
            MasterData.MasterData = new MasterDataModel();
            MasterData.MasterData.IsDisbleEditing = true;
            MasterData.ListMasterDataByLanguage = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ListMasterData));
            return PartialView("~/Administration/Views/MasterData/_AddMasterData.cshtml", MasterData);
        }
        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName, 0);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        public ActionResult SaveListMasterData(MasterDataModel data, List<MasterDataModel> listData)
        {
            data.CreatedBy = CurrentUser.UserId;
            data.DataType = DataType.MasterData;
            data.LanguageId = CurrentUser.LanguageId;
            if (data != null)
            {
                var validations = ValidationHelper.Validation(data, "data");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }

            if (listData != null && listData.Count() > 0)
            {
                foreach (var item in listData)
                {
                    if (item.LanguageId == 0)
                    {
                        item.LanguageId = null;
                    }
                }
                var validationsListModel = ValidationHelper.ListValidation(listData, "listData");
                if (validationsListModel.Count > 0)
                {
                    return Json(new { Result = validationsListModel, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }

            if (listData != null && listData.Count() > 0)
            {
                foreach (var item in listData)
                {
                    item.DataType = DataType.MasterData;
                    item.Id = data.GroupId;
                }
            }
            if (listData != null)
            {
                var filter = listData.Where(s => s.LanguageId == data.LanguageId && s.DataType == data.DataType).ToList();
                if (filter.Count() == 0)
                {
                    listData.Add(data);
                }
            }
            else
            {
                listData = new List<MasterDataModel>();
                listData.Add(data);
            }
            var convertListData = FunctionHelpers.ConvertToLanguageFromListModel(listData, DataType.MasterData);
            var LocalizedDataEntity = MapperHelper.MapList<LocalizedDataModel, LocalizedDataEntity>(convertListData);
            var masterDataEntity = MapperHelper.Map<MasterDataModel, MasterDataEntity>(data);
            foreach (var item in LocalizedDataEntity)
            {
                item.CreatedBy = CurrentUser.UserId;
            }
            string responeseResources = string.Empty;
            var result = new HrmResultModel<bool>();
            var response = this._masterDataService.SaveListMasterData(masterDataEntity, LocalizedDataEntity);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success == true)
                    {
                        if (data.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                    }
                    else
                    {
                        if (data.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                    }
                }
            }

            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddDataLanguage(List<MasterDataModel> listData)
        {
            MasterDataViewModel masterData = new MasterDataViewModel();
            if (listData == null)
            {
                listData = new List<MasterDataModel>();
            }
            listData.Add(new MasterDataModel()
            {
                Name = string.Empty,
                Description = string.Empty,
                Abbreviations = string.Empty,
                Value = string.Empty
            });
            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            masterData.ListMasterDataByLanguage = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listData));
            return PartialView("~/Administration/Views/MasterData/_ListMasterDataByLanguage.cshtml", masterData);
        }
        public ActionResult RemoveDataLanguage(int index, List<MasterDataModel> listData)
        {
            MasterDataViewModel masterData = new MasterDataViewModel();
            listData.RemoveAt(index);
            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            masterData.ListMasterDataByLanguage = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listData));
            return PartialView("~/Administration/Views/MasterData/_ListMasterDataByLanguage.cshtml", masterData);
        }

        public ActionResult ReloadDataLanguage(List<MasterDataModel> ListData)
        {
            MasterDataViewModel masterData = new MasterDataViewModel();
            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            masterData.ListMasterDataByLanguage = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ListData));
            return PartialView("~/Administration/Views/MasterData/_ListMasterDataByLanguage.cshtml", masterData);
        }
        public ActionResult GetLocalizedData(string str)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var response = localizationService.GetLocalizedData(str);
            return Json(new { response }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowFormEditMasterData(long id)
        {
            MasterDataViewModel masterData = new MasterDataViewModel();
            List<MasterDataModel> ListMasterData = new List<MasterDataModel>();
            List<LanguageModel> languages = new List<LanguageModel>();
            //list language
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var responseLanguage = localizationService.GetLanguage();
            if (responseLanguage != null)
            {
                var resultLanguage = JsonConvert.DeserializeObject<HrmResultModel<LanguageModel>>(responseLanguage);
                if (!CheckPermission(resultLanguage))
                {
                    //return to Access Denied
                }
                else
                {
                    languages = resultLanguage.Results;
                    masterData.Languages = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultLanguage.Results));
                }
            }
            var responseMasterData = _masterDataService.GetMasterDataById(id);
            if (responseMasterData != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterData);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData.MasterData = result.Results[0];
                    masterData.GroupId = result.Results[0].GroupId;

                }
            }
            var responseMasterDatas = localizationService.GetLocalizedDataByDataId(id, DataType.MasterData);
            List<LocalizedDataModel> LocalizedDatas = new List<LocalizedDataModel>();
            if (responseMasterDatas != null)
            {
                var resultMasterDatas = JsonConvert.DeserializeObject<HrmResultModel<LocalizedDataModel>>(responseMasterDatas);
                if (!CheckPermission(resultMasterDatas))
                {
                    //return to Access Denied
                }
                else
                {
                    LocalizedDatas = resultMasterDatas.Results;
                    var listProp = localizationService.GetMultipleLanguageConfiguration(DataType.MasterData);                   
                    var listData = new List<MasterDataModel>();
                    foreach (var language in languages)
                    {
                        var data = LocalizedDatas.Where(s => s.LanguageId == language.Id).ToList();
                        if (data!=null && data.Count()>0)
                        {
                            MasterDataModel MasterData = new MasterDataModel();
                            var response = FunctionHelpers.ConvertLanguageToModel(data, MasterData, listProp.Results);
                            response.LanguageId = language.Id;
                            ListMasterData.Add(response);
                        }
                    }                                   
                }
            }
            masterData.ListMasterDataByLanguage = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ListMasterData));
            return PartialView("~/Administration/Views/MasterData/_AddMasterData.cshtml", masterData);
        }
        public ActionResult DeleteMasterData(long id)
        {
            var responeseResources = string.Empty;
            var response = _masterDataService.DeleteMasterData(id, DataType.MasterData);
            bool success = false;
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count() > 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Fail");
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        success = true;
                    }
                }
            }
            return Json(new { Success = success, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        #region TableReloadConfigUrl
        public ActionResult TableReloadConfigUrl(TableViewModel tableData, BasicParamModel param, string tableConfigName)
        {
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
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
                }
                tableData.Fields = dataTableConfig.Fields;
                TableViewModel tableDataResult = RenderTable(tableData, param, tableData.TableName, param.GroupId);
                tableDataResult.TableDataUrl = tableData.TableDataUrl;
                tableDataResult.TableReloadConfigUrl = tableData.TableReloadConfigUrl;
                tableDataResult.TableConfigName = tableData.TableConfigName;
                tableDataResult.TableName = tableData.TableName;
                return View(UrlHelpers.Template("_TableContent.cshtml"), tableDataResult);
            }
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        #endregion
        #region RenderTable
        private TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type, int groupId)
        {
            //model param
            int totalRecord = 0;
            param.LanguageId = param.LanguageId;
            param.UserId = _roleId;
            param.RoleId = _userId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, groupId, out totalRecord);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
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

            tableData.CurrentPage = param.PageNumber;
            tableData.ItemsPerPage = param.PageSize;
            tableData.TotalRecord = totalRecord;
            tableData.TableName = TableName.TableMasterData;
            tableData.TableDataUrl = TableUrl.MasterDataUrl;
            tableData.TableConfigName = TableConfig.MasterData;

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
        private string GetData(string type, BasicParamModel param, int GroupId, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TableMasterData)
            {
                return this._masterDataService.GetAllMasterData(param.PageNumber, param.PageSize, GroupId, param.FilterField, Convert.ToInt32(param.LanguageId), out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}
