using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web;
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
using Hrm.Admin.ViewModels;
using Hrm.Framework.Controllers;
using Hrm.Repository.Type;
using Hrm.Framework.Helpers;
using Hrm.Admin.Models;


namespace Hrm.Admin.Controllers
{    
    public partial class OrganizationController : Hrm.Framework.Controllers.OrganizationController
    {
        // GET: Organization
        #region Fields
        private IOrganizationService _organizationService;
        private ITableConfigService _tableConfigService;
        private ITableService _tableService;
        private IStaffService _staffService;
        private IMasterDataService _masterDataService;
        private ILocalizationService _localizationService;
        private ITableColumnService _tableColumnService;

        private long _languageId,_userId,_roleId;
        private int _totalRecord;
        #endregion Fields
        #region Constructors
        public OrganizationController(IOrganizationService organizationService, ITableConfigService tableConfigService, ITableService tableService, IStaffService staffService, IMasterDataService masterDataService, ILocalizationService localizationService, ITableColumnService tableColumnService)
        {
            this._organizationService = organizationService;
            this._tableConfigService = tableConfigService;
            this._tableService = tableService;
            this._staffService = staffService;
            this._masterDataService = masterDataService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = 1;
            _localizationService = localizationService;
            _tableColumnService = tableColumnService;
        }
        #endregion

        public ActionResult List()
        {
            var organization_vm = new OrganizationViewModel();

            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffOrganization);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

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
            organization_vm.Table = RenderTable(dataTableConfig, param, TableName.TableStaffOrganization);

            //Get tree phòng ban
            param.PageSize = int.MaxValue;
            var responseTreeView = GetOrganization(_organizationService, param,out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            organization_vm.Organizations = data;
            organization_vm.OrganizationsTree = responseTreeView;
            return View(organization_vm);
        }
        public ActionResult TreeOrganization(TreeViewModel model)
        {
            return null;
        }

