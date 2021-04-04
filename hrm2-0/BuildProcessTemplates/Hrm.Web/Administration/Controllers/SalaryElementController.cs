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
    public partial class SalaryElementController : BaseController
    {
        #region Fields
        private ISalaryElementService _salaryElementService;
        private IMasterDataService _masterDataService;
        private ITableColumnService _tableColumnService;
        private ITableConfigService _tableConfigService;
        private ILocalizationService _localizationService;
        private long _languageId;
 
        #endregion Fields
        #region Constructors
        public SalaryElementController(ISalaryElementService salaryElementService, IMasterDataService masterDataService, ITableColumnService tableColumnService, ITableConfigService tableConfigService, ILocalizationService localizationService)
        {
            _tableConfigService = tableConfigService;
            _salaryElementService = salaryElementService;
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _localizationService = localizationService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion

        public ActionResult SalaryElement()
        {

            SalaryElementViewModel salaryElement_vm = new SalaryElementViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.SalaryElement);
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
                    dataTableConfig.TableConfigName = TableConfig.SalaryElement;
                    dataTableConfig.TableDataUrl = TableUrl.TableSalaryElementUrl;
                    salaryElement_vm.Table = RenderTable(dataTableConfig, param, TableName.TableSalaryElement);
                }
            }
            return View(salaryElement_vm);
        }
        public ActionResult ShowFormAddSalaryElement(long id=0)
        {
            AddSalaryElementViewModel addSalaryElement_vm = new AddSalaryElementViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Type
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.DataType
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
                addSalaryElement_vm.Types = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Type).ToList();
                addSalaryElement_vm.DataTypes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.DataType).ToList();
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
                    addSalaryElement_vm.Languages = resultLanguage.Results;
                }
            }
            if (id!=0)
            {
                var responseSalaryElement = _salaryElementService.GetSalaryElementById(id);
                if (responseSalaryElement!=null)
                {
                    var resultSalaryElement = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(responseSalaryElement);
                    if (!CheckPermission(resultSalaryElement))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addSalaryElement_vm.SalaryElement = resultSalaryElement.Results.FirstOrDefault();
                    }
                }
                var responseLocalizedDatas = localizationService.GetLocalizedDataByDataId(id, DataType.SalaryElement);
                List<LocalizedDataModel> LocalizedDatas = new List<LocalizedDataModel>();
                if (responseLocalizedDatas != null)
                {
                    var resultLocalizedDatas = JsonConvert.DeserializeObject<HrmResultModel<LocalizedDataModel>>(responseLocalizedDatas);
                    if (!CheckPermission(resultLocalizedDatas))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        LocalizedDatas = resultLocalizedDatas.Results;
                        var listProp = localizationService.GetMultipleLanguageConfiguration(DataType.SalaryElement);
                        var listData = new List<SalaryElementModel>();
                        if (addSalaryElement_vm.Languages != null && addSalaryElement_vm.Languages.Count()>0)
                        {
                            foreach (var language in addSalaryElement_vm.Languages)
                            {
                                var data = LocalizedDatas.Where(s => s.LanguageId == language.Id).ToList();
                                if (data != null && data.Count() > 0)
                                {
                                    SalaryElementModel salaryElement = new SalaryElementModel();
                                    var response = FunctionHelpers.ConvertLanguageToModel(data, salaryElement, listProp.Results);
                                    response.LanguageId = language.Id;
                                    addSalaryElement_vm.SalaryElements.Add(response);
                                }
                            }
                        }
                       
                    }
                }
                else
                {
                    addSalaryElement_vm.SalaryElements.Add(new SalaryElementModel());
                }

            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryElement", "_FormAddSalaryElement.cshtml"), addSalaryElement_vm);
        }
        public ActionResult RemoveDataLanguage(List<SalaryElementModel> listModel,int index)
        {
            AddSalaryElementViewModel addSalaryElement_vm = new AddSalaryElementViewModel();            
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
                    addSalaryElement_vm.Languages = resultLanguage.Results;
                }
            }
            if (listModel!=null && listModel.Count()>0)
            {
                listModel.RemoveAt(index);
                addSalaryElement_vm.SalaryElements = listModel;
            }
            else
            {
                addSalaryElement_vm.SalaryElements = new List<SalaryElementModel>();
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryElement", "_BodyFromAddSalaryElement.cshtml"), addSalaryElement_vm);
        }
        public ActionResult AddDataLanguage(List<SalaryElementModel> listModel)
        {
            AddSalaryElementViewModel addSalaryElement_vm = new AddSalaryElementViewModel();
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
                    addSalaryElement_vm.Languages = resultLanguage.Results;
                }
            }
            if (listModel!=null && listModel.Count()>0)
            {
                listModel.Add(new SalaryElementModel());
                addSalaryElement_vm.SalaryElements = listModel;
            }
            else
            {
                addSalaryElement_vm.SalaryElements.Add(new SalaryElementModel());
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryElement", "_BodyFromAddSalaryElement.cshtml"), addSalaryElement_vm);
        }
        public ActionResult SaveSalaryElement(SalaryElementModel model,List<SalaryElementModel> listModel)
        {
            model.LanguageId = CurrentUser.LanguageId;
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }

            if (listModel != null && listModel.Count() > 0)
            {
                foreach (var item in listModel)
                {
                    item.Code = "ok";
                    item.TypeId = 1;
                }
                var validationsListModel = ValidationHelper.ListValidation(listModel, "listModel");
                if (validationsListModel.Count > 0)
                {                  
                    return Json(new { Result = validationsListModel, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model!=null)
            {
                if (listModel!=null && listModel.Count()>0)
                {
                    var data = listModel.Where(s => s.LanguageId == model.LanguageId).ToList();
                    if ( data==null || data.Count()==0)
                    {
                        listModel.Add(model);
                    }
                }
                else
                {
                    listModel = new List<SalaryElementModel>();
                    listModel.Add(model);
                }
            }
            model.CreatedBy = CurrentUser.UserId;
            model.UpdatedBy = CurrentUser.UserId;
            var convertListData = FunctionHelpers.ConvertToLanguageFromListModel(listModel, DataType.SalaryElement);
            var LocalizedDataEntity = MapperHelper.MapList<LocalizedDataModel, LocalizedDataEntity>(convertListData);
            var DataEntity = MapperHelper.Map<SalaryElementModel, SalaryElementEntity>(model);
            var result = new HrmResultModel<SalaryElementModel>();
            string responeseResources = string.Empty;
            var response = _salaryElementService.SaveSalaryElement(DataEntity, LocalizedDataEntity);
            if (response!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Code == model.Code)
                    {
                        List<ValidationModel> validations = new List<ValidationModel>();
                        validations.Add(new ValidationModel { ColumnName = "model.Code", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.SalaryElementCode") });
                        return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                    }
                    else if (result.Results.Count() > 0 && result.Results.FirstOrDefault().Id != 0)
                    {
                        
                        if (model.Id!=0)
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
        public ActionResult checkFormula(SalaryElementModel model, CheckSalaryElementFormula check)
        {
            if (check.Formula==null)
            {
                check.Formula = model.Formula;
            }
            float resultValue = 0;
            string outString = null;
            var response = _salaryElementService.GetResultSFormular(check.Formula, out outString, out resultValue);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success == false)
                    {
                        check.Value = 0;
                        check.Error = _localizationService.GetResources("SalaryElement.CheckMesseage"); ;
                        check.Formula = check.Formula;
                        check.Success = false;
                    }
                    else
                    {
                        check.Value = Convert.ToDecimal(resultValue);
                        check.Formula = outString;
                        check.Success = true;

                    }
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("SalaryElement", "_CheckSalaryElementFormula.cshtml"), check);
        }      
        public ActionResult DeleteSalaryElement(long id)
        {
            var result = new HrmResultModel<SalaryElementModel>();
            var response = _salaryElementService.DeleteSalaryElement(id);
            var responeseResources = string.Empty;
            if (response!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count()>0 && result.Results.FirstOrDefault().Id!=0)
                    {
                        result.Success = false;
                        responeseResources = _localizationService.GetBaseResources("Message.Delete.UnSuccessful");

                    }
                    else
                    {
                        /*GetLocalizedData*/
                        result.Success = true;
                        responeseResources = _localizationService.GetBaseResources("Message.Delete.Successful");
                    }
                }               
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
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

            if (type == TableName.TableSalaryElement)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableSalaryElement;
                tableData.TableDataUrl = TableUrl.TableSalaryElementUrl;
                tableData.TableConfigName = TableConfig.SalaryElement;

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
            if (type == TableName.TableSalaryElement)
            {
                return this._salaryElementService.GetSalaryElement(paramEntity, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}