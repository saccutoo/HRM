using System.Web.Mvc;
using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Helper;
using System.Dynamic;
using Hrm.Common.Helpers;
using Hrm.Web.ViewModels;
using Hrm.Repository.Type;
using System.Linq;
using System.Globalization;
using System.Web;
using Hrm.Framework.Controllers;
using Hrm.Repository.Entity;
using Hrm.Framework.Helpers;
using Hrm.Framework.ViewModels;
using System.IO;
using System.Threading;

namespace Hrm.Web.Controllers
{
    public partial class StaffController : BaseController
    {
        #region Fields
        private IStaffService _staffService;
        private ITableService _tableService;
        private ITableConfigService _tableConfigService;
        private IMasterDataService _masterDataService;
        private IWorkingProcessService _workingProcessService;
        private IPipelineService _pipelineService;
        private IStaffRelationShipsService _staffRelationShipsService;
        private IStaffRoleService _staffRoleService;
        private IOrganizationService _organizationService;
        private IStaffBonusDisciplineService _staffBonusDisciplineService;
        private ILocalizationService _localizationService;
        private IRoleService _roleService;
        private IChecklistDetailService _checklistDetailService;
        private ITableColumnService _tableColumnService;
        private IChecklistService _checklistService;
        private IWorkingdayService _workingdayService;
        private IAttachmentService _attachmentService;
        private long _languageId;
        private long _userId;
        private long _roleId;
        #endregion Fields
        #region Constructors
        public StaffController(IStaffService staffService,
                                                        ITableService tableService,
                                                        ITableConfigService tableConfigService,
                                                        IMasterDataService masterDataService,
                                                        IWorkingProcessService workingProcessService,
                                                        IPipelineService pipelineService,
                                                        IStaffRelationShipsService staffRelationShipsService,
                                                        IStaffRoleService staffRoleService,
                                                        IOrganizationService organizationService,
                                                        IStaffBonusDisciplineService staffBonusDisciplineService,
                                                        IRoleService roleService,
                                                        ILocalizationService localizationService,
                                                        IChecklistDetailService checklistDetailService,
                                                        ITableColumnService tableColumnService,
                                                        IWorkingdayService workingdayService,
                                                        IChecklistService checklistService,
                                                        IAttachmentService attachmentService)
        {
            this._staffService = staffService;
            this._tableService = tableService;
            this._tableConfigService = tableConfigService;
            this._masterDataService = masterDataService;
            this._workingProcessService = workingProcessService;
            this._pipelineService = pipelineService;
            this._staffRelationShipsService = staffRelationShipsService;
            this._staffRoleService = staffRoleService;
            this._organizationService = organizationService;
            this._staffBonusDisciplineService = staffBonusDisciplineService;
            this._roleService = roleService;
            this._checklistDetailService = checklistDetailService;
            this._localizationService = localizationService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._tableColumnService = tableColumnService;
            this._workingdayService = workingdayService;
            this._attachmentService = attachmentService;
            this._roleId = 1;
            this._checklistService = checklistService;

        }
        #endregion
        #region Staff Information
        public ActionResult List()
        {
            StaffViewModel staff_vm = new StaffViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Staff);
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
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TableStaffReloadUrl;
                dataTableConfig.TableConfigName = TableConfig.Staff;
                dataTableConfig.TableName = TableName.TableStaff;
                staff_vm.Table = RenderTable(dataTableConfig, param, TableName.TableStaff);
            }
            return View(staff_vm);
        }
        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {

            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
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
                TableViewModel tableDataResult = RenderTable(tableData, param, tableData.TableName);
                tableDataResult.TableDataUrl = tableData.TableDataUrl;
                tableDataResult.TableReloadConfigUrl = tableData.TableReloadConfigUrl;
                tableDataResult.TableConfigName = tableData.TableConfigName;
                tableDataResult.TableName = tableData.TableName;
                return View(UrlHelpers.Template("_TableContent.cshtml"), tableDataResult);
            }
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        [HttpGet]
        public ActionResult Detail(long? staffId, int? activeTab, int? actionTab, int? viewType)
        {
            // Khai báo model
            StaffDetailsViewModel StaffDetail_VM = ActiveTabStaffDetail(staffId, activeTab, actionTab, viewType);
            return View(StaffDetail_VM);
        }
        [HttpGet]
        private StaffDetailsViewModel ActiveTabStaffDetail(long? staffId, int? activeTab, int? actionTab, int? viewType)
        {
            activeTab = activeTab ?? 0;
            actionTab = actionTab ?? 0;
            viewType = viewType ?? 0;
            staffId = staffId ?? _userId;
            // Khai báo model
            StaffDetailsViewModel staffDetail_vm = new StaffDetailsViewModel();
            staffDetail_vm.ActiveTab = activeTab ?? 0;
            staffDetail_vm.ActionTab = actionTab ?? 0;
            staffDetail_vm.ViewType = viewType ?? 0;

            //model param
            int totalRecord = 0;
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                ReferenceId = staffId ?? 0,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            //lấy staff detail
            var dataDetail = new StaffModel();
            var detail = this._staffService.GetStaffInformationById(staffId ?? 0);
            var resultStaffInforDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(detail);
            if (!CheckPermission(resultStaffInforDetail))
            {
                //return to Access Denied
            }
            else
            {
                dataDetail = resultStaffInforDetail.Results.FirstOrDefault();
            }
            if (dataDetail != null && dataDetail.ImageLink != null)
            {
                dataDetail.ImageAvataSrc = ReadFileByPath(dataDetail.ImageLink);
            }
            //Lấy danh sách role của nhân sự
            int totalRoleRecord = 0;
            var listRole = new List<StaffRoleModel>();
            var role = this._staffRoleService.GetRoleByStaff(paramEntity, staffId ?? 0, out totalRoleRecord);
            var resultRoleDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffRoleModel>>(role);
            if (!CheckPermission(resultRoleDetail))
            {
                //return to Access Denied
            }
            else
            {
                listRole = resultRoleDetail.Results;
            }
            switch (activeTab)
            {
                #region 0: Thủ tục tiếp nhận
                case 0:
                    {
                        long currentStep = 0;
                        if (dataDetail != null)
                        {
                            long.TryParse(dataDetail.PipelineStepId.ToString(), out currentStep);
                        }
                        var checklist_vm = new ChecklistViewModel();
                        checklist_vm.ChecklistDetail = new List<ChecklistDetailModel>();
                        checklist_vm.Pipelines = new PipelineGridModel()
                        {
                            CurrentStep = currentStep,
                            PipelineSteps = new List<PipelineStepModel>()
                        };
                        var pipelineResponse = this._pipelineService.GetPipelineStepByStaffId(dataDetail.PipelineId, staffId ?? 0);
                        var pipelineStepDetail = JsonConvert.DeserializeObject<HrmResultModel<PipelineStepModel>>(pipelineResponse);
                        if (!CheckPermission(pipelineStepDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            //checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;
                            if (pipelineStepDetail.Results.Count > 0)
                            {
                                if (pipelineStepDetail.Results.FirstOrDefault().PipelineDate != null)
                                {
                                    var listNomal = pipelineStepDetail.Results.Where(s => s.PipelineDate != null && s.PositionId == MasterDataId.Normal).ToList().OrderBy(s => s.PipelineDate);
                                    if (listNomal.Count() > 1)
                                    {
                                        foreach (var item in listNomal)
                                        {
                                            pipelineStepDetail.Results.Remove(item);
                                            checklist_vm.Pipelines.PipelineSteps.Add(item);
                                        }
                                        foreach (var item in pipelineStepDetail.Results)
                                        {
                                            checklist_vm.Pipelines.PipelineSteps.Add(item);
                                        }
                                    }
                                    else if (listNomal.Count() == 1)
                                    {
                                        checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;

                                    }
                                    else
                                    {
                                        checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;
                                    }
                                }
                                else
                                {
                                    foreach (var item in pipelineStepDetail.Results)
                                    {
                                        if (item.Id < currentStep && item.PipelineDate == null)
                                        {
                                            checklist_vm.Pipelines.PipelineSteps.Add(item);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    var listNomal = pipelineStepDetail.Results.Where(s => s.PipelineDate != null && s.PositionId == MasterDataId.Normal).ToList().OrderBy(s => s.PipelineDate);
                                    if (listNomal.Count() > 1)
                                    {
                                        foreach (var item in listNomal)
                                        {
                                            checklist_vm.Pipelines.PipelineSteps.Add(item);

                                        }
                                    }
                                    foreach (var item in checklist_vm.Pipelines.PipelineSteps)
                                    {
                                        pipelineStepDetail.Results.Remove(item);
                                    }
                                    foreach (var item in pipelineStepDetail.Results)
                                    {
                                        checklist_vm.Pipelines.PipelineSteps.Add(item);
                                    }
                                }
                                //var listNomal = pipelineStepDetail.Results.Where(s => s.PipelineDate != null && s.PositionId == MasterDataId.Normal).ToList().OrderBy(s => s.PipelineDate);
                                //if (listNomal.Count() > 1)
                                //{
                                //    //foreach (var item in listNomal)
                                //    //{
                                //    //    pipelineStepDetail.Results.Remove(item);                               
                                //    //}
                                //    //checklist_vm.Pipelines.PipelineSteps = listNomal.ToList();
                                //    //foreach (var item in pipelineStepDetail.Results)
                                //    //{
                                //    //    if (item.PipelineStaffStatusId<currentStep && item.PipelineDate==null && item.PositionId==MasterDataId.Normal)
                                //    //    {
                                //    //        checklist_vm.Pipelines.PipelineSteps.Add(item);
                                //    //    }
                                //    //    else if(item.PipelineStaffStatusId == currentStep)
                                //    //    {
                                //    //        break;
                                //    //    }                                       
                                //    //}
                                //    //foreach (var item in listNomal)
                                //    //{
                                //    //    checklist_vm.Pipelines.PipelineSteps.Add(item);
                                //    //}
                                //    //foreach (var item in pipelineStepDetail.Results)
                                //    //{
                                //    //    if (item.Id > currentStep)
                                //    //    {
                                //    //        checklist_vm.Pipelines.PipelineSteps.Add(item);
                                //    //    }
                                //    //}

                                //}
                                //else if (listNomal.Count() == 1)
                                //{
                                //    checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;

                                //}
                                //else
                                //{
                                //    checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;
                                //}
                            }
                        }
                        var response = _checklistDetailService.GetChecklistDetailByStaffId(staffId ?? 0);
                        var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
                        if (!CheckPermission(resultDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultDetail.Results.Count > 0)
                            {
                                checklist_vm.ChecklistDetail = resultDetail.Results;
                            }

                        }

                        staffDetail_vm.Checklist = checklist_vm;
                        checklist_vm.IsSave = true;
                    }
                    break;
                #endregion
                #region 1: Tab thông tin cá nhân
                case 1:
                    {
                        var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffRelationships);
                        var resultRelationshipsDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
                        if (!CheckPermission(resultRelationshipsDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableConfig = new TableViewModel();
                            if (resultRelationshipsDetail.Results.Count > 0)
                            {
                                dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(resultRelationshipsDetail.Results.FirstOrDefault().ConfigData);
                            }
                            dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                            dataTableConfig.TableName = TableName.TableRelationShips;
                            dataTableConfig.TableConfigName = TableConfig.StaffRelationships;
                            var param1 = new BasicParamModel()
                            {
                                FilterField = string.Empty,
                                PageNumber = 1,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };

                            //var currentWorkingProcess = this._workingProcessService.GetCurrentWorkingManagerByStaff(paramEntity, staffId ?? 0);
                            //var resultWorkingManagerDetail = JsonConvert.DeserializeObject<HrmResultModel<WorkingProcessModel>>(currentWorkingProcess);
                            //if (!CheckPermission(resultWorkingManagerDetail))
                            //{
                            //    //return to Access Denied
                            //}
                            //else
                            //{
                            //    if (resultWorkingManagerDetail.Results.Count > 0)
                            //    {
                            //        staffDetail_vm.WorkingProcess = resultWorkingManagerDetail.Results.FirstOrDefault();
                            //    }

                            //}
                            staffDetail_vm.ListStaffRelationShips = RenderTable(dataTableConfig, param1, TableName.TableRelationShips);
                        }
                        var resultTableStaffWorkingDayMachineConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffWorkingDayMachine);
                        var resultStaffWorkingDayMachineDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableStaffWorkingDayMachineConfig);
                        if (!CheckPermission(resultStaffWorkingDayMachineDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableConfig = new TableViewModel();
                            if (resultStaffWorkingDayMachineDetail.Results.Count > 0)
                            {
                                dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(resultStaffWorkingDayMachineDetail.Results.FirstOrDefault().ConfigData);
                            }
                            dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                            dataTableConfig.TableName = TableName.StaffWorkingDayMachine;
                            dataTableConfig.TableConfigName = TableConfig.StaffWorkingDayMachine;
                            var param1 = new BasicParamModel()
                            {
                                FilterField = string.Empty,
                                PageNumber = 1,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListStaffWorkingDayMachines = RenderTable(dataTableConfig, param1, TableName.StaffWorkingDayMachine);
                        }
                    }
                    break;
                #endregion
                #region 2: Tab quá trình công tác
                case 2:
                    {
                        viewType = 1;
                        if (actionTab == 0)//&& viewType != 1)
                        {
                            paramEntity.PageSize = Int32.MaxValue;
                            var wpParam = paramEntity;
                            wpParam.OrderBy = " w.StartDate desc ";
                            var listWorkingProcess = this._workingProcessService.GetWorkingProcessByStaff(wpParam, staffId ?? 0, out totalRecord);
                            var resultWorkingProcessDetail = JsonConvert.DeserializeObject<HrmResultModel<WorkingProcessModel>>(listWorkingProcess);
                            if (!CheckPermission(resultWorkingProcessDetail))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultWorkingProcessDetail.Results.Count > 0)
                                {
                                    staffDetail_vm.ListWorkingProcess = resultWorkingProcessDetail.Results;
                                }
                            }

                            var currentWorkingProcess = this._workingProcessService.GetCurrentWorkingProcessByStaff(paramEntity, staffId ?? 0);
                            var resultCurrentWorkingProcessDetail = JsonConvert.DeserializeObject<HrmResultModel<WorkingProcessModel>>(currentWorkingProcess);
                            if (!CheckPermission(resultCurrentWorkingProcessDetail))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultCurrentWorkingProcessDetail.Results.Count > 0)
                                {
                                    staffDetail_vm.WorkingProcess = resultCurrentWorkingProcessDetail.Results.FirstOrDefault();
                                }
                            }

                            //lấy danh sách phòng ban công ty
                            var organization = this._organizationService.GetParentOrganization(paramEntity, dataDetail.OrganizationId);
                            var resultOrganizationDetail = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(organization);
                            if (!CheckPermission(resultOrganizationDetail))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultOrganizationDetail.Results.Count > 0)
                                {
                                    staffDetail_vm.ListOrganization = resultOrganizationDetail.Results;
                                }
                            }
                        }
                        else
                        {
                            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingProcess);
                            var responseTableConfig = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);

                            if (!CheckPermission(responseTableConfig))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                var dataTableSalaryConfig = JsonConvert.DeserializeObject<TableViewModel>(responseTableConfig.Results.FirstOrDefault().ConfigData);
                                //dataTableSalaryConfig.ShowFooter = false;
                                //dataTableSalaryConfig.TableName = TableName.StaffSalary;
                                dataTableSalaryConfig.TableName = TableName.TableWorkingProcess;
                                dataTableSalaryConfig.TableConfigName = TableConfig.WorkingProcess;
                                var param1 = new BasicParamModel()
                                {
                                    FilterField = string.Empty,
                                    PageNumber = 1,
                                    PageSize = dataTableSalaryConfig.ItemsPerPage,
                                    LanguageId = _languageId,
                                    RoleId = _roleId,
                                    UserId = _userId,
                                    DbName = CurrentUser.DbName,
                                    ReferenceId = staffId ?? 0
                                };
                                staffDetail_vm.ListWorkingProcessTable = RenderTable(dataTableSalaryConfig, param1, TableName.TableWorkingProcess);
                            }
                        }
                    }
                    break;
                #endregion
                #region 3: Tab lương - phúc lợi
                case 3: //tab lương - phúc lợi
                    {
                        viewType = 1;
                        //Salary
                        var resultTableSalaryConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffSalary);
                        var resultConfigSalaryDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableSalaryConfig);
                        if (!CheckPermission(resultConfigSalaryDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableSalaryConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigSalaryDetail.Results.FirstOrDefault().ConfigData);
                            dataTableSalaryConfig.ShowFooter = false;
                            dataTableSalaryConfig.TableName = TableName.StaffSalary;
                            dataTableSalaryConfig.TableConfigName = TableConfig.StaffSalary;

                            var param1 = new BasicParamModel()
                            {
                                FilterField = string.Empty,
                                PageNumber = 1,
                                PageSize = dataTableSalaryConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListSalary = RenderTable(dataTableSalaryConfig, param1, TableName.StaffSalary);
                        }

                        //StaffAllowance
                        var resultTableAllowanceConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffAllowance);
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
                            dataTableAllowanceConfig.TableConfigName = TableConfig.StaffAllowance;
                            var param2 = new BasicParamModel()
                            {
                                FilterField = " b.WorkingProcessId = " + dataDetail.CurrentWorkingProcessId.ToString(),
                                PageNumber = 1,
                                PageSize = dataTableAllowanceConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListAllowance = RenderTable(dataTableAllowanceConfig, param2, TableName.StaffAllowance);
                        }


                        // StaffBenefit
                        var resultTableBenefitConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffBenefit);
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
                            dataTableBenefitConfig.TableConfigName = TableConfig.StaffBenefit;
                            var param3 = new BasicParamModel()
                            {
                                FilterField = " b.WorkingProcessId = " + dataDetail.CurrentWorkingProcessId.ToString(),
                                PageNumber = 1,
                                PageSize = dataTableBenefitConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListBenefit = RenderTable(dataTableBenefitConfig, param3, TableName.StaffBenefit);
                        }

                    }
                    break;
                #endregion
                #region 4: Tab khen thưởng - kỷ luật
                case 4: // tab khen thưởng - kỷ luật
                    {
                        viewType = 1;
                        //Reward
                        var resultTableConfigReward = this._tableConfigService.GetTableConfigByTableName(TableConfig.Reward);
                        var resultConfigRewardDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfigReward);
                        if (!CheckPermission(resultConfigRewardDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableConfigReward = JsonConvert.DeserializeObject<TableViewModel>(resultConfigRewardDetail.Results.FirstOrDefault().ConfigData);
                            dataTableConfigReward.TableDataUrl = TableUrl.WorkingProcesssUrl;
                            dataTableConfigReward.TableName = TableName.Reward;
                            var param1 = new BasicParamModel()
                            {
                                FilterField = string.Empty,
                                PageNumber = 1,
                                PageSize = dataTableConfigReward.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListReward = RenderTable(dataTableConfigReward, param1, TableName.Reward);
                        }

                        //Discipline
                        var resultTableConfigDiscipline = this._tableConfigService.GetTableConfigByTableName(TableConfig.Reward);
                        var resultConfigDisciplineDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfigDiscipline);
                        if (!CheckPermission(resultConfigDisciplineDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            var dataTableConfigDiscipline = JsonConvert.DeserializeObject<TableViewModel>(resultConfigDisciplineDetail.Results.FirstOrDefault().ConfigData);
                            dataTableConfigDiscipline.TableDataUrl = TableUrl.StaffGetDataUrl;
                            dataTableConfigDiscipline.TableName = TableName.Discipline;
                            var param2 = new BasicParamModel()
                            {
                                FilterField = string.Empty,
                                PageNumber = 1,
                                PageSize = dataTableConfigDiscipline.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                ReferenceId = staffId ?? 0
                            };
                            staffDetail_vm.ListDiscipline = RenderTable(dataTableConfigDiscipline, param2, TableName.Discipline);
                        }

                    }
                    break;
                #endregion
                #region 5: Tab bảo hiểm
                case 5: // tab bảo hiểm
                    {
                        viewType = 1;
                        //lay data cho dropdown
                        if (viewType == 1)
                        {
                            var listGroup = new List<LongTypeModel>();
                            listGroup.Add(new LongTypeModel()
                            {
                                Value = MasterDataId.InsuranceType
                            });
                            listGroup.Add(new LongTypeModel()
                            {
                                Value = MasterDataId.Currency
                            });
                            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                            var model = new HealthInsuranceViewModel();
                            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
                            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                            if (!CheckPermission(response))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                staffDetail_vm.DataDropdownInsuranceType = response.Results.Where(m => m.GroupId == MasterDataId.InsuranceType).ToList();
                                staffDetail_vm.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                            }
                        }
                        //Bảo hiểm xã hội
                        var resultSocialInsurance = this._staffService.GetStaffSocialInsurance(paramEntity, staffId ?? 0);
                        var resultSocialInsuranceDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffSocialInsuranceModel>>(resultSocialInsurance);
                        if (!CheckPermission(resultSocialInsuranceDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultSocialInsuranceDetail.Results.Count > 0)
                            {
                                staffDetail_vm.SocialInsurance = resultSocialInsuranceDetail.Results.FirstOrDefault();
                            }
                        }
                        //Bảo hiểm y tế
                        var resultHealthInsurance = this._staffService.GetStaffHealthInsurance(paramEntity, staffId ?? 0);
                        var resultHealthInsuranceDetail = JsonConvert.DeserializeObject<HrmResultModel<HealthInsuranceModel>>(resultHealthInsurance);
                        if (!CheckPermission(resultHealthInsuranceDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultHealthInsuranceDetail.Results.Count > 0)
                            {
                                staffDetail_vm.HealthInsurance = resultHealthInsuranceDetail.Results.FirstOrDefault();
                            }
                        }

                    }
                    break;
                #endregion
                #region 6: Tab kinh nghiệm - chứng nhận
                case 6: // tab kinh nghiệm - chứng nhận
                    {
                        viewType = 1;
                        var resultExperience = this._staffService.GetStaffExperience(paramEntity, staffId ?? 0);
                        var resultExperienceDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffExperienceModel>>(resultExperience);
                        if (!CheckPermission(resultExperienceDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultExperienceDetail.Results.Count > 0)
                            {
                                staffDetail_vm.ListExperience = resultExperienceDetail.Results;
                            }
                        }

                        var resultCertificate = this._staffService.GetStaffCertificate(paramEntity, staffId ?? 0);
                        var resultCertificateDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffCertificateModel>>(resultCertificate);
                        if (!CheckPermission(resultCertificateDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultCertificateDetail.Results.Count > 0)
                            {
                                staffDetail_vm.ListCertificate = resultCertificateDetail.Results;
                            }
                        }

                    }
                    break;
                    #endregion
            }
            if (viewType == 1 && (activeTab == 1 || activeTab == 5))
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

                #endregion
                var model = new StaffDetailsViewModel()
                {
                    Staff = new StaffModel(),
                    WorkingProcess = new WorkingProcessModel(),
                    Contract = new StaffContractModel(),
                    ListRole = new List<StaffRoleModel>()
                {
                    new StaffRoleModel()
                }
                };
                var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);

                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
                var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(response))
                {
                    //return to Access Denied
                }
                else
                {
                    staffDetail_vm.DataDropdownWorkingProcessType = response.Results.Where(m => m.GroupId == MasterDataId.WorkingProcessType).ToList();
                    staffDetail_vm.DataDropdownClassification = response.Results.Where(m => m.GroupId == MasterDataId.Classification).ToList();
                    staffDetail_vm.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                    //staffDetail_vm.DataDropdownOffice = response.Results.Where(m => m.GroupId == MasterMasterDataId.Office).ToList();
                    staffDetail_vm.DataDropdownPaymentForm = response.Results.Where(m => m.GroupId == MasterDataId.PaymentForm).ToList();
                    staffDetail_vm.DataDropdownPaymentMethod = response.Results.Where(m => m.GroupId == MasterDataId.PaymentMethod).ToList();
                    staffDetail_vm.DataDropdownPolicy = response.Results.Where(m => m.GroupId == MasterDataId.Policy).ToList();
                    staffDetail_vm.DataDropdownPosition = response.Results.Where(m => m.GroupId == MasterDataId.Position).ToList();
                    staffDetail_vm.DataDropdownStaffLevel = response.Results.Where(m => m.GroupId == MasterDataId.StaffLevel).ToList();
                    staffDetail_vm.DataDropdownStatus = response.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
                    staffDetail_vm.DataDropdownStatusAprove = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                    staffDetail_vm.DataDropdownContractTime = response.Results.Where(m => m.GroupId == MasterDataId.ContractTime).ToList();
                    staffDetail_vm.DataDropdownContractType = response.Results.Where(m => m.GroupId == MasterDataId.ContractType).ToList();
                    staffDetail_vm.DataDropdownWorkingStatus = response.Results.Where(m => m.GroupId == MasterDataId.WorkingStatus).ToList();
                    staffDetail_vm.DataDropdownMaritalStatus = response.Results.Where(m => m.GroupId == MasterDataId.MaritalStatus).ToList();
                    staffDetail_vm.DataDropdownNationality = response.Results.Where(m => m.GroupId == MasterDataId.Nationality).ToList();
                    staffDetail_vm.DataDropdownProvince = response.Results.Where(m => m.GroupId == MasterDataId.Province).ToList();
                    staffDetail_vm.DataDropdownEthnicity = response.Results.Where(m => m.GroupId == MasterDataId.Ethnicity).ToList();
                    staffDetail_vm.DataDropdownTimekeepingForm = response.Results.Where(m => m.GroupId == MasterDataId.TimekeepingForm).ToList();
                }
                var resultOrganization = this._organizationService.GetAllOrganizationForDropDown(paramEntity);
                staffDetail_vm.DataDropdownOrganization = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultOrganization).Results;
                var resultStaff = this._staffService.GetAllStaffForDropDown(paramEntity);
                staffDetail_vm.DataDropdownStaff = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultStaff).Results;
                var responeRole = _roleService.GetAllRole();
                staffDetail_vm.DataDropdownRole = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(responeRole).Results;
                var shiftTotal = 0;
                paramEntity.PageSize = Int32.MaxValue;
                var resultShift = this._workingdayService.GetWorkingdayShift(paramEntity, out shiftTotal);
                model.DataDropdownShift = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultShift).Results;

            }
            staffDetail_vm.Staff = dataDetail;
            staffDetail_vm.ListRole = listRole;
            return staffDetail_vm;
        }
        public ActionResult _DetailByTab(long? staffId, int? activeTab, int? action, int? viewType)
        {
            StaffDetailsViewModel StaffDetail_VM = ActiveTabStaffDetail(staffId, activeTab, action, viewType);
            return PartialView(UrlHelpers.View("~/Views/Staff/_DetailByTab.cshtml"), StaffDetail_VM);
        }
        public ActionResult _SalaryDetail(long id)
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
            var model = new WorkingProcessModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._workingProcessService.GetSalaryDetailById(paramEntity, id);
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
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SalaryDetail.cshtml"), model);
        }

        public ActionResult _BonusDetail(long id)
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
            var model = new StaffBonusDisciplineModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffBonusDisciplineService.GetStaffBonusDisciplineById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffBonusDisciplineModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_BonusDetail.cshtml"), model);
        }

        public ActionResult _AddStaff(bool isOnboarding)
        {

            #region
            var listGroup = new List<StringTypeModel>();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            listGroup.Add(new StringTypeModel()
            {
                OrderNo = 1,
                Value = MasterGroup.Classification
            });

            #endregion
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var stringTypeEntity = MapperHelper.MapList<StringTypeModel, StringType>(listGroup);
            var model = new StaffModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByGroupName(stringTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownClassification = response.Results.Where(m => m.GroupName == MasterGroup.Classification).ToList();

            }
            var resultOrganization = this._organizationService.GetAllOrganizationForDropDown(paramEntity);
            model.Organizations = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultOrganization).Results;
            model.IsOnboarding = isOnboarding;
            return PartialView("~/Views/Staff/_AddStaff.cshtml", model);
        }

        public ActionResult ReloadStaffChecklist(long staffId)
        {
            ChecklistViewModel checklist_vm = new ChecklistViewModel();
            checklist_vm.ChecklistDetail = new List<ChecklistDetailModel>();
            checklist_vm.StaffId = staffId;
            var response = _checklistDetailService.GetChecklistDetailByStaffId(staffId);
            if (response != null)
            {
                var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
                if (!CheckPermission(resultDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDetail.Results.Count > 0)
                    {
                        checklist_vm.ChecklistDetail = resultDetail.Results;
                    }
                }
            }
            //lấy staff detail
            var dataDetail = new StaffModel();
            var detail = this._staffService.GetStaffInformationById(staffId);
            var resultStaffInforDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(detail);
            if (!CheckPermission(resultStaffInforDetail))
            {
                //return to Access Denied
            }
            else
            {
                dataDetail = resultStaffInforDetail.Results.FirstOrDefault();
            }
            checklist_vm.Pipelines = new PipelineGridModel()
            {
                CurrentStep = dataDetail.PipelineStepId,
                PipelineSteps = new List<PipelineStepModel>()
            };
            var pipelineResponse = this._pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding, staffId);
            var pipelineStepDetail = JsonConvert.DeserializeObject<HrmResultModel<PipelineStepModel>>(pipelineResponse);
            if (!CheckPermission(pipelineStepDetail))
            {
                //return to Access Denied
            }
            else
            {
                checklist_vm.Pipelines.PipelineSteps = pipelineStepDetail.Results;
            }
            return PartialView("~/Views/Shared/Checklist/_ChecklistSummary.cshtml", checklist_vm);
        }

        public ActionResult EditChecklistModal()
        {
            ChecklistDetailModel checklist_VM = new ChecklistDetailModel();
            var responseTableColumn = _tableColumnService.GetTableColumn(TableConfig.Staff, true);
            if (responseTableColumn != null)
            {
                var ressultTableColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnEntity>>(responseTableColumn);
                if (!CheckPermission(ressultTableColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    checklist_VM.ListTabelColumnId = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(ressultTableColumn.Results));
                }
            }
            return PartialView("~/Views/Shared/Checklist/_EditChecklist.cshtml", checklist_VM);
        }
        public ActionResult SendStaffCheckList(long staffId, long checklistId)
        {
            var checklistResult = this._checklistDetailService.SendStaffChecklist(staffId, checklistId);
            var checklistDetail = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(checklistResult);
            if (checklistDetail.Results.FirstOrDefault().Id != 0)
            {
                return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsSuccess = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Benefit
        public ActionResult _BenefitDetail(long id)
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
            var model = new StaffBenefitsModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffService.GetStaffBenefitById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffBenefitsModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_BenefitDetail.cshtml"), model);
        }
        public ActionResult _HistoryBenefit(long? staffId)
        {
            var tableConfigName = TableConfig.StaffBenefitHistory;
            var tableName = TableName.StaffBenefit;
            var benefit = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableName = tableName;
                dataTableConfig.TableConfigName = tableConfigName;
                var param1 = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = dataTableConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = staffId ?? 0
                };
                //Reward
                benefit = RenderTable(dataTableConfig, param1, tableName);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_HistoryBenefit.cshtml"), benefit);
        }
        #endregion
        #region Reward Discipline
        public ActionResult GetRewardDisciplineGrid(long? staffId, long? type)
        {
            var tableConfigName = type == MasterDataId.Reward ? TableConfig.Reward : TableConfig.Discipline;
            var tableName = type == MasterDataId.Reward ? TableName.Reward : TableName.Discipline;
            var staffRewardDiscipline = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableName = tableName;
                dataTableConfig.TableConfigName = tableConfigName;
                var param1 = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = dataTableConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = staffId ?? 0
                };
                //Reward
                staffRewardDiscipline = RenderTable(dataTableConfig, param1, tableName);
            }
            return PartialView(UrlHelpers.View("~/Views/Shared/Template/_Table.cshtml"), staffRewardDiscipline);
        }
        public ActionResult GetRewardDisciplineCard(long? staffId, long? type)
        {
            List<StaffBonusDisciplineModel> listBonusDiscipline = new List<StaffBonusDisciplineModel>();
            var BonusDisciplineViewModel = new StaffBonusDisciplineViewModel();
            int totalRecord = 1;
            var param1 = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.Parse(Config.GetConfig(Constant.ItemRewarDisciplineDefault)),
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName,
                ReferenceId = staffId ?? 0
            };
            //Reward
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param1);
            var result = this._staffBonusDisciplineService.GetBonusDisciplineByStaff(paramEntity, staffId ?? 0, type ?? 0, out totalRecord);
            var disciplineByStaffResult = JsonConvert.DeserializeObject<HrmResultModel<StaffBonusDisciplineModel>>(result);
            if (!CheckPermission(disciplineByStaffResult))
            {
                //return to Access Denied
            }
            else
            {
                listBonusDiscipline = disciplineByStaffResult.Results;
            }
            BonusDisciplineViewModel.ListStaffBonusDiscipline = listBonusDiscipline;
            return PartialView(UrlHelpers.View("~/Views/Shared/Template/_SectionReward_Discipline.cshtml"), BonusDisciplineViewModel);
        }

        public ActionResult _RewardDetail(long id, int? viewType)
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
            var model = new StaffBonusDisciplineViewModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffBonusDisciplineService.GetStaffBonusDisciplineById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffBonusDisciplineModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.StaffBonusDiscipline = result.Results.FirstOrDefault();
                        var responseAttackment = _attachmentService.GetAttackmenByRecordId(model.StaffBonusDiscipline.Id, DataType.RewarFile);
                        if (responseAttackment != null)
                        {
                            var resultAttackment = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseAttackment);
                            if (!CheckPermission(resultAttackment))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultAttackment.Results.Count > 0)
                                {
                                    model.Attachments = resultAttackment.Results;
                                }
                            }
                        }
                    }
                }
            }
            if (viewType == 1)
            {
                var listGroup = new List<LongTypeModel>();
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.RewardForm
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.StatusAprove
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Currency
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.RewardType
                });
                var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
                var responseMas = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(responseMas))
                {
                    //return to Access Denied
                }
                else
                {
                    model.DataDropdownRewardForm = responseMas.Results.Where(m => m.GroupId == MasterDataId.RewardForm).ToList();
                    model.DataDropdownRewardType = responseMas.Results.Where(m => m.GroupId == MasterDataId.RewardType).ToList();
                    model.DataDropdownCurrency = responseMas.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                    model.DataDropdownStatusAprove = responseMas.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                }
                return PartialView(UrlHelpers.View("~/Views/Staff/_SaveReward.cshtml"), model);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_RewardDetail.cshtml"), model);
        }
        public ActionResult _SaveReward()
        {
            //var model = new Reward
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
                Value = MasterDataId.RewardType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.RewardForm
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new StaffBonusDisciplineViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownRewardType = response.Results.Where(m => m.GroupId == MasterDataId.RewardType).ToList();
                model.DataDropdownRewardForm = response.Results.Where(m => m.GroupId == MasterDataId.RewardForm).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                model.DataDropdownStatusAprove = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveReward.cshtml"), model);
        }
        public ActionResult _DisciplineDetail(long id, int? viewType)
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
            var model = new StaffBonusDisciplineViewModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffBonusDisciplineService.GetStaffBonusDisciplineById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffBonusDisciplineModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.StaffBonusDiscipline = result.Results.FirstOrDefault();
                        var responseAttackment = _attachmentService.GetAttackmenByRecordId(model.StaffBonusDiscipline.Id, DataType.RewarFile);
                        if (responseAttackment != null)
                        {
                            var resultAttackment = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseAttackment);
                            if (!CheckPermission(resultAttackment))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultAttackment.Results.Count > 0)
                                {
                                    model.Attachments = resultAttackment.Results;
                                }
                            }
                        }
                    }
                }
            }
            if (viewType == 1)
            {
                var listGroup = new List<LongTypeModel>();
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.DisciplineType
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.StatusAprove
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Currency
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.DisciplineForm
                });
                var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
                var responseMas = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(responseMas))
                {
                    //return to Access Denied
                }
                else
                {
                    model.DataDropdownDisciplineForm = responseMas.Results.Where(m => m.GroupId == MasterDataId.DisciplineForm).ToList();
                    model.DataDropdownDisciplineType = responseMas.Results.Where(m => m.GroupId == MasterDataId.DisciplineType).ToList();
                    model.DataDropdownCurrency = responseMas.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                    model.DataDropdownStatusAprove = responseMas.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                }
                return PartialView(UrlHelpers.View("~/Views/Staff/_SaveDiscipline.cshtml"), model);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_DisciplineDetail.cshtml"), model);
        }
        public ActionResult _SaveDiscipline()
        {
            //var model = new Reward
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
                Value = MasterDataId.DisciplineType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.StatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.DisciplineForm
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new StaffBonusDisciplineViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownDisciplineType = response.Results.Where(m => m.GroupId == MasterDataId.DisciplineType).ToList();
                model.DataDropdownDisciplineForm = response.Results.Where(m => m.GroupId == MasterDataId.DisciplineForm).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                model.DataDropdownStatusAprove = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveDiscipline.cshtml"), model);
        }
        public ActionResult SaveStaffBonusDiscipline()
        {
            var model = new StaffBonusDisciplineModel();
            var validations = new List<ValidationModel>();
            if (Request.Form != null)
            {
                var formData = HttpUtility.UrlDecode(Request.Form.ToString()).Replace("Data=", "");
                //model = JsonConvert.DeserializeObject<StaffBonusDisciplineModel>(formData);
                model = JsonConvert.DeserializeObject<StaffBonusDisciplineModel>(formData);
                validations = new List<ValidationModel>();
                validations.AddRange(ValidationHelper.Validation(model, ""));
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }

                var result = false;
                var msg = "";
                var staffBonusDiscipline = MapperHelper.Map<StaffBonusDisciplineModel, StaffBonusDisciplineEntity>(model);
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

                var response = this._staffService.SaveBonusDiscipline(staffBonusDiscipline, paramEntity);
                var dataResult = JsonConvert.DeserializeObject<HrmResultModel<StaffBonusDisciplineModel>>(response);
                if (!CheckPermission(dataResult))
                {
                    //return to Access Denied
                }
                else
                {
                    if (dataResult.Results.Count > 0)
                    {
                        if (dataResult.Results.FirstOrDefault().Id > 0)
                        {
                            msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                            result = true;
                        }
                        else
                        {
                            msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                            result = false;
                        }
                        if (model.IsDeletedFile && model.FileId > 0)
                        {
                            RemoveFile(model.FileName, model.FileId);
                        }
                        if (Request.Files.Count > 0)
                        {
                            HttpPostedFileBase file = Request.Files[0];
                            if (file != null && file.ContentLength > 0)
                            {
                                List<HttpPostedFileBase> listFile = new List<HttpPostedFileBase>();
                                listFile.Add(file);
                                SaveFile(listFile, DataType.RewarFile, dataResult.Results.FirstOrDefault().Id);
                            };
                        }
                    }
                }
                return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
            }

            validations.AddRange(ValidationHelper.Validation(model, ""));
            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Relationship
        public ActionResult _RelationShipView(long Id)
        {
            return PartialView(UrlHelpers.View("~/Views/Staff/_RelationShipView.cshtml"));

        }
        #endregion
        #region Staff Org Chart
        public ActionResult OrgChart()
        {
            var id = CurrentUser.UserId;
            var date = DateTime.Now;
            var orgChart_vm = new OrgChartViewModel();
            orgChart_vm.StaffsByLevel = GetOrgChartData(id, date);
            return View(UrlHelpers.View("~/Views/Staff/OrgChart.cshtml"), orgChart_vm);

        }
        public ActionResult OrgChartView(long id, string date)
        {
            var orgChart_vm = new OrgChartViewModel();
            var realDate = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            orgChart_vm.StaffsByLevel = GetOrgChartData(id, realDate);
            return View(UrlHelpers.View("~/Views/Staff/_OrgChartView.cshtml"), orgChart_vm);

        }
        private List<StaffByLevel> GetOrgChartData(long id, DateTime date)
        {
            var listStaff = new List<StaffByLevel>();
            var result = new List<StaffModel>();
            var response = _staffService.GetStaffParentById(id, date);
            var staffParentDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);
            if (!CheckPermission(staffParentDetail))
            {
                //return to Access Denied
            }
            else
            {
                result = staffParentDetail.Results;
            }

            if (response != null)
            {
                //re level
                if (result.Count > 0)
                {
                    //selected staff
                    listStaff.Add(new StaffByLevel()
                    {
                        Staff = result.FirstOrDefault(x => x.Id == id),
                        Level = 0
                    });
                    var selectedStaff = listStaff.FirstOrDefault(x => x.Level == 0).Staff;
                    if (selectedStaff != null)
                    {
                        //staff's father
                        var father = result.FirstOrDefault(x => x.Id == selectedStaff.ParentId);
                        if (father != null)
                        {
                            listStaff.Add(new StaffByLevel()
                            {
                                Staff = father,
                                Level = -1
                            });
                            //staff's grandfather

                            var grandFather = result.FirstOrDefault(x => x.Id == father.ParentId);
                            if (grandFather != null)
                            {
                                listStaff.Add(new StaffByLevel()
                                {
                                    Staff = grandFather,
                                    Level = -2
                                });
                            }
                        }
                        //childs
                        var childs = result.Where(x => x.ParentId == selectedStaff.Id);
                        if (childs != null && childs.Count() > 0)
                        {
                            foreach (var child in childs)
                            {
                                listStaff.Add(new StaffByLevel()
                                {
                                    Staff = child,
                                    Level = 1
                                });
                            }
                        }
                    }
                }
            }
            return listStaff;
        }
        #endregion
        #region Staff Onboarding
        [HttpGet]
        public ActionResult Onboarding()
        {
            var type = Common.ViewType.Card;
            var query = Request.Url.Query;
            if (!string.IsNullOrEmpty(query))
            {
                var viewType = HttpUtility.ParseQueryString(query).Get("viewtype");
                if (viewType != null && viewType.ToLower() == "list")
                {
                    type = Common.ViewType.List;
                }
            }
            var onboarding_vm = new OnboardingViewModel();
            onboarding_vm.Onboardings = new List<OnboardingModel>();
            onboarding_vm.ViewType = type;
            List<OnboardingModel> listOnboarding = new List<OnboardingModel>();
            var pipelineSteps = new List<PipelineStepModel>();
            var staffs = new List<StaffModel>();
            var responseTableConfig = new List<TableConfigModel>();
            var pipelineResponse = this._pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding);
            var pipelineStepDetail = JsonConvert.DeserializeObject<HrmResultModel<PipelineStepModel>>(pipelineResponse);
            if (!CheckPermission(pipelineStepDetail))
            {
                //return to Access Denied
            }
            else
            {
                pipelineSteps = pipelineStepDetail.Results;
            }
            if (!string.IsNullOrEmpty(pipelineResponse))
            {
                var param = new BasicParamType()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = int.MaxValue,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName
                };
                int totalStaff = 0;
                var staffCardResponse = _staffService.GetPipelineStepStaffByMenuName(param, MenuName.Onboarding, out totalStaff);
                var staffResult = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(staffCardResponse);
                if (!CheckPermission(staffResult))
                {
                    //return to Access Denied
                }
                else
                {
                    staffs = staffResult.Results;
                }

                var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Onboarding);
                var configOnBoarding = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
                if (!CheckPermission(configOnBoarding))
                {
                    //return to Access Denied
                }
                else
                {
                    responseTableConfig = configOnBoarding.Results;
                }
                var dataTableConfig = new TableViewModel();
                if (responseTableConfig.Count > 0)
                {
                    var configData = responseTableConfig[0].ConfigData;
                    if (configData != null)
                    {
                        dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(configData);
                    }
                }
                param.PageSize = dataTableConfig.ItemsPerPage;
                var staffResponse = _staffService.GetPipelineStepStaffByMenuName(param, MenuName.Onboarding, out totalStaff);
                var pipelineStepResult = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(staffResponse);
                if (!CheckPermission(pipelineStepResult))
                {
                    //return to Access Denied
                }
                else
                {
                    var tableData = pipelineStepResult.Results;
                    dataTableConfig.TableData = tableData;
                    dataTableConfig.TotalRecord = totalStaff;
                }


                var resultMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, _languageId);
                var selectListResult = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultMasterDataSelectList);
                if (!CheckPermission(selectListResult))
                {
                    //return to Access Denied
                }
                else
                {
                    var dataSelectList = selectListResult.Results;
                    var dataDropDownList = MapperHelper.MapList<MasterDataModel, DropdownListContentModel>(dataSelectList);


                    foreach (var item in dataDropDownList)
                    {
                        if (Convert.ToInt32(item.Value) == dataTableConfig.ItemsPerPage)
                        {
                            item.IsSelected = true;
                            break;
                        }
                    }
                    List<dynamic> dataDropDownListDynamic = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(dataDropDownList));
                    dataTableConfig.lstItemsPerPage = dataDropDownListDynamic;
                }
                onboarding_vm.Table = dataTableConfig;
                var responseColumn = this._tableColumnService.GetTableColumn(TableConfig.Onboarding);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        onboarding_vm.Table.ListTableColumns = resultColumn.Results;
                    }
                }
            }
            foreach (var pipelineStep in pipelineSteps)
            {
                onboarding_vm.Onboardings.Add(new OnboardingModel()
                {
                    Pipeline = pipelineStep,
                    Staffs = staffs.Where(x => x.PipelineStepId == pipelineStep.Id).ToList()
                });
            }

            return View(onboarding_vm);
        }
        public ActionResult CheckListModal(long id, string staffName, long officePositionId)
        {
            List<dynamic> dataSourceChecklist = new List<dynamic>();
            var resultChecklist = _checklistService.GetChecklist();
            var checklistResult = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultChecklist);
            if (!CheckPermission(checklistResult))
            {
                //return to Access Denied
            }
            else
            {
                dataSourceChecklist = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(checklistResult.Results));
            }
            StaffModel staff_vm = new StaffModel { Id = id, Name = staffName, OfficePositionId = officePositionId, ListChecklist = dataSourceChecklist };
            return PartialView("~/Views/Staff/_CheckListModal.cshtml", staff_vm);
        }
        public ActionResult AssignedChecklist(long id)
        {
            ChecklistViewModel checklist_vm = new ChecklistViewModel();
            var response = _checklistDetailService.GetChecklistDetailByStaffId(id);
            var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<ChecklistDetailModel>>(response);
            if (!CheckPermission(resultDetail))
            {
                //return to Access Denied
            }
            else
            {
                if (resultDetail.Results.Count > 0)
                {
                    checklist_vm.ChecklistDetail = resultDetail.Results;
                }

            }
            checklist_vm.StaffId = id;
            checklist_vm.ControlAction = false;
            checklist_vm.IsSave = true;
            return PartialView("~/Views/Staff/_AssignedChecklist.cshtml", checklist_vm);
        }
        #endregion
        #region Staff Checklist
        public ActionResult StaffChecklist()
        {
            return null;
        }
        #endregion
        #region RenderTable

        public TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.UserId = _roleId;
            param.RoleId = _userId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 

            var response = GetData(type, param, out totalRecord);
            var resultData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(response);
            if (!CheckPermission(resultData))
            {
                //return to Access Denied
            }
            else
            {
                tableData.TableData = resultData.Results;
            }
            tableData.CurrentPage = param.PageNumber;
            tableData.ItemsPerPage = param.PageSize;
            tableData.TotalRecord = totalRecord;
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
                tableData.lstItemsPerPage = dataDropDownListDynamic;
            }

            return tableData;
        }

        private string GetData(string type, BasicParamModel param, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TableStaff)
            {
                return this._staffService.GetStaff(paramEntity, out totalRecord);
            }
            else if (type == TableName.TableRelationShips)
            {

                return this._staffRelationShipsService.GetRelationShipsByStaff(paramEntity, param.ReferenceId, out totalRecord);
            }
            else if (type == TableName.TableWorkingProcess)
            {
                return this._workingProcessService.GetWorkingProcessByStaff(paramEntity, param.ReferenceId, out totalRecord);
            }
            else if (type == TableName.Reward)
            {
                return this._staffBonusDisciplineService.GetBonusDisciplineByStaff(paramEntity, param.ReferenceId, MasterDataId.Reward, out totalRecord);
            }
            else if (type == TableName.Discipline)
            {
                return this._staffBonusDisciplineService.GetBonusDisciplineByStaff(paramEntity, param.ReferenceId, MasterDataId.Discipline, out totalRecord);
            }
            else if (type == TableName.StaffSalary)
            {
                totalRecord = 0;
                return this._workingProcessService.GetStaffSalary(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.StaffAllowance)
            {
                totalRecord = 0;
                return this._staffService.GetStaffAllowanceByStaff(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.StaffBenefit)
            {
                totalRecord = 0;
                return this._staffService.GetStaffBenefitByStaff(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.TableHealthInsurance)
            {
                totalRecord = 0;
                return this._staffService.GetHealthInsuranceByStaff(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.TableSocialInsurance)
            {
                totalRecord = 0;
                return this._staffService.GetSocialInsuranceByStaff(paramEntity, param.ReferenceId);
            }
            else if (type == TableName.StaffWorkingDayMachine)
            {
                totalRecord = 0;
                return this._staffService.GetStaffWorkingDayMachineByStaff(paramEntity, param.ReferenceId, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
        #region Save Staff

        public ActionResult Add(int? type)
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

            #endregion
            var model = new StaffDetailsViewModel()
            {
                Staff = new StaffModel(),
                WorkingProcess = new WorkingProcessModel(),
                Contract = new StaffContractModel(),
                ListRole = new List<StaffRoleModel>()
                {
                    new StaffRoleModel()
                }
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);

            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
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
                //model.DataDropdownOffice = response.Results.Where(m => m.GroupId == MasterMasterDataId.Office).ToList();
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
                model.DataDropdownMaritalStatus = response.Results.Where(m => m.GroupId == MasterDataId.MaritalStatus).ToList();
                model.DataDropdownNationality = response.Results.Where(m => m.GroupId == MasterDataId.Nationality).ToList();
                model.DataDropdownProvince = response.Results.Where(m => m.GroupId == MasterDataId.Province).ToList();
                model.DataDropdownEthnicity = response.Results.Where(m => m.GroupId == MasterDataId.Ethnicity).ToList();
                model.DataDropdownTimekeepingForm = response.Results.Where(m => m.GroupId == MasterDataId.TimekeepingForm).ToList();
            }
            var resultOrganization = this._organizationService.GetAllOrganizationForDropDown(paramEntity);
            model.DataDropdownOrganization = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultOrganization).Results;
            var resultStaff = this._staffService.GetAllStaffForDropDown(paramEntity);
            model.DataDropdownStaff = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultStaff).Results;
            var responeRole = _roleService.GetAllRole();
            model.DataDropdownRole = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(responeRole).Results;
            var pipelineRespone = _pipelineService.GetPipelineByMenuName(MenuName.Onboarding);
            model.DataDropdownPipeline = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineRespone).Results;
            var pipelineStepRespone = _pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding);
            model.DataDropdownPipelineStep = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineStepRespone).Results;
            var shiftTotal = 0;
            paramEntity.PageSize = Int32.MaxValue;
            var resultShift = this._workingdayService.GetWorkingdayShift(paramEntity, out shiftTotal);
            model.DataDropdownShift = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultShift).Results;

            #region Lấy thông tin column bảng thân nhân
            var resultTableRelationshipConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffRelationships);
            var resultConfigRelationship = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableRelationshipConfig);
            if (!CheckPermission(resultConfigRelationship))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableRelationshipConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigRelationship.Results.FirstOrDefault().ConfigData);
                dataTableRelationshipConfig.ShowFooter = false;
                dataTableRelationshipConfig.TableName = TableName.TableRelationShips;
                dataTableRelationshipConfig.TableConfigName = TableConfig.StaffRelationships;
                //var param2 = new BasicParamModel()
                //{
                //    FilterField = "",
                //    OrderBy = " Id ASC ",
                //    PageNumber = 1,
                //    PageSize = dataTableAllowanceConfig.ItemsPerPage,
                //    LanguageId = _languageId,
                //    RoleId = _roleId,
                //    UserId = _userId,
                //    DbName = CurrentUser.DbName,
                //    ReferenceId = model.WorkingProcess.Id
                //};
                model.ListStaffRelationShips = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(dataTableRelationshipConfig.Clone()));
                var responseColumn = this._tableColumnService.GetTableColumn(TableConfig.StaffRelationships);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.ListStaffRelationShips.ListTableColumns = resultColumn.Results;
                    }
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
                model.ListStaffRelationShips.lstItemsPerPage = dataDropDownListDynamic;
            }
            //kết thúc Lấy thông tin column bảng thân nhân
            #endregion 
            #region Lấy thông tin column bảng máy chấm công
            var resultTableWorkingdayMachineConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffWorkingDayMachine);
            var resultConfigWorkingdayMachine = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableWorkingdayMachineConfig);
            if (!CheckPermission(resultConfigWorkingdayMachine))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigWorkingdayMachine.Results.FirstOrDefault().ConfigData);
                dataTableConfig.ShowFooter = false;
                dataTableConfig.TableName = TableName.StaffWorkingDayMachine;
                dataTableConfig.TableConfigName = TableConfig.StaffWorkingDayMachine;
                model.ListStaffWorkingDayMachines = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(dataTableConfig.Clone()));
                var responseColumn = this._tableColumnService.GetTableColumn(TableConfig.StaffWorkingDayMachine);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.ListStaffWorkingDayMachines.ListTableColumns = resultColumn.Results;
                    }
                }
            }

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
                model.ListStaffWorkingDayMachines.lstItemsPerPage = dataDropDownListDynamic;
            }
            //kết thúc Lấy thông tin column bảng máy chấm công
            #endregion
            #region Lấy thông tin column bảng phụ cấp
            var resultTableAllowanceConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffAllowancePopupWorkingprocess);
            var resultConfigAllowance = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableAllowanceConfig);
            if (!CheckPermission(resultConfigAllowance))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigAllowance.Results.FirstOrDefault().ConfigData);
                dataTableConfig.ShowFooter = false;
                dataTableConfig.TableName = TableName.StaffAllowance;
                dataTableConfig.TableConfigName = TableConfig.StaffAllowancePopupWorkingprocess;
                model.ListAllowance = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(dataTableConfig.Clone()));
                var responseColumn = this._tableColumnService.GetTableColumn(TableConfig.StaffAllowancePopupWorkingprocess);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.ListAllowance.ListTableColumns = resultColumn.Results;
                    }
                }
            }

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
                model.ListAllowance.lstItemsPerPage = dataDropDownListDynamic;
            }
            #endregion
            #region Lấy thông tin column bảng phúc lợi
            var resultTableListBenefitConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.StaffBenefitPopupWorkingprocess);
            var resultConfigListBenefit = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableListBenefitConfig);
            if (!CheckPermission(resultConfigListBenefit))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigListBenefit.Results.FirstOrDefault().ConfigData);
                dataTableConfig.ShowFooter = false;
                dataTableConfig.TableName = TableName.StaffBenefit;
                dataTableConfig.TableConfigName = TableConfig.StaffBenefitPopupWorkingprocess;
                model.ListBenefit = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(dataTableConfig.Clone()));
                var responseColumn = this._tableColumnService.GetTableColumn(TableConfig.StaffBenefitPopupWorkingprocess);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.ListBenefit.ListTableColumns = resultColumn.Results;
                    }
                }
            }

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
                model.ListBenefit.lstItemsPerPage = dataDropDownListDynamic;
            }
            #endregion
            if (type == 1)
            {
                model.Staff.IsOnboarding = true;
            }
            return View(model);
        }
        public ActionResult SaveStaff(ShortStaffModel model)
        {
            model = MapperHelper.ConvertModel(model);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            //if (model.IsOnboarding)
            //{
            //    validations.Add(new ValidationModel { ColumnName = "OnboardingDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.Required") });
            //}
            var staffData = this._staffService.CheckStaffInfoByStaffCode(model.StaffCode);
            if (staffData != null)
            {
                var st = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(staffData);
                if (!CheckPermission(st))
                {
                    //return to Access Denied
                }
                else
                {
                    if (st.Results != null && st.Results.Count > 0)
                    {
                        validations.Add(new ValidationModel { ColumnName = "StaffCode", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.StaffCodeExists") });
                    }
                }
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }
            var success = false;
            var msg = "";
            var staffResult = new StaffModel();

            var listWorkingProcess = new List<WorkingProcessType>();
            var listStaff = new List<StaffType>();
            var listContract = new List<StaffContractType>();
            var listRole = new List<StaffRoleType>();
            var staffRelationship = new List<StaffRelationshipsType>();
            var staffWorkingDayMachine = new List<StaffWorkingDayMachineType>();



            var staff = MapperHelper.Map<ShortStaffModel, StaffType>(model);
            var workingProcess = MapperHelper.Map<ShortStaffModel, WorkingProcessType>(model);
            workingProcess.Status = MasterDataId.ApprovedStatus;
            workingProcess.WorkingprocessTypeId = MasterDataId.WorkingProcessTypeOnboarding;
            workingProcess.WorkingStatus = MasterDataId.WorkingStatusActive;
            var staffOnboardInfo = MapperHelper.Map<ShortStaffModel, StaffOnboardInfoType>(model);
            var staffAllowance = new List<StaffAllowanceType>();
            var staffBenefit = new List<StaffBenefitsType>();
            if (model.IsOnboarding)
            {
                var pipelineRespone = _pipelineService.GetPipelineByMenuName(MenuName.Onboarding);
                var resultPipeline = JsonConvert.DeserializeObject<HrmResultModel<PipelineEntity>>(pipelineRespone).Results;
                if (resultPipeline != null && resultPipeline.Count > 0)
                {
                    staffOnboardInfo.PipelineId = resultPipeline.FirstOrDefault().Id;
                }
                var pipelineStepRespone = _pipelineService.GetPipelineStepByMenuName(MenuName.Onboarding);
                var resultpipelineStep = JsonConvert.DeserializeObject<HrmResultModel<PipelineStepEntity>>(pipelineStepRespone).Results;
                if (resultpipelineStep != null && resultpipelineStep.Count > 0)
                {
                    staffOnboardInfo.PipelineStepId = resultpipelineStep.FirstOrDefault().Id;
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

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            listStaff.Add(staff);
            listWorkingProcess.Add(workingProcess);

            var staffResponse = this._staffService.SaveStaffFull(listStaff, listWorkingProcess, listContract, listRole, staffOnboardInfo
                                    , staffWorkingDayMachine, staffRelationship, staffAllowance, staffBenefit, paramEntity);
            if (staffResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(staffResponse);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        msg = _localizationService.GetResources("Message.Create.Successful");
                        success = true;
                        staffResult = result.Results.FirstOrDefault();
                    }
                }
            }
            Thread thread = new Thread(() => UpdateStaffParent())
            {
                Name = "UpdateStaffParent"
            };
            thread.Start();
            return Json(new { Result = staffResult, Message = msg, Success = success }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveStaffFull(StaffDetailsViewModel model)
        {
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model.Staff, "Staff"));
            validations.AddRange(ValidationHelper.Validation(model.WorkingProcess, "WorkingProcess"));
            validations.AddRange(ValidationHelper.Validation(model.Contract, "StaffContract"));
            validations.AddRange(ValidationHelper.ListValidation(model.ListRole, "ListRole"));
            if (model.WorkingProcess.StartDate >= model.WorkingProcess.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "WorkingProcess.EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (model.Contract.ContractStartDate >= model.Contract.ContractEndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "StaffContract.ContractEndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (model.StaffOnboardInfo.IsOnboarding)
            {
                validations.AddRange(ValidationHelper.Validation(model.StaffOnboardInfo, "StaffOnboardInfo"));
            }
            var userData = this._staffService.GetDataUserByUserName(model.Staff.UserName);
            if (userData != null)
            {
                var user = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(userData);
                if (!CheckPermission(user))
                {
                    //return to Access Denied
                }
                else
                {
                    if (user.Results != null && user.Results.Count > 0)
                    {
                        validations.Add(new ValidationModel { ColumnName = "Staff.UserName", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.UsernameExists") });
                    }
                }
            }
            var staffData = this._staffService.CheckStaffInfoByStaffCode(model.Staff.StaffCode);
            if (staffData != null)
            {
                var st = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(staffData);
                if (!CheckPermission(st))
                {
                    //return to Access Denied
                }
                else
                {
                    if (st.Results != null && st.Results.Count > 0)
                    {
                        validations.Add(new ValidationModel { ColumnName = "Staff.StaffCode", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.StaffCodeExists") });
                    }
                }
            }
            if (model.StaffAllowancePopupWorkingprocess != null && model.StaffAllowancePopupWorkingprocess.Count > 0)
            {
                validations.AddRange(ValidationHelper.ListValidation(model.StaffAllowancePopupWorkingprocess, "StaffAllowancePopupWorkingprocess"));
                for (int i = 0; i < model.StaffAllowancePopupWorkingprocess.Count; i++)
                {
                    if (model.StaffAllowancePopupWorkingprocess[i].StartDate > model.StaffAllowancePopupWorkingprocess[i].EndDate)
                    {
                        validations.Add(new ValidationModel { ColumnName = "StaffAllowancePopupWorkingprocess[" + i.ToString() + "].EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
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
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            model = MapperHelper.ConvertModel(model);
            var success = false;
            var staffResult = new StaffModel();
            var msg = _localizationService.GetResources("Message.Create.Successful");
            var staff = MapperHelper.Map<StaffModel, StaffType>(MapperHelper.ConvertModel(model.Staff));
            staff.Password = !String.IsNullOrEmpty(staff.Password) ? Security.EncryptKey(staff.Password) : staff.Password;
            var workingProcess = MapperHelper.Map<WorkingProcessModel, WorkingProcessType>(MapperHelper.ConvertModel(model.WorkingProcess));
            var contract = MapperHelper.Map<StaffContractModel, StaffContractType>(MapperHelper.ConvertModel(model.Contract));
            var staffOnboardInfo = MapperHelper.Map<StaffOnboardInfoModel, StaffOnboardInfoType>(MapperHelper.ConvertModel(model.StaffOnboardInfo));

            var listWorkingProcess = new List<WorkingProcessType>();
            var listStaff = new List<StaffType>();
            var listContract = new List<StaffContractType>();
            var listRole = MapperHelper.MapList<StaffRoleModel, StaffRoleType>(model.ListRole);
            var staffWorkingDayMachine = MapperHelper.MapList<StaffWorkingDayMachineModel, StaffWorkingDayMachineType>(model.StaffWorkingDayMachine);
            var staffRelationship = MapperHelper.MapList<StaffRelationShipsModel, StaffRelationshipsType>(model.StaffRelationships);
            var staffAllowance = MapperHelper.MapList<StaffAllowanceModel, StaffAllowanceType>(MapperHelper.ConvertModel(model.StaffAllowancePopupWorkingprocess));
            var staffBenefit = MapperHelper.MapList<StaffBenefitsModel, StaffBenefitsType>(MapperHelper.ConvertModel(model.StaffBenefitPopupWorkingprocess));

            listStaff.Add(staff);
            listWorkingProcess.Add(workingProcess);
            listContract.Add(contract);
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
            var staffResponse = this._staffService.SaveStaffFull(listStaff, listWorkingProcess, listContract, listRole, staffOnboardInfo
                                                                , staffWorkingDayMachine, staffRelationship, staffAllowance, staffBenefit, paramEntity);
            if (staffResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(staffResponse);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        msg = _localizationService.GetResources("Message.Create.Successful");
                        success = true;
                        staffResult = result.Results.FirstOrDefault();
                    }
                }
            }
            Thread thread = new Thread(() => UpdateStaffParent())
            {
                Name = "UpdateStaffParent"
            };
            thread.Start();
            return Json(new { Result = staffResult, Message = msg, Success = success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveStaffInfomation(StaffDetailsViewModel model)
        {
            model = MapperHelper.ConvertModel(model);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model.Staff, "Staff"));
            validations.AddRange(ValidationHelper.ListValidation(model.ListRole, "ListRole"));
            validations.AddRange(ValidationHelper.ListValidation(model.StaffRelationships, "StaffRelationships"));
            var userData = this._staffService.GetDataUserByUserName(model.Staff.UserName);
            if (userData != null)
            {
                var user = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(userData);
                if (!CheckPermission(user))
                {
                    //return to Access Denied
                }
                else
                {
                    if (user.Results != null && user.Results.Count > 0)
                    {
                        foreach (var item in user.Results)
                        {
                            if (item.UserId != model.Staff.Id)
                            {
                                validations.Add(new ValidationModel { ColumnName = "Staff.UserName", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.UsernameExists") });
                                break;
                            }
                        }

                    }
                }
            }

            var staffData = this._staffService.CheckStaffInfoByStaffCode(model.Staff.StaffCode);
            if (staffData != null)
            {
                var st = JsonConvert.DeserializeObject<HrmResultModel<UserModel>>(staffData);
                if (!CheckPermission(st))
                {
                    //return to Access Denied
                }
                else
                {
                    if (st.Results != null && st.Results.Count > 0)
                    {
                        foreach (var item in st.Results)
                        {
                            if (item.UserId != model.Staff.Id)
                            {
                                validations.Add(new ValidationModel { ColumnName = "Staff.StaffCode", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.StaffCodeExists") });
                                break;
                            }
                        }
                    }
                }
            }
            foreach (var item in model.ListRole)
            {
                item.StaffId = model.Staff.Id;
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }
            var success = false;
            var staffResult = new StaffModel();
            var msg = _localizationService.GetResources("Message.Update.Successful");
            var staff = MapperHelper.Map<StaffModel, StaffType>(MapperHelper.ConvertModel(model.Staff));
            var staffRelationship = MapperHelper.MapList<StaffRelationShipsModel, StaffRelationshipsType>(MapperHelper.ConvertModel(model.StaffRelationships));
            staff.Password = !String.IsNullOrEmpty(staff.Password) ? Security.EncryptKey(staff.Password) : staff.Password;
            var listStaff = new List<StaffType>();
            var listRelationships = new List<StaffRelationshipsType>();
            var listRole = MapperHelper.MapList<StaffRoleModel, StaffRoleType>(model.ListRole);
            var staffWorkingDayMachine = MapperHelper.MapList<StaffWorkingDayMachineModel, StaffWorkingDayMachineType>(model.StaffWorkingDayMachine);
            listStaff.Add(staff);
            //listRelationships.Add(staffRelationship);
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
            var staffResponse = this._staffService.SaveStaffInformation(listStaff, listRole, staffRelationship, staffWorkingDayMachine, paramEntity);
            if (staffResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(staffResponse);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        msg = _localizationService.GetResources("Message.Update.Successful");
                        success = true;
                        staffResult = result.Results.FirstOrDefault();
                    }
                }
            }
            return Json(new { Result = staffResult, Message = msg, Success = success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveStaffCheckList(List<ChecklistDetailModel> list, long StaffId)
        {

            BaseModel baseModel = new BaseModel();
            baseModel.Id = StaffId;
            baseModel.CreatedBy = _userId;
            baseModel.UpdatedBy = _userId;


            //var validations = ValidationHelper.ListValidation(list, "ChecklistDetail");
            //if (validations.Count > 0)
            //{
            //    foreach (var validation in validations)
            //    {
            //        validation.ErrorMessage = _localizationService.GetResources(validation.ErrorMessage);
            //    }
            //    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            //}

            var type = MapperHelper.MapList<ChecklistDetailModel, Repository.Type.ChecklistDetailType>(list);
            var staff = MapperHelper.Map<BaseModel, BaseEntity>(baseModel);
            string responeseResources = string.Empty;
            HrmResultModel<bool> result = new HrmResultModel<bool>();
            var response = _staffService.SaveStaffChecklist(type, staff);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    var _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                    if (result.Success == true)
                    {
                        responeseResources = _localizationService.GetResources("Message.Create.Successful");
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Create.UpdateFail");
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveStatusPipeline(PipelineStepModel pipelineStep, long staffId)
        {
            var pipelineEntity = MapperHelper.Map<PipelineStepModel, PipelineStepEntity>(pipelineStep);
            pipelineEntity.PipelineDate = DateTime.Now;
            pipelineEntity.IsDeleted = false;
            string responeseResources = string.Empty;
            if (pipelineEntity.PipelineStaffStatusId == 0)
            {
                pipelineEntity.PipelineStaffStatusId = -1;
            }
            var pipelineSteps = new List<PipelineStepModel>();
            HrmResultModel<bool> result = new HrmResultModel<bool>();
            var response = _staffService.SaveStatusPipeline(pipelineEntity, staffId);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    var _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                    if (result.Success == false)
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");

                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.Successful");
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Allowance
        public ActionResult _SaveAllowance()
        {
            //var model = new Reward
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
                Value = MasterDataId.RewardType
            });
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
            var model = new StaffAllowanceViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownAllowanceType = response.Results.Where(m => m.GroupId == MasterDataId.AllowanceType).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                model.DataDropdownStatusAprove = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveAllowance.cshtml"), model);
        }
        public ActionResult _AllowanceDetail(long id)
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
            var model = new StaffAllowanceModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffService.GetStaffAllowanceById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffAllowanceModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_AllowanceDetail.cshtml"), model);
        }
        public ActionResult _HistoryAllowance(long? staffId)
        {
            var tableConfigName = TableConfig.StaffAllowanceHistory;
            var tableName = TableName.StaffAllowance;
            var allowance = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableName = tableName;
                dataTableConfig.TableConfigName = tableConfigName;
                var param1 = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = dataTableConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = staffId ?? 0
                };
                //Reward
                allowance = RenderTable(dataTableConfig, param1, tableName);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_HistoryAllowance.cshtml"), allowance);
        }
        #endregion
        #region Experience
        public ActionResult _ExperienceDetail(long id, int? viewType)
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
            var model = new StaffExperienceModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffService.GetStaffExperienceById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffExperienceModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            if (model == null)
            {
                model = new StaffExperienceModel();
            }
            if (viewType == 1)
            {
                var listGroup = new List<LongTypeModel>();
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Position
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Classification
                });
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Currency
                });
                var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                var saveModel = new StaffExperienceViewModel();
                saveModel.StaffExperience = model;
                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
                var responseData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(responseData))
                {
                    //return to Access Denied
                }
                else
                {
                    saveModel.DataDropdownPositionId = responseData.Results.Where(m => m.GroupId == MasterDataId.Position).ToList();
                    saveModel.DataDropdownClassification = responseData.Results.Where(m => m.GroupId == MasterDataId.Classification).ToList();
                    saveModel.DataDropdownCurrency = responseData.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                }
                return PartialView(UrlHelpers.View("~/Views/Staff/_SaveExperience.cshtml"), saveModel);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_ExperienceDetail.cshtml"), model);
        }
        public ActionResult _SaveExperience()
        {
            //var model = new Reward
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
                Value = MasterDataId.Position
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Classification
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new StaffExperienceViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownPositionId = response.Results.Where(m => m.GroupId == MasterDataId.Position).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
                model.DataDropdownClassification = response.Results.Where(m => m.GroupId == MasterDataId.Classification).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveExperience.cshtml"), model);
        }
        public ActionResult SaveExperience(StaffExperienceModel model)
        {
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            if (model.FromDate >= model.ToDate)
            {
                validations.Add(new ValidationModel { ColumnName = "ToDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            var result = false;
            var msg = "";
            var staffExperienceEntity = MapperHelper.Map<StaffExperienceModel, StaffExperienceEntity>(model);
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

            var response = this._staffService.SaveExperience(staffExperienceEntity, paramEntity);
            var dataResult = JsonConvert.DeserializeObject<HrmResultModel<StaffExperienceModel>>(response);
            if (!CheckPermission(dataResult))
            {
                //return to Access Denied
            }
            else
            {
                if (dataResult.Results.Count > 0)
                {
                    if (dataResult.Results.FirstOrDefault().Id > 0)
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                        result = true;
                    }
                    else
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                        result = false;
                    }
                }
            }
            return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Certificate
        public ActionResult _CertificationDetail(long id, int? viewType)
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
            var model = new StaffCertificateModel();
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._staffService.GetStaffCertificateById(paramEntity, id);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffCertificateModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model = result.Results.FirstOrDefault();
                    }
                }
            }
            if (viewType == 1)
            {
                var listGroup = new List<LongTypeModel>();
                listGroup.Add(new LongTypeModel()
                {
                    Value = MasterDataId.Rank
                });
                var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                var saveModel = new StaffCertificateViewModel();
                saveModel.StaffCertificate = model;
                var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
                var responseData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
                if (!CheckPermission(responseData))
                {
                    //return to Access Denied
                }
                else
                {
                    saveModel.DataDropdownRank = responseData.Results.Where(m => m.GroupId == MasterDataId.Rank).ToList();
                }
                return PartialView(UrlHelpers.View("~/Views/Staff/_SaveCertification.cshtml"), saveModel);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_CertificationDetail.cshtml"), model);
        }
        public ActionResult SaveCertificate(StaffCertificateModel model)
        {
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            if (model.FromDate >= model.ToDate)
            {
                validations.Add(new ValidationModel { ColumnName = "ToDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            var result = false;
            var msg = "";
            var staffCertificate = MapperHelper.Map<StaffCertificateModel, StaffCertificateEntity>(model);
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

            var response = this._staffService.SaveCertificate(staffCertificate, paramEntity);
            var dataResult = JsonConvert.DeserializeObject<HrmResultModel<StaffCertificateModel>>(response);
            if (!CheckPermission(dataResult))
            {
                //return to Access Denied
            }
            else
            {
                if (dataResult.Results.Count > 0)
                {
                    if (dataResult.Results.FirstOrDefault().Id > 0)
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                        result = true;
                    }
                    else
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                        result = false;
                    }
                }
            }
            return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _SaveCertification()
        {
            //var model = new Reward
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
                Value = MasterDataId.Rank
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new StaffCertificateViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownRank = response.Results.Where(m => m.GroupId == MasterDataId.Rank).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveCertification.cshtml"), model);
        }
        #endregion
        #region Insurance
        public ActionResult _SaveHealthInsurance(int? staffId)
        {
            //var model = new Reward
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
                Value = MasterDataId.InsuranceType
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new HealthInsuranceViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownType = response.Results.Where(m => m.GroupId == MasterDataId.InsuranceType).ToList();
            }
            var resultHealthInsurance = this._staffService.GetStaffHealthInsurance(paramEntity, staffId ?? 0);
            var resultHealthInsuranceDetail = JsonConvert.DeserializeObject<HrmResultModel<HealthInsuranceModel>>(resultHealthInsurance);
            if (!CheckPermission(resultHealthInsuranceDetail))
            {
                //return to Access Denied
            }
            else
            {
                if (resultHealthInsuranceDetail.Results.Count > 0)
                {
                    model.HealthInsurance = resultHealthInsuranceDetail.Results.FirstOrDefault();
                    model.HealthInsurance.StartDate = null;
                    model.HealthInsurance.EndDate = null;
                    model.HealthInsurance.Note = "";
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveHealthInsurance.cshtml"), model);
        }
        public ActionResult SaveHealthInsurance(HealthInsuranceModel model)
        {
            model = MapperHelper.ConvertModel(model);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            if (model.StartDate >= model.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            var result = false;
            var msg = "";
            var healthInsurance = MapperHelper.Map<HealthInsuranceModel, HealthInsuranceEntity>(model);
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

            var response = this._staffService.SaveHealthInsurance(healthInsurance, paramEntity);
            var dataResult = JsonConvert.DeserializeObject<HrmResultModel<HealthInsuranceModel>>(response);
            if (!CheckPermission(dataResult))
            {
                //return to Access Denied
            }
            else
            {
                if (dataResult.Results.Count > 0)
                {
                    if (dataResult.Results.FirstOrDefault().Id > 0)
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                        result = true;
                    }
                    else
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                        result = false;
                    }
                }
            }
            return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _SaveSocialInsurance(int? staffId)
        {
            //var model = new Reward
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
                Value = MasterDataId.InsuranceType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
            var model = new StaffSocialInsuranceViewModel();
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownType = response.Results.Where(m => m.GroupId == MasterDataId.InsuranceType).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
            }
            var resultSocialInsurance = this._staffService.GetStaffSocialInsurance(paramEntity, staffId ?? 0);
            var resultSocialInsuranceDetail = JsonConvert.DeserializeObject<HrmResultModel<StaffSocialInsuranceModel>>(resultSocialInsurance);
            if (!CheckPermission(resultSocialInsuranceDetail))
            {
                //return to Access Denied
            }
            else
            {
                if (resultSocialInsuranceDetail.Results.Count > 0)
                {
                    model.StaffSocialInsurance = resultSocialInsuranceDetail.Results.FirstOrDefault();
                    model.StaffSocialInsurance.StartDate = null;
                    model.StaffSocialInsurance.EndDate = null;
                    model.StaffSocialInsurance.DateReturn = null;
                    model.StaffSocialInsurance.Note = "";
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_SaveSocialInsurance.cshtml"), model);
        }
        public ActionResult SaveSocialInsurance(StaffSocialInsuranceModel model)
        {
            model = MapperHelper.ConvertModel(model);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            if (model.StartDate >= model.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            var result = false;
            var msg = "";
            var socialInsurance = MapperHelper.Map<StaffSocialInsuranceModel, StaffSocialInsuranceEntity>(model);
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

            var response = this._staffService.SaveSocialInsurance(socialInsurance, paramEntity);
            var dataResult = JsonConvert.DeserializeObject<HrmResultModel<StaffSocialInsuranceModel>>(response);
            if (!CheckPermission(dataResult))
            {
                //return to Access Denied
            }
            else
            {
                if (dataResult.Results.Count > 0)
                {
                    if (dataResult.Results.FirstOrDefault().Id > 0)
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.Successful") : _localizationService.GetResources("Message.Update.Successful");
                        result = true;
                    }
                    else
                    {
                        msg = model.Id == 0 ? _localizationService.GetResources("Message.Create.UnSuccessful") : _localizationService.GetResources("Message.Update.UnSuccessful");
                        result = false;
                    }
                }
            }
            return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _HistoryHealthInsurance(long? staffId)
        {
            var tableConfigName = TableConfig.HealthInsurance;
            var tableName = TableName.TableHealthInsurance;
            var healthInsurance = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableName = tableName;
                dataTableConfig.TableConfigName = tableConfigName;
                var param1 = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = dataTableConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = staffId ?? 0
                };
                //Reward
                healthInsurance = RenderTable(dataTableConfig, param1, tableName);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_HistoryHealthInsurance.cshtml"), healthInsurance);
        }
        public ActionResult _HistorySocialInsurance(long? staffId)
        {
            var tableConfigName = TableConfig.SocialInsurance;
            var tableName = TableName.TableSocialInsurance;
            var healthInsurance = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(tableConfigName);
            var tableConfigDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableConfig);
            if (!CheckPermission(tableConfigDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableConfig = JsonConvert.DeserializeObject<TableViewModel>(tableConfigDetail.Results.FirstOrDefault().ConfigData);
                dataTableConfig.TableDataUrl = TableUrl.StaffGetDataUrl;
                dataTableConfig.TableName = tableName;
                dataTableConfig.TableConfigName = tableConfigName;
                var param1 = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = dataTableConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = staffId ?? 0
                };
                //Reward
                healthInsurance = RenderTable(dataTableConfig, param1, tableName);
            }
            return PartialView(UrlHelpers.View("~/Views/Staff/_HistorySocialInsurance.cshtml"), healthInsurance);
        }
        public ActionResult SaveInsurance(InsuranceViewModel model)
        {
            model.HealthInsurance = MapperHelper.ConvertModel(model.HealthInsurance);
            model.StaffSocialInsurance = MapperHelper.ConvertModel(model.StaffSocialInsurance);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model.HealthInsurance, "HealthInsurance"));
            validations.AddRange(ValidationHelper.Validation(model.StaffSocialInsurance, "StaffSocialInsurance"));
            if (model.HealthInsurance.StartDate >= model.HealthInsurance.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "HealthInsurance.EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (model.StaffSocialInsurance.StartDate >= model.StaffSocialInsurance.EndDate)
            {
                validations.Add(new ValidationModel { ColumnName = "StaffSocialInsurance.EndDate", ErrorMessage = _localizationService.GetResources("ErrorMessage.Validation.EndDateMustBeGreaterStarddate") });
            }
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }

            var result = false;
            var msg = "";
            var socialInsurance = MapperHelper.Map<StaffSocialInsuranceModel, StaffSocialInsuranceEntity>(model.StaffSocialInsurance);
            var healthInsurance = MapperHelper.Map<HealthInsuranceModel, HealthInsuranceEntity>(model.HealthInsurance);
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

            var responseSocialInsurance = this._staffService.SaveSocialInsurance(socialInsurance, paramEntity);
            var dataResult = JsonConvert.DeserializeObject<HrmResultModel<StaffSocialInsuranceModel>>(responseSocialInsurance);
            if (!CheckPermission(dataResult))
            {
                //return to Access Denied
            }
            else
            {
                if (dataResult.Results.Count > 0)
                {
                    if (dataResult.Results.FirstOrDefault().Id > 0)
                    {
                        var responseHealthInsurance = this._staffService.SaveHealthInsurance(healthInsurance, paramEntity);
                        msg = _localizationService.GetResources("Message.Update.Successful");
                        result = true;
                    }
                    else
                    {
                        msg = _localizationService.GetResources("Message.Update.UnSuccessful");
                        result = false;
                    }
                }
            }
            return Json(new { Result = result, Message = msg }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Pipeline
        public ActionResult ReloadDropDownPipelineStepId(long? pipelineId)
        {
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = CurrentUser.LanguageId,
                RoleId = 1,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var model = new List<dynamic>();
            if (pipelineId > 0)
            {
                var pipelineStepResponse = this._pipelineService.GetPipelineStepByPipelineId(pipelineId ?? 0);
                if (!string.IsNullOrEmpty(pipelineStepResponse))
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(pipelineStepResponse);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model = result.Results;
                    }

                }
            }
            return PartialView(UrlHelpers.FloatingTemplate("_Dropdown.cshtml"), new DropdownListModel() { LabelName = _localizationService.GetResources("Staff.OnboardingStatus"), IsRequired = true, IsAnimationLabel = true, Data = model, ValueField = "Id", DisplayField = "PipelineStepName", Value = "50", Name = "StaffOnboardInfo.PipelineStepId", IsMultipleLanguage = true, DataType = DataType.PipelineStep, PropertyName = "PipelineName" });
            //return PartialView(UrlHelpers.FloatingTemplate("_Dropdown.cshtml"), model);
        }
        #endregion
        #region Export Data
        public ActionResult ExportStaff(TableViewModel tableConfig)
        {
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = Int32.MaxValue,
                LanguageId = CurrentUser.LanguageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            return GetData(tableConfig, param);
        }
        #endregion

        private string ReadFileByPath(string name)
        {
            String file = string.Empty;
            var path = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"] + "\\" + name.ToString();
            if (!string.IsNullOrEmpty(name))
            {
                var extension = name.Substring(name.LastIndexOf('.'), name.Count() - name.LastIndexOf('.'));
                var fileName = name.Substring(0, name.LastIndexOf('.'));
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                file = string.Format("data:image/{0};base64, " + Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks), extension.ToLower());
            }
            return file;
        }
        public void UpdateStaffParent()
        {
            //0 bt 1 truc tiep 2 nhân sự
            _workingProcessService.UpdateStaffParent();

        }
    }
}