        public ActionResult ShowFormAddOrganization(long id=0)
        {
            var organization = new OrganizationViewModel();
            organization.MasterDataStatus = new List<dynamic>();
            organization.MasterDataCurrency = new List<dynamic>();
            organization.MasterDataCategoryCompany = new List<dynamic>();
            organization.MasterDataBrand = new List<dynamic>();
            organization.Organization = new OrganizationModel();
            //Dropdownlist trạng thái
            var responseMasterDataStatus = this._masterDataService.GetAllMasterDataByName(MasterGroup.Status, _languageId);
            if (responseMasterDataStatus!=null)
            {
                var resultMasterDataStatus = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataStatus);
                if (!CheckPermission(resultMasterDataStatus))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataStatus= JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataStatus.Results));
                }
            }

            //Dropdownlist currency
            var responseMasterDataCurrency = this._masterDataService.GetAllMasterDataByName(MasterGroup.Currency, _languageId);
            if (responseMasterDataCurrency != null)
            {
                var resultMasterDataCurrency = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCurrency);
                if (!CheckPermission(resultMasterDataCurrency))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCurrency = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCurrency.Results));
                }
            }

            //Dropdownlist loại
            var responseMasterDataCategoryCompany = this._masterDataService.GetAllMasterDataByName(MasterGroup.CategoryCompany, _languageId);
            if (responseMasterDataCategoryCompany != null)
            {
                var resultMasterDataCategoryCompany = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCategoryCompany);
                if (!CheckPermission(resultMasterDataCategoryCompany))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCategoryCompany = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCategoryCompany.Results));
                }
            }

            //Dropdownlist Chi nhánh
            var responseMasterDataBrand = this._masterDataService.GetAllMasterDataByName(MasterGroup.Brand, _languageId);
            if (responseMasterDataBrand != null)
            {
                var resultMasterDataBrand = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataBrand);
                if (!CheckPermission(resultMasterDataBrand))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataBrand = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataBrand.Results));
                }
            }

            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var responseTreeView = GetOrganization(_organizationService, param,out _totalRecord);

            if (id != 0)
            {
                var responese = _organizationService.GetOrganizationById(id);
                if (responese!=null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(responese);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        organization.Organization = result.Results.FirstOrDefault();
                    }
                }
            }
            organization.Organizations = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));

            return PartialView(UrlHelpers.TemplateAdmin("Organization","_AddOrganization.cshtml"), organization);

        }
        public ActionResult ShowFormMergerOrganization(List<BaseModel> listData)
        {
            var organization = new OrganizationViewModel();
            //Dropdownlist trạng thái
            var responseMasterDataStatus = this._masterDataService.GetAllMasterDataByName(MasterGroup.Status, _languageId);
            if (responseMasterDataStatus != null)
            {
                var resultMasterDataStatus = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataStatus);
                if (!CheckPermission(resultMasterDataStatus))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataStatus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataStatus.Results));
                }
            }

            //Dropdownlist currency
            var responseMasterDataCurrency = this._masterDataService.GetAllMasterDataByName(MasterGroup.Currency, _languageId);
            if (responseMasterDataCurrency != null)
            {
                var resultMasterDataCurrency = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCurrency);
                if (!CheckPermission(resultMasterDataCurrency))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCurrency = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCurrency.Results));
                }
            }

            //Dropdownlist loại
            var responseMasterDataCategoryCompany = this._masterDataService.GetAllMasterDataByName(MasterGroup.CategoryCompany, _languageId);
            if (responseMasterDataCategoryCompany != null)
            {
                var resultMasterDataCategoryCompany = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCategoryCompany);
                if (!CheckPermission(resultMasterDataCategoryCompany))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCategoryCompany = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCategoryCompany.Results));
                }
            }

            //Dropdown Organization
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            organization.Organizations = data;

            //Dropdownlist Chi nhánh
            var responseMasterDataBrand = this._masterDataService.GetAllMasterDataByName(MasterGroup.Brand, _languageId);
            if (responseMasterDataBrand!=null)
            {
                var resultMasterDataBrand= JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataBrand);
                if (!CheckPermission(resultMasterDataBrand))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataBrand = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataBrand.Results));
                }
            }
            if (listData!=null && listData.Count>1)
            {
                var listDataType = MapperHelper.MapList<BaseModel, ListDataSelectedIdType>(listData);
                var response = _organizationService.CheckOrganizationType(listDataType);
                if (response!=null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (result.Results.Count>1)
                        {
                            organization.IsOrganizationOld = false;
                        }
                        else
                        {
                            organization.OrganizationType = result.Results.FirstOrDefault().OrganizationType.Value;
                        }
                    }
                }
            }
            return PartialView("~/Administration/Views/Organization/_MergerOrganization.cshtml", organization);

        }
        public ActionResult ReloadMergerOrganizationOld(long id)
        {
            var organization = new OrganizationViewModel();
            //Dropdownlist trạng thái
            var responseMasterDataStatus = this._masterDataService.GetAllMasterDataByName(MasterGroup.Status, _languageId);
            if (responseMasterDataStatus != null)
            {
                var resultMasterDataStatus = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataStatus);
                if (!CheckPermission(resultMasterDataStatus))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataStatus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataStatus.Results));
                }
            }

            //Dropdownlist currency
            var responseMasterDataCurrency = this._masterDataService.GetAllMasterDataByName(MasterGroup.Currency, _languageId);
            if (responseMasterDataCurrency != null)
            {
                var resultMasterDataCurrency = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCurrency);
                if (!CheckPermission(resultMasterDataCurrency))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCurrency = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCurrency.Results));
                }
            }

            //Dropdownlist loại
            var responseMasterDataCategoryCompany = this._masterDataService.GetAllMasterDataByName(MasterGroup.CategoryCompany, _languageId);
            if (responseMasterDataCategoryCompany != null)
            {
                var resultMasterDataCategoryCompany = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCategoryCompany);
                if (!CheckPermission(resultMasterDataCategoryCompany))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCategoryCompany = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCategoryCompany.Results));
                }
            }

            //Dropdown Organization
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            organization.Organizations = data;

            //Dropdownlist Chi nhánh
            var responseMasterDataBrand = this._masterDataService.GetAllMasterDataByName(MasterGroup.Brand, _languageId);
            if (responseMasterDataBrand != null)
            {
                var resultMasterDataBrand = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataBrand);
                if (!CheckPermission(resultMasterDataBrand))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataBrand = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataBrand.Results));
                }
            }
            var responseOrganization = _organizationService.GetOrganizationById(id);
            if (responseOrganization!=null)
            {
                var resultOrganization= JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(responseOrganization);
                if (!CheckPermission(resultOrganization))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.Organization = resultOrganization.Results.FirstOrDefault();
                    organization.OrganizationType = resultOrganization.Results.FirstOrDefault().OrganizationType.Value;
                }
            }

            return PartialView(UrlHelpers.TemplateAdmin("organization", "_MergerOrganizationOldBody.cshtml"), organization);

        }
        public ActionResult ShowFormPersonnelTransfer()
        {
            var organization = new OrganizationViewModel();

            //Dropdownlist trạng thái
            var responseMasterDataStatus = this._masterDataService.GetAllMasterDataByName(MasterGroup.Status, _languageId);
            if (responseMasterDataStatus != null)
            {
                var resultMasterDataStatus = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataStatus);
                if (!CheckPermission(resultMasterDataStatus))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataStatus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataStatus.Results));
                }
            }

            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };

            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            organization.Organizations = data;

            return PartialView("~/Administration/Views/Organization/_personnelTransfer.cshtml", organization);

        }
        public ActionResult ReloadFormMergerOrganization(long check, long parentId)
        {
            var organization = new OrganizationViewModel();
            organization.MasterDataStatus = new List<dynamic>();
            organization.MasterDataCurrency = new List<dynamic>();
            organization.MasterDataCategoryCompany = new List<dynamic>();
            organization.MasterDataBrand = new List<dynamic>();
            organization.ParentId = parentId;

            //Dropdownlist trạng thái
            var responseMasterDataStatus = this._masterDataService.GetAllMasterDataByName(MasterGroup.Status, _languageId);
            if (responseMasterDataStatus != null)
            {
                var resultMasterDataStatus = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataStatus);
                if (!CheckPermission(resultMasterDataStatus))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataStatus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataStatus.Results));
                }
            }


            //Dropdownlist currency
            var responseMasterDataCurrency = this._masterDataService.GetAllMasterDataByName(MasterGroup.Currency, _languageId);
            if (responseMasterDataCurrency != null)
            {
                var resultMasterDataCurrency = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCurrency);
                if (!CheckPermission(resultMasterDataCurrency))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCurrency = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCurrency.Results));
                }
            }

            //Dropdownlist loại
            var responseMasterDataCategoryCompany = this._masterDataService.GetAllMasterDataByName(MasterGroup.CategoryCompany, _languageId);
            if (responseMasterDataCategoryCompany != null)
            {
                var resultMasterDataCategoryCompany = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataCategoryCompany);
                if (!CheckPermission(resultMasterDataCategoryCompany))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataCategoryCompany = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataCategoryCompany.Results));
                }
            }

            //Dropdown Organization
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            organization.Organizations = data;

            //Dropdownlist Chi nhánh
            var responseMasterDataBrand = this._masterDataService.GetAllMasterDataByName(MasterGroup.Brand, _languageId);
            if (responseMasterDataBrand != null)
            {
                var resultMasterDataBrand = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterDataBrand);
                if (!CheckPermission(resultMasterDataBrand))
                {
                    //return to Access Denied
                }
                else
                {
                    organization.MasterDataBrand = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultMasterDataBrand.Results));
                }
            }
            if (check==1)
            {
                return PartialView("~/Administration/Views/Organization/_MergerOrganizationOld.cshtml", organization);
            }
            else
            {
                return PartialView("~/Administration/Views/Organization/_MergerOrganizationNew.cshtml", organization);

            }
        }
        public ActionResult GetStaffById(BasicParamModel param)
        {
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffOrganization);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

            TableViewModel tableData = new TableViewModel();
            tableData = RenderTable(dataTableConfig, param, TableName.TableStaffOrganization);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);

        }

        public ActionResult SaveOrganization(OrganizationModel data)
        {
            if (data!=null)
            {
                if (data.OrganizationType==0)
                {
                    data.OrganizationType = null;
                }
                var validations = ValidationHelper.Validation(data, "data");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            data.UpdatedBy = CurrentUser.UserId;
            data.CreatedBy = CurrentUser.UserId;
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var organizationEntity = MapperHelper.Map<OrganizationModel, OrganizationEntity>(data);
            var responeseResources = string.Empty;
            HrmResultModel<OrganizationModel> result = new HrmResultModel<OrganizationModel>();
            var response = this._organizationService.SaveOrganization(organizationEntity, paramEntity);
            if (response!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count==1)
                    {
                        result.Success=false;
                        if (data.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");

                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                    }
                    else
                    {
                        result.Success = true;                      
                        if (data.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                    }
                }
            }            
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListOrganization()
        {
            var organization_vm = new OrganizationViewModel();

            //Lấy table nhân viên
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Organization);
            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results[0].ConfigData);

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
            organization_vm.Table = RenderTable(dataTableConfig, param, TableName.TableOrganization);

            return View(organization_vm);
        }

        public ActionResult GetDataStaff(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }

        public ActionResult GetDataOrganization(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }

        public ActionResult ReloadTreeOrganization()
        {
            var treeViewModel = new TreeViewModel();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));
            treeViewModel.TreeData = responseTreeView;
            treeViewModel.DisplayField = "OrganizationName";
            treeViewModel.ValueField = "OrganizationCode";
            treeViewModel.Name = "tree-organization";
            return View(UrlHelpers.Template("_TreeView.cshtml"), treeViewModel);
        }

        public ActionResult savePersonnelTransfer(WorkingProcessModel data)
        {
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            bool isSuccess = false;
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var dataEntity = MapperHelper.Map<WorkingProcessModel, WorkingProcessEntity>(data);

            var response = this._organizationService.savePersonnelTransfer(dataEntity, paramEntity);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success == true)
                    {
                        isSuccess = true;
                    }
                }
            }
            return Json(new { isSuccess }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult saveMergerOrganization(OrganizationModel data,List<BaseModel> listData,int checkRadio)
        {
            if (data != null)
            {
                if (checkRadio == 2)
                {
                    var dataOrganizationNew = MapperHelper.Map<OrganizationModel, OrganizationNewModel>(data);
                    var validations = ValidationHelper.Validation(dataOrganizationNew, "data");
                    if (validations.Count > 0)
                    {
                        var filter = validations.Where(s => s.ColumnName.Contains("StartDate")).ToList();
                        if (filter != null && filter.Count > 0)
                        {
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (data.StartDate != null && data.StartDate > DateTime.Now)
                            {
                                validations.Add(new ValidationModel { ColumnName = "data.StartDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.StartDate") });
                            }
                            else if (data.StartDate != null && data.EndDate != null)
                            {

                                if (data.StartDate > data.EndDate)
                                {
                                    validations.Add(new ValidationModel { ColumnName = "data.StartDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
                                }
                            }
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }
                else
                {
                    var dataOrganizationOld = MapperHelper.Map<OrganizationModel, OrganizationOldModel>(data);
                    var validations = ValidationHelper.Validation(dataOrganizationOld, "data");
                    if (validations.Count > 0)
                    {
                        var filter = validations.Where(s => s.ColumnName.Contains("StartDate")).ToList();
                        if (filter != null && filter.Count > 0)
                        {
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (data.StartDate != null && data.StartDate > DateTime.Now)
                            {
                                validations.Add(new ValidationModel { ColumnName = "data.StartDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.StartDate") });
                            }
                            else if (data.StartDate != null && data.EndDate != null)
                            {

                                if (data.StartDate > data.EndDate)
                                {
                                    validations.Add(new ValidationModel { ColumnName = "data.StartDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
                                }
                            }
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                              
            }

            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            data.CreatedBy = CurrentUser.UserId;
            data.UpdatedBy = CurrentUser.UserId;

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var dataEntity = MapperHelper.Map<OrganizationModel, OrganizationEntity>(data);
            var listDataType = MapperHelper.MapList<BaseModel, ListDataSelectedIdType>(listData);
            var response = this._organizationService.SaveMergerOrganization(dataEntity, listDataType, paramEntity);
            var responeseResources = string.Empty;
            var result = new HrmResultModel<OrganizationModel>();
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count > 0 && result.Results.FirstOrDefault().Id!=0)
                    {
                        if (checkRadio==1 && result.Results.FirstOrDefault().Id==data.Id)
                        {
                            responeseResources = _localizationService.GetResources("ErrorMessage.Validation.OrganizationCode");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        result.Success = true;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }

        #region Staff Org Chart
        public ActionResult OrganizationChart()
        {
            OrganizationChartViewModel organizationChart_vm = new OrganizationChartViewModel();
            organizationChart_vm.OrganizationsByLevel = GetOrganizationChartData(1);
            return View(UrlHelpers.View("~/Administration/Views/Organization/OrganizationChart.cshtml"), organizationChart_vm);

        }
        public ActionResult OrganizationChartView(long id)
        {
            var organizationChart_vm = new OrganizationChartViewModel();
            organizationChart_vm.OrganizationsByLevel = GetOrganizationChartData(id);
            return View(UrlHelpers.View("~/Administration/Views/Organization/_OrganizationChartView.cshtml"), organizationChart_vm);

        }
        private List<OrganizationByLevel> GetOrganizationChartData(long id)
        {
            var listOrganization = new List<OrganizationByLevel>();
            var result = new List<OrganizationModel>();
            var response = _organizationService.GetAllOrganization();
            var organizationDetail = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
            if (!CheckPermission(organizationDetail))
            {
                //return to Access Denied
            }
            else
            {
                result = organizationDetail.Results;
            }

            if (response != null)
            {
                //re level
                if (result.Count > 0)
                {
                    //selected staff
                    listOrganization.Add(new OrganizationByLevel()
                    {
                        Organization = result.FirstOrDefault(x => x.Id == id),
                        Level = 0
                    });
                    var selectedOrganization = listOrganization.FirstOrDefault(x => x.Level == 0).Organization;
                    if (selectedOrganization != null)
                    {
                        //staff's father
                        var father = result.FirstOrDefault(x => x.Id == selectedOrganization.ParentId);
                        if (father != null)
                        {
                            listOrganization.Add(new OrganizationByLevel()
                            {
                                Organization = father,
                                Level = -1
                            });
                            //staff's grandfather

                            var grandFather = result.FirstOrDefault(x => x.Id == father.ParentId);
                            if (grandFather != null)
                            {
                                listOrganization.Add(new OrganizationByLevel()
                                {
                                    Organization = grandFather,
                                    Level = -2
                                });
                            }
                        }
                        //childs
                        var childs = result.Where(x => x.ParentId == selectedOrganization.Id);
                        if (childs != null && childs.Count() > 0)
                        {
                            foreach (var child in childs)
                            {
                                listOrganization.Add(new OrganizationByLevel()
                                {
                                    Organization = child,
                                    Level = 1
                                });
                            }
                        }
                    }
                }
            }
            return listOrganization;
        }
        #endregion 
        #region RenderTable
        private TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.RoleId = _userId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, out totalRecord);
            if (response!=null)
            {
                if (type == TableName.TableOrganization)
                {
                    tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(response);
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
            }
            
            tableData.CurrentPage = param.PageNumber;
            tableData.ItemsPerPage = param.PageSize;
            tableData.TotalRecord = totalRecord;
            
            if (type == TableName.TableOrganization)
            {
                tableData.TableName = TableName.TableOrganization;
                tableData.TableDataUrl = TableUrl.OrganizationUrl;
                tableData.TableConfigName = TableConfig.Organization;
            }
            else
            {
                tableData.TableName = TableName.TableStaffOrganization;
                tableData.TableDataUrl = TableUrl.OrganizationStaffUrl;
                tableData.TableConfigName = TableConfig.StaffOrganization;
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
            long parentId = 1;
            long n = 0;
            if (long.TryParse(param.FilterField, out n))
            {
                param.FilterField = string.Empty;
                parentId = n;
            }
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TableStaffOrganization)
            {
                return this._staffService.GetStaffByOrganizationId(paramEntity, n==0 ? 1: parentId, out totalRecord);
            }
            if (type == TableName.TableOrganization)
            {
                OrganizationModel organization = new OrganizationModel();
                var responseTreeView = GetOrganization(_organizationService, param, out totalRecord);
                if (responseTreeView.Count() > 0 && responseTreeView != null)
                {
                    return JsonConvert.SerializeObject(responseTreeView);
                }
            }          
            totalRecord = 0;
            return string.Empty;
        }

        public ActionResult DeleteOrganization(long id)
        {
            bool isSucces = false;
            var responeseResources = string.Empty;
            var response = _organizationService.DeleteOrganization(id,CurrentUser.UserId,CurrentUser.RoleId);
            if (response!=null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results==null || result.Results.Count==0)
                    {
                        isSucces = true;
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.UnSuccessful");
                    }
                }
            }
            return Json(new { IsSucces= isSucces, ResponeseResources= responeseResources }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}