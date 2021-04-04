using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Repository.Type;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hrm.Service.IServices;

namespace Hrm.Admin.Controllers
{
    public partial class WorkingdaySupplementConfigurationController : BaseController
    {
        #region Fields
        private IMasterDataService _masterDataService;
        private IRoleService _roleService;
        private ILocalizationService _localizationService;
        private ITableColumnService _tableColumnService;
        private ITableConfigService _tableConfigService;
        private IWorkingDaySupplementConfigurationService _workingDaySupplementConfigurationService;
        private IWorkingDaySupplementConfigurationExceptionService _workingDaySupplementConfigurationExceptionService;
        private long _languageId;
        #endregion
        #region Constructor
        public WorkingdaySupplementConfigurationController(ILocalizationService localizationService, IMasterDataService masterDataService, IRoleService roleService, ITableColumnService tableColumnService, ITableConfigService tableConfigService, IWorkingDaySupplementConfigurationService workingDaySupplementConfigurationService, IWorkingDaySupplementConfigurationExceptionService workingDaySupplementConfigurationExceptionService)
        {
            _roleService = roleService;
            _masterDataService = masterDataService;
            _localizationService = localizationService;
            _tableColumnService = tableColumnService;
            _tableConfigService = tableConfigService;
            _workingDaySupplementConfigurationService = workingDaySupplementConfigurationService;
            _workingDaySupplementConfigurationExceptionService = workingDaySupplementConfigurationExceptionService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion
        #region GetData
        private string GetData(string type, BasicParamModel param, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TableWorkingdaySupplementConfiguration)
            {
                return this._workingDaySupplementConfigurationService.GetSupplementConfigurationByRoleId(paramEntity, out totalRecord);
            }
            if (type == TableName.TableWorkingdaySupplementConfigurationException)
            {
                return this._workingDaySupplementConfigurationExceptionService.GetSupplementConfigurationException(paramEntity, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        #endregion
        #region RenderData
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
            //get column 
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
            if (type == TableName.TableWorkingdaySupplementConfiguration)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdaySupplementConfiguration;
                tableData.TableDataUrl = TableUrl.TableWorkingdaySupplementConfiguration;
            }
            if (type == TableName.TableWorkingdaySupplementConfigurationException)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationExceptionModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdaySupplementConfigurationException;
                tableData.TableDataUrl = TableUrl.TableWorkingdaySupplementConfiguration;
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
        #endregion
        #region Index
        // GET: WorkingdaySupplementConfiguraion (Default-Exception)
        public ActionResult Index()
        {
            WorkingdaySupplementConfigurationViewModel supplementConfigurationViewModel = new WorkingdaySupplementConfigurationViewModel();
            List<RoleModel> listRole = new List<RoleModel>();
            var responseRoles = _roleService.GetRole();
            if (responseRoles != null)
            {
                var resultRoles = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(responseRoles);
                if (!CheckPermission(resultRoles))
                {
                    //return to access denied
                }
                else
                {
                    listRole = resultRoles.Results;
                }
            }
            supplementConfigurationViewModel.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listRole));
            supplementConfigurationViewModel.ListRoles = JsonConvert.DeserializeObject<List<RoleModel>>(JsonConvert.SerializeObject(listRole));
            //get data for gereral-right
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdaySupplementConfiguration);
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
                        FilterField = supplementConfigurationViewModel.ListRoles[0].Id.ToString(),
                        PageNumber = 1,
                        PageSize = dataTableConfig.ItemsPerPage,
                        LanguageId = _languageId,
                        DbName = CurrentUser.DbName
                    };
                    dataTableConfig.TableConfigName = TableConfig.WorkingdaySupplementConfiguration;
                    supplementConfigurationViewModel.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdaySupplementConfiguration);
                }

            }
            supplementConfigurationViewModel.RoleId = supplementConfigurationViewModel.ListRoles[0].Id;
            return View(supplementConfigurationViewModel);
        }
        #endregion
        #region Show Form for Add or Edit WorkingdaySupplementConfiguration
        public ActionResult AddWorkingdaySupplementConfiguration(long id = 0, long roleId = 0)
        {
            WorkingdaySupplementConfigurationViewModel supplementConfigurationViewModel = new WorkingdaySupplementConfigurationViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementConfigurationStatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementConfigurationActions
            });
            var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
            var responseMasterDataDetail = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(responseMasterDataDetail))
            {
                //return to Access Denied
            }
            else
            {
                supplementConfigurationViewModel.SupplementConfigurationStatusApprove = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementConfigurationStatusAprove).ToList();
                supplementConfigurationViewModel.SupplementConfigurationActions = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementConfigurationActions).ToList();
            }
            if (id != 0)
            {
                var responseSupplement = _workingDaySupplementConfigurationService.GetSupplementConfigurationById(id);
                if (responseSupplement != null)
                {
                    var resultSupplement = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationModel>>(responseSupplement);
                    if (!CheckPermission(resultSupplement))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        supplementConfigurationViewModel.SupplementConfiguration = resultSupplement.Results.FirstOrDefault();
                    }
                }
            }
            supplementConfigurationViewModel.SupplementConfiguration.RequesterId = roleId;
            return PartialView(UrlHelpers.TemplateAdmin("WorkingdaySupplementConfiguration", "_AddSupplementConfiguration.cshtml"), supplementConfigurationViewModel);
        }
        #endregion
        public ActionResult AddWorkingdaySupplementConfigurationException(long id = 0)
        {
            WorkingdaySupplementConfigurationViewModel supplementConfigurationViewModel = new WorkingdaySupplementConfigurationViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementConfigurationStatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementConfigurationActions
            });
            var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
            var responseMasterDataDetail = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(responseMasterDataDetail))
            {
                //return to Access Denied
            }
            else
            {
                //supplementConfigurationViewModel.SupplementConfigurationStatusApprove = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementConfigurationStatusAprove).ToList();
            }
            var responseStaffs = this._workingDaySupplementConfigurationExceptionService.GetApprovedSaff();
            if (responseStaffs != null)
            {
                var resultStaffs = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(responseStaffs);
                if (!CheckPermission(resultStaffs))
                {
                    //return to Access Denied
                }
                else
                {
                    supplementConfigurationViewModel.ListStaffs = resultStaffs.Results;
                    supplementConfigurationViewModel.ListApprovedByStaffs = resultStaffs.Results;
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("WorkingdaySupplementConfiguration", "_AddSupplementConfigurationException.cshtml"), supplementConfigurationViewModel);
        }
        #region SaveSupplementConfiguration
        public ActionResult SaveSupplementConfiguration(WorkingdaySupplementConfigurationModel model)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.IsLastStepChecked == true)
            {
                model.IsLastStep = MasterDataId.SupplementConfigurationEndFailure;
            }
            else
            {
                model.IsLastStep = 0;
            }
            var entity = MapperHelper.Map<WorkingdaySupplementConfigurationModel, WorkingdaySupplementConfigurationEntity>(model);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<WorkingdaySupplementConfigurationModel>();
            var response = this._workingDaySupplementConfigurationService.SaveSupplementConfiguration(entity);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count == 0 || result.Results == null)
                    {
                        if (model.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                        result.Success = true;
                    }
                    else
                    {
                        if (model.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                        result.Success = false;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Delete
        public ActionResult DeleteSupplementConfiguration(long id)
        {
            var result = new HrmResultModel<WorkingdaySupplementConfigurationModel>();
            string responeseResources = string.Empty;
            var response = this._workingDaySupplementConfigurationService.DeleteSupplementConfiguration(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count() > 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Fail");
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        result.Success = true;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region
        public ActionResult GetSupplementConfigurationById(BasicParamModel param)
        {
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdaySupplementConfiguration);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

            TableViewModel tableData = new TableViewModel();
            dataTableConfig.TableConfigName = TableConfig.WorkingdaySupplementConfiguration;
            tableData = RenderTable(dataTableConfig, param, TableName.TableWorkingdaySupplementConfiguration);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);

        }
        #endregion
        #region Search Role
        public ActionResult SearchRole(string searchKey)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            menuPermissionView.Roles = new List<dynamic>();
            var response = _roleService.SearchRole(searchKey, CurrentUser.LanguageId);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    menuPermissionView.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Permission", "_ListRole.cshtml"), menuPermissionView);

        }
        #endregion
        #region Exception
        public ActionResult Exception()
        {
            return View();
        }
        #endregion
        #region GetSupplementConfigurationException
        public ActionResult GetSupplementConfigurationException(BasicParamModel param)
        {
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdaySupplementConfigurationException);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);
            TableViewModel tableData = new TableViewModel();
            dataTableConfig.TableConfigName = TableConfig.WorkingdaySupplementConfigurationException;
            tableData = RenderTable(dataTableConfig, param, TableName.TableWorkingdaySupplementConfigurationException);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);

        }
        #endregion

        #region
        public ActionResult SaveSupplementConfigurationException(WorkingdaySupplementConfigurationExceptionModel model)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            model.ListStaffId = string.Join(",", model.ListStaffApply);
            model.ApprovedBy = string.Join(",", model.ListApprovedBy);
            var entity = MapperHelper.Map<WorkingdaySupplementConfigurationExceptionModel, WorkingdaySupplementConfigurationExceptionEntity>(model);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<WorkingdaySupplementConfigurationExceptionModel>();
            var response = this._workingDaySupplementConfigurationExceptionService.SaveSupplementConfigurationException(entity);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementConfigurationExceptionModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count == 0 || result.Results == null)
                    {
                        if (model.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                        result.Success = true;
                    }
                    else
                    {
                        if (model.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                        result.Success = false;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Get approvedby staff
        public ActionResult GetApprovedBySaff(string listApprovedSaffs)
        {
            WorkingdaySupplementConfigurationViewModel supplementConfigurationViewModel = new WorkingdaySupplementConfigurationViewModel();
            var response = this._workingDaySupplementConfigurationExceptionService.GetApprovedBySaff(listApprovedSaffs);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    supplementConfigurationViewModel.ListApprovedByStaffs = result.Results;
                    var result_01 = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);


                    var listGroup = new List<LongTypeModel>();
                    listGroup.Add(new LongTypeModel()
                    {
                        Value = MasterDataId.SupplementConfigurationStatusAprove
                    });
                    listGroup.Add(new LongTypeModel()
                    {
                        Value = MasterDataId.SupplementConfigurationActions
                    });
                    var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                    var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
                    var responseMasterDataDetail = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                    if (!CheckPermission(responseMasterDataDetail))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        supplementConfigurationViewModel.SupplementConfigurationStatusApprove = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementConfigurationStatusAprove).ToList();
                    }
                    supplementConfigurationViewModel.SupplementConfiguration.PrevStatus = 0;
                    if (result_01.Results.Select(m => m.NextRequestStatusId).ToList().Count() > 0)
                    {
                        supplementConfigurationViewModel.SupplementConfiguration.PrevStatus = int.Parse(result_01.Results.Select(m => m.NextRequestStatusId).FirstOrDefault());
                    }
                  
                    
                }
            }
            return PartialView(UrlHelpers.View("~/Administration/Views/WorkingdaySupplementConfiguration/_AddSupplementConfigurationExceptionBody.cshtml"), supplementConfigurationViewModel);
        }
        #endregion
    }
}