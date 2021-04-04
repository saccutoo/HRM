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
    public partial class SalaryTypeController : Hrm.Framework.Controllers.OrganizationController
    {
        #region Fields
        private ISalaryTypeService _salaryTypeService;
        private ISalaryElementService _salaryElementService;
        private IMasterDataService _masterDataService;
        private ITableColumnService _tableColumnService;
        private ITableConfigService _tableConfigService;
        private ILocalizationService _localizationService;
        private IOrganizationService _organizationService;

        private long _languageId;
        private int _totalRecord = 0;

        #endregion Fields
        #region Constructors
        public SalaryTypeController(ISalaryTypeService salaryTypeService, ISalaryElementService salaryElementService, IMasterDataService masterDataService, ITableColumnService tableColumnService, ITableConfigService tableConfigService, ILocalizationService localizationService, IOrganizationService organizationService)
        {
            _tableConfigService = tableConfigService;
            _salaryTypeService = salaryTypeService;
            _salaryElementService = salaryElementService;
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _localizationService = localizationService;
            _organizationService = organizationService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion

        public ActionResult SalaryType()
        {
            SalaryTypeViewModel salaryType_vm = new SalaryTypeViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.SalaryType);
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
                    dataTableConfig.TableConfigName = TableConfig.SalaryType;
                    dataTableConfig.TableDataUrl = TableUrl.TableSalaryTypeUrl;
                    salaryType_vm.Table = RenderTable(dataTableConfig, param, TableName.TableSalaryType);
                }
            }
            return View(salaryType_vm);
        }
        public ActionResult AddSalaryType(long id=0)
        {
            AddSalaryTypeViewModel addSalaryType = new AddSalaryTypeViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Status
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
                addSalaryType.Status = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
            }

            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            }; 
            var responseTreeView = GetOrganization(_organizationService, param, out _totalRecord);
            addSalaryType.Organizations = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(responseTreeView));

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElement=this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElement!=null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElement);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    addSalaryType.AddSalaryTypeTableSalaryElement.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }

            if (id!=0)
            {
                var responseSalaryType = _salaryTypeService.GetSalaryTypeById(id);
                if (responseSalaryType != null)
                {
                    var resultSalaryType = JsonConvert.DeserializeObject<HrmResultModel<SalaryTypeModel>>(responseSalaryType);
                    if (!CheckPermission(resultSalaryType))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addSalaryType.SalaryType = resultSalaryType.Results.FirstOrDefault();
                    }
                }
                var responseListSalaryElement = _salaryElementService.GetSalaryElementBySalaryTypeId(id);
                if (responseListSalaryElement != null)
                {
                    var resultListSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseListSalaryElement);
                    if (!CheckPermission(resultListSalaryElement))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addSalaryType.AddSalaryTypeTableSalaryElement.SalaryElements = resultListSalaryElement.Results;
                    }
                }
                var responseListSalarytypeMapper = _salaryTypeService.GetSalarytypeMapperBySalaryTypeId(id);
                if (responseListSalarytypeMapper != null)
                {
                    var resultListSalarytypeMapper = JsonConvert.DeserializeObject<HrmResultModel<SalarytypeMapperModel>>(responseListSalarytypeMapper);
                    if (!CheckPermission(resultListSalarytypeMapper))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (resultListSalarytypeMapper.Results.Count()>0)
                        {
                            foreach (var item in resultListSalarytypeMapper.Results)
                            {
                                addSalaryType.ListOrganization.Add(item.DataId.ToString());
                            }
                        }
                    }
                }
            }
            return View(addSalaryType);
        }
        public ActionResult ReloadRowTable(List<SalaryElementModel> listModel, long salaryElementId,int index)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            if (salaryElementId!=0)
            {
                var responseSalaryElement = _salaryElementService.GetSalaryElementById(salaryElementId);
                if (responseSalaryElement != null)
                {
                    var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElement);
                    if (!CheckPermission(resultSalaryElement))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        AddSalaryTypeTableSalary.SalaryElement = resultSalaryElement.Results.FirstOrDefault();
                        AddSalaryTypeTableSalary.SalaryElement.Index = index;
                        var model = listModel.Where(s => s.Index == index).ToList().FirstOrDefault();
                        AddSalaryTypeTableSalary.SalaryElement.IsShowPayslip = model.IsShowPayslip;
                        AddSalaryTypeTableSalary.SalaryElement.IsShowSalary = model.IsShowSalary;

                    }
                }
            }
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_ReloadRowTable.cshtml"), AddSalaryTypeTableSalary);
        }
        public ActionResult AddNewRowTable(List<SalaryElementModel> listModel)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            if (listModel==null || listModel.Count()==0)
            {
                AddSalaryTypeTableSalary.SalaryElements.Add(new SalaryElementModel { IsEditRow=true});
            }
            else
            {
                AddSalaryTypeTableSalary.SalaryElements = listModel;
                AddSalaryTypeTableSalary.SalaryElements.Add(new SalaryElementModel { });
            }
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_TableAddSalaryElemnt.cshtml"), AddSalaryTypeTableSalary);
        }
        public ActionResult EditRowTable(List<SalaryElementModel> listModel,long id)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            listModel.Where(x => x.Id == id).FirstOrDefault().IsEditRow=true;
            AddSalaryTypeTableSalary.SalaryElements = listModel;
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_TableAddSalaryElemnt.cshtml"), AddSalaryTypeTableSalary);
        }
        public ActionResult SaveSalaryType(SalaryTypeModel model,List<SalaryElementModel> listModel)
        {
            var result = new HrmResultModel<SalaryTypeModel>();
            string responeseResources = string.Empty;
            model.CreatedBy = CurrentUser.UserId;
            model.UpdatedBy = CurrentUser.UserId;

            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            List<SalarytypeMapperModel> listModelSalarytypeMapper = new List<SalarytypeMapperModel>();
            if (model.ListOrganization!=null && model.ListOrganization.Count()>0)
            {
                foreach (var item in model.ListOrganization)
                {
                    SalarytypeMapperModel salary = new SalarytypeMapperModel();
                    salary.DataId = long.Parse(item);
                    salary.TypeId = MasterDataId.TypeOrganization;
                    listModelSalarytypeMapper.Add(salary);
                }
            }
            var modelEntity = MapperHelper.Map<SalaryTypeModel, SalaryTypeEntity>(model);
            var listType = MapperHelper.MapList<SalaryElementModel, SalaryElementType>(listModel);
            var listSalarytypeMapper = MapperHelper.MapList<SalarytypeMapperModel, SalarytypeMapperType>(listModelSalarytypeMapper);
            var responseSave = _salaryTypeService.SaveSalaryType(modelEntity, listType, listSalarytypeMapper);
            if (responseSave!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<SalaryTypeModel>>(responseSave);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if(result.Results.Count() > 0 && result.Results.FirstOrDefault().Id != 0)
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
                    else
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
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRowTable(List<SalaryElementModel> listModel,int index)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            if (listModel!=null && listModel.Count()>0)
            {
                AddSalaryTypeTableSalary.SalaryElements = listModel.Where(s => s.Index != index).ToList();
            }
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_TableAddSalaryElemnt.cshtml"), AddSalaryTypeTableSalary);

        }
        public ActionResult SaveRow(List<SalaryElementModel> listModel)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            AddSalaryTypeTableSalary.SalaryElements = listModel;
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_TableAddSalaryElemnt.cshtml"), AddSalaryTypeTableSalary);

        }
        public ActionResult DeleteSalaryType(long id)
        {
            var result = new HrmResultModel<SalaryTypeModel>();
            string responeseResources = string.Empty;
            var response = _salaryTypeService.DeleteSalaryType(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<SalaryTypeModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count() > 0 && result.Results.FirstOrDefault().Id != 0)
                    {
                        result.Success = false;
                        responeseResources = _localizationService.GetBaseResources("Message.Delete.UnSuccessful");

                    }
                    else
                    {
                        result.Success = true;
                        responeseResources = _localizationService.GetBaseResources("Message.Delete.Successful");
                    }
                }
            } 
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowFormPaySlip(SalaryTypeModel model, List<SalaryElementModel> listModel)
        {
            SalaryPaySlipViewModel salaryPaySlip = new SalaryPaySlipViewModel();
            salaryPaySlip.SalaryElements = listModel;
            salaryPaySlip.IsViewOrder = true;
            salaryPaySlip.IsSave = true;
            return PartialView(UrlHelpers.View("~/Views/Shared/_SalaryPaySlip.cshtml"), salaryPaySlip);
        }
        public ActionResult ReloadTableAfterOrder(List<SalaryElementModel> listModel, List<SalaryElementModel> listData)
        {
            AddSalaryTypeTableSalaryElementViewModel AddSalaryTypeTableSalary = new AddSalaryTypeTableSalaryElementViewModel();
            if (listData!=null && listData.Count()>0)
            {
                foreach (var item in listData)
                {
                    listModel.Where(s => s.Id == item.Id).FirstOrDefault().OrderNo = item.OrderNo;
                }
            }
            AddSalaryTypeTableSalary.SalaryElements = listModel.OrderBy(s=>s.OrderNo).ToList();
            //model param
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseSalaryElements = this._salaryElementService.GetSalaryElement(paramEntity, out _totalRecord);
            if (responseSalaryElements != null)
            {
                var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElements);
                if (!CheckPermission(resultSalaryElement))
                {
                    //return to Access Denied
                }
                else
                {
                    AddSalaryTypeTableSalary.DynamicSalaryElements = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultSalaryElement.Results));

                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryType", "_TableAddSalaryElemnt.cshtml"), AddSalaryTypeTableSalary);
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

            if (type == TableName.TableSalaryType)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<SalaryTypeModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableSalaryType;
                tableData.TableDataUrl = TableUrl.TableSalaryTypeUrl;
                tableData.TableConfigName = TableConfig.SalaryType;

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
            if (type == TableName.TableSalaryType)
            {
                return this._salaryTypeService.GetSalaryType(paramEntity, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}