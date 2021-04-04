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
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Hrm.Web.Controllers
{
    public class WorkingProcessController : BaseController
    {
        #region Fields
        #endregion Fields
        #region Constructors

        private IWorkingProcessService _workingProcessService;
        private IWorkingdayService _workingdayService;
        private ITableService _tableService;
        private ITableConfigService _tableConfigService;
        private IMasterDataService _masterDataService;
        private IOrganizationService _organizationService;
        private ILocalizationService _localizationService;
        private IStaffService _staffService;
        private ITableColumnService _tableColumnService;
        private IPipelineService _pipelineService;
        private long _languageId;
        private long _userId;
        private long _roleId;

        public WorkingProcessController(IWorkingProcessService workingProcessService
                                        , ITableService tableService
                                        , ITableConfigService tableConfigService
                                        , IMasterDataService masterDataService
                                        , IOrganizationService organizationService
                                        , IStaffService staffService
                                        , ILocalizationService localizationService
                                        , ITableColumnService tableColumnService
                                        , IWorkingdayService workingdayService
                                        , IPipelineService pipelineService)
        {
            this._workingProcessService = workingProcessService;
            this._tableService = tableService;
            this._tableConfigService = tableConfigService;
            this._masterDataService = masterDataService;
            this._organizationService = organizationService;
            this._localizationService = localizationService;
            this._staffService = staffService;
            this._workingdayService = workingdayService;
            this._tableColumnService = tableColumnService;
            this._pipelineService = pipelineService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = 1;

        }
        #endregion Constructors
        // GET: WorkingProcess
        public ActionResult List()
        {
            return View();
        }
        public ActionResult ListByStaff(int? staffId)
        {
            WorkingProcessModel workimgProcess = new WorkingProcessModel();
            var resultTable = this._tableService.GetTable(TableConfig.Staff);
            var responseTable = JsonConvert.DeserializeObject<TableModel>(resultTable);

            var resultTableConfig = this._tableConfigService.GetTableConfig(responseTable.Id);
            var responseTableConfig = JsonConvert.DeserializeObject<List<TableConfigModel>>(resultTableConfig);
            var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig[0].ConfigData);
            //model param
            int totalRecord = 0;
            //int totalRecord = 0;
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

            //Gọi hàm lấy quá trình công tác theo nhân viên
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._workingProcessService.GetWorkingProcessByStaff(paramEntity, staffId ?? 0, out totalRecord);
            //Get table config by user
            //Assume already have table config data
            //var rawDataFromSql = responseTableConfig[0].ConfigData;
            var tableData = JsonConvert.DeserializeObject<List<WorkingProcessModel>>(response);
            //var tableConfig = JsonConvert.DeserializeObject<TableViewModel>(rawDataFromSql);
            //tableConfig.TableData = tableData;
            //tableConfig.TotalRecord = totalRecord;

            //var resultMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, _languageId);

            //var dataSelectList = JsonConvert.DeserializeObject<List<MasterDataModel>>(resultMasterDataSelectList);
            //var DataDropDownList = MapperHelper.MapList<MasterDataModel, DropdownListContentModel>(dataSelectList);


            //foreach (var item in DataDropDownList)
            //{
            //    if (Convert.ToInt32(item.Value) == dataTableConfig.ItemsPerPage)
            //    {
            //        item.IsSelected = true;
            //        break;
            //    }
            //}
            //List<dynamic> dataDropDownListDynamic = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(DataDropDownList));
            //tableConfig.lstItemsPerPage = dataDropDownListDynamic;
            return PartialView("~/Views/Staff/_WorkingProcess.cshtml", tableData);
        }
        public ActionResult _AddWorkingPress()
        {
            #region
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
                Value = MasterDataId.WorkingProcessType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StatusAprove
            });
            //listGroup.Add(new LongTypeModel()
            //{
            //    Value = MasterDataId.Office
            //});
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Position
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StaffLevel
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Policy
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Classification
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.PaymentForm
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.PaymentMethod
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Status
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.ContractType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.ContractTime
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.WorkingStatus
            });
            #endregion
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new WorkingProcessViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownWorkingProcessType = response.Results.Where(m => m.GroupId == MasterDataId.WorkingProcessType).ToList();
                model.DataDropdownClassification = response.Results.Where(m => m.GroupId == MasterDataId.Classification).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                model.DataDropdownPaymentForm = response.Results.Where(m => m.GroupId == MasterDataId.PaymentForm).ToList();
                model.DataDropdownPaymentMethod = response.Results.Where(m => m.GroupId == MasterDataId.PaymentMethod).ToList();
                model.DataDropdownPolicy = response.Results.Where(m => m.GroupId == MasterDataId.Policy).ToList();
                model.DataDropdownPosition = response.Results.Where(m => m.GroupId == MasterDataId.Position).ToList();
                model.DataDropdownStaffLevel = response.Results.Where(m => m.GroupId == MasterDataId.StaffLevel).ToList();
                model.DataDropdownStatus = response.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
                model.DataDropdownStatusAprove = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                model.DataDropdownContractTime = response.Results.Where(m => m.GroupId == MasterDataId.ContractTime).ToList();
                model.DataDropdownContractType = response.Results.Where(m => m.GroupId == MasterDataId.ContractType).ToList();
                model.DataDropdownWorkingStatus = response.Results.Where(m => m.GroupId == MasterDataId.WorkingStatus).ToList();
            }
            var resultOrganization = this._organizationService.GetAllOrganizationForDropDown(paramEntity);
            model.DataDropdownOrganization = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultOrganization).Results;
            var resultStaff = this._staffService.GetAllStaffForDropDown(paramEntity);
            model.DataDropdownStaff = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultStaff).Results;
            var shiftTotal = 0;
            paramEntity.PageSize = Int32.MaxValue;
            var resultShift = this._workingdayService.GetWorkingdayShift(paramEntity, out shiftTotal);
            model.DataDropdownShift = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultShift).Results;
            //Thông tin Phúc lợi
            // StaffBenefit
            var resultTableBenefitConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffBenefitPopupWorkingprocess);
            var resultConfigBenefitDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableBenefitConfig);
            if (!CheckPermission(resultConfigBenefitDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableBenefitConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigBenefitDetail.Results.FirstOrDefault().ConfigData);
                dataTableBenefitConfig.ShowFooter = false;
                dataTableBenefitConfig.TableName = TableName.StaffBenefit;
                dataTableBenefitConfig.TableConfigName = TableConfig.StaffBenefitPopupWorkingprocess;
                var param3 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableBenefitConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.WorkingProcess.Id
                };
                model.ListBenefit = RenderTable(dataTableBenefitConfig, param3, TableName.StaffBenefit);
            }
            //StaffAllowance
            //Thông tin Phụ cấp
            var resultTableAllowanceConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffAllowancePopupWorkingprocess);
            var resultConfigAllowanceDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableAllowanceConfig);
            if (!CheckPermission(resultConfigAllowanceDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableAllowanceConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigAllowanceDetail.Results.FirstOrDefault().ConfigData);
                dataTableAllowanceConfig.ShowFooter = false;
                dataTableAllowanceConfig.TableName = TableName.StaffAllowance;
                dataTableAllowanceConfig.TableConfigName = TableConfig.StaffAllowancePopupWorkingprocess;
                var param2 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableAllowanceConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.WorkingProcess.Id
                };
                model.ListAllowance = RenderTable(dataTableAllowanceConfig, param2, TableName.StaffAllowance);
            }

            return PartialView("~/Views/WorkingProcess/_AddWorkingProcess.cshtml", model);

        }
        public ActionResult _GetWorkingProcessById(long id)
        {
            return PartialView("~/Views/WorkingProcess/_WokingProcessDetail.cshtml");
        }
        public ActionResult Save(WorkingProcessViewModel model)
        {
            model = MapperHelper.ConvertModel(model);
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
            var validations = new List<ValidationModel>();
            bool check = false;
            //var checkDecisionNo = this._workingProcessService.CheckDecisionNoExisted(paramEntity, model.WorkingProcess.DecisionNo, model.WorkingProcess.Id, out check);
            //if (checkDecisionNo != null)
            //{
            //    var user = JsonConvert.DeserializeObject<HrmResultModel<bool>>(checkDecisionNo);
            //    if (!CheckPermission(user))
            //    {
            //        //return to Access Denied
            //    }
            //    else
            //    {
            //        if (user.Results != null && check)
            //        {
            //            validations.Add(new ValidationModel { ColumnName = "WorkingProcess.DecisionNo", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.DecisionNoExists") });
            //        }
            //    }
            //}
            var checkTimeApply = this._workingProcessService.CheckWorkingProcessDate(model.WorkingProcess.StartDate, model.WorkingProcess.EndDate, model.WorkingProcess.StaffId, !model.IsPossition && model.WorkingProcess.Id == 0 ? 0 : model.WorkingProcess.OrganizationId, model.WorkingProcess.Id, out check);
            if (checkTimeApply != null)
            {
                var checkTime = JsonConvert.DeserializeObject<HrmResultModel<bool>>(checkTimeApply);
                if (!CheckPermission(checkTime))
                {
                    //return to Access Denied
                }
                else
                {
                    if (checkTime.Results != null && check)
                    {
                        validations.Add(new ValidationModel { ColumnName = "WorkingProcess.StartDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.WorkingProcessTimeApplyCannotDuplicate") });
                        validations.Add(new ValidationModel { ColumnName = "WorkingProcess.EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.WorkingProcessTimeApplyCannotDuplicate") });
                    }
                }
            }
            validations.AddRange(ValidationHelper.Validation(model.WorkingProcess, "WorkingProcess"));
            if (model.IsContract || model.Contract.Id > 0)
            {
                validations.AddRange(ValidationHelper.Validation(model.Contract, "Contract"));
            }
            if (model.WorkingProcess.StartDate > model.WorkingProcess.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "WorkingProcess.EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (model.Contract.ContractStartDate > model.Contract.ContractEndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "Contract.ContractEndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            var checkContractCode = this._workingProcessService.CheckContractCodeExisted(paramEntity, model.Contract.ContractCode, model.Contract.Id, out check);
            if (checkContractCode != null)
            {
                var user = JsonConvert.DeserializeObject<HrmResultModel<bool>>(checkContractCode);
                if (!CheckPermission(user))
                {
                    //return to Access Denied
                }
                else
                {
                    if (user.Results != null && check)
                    {
                        validations.Add(new ValidationModel { ColumnName = "Contract.ContractCode", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.ContractCodeExists") });
                    }
                }
            }
            var checkPipelineStep = this._staffService.CheckNextPipeLineStep(paramEntity, model.StaffOnboardInfo.PipelineStepId, model.WorkingProcess.StaffId, out check);
            if (checkPipelineStep != null)
            {
                var user = JsonConvert.DeserializeObject<HrmResultModel<bool>>(checkPipelineStep);
                if (!CheckPermission(user))
                {
                    //return to Access Denied
                }
                else
                {
                    if (user.Results != null && check)
                    {
                        validations.Add(new ValidationModel { ColumnName = "StaffOnboardInfo.PipelineStepId", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.PipelineStepIsInValid") });
                    }
                }
            }
            if (model.StaffAllowancePopupWorkingprocess != null && model.StaffAllowancePopupWorkingprocess.Count > 0)
            {
                validations.AddRange(ValidationHelper.ListValidation(model.StaffAllowancePopupWorkingprocess, "StaffAllowancePopupWorkingprocess"));
                for (int i = 0; i< model.StaffAllowancePopupWorkingprocess.Count; i++)
                {
                    if (model.StaffAllowancePopupWorkingprocess[i].StartDate > model.StaffAllowancePopupWorkingprocess[i].EndDate)
                    {
                        validations.Add(new ValidationModel { ColumnName = "StaffAllowancePopupWorkingprocess["+i.ToString()+"].EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
                    }
                }
            }
            if (model.StaffBenefitPopupWorkingprocess != null && model.StaffBenefitPopupWorkingprocess.Count > 0)
            {
                validations.AddRange(ValidationHelper.ListValidation(model.StaffBenefitPopupWorkingprocess, "StaffBenefitPopupWorkingprocess"));
                for (int i = 0; i < model.StaffBenefitPopupWorkingprocess.Count; i++)
                {
                    if (model.StaffBenefitPopupWorkingprocess[i].StartDate > model.StaffBenefitPopupWorkingprocess[i].EndDate)
                    {
                        validations.Add(new ValidationModel { ColumnName = "StaffBenefitPopupWorkingprocess[" + i.ToString() + "].EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
                    }
                }
            }
            if (model.StaffOnboardInfo.IsOnboarding)
            {
                validations.AddRange(ValidationHelper.Validation(model.StaffOnboardInfo, "StaffOnboardInfo"));
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            long contractId = 0;
            if (!model.IsSalary && model.WorkingProcess.Id == 0)
            {
                model.StaffAllowancePopupWorkingprocess = new List<StaffAllowanceModel>();
            }
            if (!model.IsBennefit && model.WorkingProcess.Id == 0)
            {
                model.StaffBenefitPopupWorkingprocess = new List<StaffBenefitsModel>();
            }
            var workingProcessResult = false;
            var msg = "";
            long id = 0;
            var contract = MapperHelper.Map<StaffContractModel, StaffContractType>(model.Contract);
            var workingProcess = MapperHelper.Map<WorkingProcessModel, WorkingProcessType>(model.WorkingProcess);
            var staffAllowance = MapperHelper.MapList<StaffAllowanceModel, StaffAllowanceType>(MapperHelper.ConvertModel(model.StaffAllowancePopupWorkingprocess));
            var staffBenefit = MapperHelper.MapList<StaffBenefitsModel , StaffBenefitsType>(MapperHelper.ConvertModel(model.StaffBenefitPopupWorkingprocess));
            var staffOnboardInfo = MapperHelper.Map<StaffOnboardInfoModel, StaffOnboardInfoType>(MapperHelper.ConvertModel(model.StaffOnboardInfo));
            var listContract = new List<StaffContractType>();
            var listWorkingProcess = new List<WorkingProcessType>();
            listContract.Add(contract);
            listWorkingProcess.Add(workingProcess);

            workingProcess.ContractId = contractId;
            var wpResponse = this._workingProcessService.SaveWorkingProcess(listWorkingProcess, listContract, staffAllowance, staffBenefit, staffOnboardInfo, model.IsSalary, model.IsPossition, model.IsContract, paramEntity);
            if (wpResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingProcessEntity>>(wpResponse);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {

                    id = result.Results.FirstOrDefault().Id;
                    if (id > 0)
                    {
                        msg = model.WorkingProcess.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                        workingProcessResult = true;
                    }
                    else
                    {
                        msg = model.WorkingProcess.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                        workingProcessResult = false;
                    }
                }
            }

            Thread thread = new Thread(() => UpdateStaffParent())
            {
                Name = "UpdateStaffParent"
            };
            thread.Start();
            return Json(new { Result = workingProcessResult, Message = msg, Id = id }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkingProcessDetailById(long id, int? viewType)
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
            var model = new WorkingProcessViewModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._workingProcessService.GetWorkingProcessById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingProcessModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.WorkingProcess = result.Results.FirstOrDefault();
                    }
                }
            }
            if (model.WorkingProcess.OrganizationId > 0)
            {
                var organization = this._organizationService.GetParentOrganization(paramEntity, model.WorkingProcess.OrganizationId);
                var resultOrganizationDetail = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(organization);
                if (!CheckPermission(resultOrganizationDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultOrganizationDetail.Results.Count > 0)
                    {
                        model.ListOrganization = resultOrganizationDetail.Results;
                    }
                }
            }

            if (model.WorkingProcess.ContractId > 0)
            {
                var contract = this._staffService.GetStaffContractById(paramEntity, model.WorkingProcess.ContractId);
                var resultcontract = JsonConvert.DeserializeObject<HrmResultModel<StaffContractModel>>(contract);
                if (!CheckPermission(resultcontract))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultcontract.Results.Count > 0)
                    {
                        model.Contract = resultcontract.Results.FirstOrDefault();
                    }
                }
            }
            var onboard = this._staffService.GetPipelineStepStaffByStaffIdAndMenuName(paramEntity, MenuName.Onboarding, model.WorkingProcess.StaffId);
            var resultOnboard = JsonConvert.DeserializeObject<HrmResultModel<StaffOnboardInfoModel>>(onboard);
            if (!CheckPermission(resultOnboard))
            {
                //return to Access Denied
            }
            else
            {
                if (resultOnboard.Results.Count > 0)
                {
                    model.StaffOnboardInfo = resultOnboard.Results.FirstOrDefault();
                    model.StaffOnboardInfo.IsOnboarding = true;
                }
            }
            //Lấy danh sách Phúc lợi
            // StaffBenefit
            var resultTableBenefitConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffBenefitPopupWorkingprocess);
            var resultConfigBenefitDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableBenefitConfig);
            if (!CheckPermission(resultConfigBenefitDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableBenefitConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigBenefitDetail.Results.FirstOrDefault().ConfigData);
                dataTableBenefitConfig.ShowFooter = false;
                dataTableBenefitConfig.TableName = TableName.StaffBenefit;
                dataTableBenefitConfig.TableConfigName = TableConfig.StaffBenefitPopupWorkingprocess;
                var param3 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableBenefitConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.WorkingProcess.Id
                };
                model.ListBenefit = RenderTable(dataTableBenefitConfig, param3, TableName.StaffBenefit);
            }
            //StaffAllowance
            //Lấy danh sách Phụ cấp
            var resultTableAllowanceConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffAllowancePopupWorkingprocess);
            var resultConfigAllowanceDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableAllowanceConfig);
            if (!CheckPermission(resultConfigAllowanceDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableAllowanceConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigAllowanceDetail.Results.FirstOrDefault().ConfigData);
                dataTableAllowanceConfig.ShowFooter = false;
                dataTableAllowanceConfig.TableName = TableName.StaffAllowance;
                dataTableAllowanceConfig.TableConfigName = TableConfig.StaffAllowancePopupWorkingprocess;
                var param2 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableAllowanceConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.WorkingProcess.Id
                };
                model.ListAllowance = RenderTable(dataTableAllowanceConfig, param2, TableName.StaffAllowance);
            }
            if (viewType == 1)
            {
                #region
                var listGroup = new List<LongTypeModel>();

                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.WorkingProcessType
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.StatusAprove
                });
                //listGroup.Add(new LongTypeModel()
                //{
                //    Value = MasterMasterDataId.Office
                //});
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Position
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.StaffLevel
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Policy
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Currency
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Classification
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.PaymentForm
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.PaymentMethod
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Status
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.ContractType
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.ContractTime
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.WorkingStatus
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.MaritalStatus
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Nationality
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Province
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Ethnicity
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.TimekeepingForm
                });

                var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
                var responseMas = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(responseMas))
                {
                    //return to Access Denied
                }
                else
                {
                    model.DataDropdownWorkingProcessType = responseMas.Results.Where(m => m.GroupId == MasterDataId.WorkingProcessType).ToList();
                    model.DataDropdownClassification = responseMas.Results.Where(m => m.GroupId == MasterDataId.Classification).ToList();
                    model.DataDropdownCurrency = responseMas.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                    model.DataDropdownPaymentForm = responseMas.Results.Where(m => m.GroupId == MasterDataId.PaymentForm).ToList();
                    model.DataDropdownPaymentMethod = responseMas.Results.Where(m => m.GroupId == MasterDataId.PaymentMethod).ToList();
                    model.DataDropdownPolicy = responseMas.Results.Where(m => m.GroupId == MasterDataId.Policy).ToList();
                    model.DataDropdownPosition = responseMas.Results.Where(m => m.GroupId == MasterDataId.Position).ToList();
                    model.DataDropdownStaffLevel = responseMas.Results.Where(m => m.GroupId == MasterDataId.StaffLevel).ToList();
                    model.DataDropdownStatus = responseMas.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
                    model.DataDropdownStatusAprove = responseMas.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                    model.DataDropdownContractTime = responseMas.Results.Where(m => m.GroupId == MasterDataId.ContractTime).ToList();
                    model.DataDropdownContractType = responseMas.Results.Where(m => m.GroupId == MasterDataId.ContractType).ToList();
                    model.DataDropdownWorkingStatus = responseMas.Results.Where(m => m.GroupId == MasterDataId.WorkingStatus).ToList();
                }
                var resultOrganization = this._organizationService.GetAllOrganizationForDropDown(paramEntity);
                model.DataDropdownOrganization = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultOrganization).Results;
                var resultStaff = this._staffService.GetAllStaffForDropDown(paramEntity);
                model.DataDropdownStaff = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultStaff).Results;
                var shiftTotal = 0;
                paramEntity.PageSize = Int32.MaxValue;
                var resultShift = this._workingdayService.GetWorkingdayShift(paramEntity, out shiftTotal);
                model.DataDropdownShift = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultShift).Results;
                var pipelineRespone = _pipelineService.GetPipelineByMenuName(MenuName.Onboarding);
                model.DataDropdownPipeline = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineRespone).Results;
                var pipelineStepRespone = _pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding);
                model.DataDropdownPipelineStep = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineStepRespone).Results;
                #endregion
                return PartialView(UrlHelpers.View("~/Views/WorkingProcess/_SaveWokingProcessDetail.cshtml"), model);
            }
            else
            {
                return PartialView(UrlHelpers.View("~/Views/WorkingProcess/_WokingProcessDetail.cshtml"), model);
            }

        }

        public ActionResult ChangeWorkingprocessType(WorkingProcessViewModel model)
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
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            #region
            var listGroup = new List<LongTypeModel>();

            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.WorkingProcessType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StatusAprove
            });
           
            var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
            var responseMas = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(responseMas))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownWorkingProcessType = responseMas.Results.Where(m => m.GroupId == MasterDataId.WorkingProcessType).ToList();
                model.DataDropdownStatusAprove = responseMas.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
            }

            var pipelineRespone = _pipelineService.GetPipelineByMenuName(MenuName.Onboarding);
            model.DataDropdownPipeline = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineRespone).Results;
            var pipelineStepRespone = _pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding);
            model.DataDropdownPipelineStep = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineStepRespone).Results;
            #endregion
            if (model.WorkingProcess.WorkingprocessTypeId == MasterDataId.Onboarding)
            {
                return PartialView(UrlHelpers.View("~/Views/WorkingProcess/_WorkingProcessDecisionOnboarding.cshtml"), model);
            }
            else
            {
                return PartialView(UrlHelpers.View("~/Views/WorkingProcess/_WorkingProcessDecision.cshtml"), model);
            }
        }
        public void UpdateStaffParent()
        {
            //0 bt 1 truc tiep 2 nhân sự
            _workingProcessService.UpdateStaffParent();

        }
        public TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            var result = new TableViewModel();
            result = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(tableData.Clone()));
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.UserId = _roleId;
            param.RoleId = _userId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, out totalRecord);
            var resultData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(response);
            if (String.IsNullOrEmpty(response))
            {
                result.TableData = new List<dynamic>();
            }
            else
            if (!CheckPermission(resultData))
            {
                
                //return to Access Denied
            }
            else
            {
                result.TableData = resultData.Results;
            }
            result.CurrentPage = param.PageNumber;
            result.ItemsPerPage = param.PageSize;
            result.TotalRecord = totalRecord;
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
        private string GetData(string type, BasicParamModel param, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.StaffAllowance && param.ReferenceId > 0)
            {
                totalRecord = 0;
                return this._staffService.GetStaffAllowanceByWorkingprocess(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.StaffBenefit && param.ReferenceId > 0)
            {
                totalRecord = 0;
                return this._staffService.GetStaffBenefitByWorkingprocess(paramEntity, param.ReferenceId);
            }
            
            totalRecord = 0;
            return string.Empty;
        }
    }
}
