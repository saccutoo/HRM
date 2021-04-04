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
    public class WorkingdayController : BaseController
    {
        private long _languageId;
        private long _userId;
        private long _roleId;
        private IWorkingdayService _workingdayService;
        private IStaffService _staffService;
        private IMasterDataService _masterDataService;
        private ITableConfigService _tableConfigService;
        private ILocalizationService _localizationService;
        private ITableColumnService _tableColumnService;
        private IOrganizationService _IOrganizationService;
        public WorkingdayController(IWorkingdayService workingdayService, IMasterDataService masterDataService, IStaffService staffService, ITableConfigService tableConfigService, ILocalizationService localizationService, ITableColumnService tableColumnService, IOrganizationService _OrganizationService)
        {
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = CurrentUser.RoleId;
            this._workingdayService = workingdayService;
            this._staffService = staffService;
            this._masterDataService = masterDataService;
            this._tableConfigService = tableConfigService;
            this._localizationService = localizationService;
            this._IOrganizationService = _OrganizationService;
            _tableColumnService = tableColumnService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? activetab)
        {
            var tabActive = activetab ?? 0;
            var query = Request.Url.Query;
            var type = Common.ViewType.Calendar;

            var viewType = HttpUtility.ParseQueryString(query).Get("viewtype");
            if (viewType != null && (viewType == Common.ViewType.List))
            {
                type = Common.ViewType.List;
            }
            if (viewType != null && (viewType == Common.ViewType.Card))
            {
                type = Common.ViewType.Card;
            }

            long staffId = 0;
            long organizationId = 0;
            int month = 0;
            int year = 0;
            var staffIdQ = HttpUtility.ParseQueryString(query).Get("staffid");
            if (staffIdQ == null || staffIdQ == string.Empty || staffIdQ=="null")
            {
                staffId = CurrentUser.UserId;
            }
            else
            {
                long.TryParse(staffIdQ, out staffId);
            }
            var organizationIdQ = HttpUtility.ParseQueryString(query).Get("organizationId");
            if (organizationIdQ == null || organizationIdQ == string.Empty)
            {
                organizationId = 0;
            }
            else
            {
                long.TryParse(organizationIdQ, out organizationId);
            }
            var monthQ = HttpUtility.ParseQueryString(query).Get("month");
            if (monthQ == null || monthQ == "null")
            {
                month = DateTime.Now.Month;
            }
            else
            {
                int.TryParse(monthQ, out month);
            }
            var yearQ = HttpUtility.ParseQueryString(query).Get("year");
            if (yearQ == null || yearQ == "null")
            {
                year = DateTime.Now.Year;
            }
            else
            {
                int.TryParse(yearQ, out year);
            }
            var workingdayDetail_vm = new WorkingdayViewModel()
            {

                WorkingdaySummary = new WorkingdaySummaryViewModel { Month = month, Year = year, ViewType = type, StaffId = CurrentUser.UserId,StaffIdFilter=staffId,organizationId=organizationId },
                WorkingdaySupplement = new WorkingdaySupplementViewModel { Month = month, Year = year, StaffId = staffId, ViewType = type, StaffIdFilter = staffId, organizationId = organizationId },
                WorkingdaySupplementApproval = new WorkingdaySupplementApprovalViewModel(),
                WorkingdayAllStaff = new WorkingdayAllStaffViewModel { Month = month, Year = year, StaffId = staffId, StaffIdFilter = staffId, OrganizationId = organizationId },
                WorkingdayFurlough = new WorkingdayFurloughViewModel { Month = month, Year = year, StaffIdFilter = staffId, OrganizationId = organizationId },
                StaffId = CurrentUser.UserId,
                ActiveTab = tabActive,
                Month=month,
                Year=year,
                StaffIdFilter= staffId,
                OrganizationId= organizationId
            };
            workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements = new List<WorkingdaySupplementModel>();
            DateTime datefilter = new DateTime();
            if (year==0 || month==0)
            {
               datefilter = DateTime.Now;
            }
            else
            {
               datefilter = new DateTime(year, month, 15);
            }
            var supplementNeedApprovalResponse = _workingdayService.GetNeedApproveWorkingdaySupplement(CurrentUser.UserId, CurrentUser.UserId, CurrentUser.RoleId, datefilter, 1);
            var supplementNeedApprovalResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementNeedApprovalResponse);
            if (!CheckPermission(supplementNeedApprovalResult))
            {
                //return to Access Denied
            }
            else
            {
                workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements = supplementNeedApprovalResult.Results;
                workingdayDetail_vm.CountWorkingdaySupplementNeedApproval = workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Where(s => s.RequestStatus == MasterDataId.RequestPending).Count();
                workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplements = workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements;
            }

            switch (tabActive)
            {
                #region // MY SUMMARY
               
                case 0: 
                    {
                        var listGroup = new List<LongTypeModel>();
                        listGroup.Add(new LongTypeModel()
                        {
                            Value = MasterDataId.TypeOfWorking
                        });
  
                        var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
                        var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
                        var responseMasterDataDetail = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultMasterData);
                        if (!CheckPermission(responseMasterDataDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            workingdayDetail_vm.WorkingdaySummary.DayOffTypes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.TypeOfWorking).ToList();
                        }
                      

                        var responseMachine = _workingdayService.GetWorkingdayMachineByStaffAndMonth(workingdayDetail_vm.StaffIdFilter, month, year);
                        var resultMachine = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayCheckInOutModel>>(responseMachine);
                        if (!CheckPermission(resultMachine))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            workingdayDetail_vm.WorkingdaySummary.WorkingdayMachines = resultMachine.Results;
                        }


                        var supplementResponse = _workingdayService.GetWorkingdaySupplementByStaffAndMonth(workingdayDetail_vm.StaffIdFilter, month, year, "");
                        var supplementResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementResponse);
                        if (!CheckPermission(supplementResult))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            workingdayDetail_vm.WorkingdaySummary.WorkingdaySupplements = supplementResult.Results;
                        }
                        var lstStaff = _staffService.GetStaffByParentId(CurrentUser.UserId);
                        var lstStaffResult = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(lstStaff);
                        if (!CheckPermission(lstStaffResult))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (lstStaffResult.Results.Count()>0)
                            {
                                foreach (var item in lstStaffResult.Results)
                                {
                                    var stringFormat = item.OrganizationId.ToString() + ";" + DataType.Organization + ";OrganizationName";
                                    item.Name = item.Name + " - " + _localizationService.GetLocalizedData(stringFormat).ToString();
                                }
                            }
                            workingdayDetail_vm.StaffMangers = lstStaffResult.Results.ToList();

                        }

                        var responsePeriod = _workingdayService.GetCalculateWorkingDayPeriodByIsDefault();
                        if (responsePeriod != null)
                        {
                            var resultPeriod = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayPeriodModel>>(responsePeriod);
                            if (!CheckPermission(resultPeriod))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod = resultPeriod.Results.FirstOrDefault();
                                if ((workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.FromDay- workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.Today)>=0)
                                {                                   
                                    workingdayDetail_vm.WorkingdaySummary.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, Convert.ToInt32(workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.FromDay));
                                    workingdayDetail_vm.WorkingdaySummary.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.Today));
                                }
                                if ((workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.FromDay - workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.Today) < 0)
                                {
                                    try
                                    {
                                        if (DateTime.Now.Month==2)
                                        {
                                            if (workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.Today>=30)
                                            {
                                                
                                                workingdayDetail_vm.WorkingdaySummary.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.FromDay));
                                                workingdayDetail_vm.WorkingdaySummary.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(29));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        
                                        workingdayDetail_vm.WorkingdaySummary.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(workingdayDetail_vm.WorkingdaySummary.WorkingdayPeriod.FromDay));
                                        workingdayDetail_vm.WorkingdaySummary.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(28));
                                    }
                                   
                                }
                            }
                        }
                        if (type==Common.ViewType.List || type== Common.ViewType.Card)
                        {

                            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdaySummaryFinal);
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
                                        FilterField = string.Empty,
                                        StringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + staffId + "}",
                                        PageNumber = 1,
                                        PageSize = dataTableConfig.ItemsPerPage,
                                        LanguageId = _languageId,
                                        RoleId = _roleId,
                                        UserId = _userId,
                                        DbName = CurrentUser.DbName
                                    };
                                    workingdayDetail_vm.WorkingdaySummary.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdaySummaryFinal);
                                    workingdayDetail_vm.WorkingdaySummary.Table.TableName = TableName.TableWorkingdaySummaryFinal;
                                    workingdayDetail_vm.WorkingdaySummary.Table.TableDataUrl = TableUrl.TableWorkingdaySummaryFinalUrl;
                                }
                                workingdayDetail_vm.WorkingdaySummary.Workingdays = JsonConvert.DeserializeObject<List<WorkingdayModel>>(JsonConvert.SerializeObject(workingdayDetail_vm.WorkingdaySummary.Table.TableData));
                            }
                            var responseWorkingdayShiftMapper = _workingdayService.GetWorkingdayShiftMapperByStaffId(workingdayDetail_vm.StaffIdFilter);
                            if (responseWorkingdayShiftMapper!=null)
                            {
                                var resultWorkingdayShiftMapper = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftMapperModel>>(responseWorkingdayShiftMapper);
                                if (!CheckPermission(resultWorkingdayShiftMapper))
                                {
                                    //return to Access Denied
                                }
                                else
                                {
                                    workingdayDetail_vm.WorkingdaySummary.WorkingdayShiftMapper = resultWorkingdayShiftMapper.Results.FirstOrDefault();
                                }
                            }
                        }
                        else
                        {
                            var response = _workingdayService.GetWorkingdayByStaffAndMonth(workingdayDetail_vm.StaffIdFilter, month, year);
                            var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayModel>>(response);
                            if (!CheckPermission(result))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                workingdayDetail_vm.WorkingdaySummary.Workingdays = result.Results;

                            }
                        }
                        break;
                    }
                #endregion
                #region //SUPPLEMENT
                case 1: 
                    {
                        var listGroups = new List<LongType>();
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestDraff });
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestApprove });
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestPending });
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestReject });
                        workingdayDetail_vm.WorkingdaySupplement.longTypes = listGroups;
                        workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements = new List<WorkingdaySupplementModel>();
                        if (type == Common.ViewType.List)
                        {
                            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingDaySupplement);
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
                                        FilterField =string.Empty,
                                        StringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + CurrentUser.UserId + "}",
                                        PageNumber = 1,
                                        PageSize = dataTableConfig.ItemsPerPage,
                                        LanguageId = _languageId,
                                        RoleId = _roleId,
                                        UserId = _userId,
                                        DbName = CurrentUser.DbName
                                    };
                                    workingdayDetail_vm.WorkingdaySupplement.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingDaySupplement);
                                    workingdayDetail_vm.WorkingdaySupplement.Table.TableName = TableName.TableWorkingDaySupplement;
                                    workingdayDetail_vm.WorkingdaySupplement.Table.TableDataUrl = TableUrl.WorkingDaySupplementUrl;
                                }

                            }
                        }
                        else
                        {
                            var supplementResponse = _workingdayService.GetWorkingdaySupplementByStaffAndMonth(CurrentUser.UserId, month, year, "");
                            var supplementResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementResponse);
                            if (!CheckPermission(supplementResult))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements = supplementResult.Results;
                            }
                            if (workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Count() > 0)
                            {                              
                                foreach (var item in listGroups)
                                {
                                    if (workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Where(s => s.RequestStatus == item.Value).Count() > 0)
                                    {
                                        workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplement = workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Where(s => s.RequestStatus == item.Value).FirstOrDefault();
                                        break;
                                    }
                                }

                                if (workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplement != null && workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplement.Id!=0)
                                {
                                    long requestStatusId = workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplement.RequestStatusId;
                                    if (requestStatusId != MasterDataId.Draff || requestStatusId != MasterDataId.WaitingForTheManagerToApprove)
                                    {
                                        var response = _workingdayService.GetWorkingdaySupplementApprovalById(workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplement.Id);
                                        if (response != null)
                                        {
                                            var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementApprovalModel>>(response);
                                            if (!CheckPermission(result))
                                            {
                                                //return to Access Denied
                                            }
                                            else
                                            {
                                                workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplementDetail.WorkingdaySupplementApprovals = result.Results;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                #endregion
                #region  // WORKING DAY ALL STAFF
                case 2: // WORKING DAY ALL STAFF
                    {

                        var lstStaff = _staffService.GetStaffByParentId(CurrentUser.UserId);
                        if (lstStaff != null)
                        {
                            var resultStaff = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(lstStaff);
                            if (!CheckPermission(resultStaff))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultStaff.Results.Count() > 0)
                                {
                                    foreach (var item in resultStaff.Results)
                                    {
                                        var stringFormat = item.OrganizationId.ToString() + ";" + DataType.Organization + ";OrganizationName";
                                        item.Name = item.Name + " - " + _localizationService.GetLocalizedData(stringFormat).ToString();
                                    }
                                    workingdayDetail_vm.WorkingdayAllStaff.StaffMangers = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultStaff.Results));
                                }
                            }
                        }

                        var responseOrganization = _IOrganizationService.GetOrganizationByParentId(CurrentUser.UserId);
                        if (responseOrganization != null)
                        {
                            var resultOrganization = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(responseOrganization);
                            if (!CheckPermission(resultOrganization))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                workingdayDetail_vm.WorkingdayAllStaff.Organizations = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultOrganization.Results));
                            }
                        }

                        var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayAllStaff);
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
                                FilterField = "AND T.Id = " + workingdayDetail_vm.StaffIdFilter,
                                StringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + workingdayDetail_vm.StaffIdFilter + "}",
                                PageNumber = 1,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName,
                                OrderBy = "DSI.Name "
                            };
                            workingdayDetail_vm.WorkingdayAllStaff.Table = RenderTable(dataTableConfig, param, TableName.WorkingdayAllStaff);
                            workingdayDetail_vm.WorkingdayAllStaff.Table.TableName = TableName.WorkingdayAllStaff;
                            workingdayDetail_vm.WorkingdayAllStaff.Table.TableDataUrl = TableUrl.WorkingdayGetDataUrl;
                        }
                        break;
                    }
                #endregion
                #region // FURLOUGH

                case 3:
                    {
                        var lstStaff = _staffService.GetStaffByParentId(CurrentUser.UserId);
                        if (lstStaff!=null)
                        {
                            var resultStaff = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(lstStaff);
                            if (!CheckPermission(resultStaff))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                if (resultStaff.Results.Count()>0)
                                {
                                    foreach (var item in resultStaff.Results)
                                    {
                                        var stringFormat= item.OrganizationId.ToString() + ";" + DataType.Organization + ";OrganizationName";
                                        item.Name = item.Name+ " - " + _localizationService.GetLocalizedData(stringFormat).ToString();
                                    }
                                    workingdayDetail_vm.WorkingdayFurlough.StaffMangers = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultStaff.Results));
                                }
                            }
                        }
                        var responseOrganization = _IOrganizationService.GetOrganizationByParentId(CurrentUser.UserId);
                        if (responseOrganization!=null)
                        {
                            var resultOrganization = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(responseOrganization);
                            if (!CheckPermission(resultOrganization))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                workingdayDetail_vm.WorkingdayFurlough.Organizations = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultOrganization.Results));
                            }
                        }
                        var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayFurlough);
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
                                FilterField = "AND T.Id = " + workingdayDetail_vm.StaffIdFilter,
                                StringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + workingdayDetail_vm.StaffIdFilter + "}",
                                PageNumber = 1,
                                PageSize = dataTableConfig.ItemsPerPage,
                                LanguageId = _languageId,
                                RoleId = _roleId,
                                UserId = _userId,
                                DbName = CurrentUser.DbName
                            };
                            workingdayDetail_vm.WorkingdayFurlough.Table = RenderTable(dataTableConfig, param, TableName.Furlough);
                            workingdayDetail_vm.WorkingdayFurlough.Table.TableName = TableName.Furlough;
                            workingdayDetail_vm.WorkingdayFurlough.Table.TableDataUrl = TableUrl.WorkingdayGetDataUrl;
                        }
                        break;
                    }
                #endregion
                #region //APPROVAL

                case 4: 
                    {
                        var listGroups = new List<LongType>();
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestPending });
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestApprove });
                        listGroups.Add(new LongType() { Value = MasterDataId.RequestReject });
                        workingdayDetail_vm.WorkingdaySupplement.longTypes = listGroups;
                        if (type == Common.ViewType.List)
                        {
                            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingDaySupplementApproval);
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
                                        FilterField = string.Empty,
                                        StringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + CurrentUser.UserId + "}",
                                        PageNumber = 1,
                                        PageSize = dataTableConfig.ItemsPerPage,
                                        LanguageId = _languageId,
                                        RoleId = _roleId,
                                        UserId = _userId,
                                        DbName = CurrentUser.DbName
                                    };
                                    workingdayDetail_vm.WorkingdaySupplement.Table = RenderTable(dataTableConfig, param, TableName.WorkingDaySupplementNeedApproval);
                                    workingdayDetail_vm.WorkingdaySupplement.Table.TableName = TableName.WorkingDaySupplementNeedApproval;
                                    workingdayDetail_vm.WorkingdaySupplement.Table.TableDataUrl = TableUrl.WorkingDaySupplementNeedApprovalUrl;
                                }

                            }
                        }
                        if (workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Count() > 0)
                        {
                            foreach (var item in listGroups)
                            {
                                if (workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Where(s => s.RequestStatus == item.Value).Count() > 0)
                                {
                                    workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplement = workingdayDetail_vm.WorkingdaySupplement.WorkingdaySupplements.Where(s => s.RequestStatus == item.Value).FirstOrDefault();
                                    break;
                                }
                            }
                            if (workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplement!=null && workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplement.Id!=0)
                            {
                                var response = _workingdayService.GetWorkingdaySupplementByStaffAndMonth(workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplement.StaffId, month, year, "AND DWSA.RequestStatusId<>323 AND DWS.Id<>" + workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplement.Id);
                                if (response != null)
                                {
                                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                                    if (!CheckPermission(result))
                                    {
                                        //return to Access Denied
                                    }
                                    else
                                    {
                                        workingdayDetail_vm.WorkingdaySupplement.NeedApprovalWorkingdaySupplementDetail.WorkingdaySupplements = result.Results;
                                    }
                                }
                            }
                            
                        }
                        break;
                    }
                    #endregion
            }
            return View(UrlHelpers.View("~/Views/Workingday/Detail.cshtml"), workingdayDetail_vm);
        }
        public ActionResult SummaryDetailByDay()
        {
            return View(UrlHelpers.View("~/Views/Workingday/_SummaryDetailByDay.cshtml"));
        }
        public ActionResult GetWorkingdaySupplenmentById(long id, int activeTab, int month, int year)
        {
            WorkingdaySupplementDetailViewModel workingdaySupplementDetail_vm = new WorkingdaySupplementDetailViewModel();
            NeedApprovalWorkingdaySupplementDetailViewModel needApprovalWorkingdaySupplementDetail_vm = new NeedApprovalWorkingdaySupplementDetailViewModel();
            WorkingdaySupplementModel supplement = new WorkingdaySupplementModel();
            var supplementResponse = _workingdayService.GetWorkingdaySupplementById(id);
            var supplementResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementResponse);
            if (!CheckPermission(supplementResult))
            {
                //return to Access Denied
            }
            else
            {
                supplement = supplementResult.Results.FirstOrDefault();
            }
            if (activeTab == 1)
            {
                workingdaySupplementDetail_vm.WorkingdaySupplement = supplement;
                if (workingdaySupplementDetail_vm.WorkingdaySupplement.RequestStatusId != MasterDataId.Draff || workingdaySupplementDetail_vm.WorkingdaySupplement.RequestStatusId != MasterDataId.WaitingForTheManagerToApprove)
                {
                    var response = _workingdayService.GetWorkingdaySupplementApprovalById(id);
                    if (response != null)
                    {
                        var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementApprovalModel>>(response);
                        if (!CheckPermission(result))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            workingdaySupplementDetail_vm.WorkingdaySupplementApprovals = result.Results;
                        }
                    }
                }
                return PartialView(UrlHelpers.View("~/Views/Workingday/_WorkingdaySupplenmentDetail.cshtml"), workingdaySupplementDetail_vm);
            }
            else
            {
                needApprovalWorkingdaySupplementDetail_vm.WorkingdaySupplement = supplement;
                var response = _workingdayService.GetWorkingdaySupplementByStaffAndMonth(needApprovalWorkingdaySupplementDetail_vm.WorkingdaySupplement.StaffId, month, year, "AND DWSA.RequestStatusId<>323 AND DWS.Id<>" + needApprovalWorkingdaySupplementDetail_vm.WorkingdaySupplement.Id);
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        needApprovalWorkingdaySupplementDetail_vm.WorkingdaySupplements = result.Results;
                    }
                }
                return PartialView(UrlHelpers.View("~/Views/Workingday/_NeedApproveWorkingdaySupplementDetail.cshtml"), needApprovalWorkingdaySupplementDetail_vm);
            }

        }
        public ActionResult AddWorkingdaySupplement(List<WorkingdayModel> data, bool isSendApproval = false)
        {
            var listGroup = new List<LongTypeModel>();
            AddWorkingdaySupplementViewModel addWorkingdaySupplement_vm = new AddWorkingdaySupplementViewModel();
            addWorkingdaySupplement_vm.WorkingdaySupplement = new WorkingdaySupplementModel();
            addWorkingdaySupplement_vm.Workingdays = new List<WorkingdayModel>();
            addWorkingdaySupplement_vm.isSendApproval = isSendApproval;
            if (data != null)
            {
                addWorkingdaySupplement_vm.Workingdays = data;
            }
            addWorkingdaySupplement_vm.SupplementTypeIdSelected = MasterDataId.LateDuration;
            //get master data
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.ReasonType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementType
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
                addWorkingdaySupplement_vm.DataDropdownReasonType = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.ReasonType).ToList();
                addWorkingdaySupplement_vm.DataDropdownSupplementType = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementType).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Workingday/_AddWorkingdaySupplement.cshtml"), addWorkingdaySupplement_vm);
        }
        public ActionResult WorkingdaySupplementBodyModal(long supplementTypeId, List<WorkingdayModel> workingdays, bool isClickByDate, DateTime? date)
        {
            var listGroup = new List<LongTypeModel>();
            var addWorkingdaySupplement_vm = new AddWorkingdaySupplementViewModel();
            addWorkingdaySupplement_vm.SupplementTypeIdSelected = supplementTypeId;
            addWorkingdaySupplement_vm.isClickByDate = isClickByDate;

            addWorkingdaySupplement_vm.WorkingdaySupplement = new WorkingdaySupplementModel();
            if (addWorkingdaySupplement_vm.isClickByDate == true && date != null)
            {
                addWorkingdaySupplement_vm.Date = date;
                if (addWorkingdaySupplement_vm.SupplementTypeIdSelected == MasterDataId.LateDuration || addWorkingdaySupplement_vm.SupplementTypeIdSelected == MasterDataId.EarlyDuration)
                {
                    addWorkingdaySupplement_vm.WorkingdaySupplement.Date = date;
                }
                else
                {
                    addWorkingdaySupplement_vm.WorkingdaySupplement.StartTime = date;
                    addWorkingdaySupplement_vm.WorkingdaySupplement.EndTime = date;

                }
            }
            if (workingdays == null)
            {
                addWorkingdaySupplement_vm.Workingdays = new List<WorkingdayModel>();
            }
            else
            {
                addWorkingdaySupplement_vm.Workingdays = workingdays;
            }
            //get master data
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.ReasonType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Coefficient
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
                addWorkingdaySupplement_vm.DataDropdownReasonType = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.ReasonType).ToList();
                addWorkingdaySupplement_vm.DataDropdownOverTimeRate = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Coefficient).ToList();
            }
            return PartialView(UrlHelpers.View("~/Views/Workingday/_AddWorkingdaySupplementBodyModal.cshtml"), addWorkingdaySupplement_vm);
        }
        public ActionResult ChangeDate(DateTime date, long supplementTypeId, List<WorkingdayModel> workingdays)
        {
            var time = string.Empty;
            if (workingdays != null)
            {
                var workingsupplement = workingdays.Where(s => s.Date.Date == date.Date).ToList().FirstOrDefault();
                if (workingsupplement != null)
                {
                    if (supplementTypeId == MasterDataId.LateDuration)
                    {
                        time = Convert.ToDateTime(workingsupplement.LateDuration).ToString("HH:mm");
                    }
                    else
                    {
                        time = Convert.ToDateTime(workingsupplement.EarlyDuration).ToString("HH:mm");
                    }
                }
            }

            return Json(new { result = time }, JsonRequestBehavior.AllowGet);
            //return null;
        }
        public ActionResult SaveWorkingdaySupplement(WorkingdaySupplementModel model, bool isApproval)
        {
            model = MapperHelper.ConvertModel(model);
            List<ValidationModel> validations = new List<ValidationModel>();
            if (model != null)
            {
                if (model.SupplementTypeId == MasterDataId.LateDuration || model.SupplementTypeId == MasterDataId.EarlyDuration)
                {
                    var pleaseLateEarly = MapperHelper.Map<WorkingdaySupplementModel, PleaseLateEarlyModel>(model);
                    validations = ValidationHelper.Validation(pleaseLateEarly, "model");
                }
                else if ((model.SupplementTypeId == MasterDataId.AdditionalWorkOverTime))
                {
                    var additionalWorkOverTime = MapperHelper.Map<WorkingdaySupplementModel, AdditionalWorkOverTimeModel>(model);
                    validations = ValidationHelper.Validation(additionalWorkOverTime, "model");
                }
                else if ((model.SupplementTypeId == MasterDataId.AdditionalWork || model.SupplementTypeId == MasterDataId.NoSalary || model.SupplementTypeId == MasterDataId.Vacation))
                {
                    var additionalWork = MapperHelper.Map<WorkingdaySupplementModel, AdditionalWorkModel>(model);
                    validations = ValidationHelper.Validation(additionalWork, "model");
                }
                else
                {
                    var additionalWorkOverTime = MapperHelper.Map<WorkingdaySupplementModel, AdditionalWorkOverTimeModel>(model);
                    validations = ValidationHelper.Validation(additionalWorkOverTime, "model");
                }
            }

            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }
            if (model.Id != 0)
            {
                if (model.SupplementTypeId != MasterDataId.LateDuration && model.SupplementTypeId != MasterDataId.EarlyDuration)
                {
                    if (model.StartTime.Value.Date != model.EndTime.Value.Date)
                    {
                        return Json(new { Result = _localizationService.GetResources("Workingday.NoticationFailEditTwoDate"), InvalidTwoDate = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
           
            List<WorkingdaySupplementModel> listModel = new List<WorkingdaySupplementModel>();

            model.StaffId = CurrentUser.UserId;
            if (model.SupplementTypeId == MasterDataId.LateDuration || model.SupplementTypeId == MasterDataId.EarlyDuration)
            {
                if (model.Date != null)
                {
                    string Date = model.Date.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"]);
                    string isDatetime = Date + " " + model.MissingTime;
                    DateTime datetime;
                    if (DateTime.TryParse(isDatetime,out datetime))
                    {
                        model.MissingTimeDuration = DateTime.ParseExact(isDatetime, System.Configuration.ConfigurationManager.AppSettings["SqlDateTimeFormat"], System.Globalization.CultureInfo.InstalledUICulture);
                    }
                    else
                    {
                        validations.Add(new ValidationModel { ColumnName = "model.MissingTime", ErrorMessage = _localizationService.GetResources("Workingday.WrongTimeFormat") });
                        return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                    }
                   
                }
                listModel.Add(model);
            }
            else
            {
                string Date = string.Empty;
                string isDatetime = string.Empty;
                DateTime StartTime = model.StartTime.Value;
                DateTime EndTime = model.EndTime.Value;

                List<DateTime> listDate = new List<DateTime>();

                if (model.StartTime != null && model.EndTime != null)
                {
                    for (DateTime date = StartTime; date <= EndTime; date = date.AddDays(1))
                    {
                        Date = date.ToString(System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"]);
                        isDatetime = Date + " " + model.FromTime;
                        model.StartTime = DateTime.ParseExact(isDatetime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InstalledUICulture);

                        isDatetime = Date + " " + model.ToTime;
                        model.EndTime = DateTime.ParseExact(isDatetime, System.Configuration.ConfigurationManager.AppSettings["SqlDateTimeFormat"], System.Globalization.CultureInfo.InstalledUICulture);

                        model.Date = DateTime.ParseExact(model.StartTime.Value.ToString(System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"]), System.Configuration.ConfigurationManager.AppSettings["SqlDateFormat"], System.Globalization.CultureInfo.InstalledUICulture);

                        WorkingdaySupplementModel dataNew = new WorkingdaySupplementModel()
                        {
                            Id = model.Id,
                            Date = model.Date,
                            SupplementTypeId = model.SupplementTypeId,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            ReasonTypeId = model.ReasonTypeId,
                            OvertimeRate = model.OvertimeRate,
                            Note = model.Note,
                            RequestStatusId = model.RequestStatusId,
                            StaffId = model.StaffId
                        };

                        listModel.Add(dataNew);
                    }
                }

            }

            var listType = MapperHelper.MapList<WorkingdaySupplementModel, WorkingdaySupplementType>(listModel);
            if (model.Id==0)
            {
                var responseCountSupplement = _workingdayService.CheckSaveWorkingdaySupplement(listType,listType.FirstOrDefault().Date.Value,CurrentUser.UserId);
                if (responseCountSupplement!=null)
                {
                    var resultCountSupplement = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(responseCountSupplement);
                    if (!CheckPermission(resultCountSupplement))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (resultCountSupplement.Results.Count() > 0 && (resultCountSupplement.Results.FirstOrDefault().SupplementTypeId != MasterDataId.EarlyDuration && resultCountSupplement.Results.FirstOrDefault().SupplementTypeId != MasterDataId.LateDuration))
                        {
                            var responese = _localizationService.GetResources("Workingday.TheRecordExceededTheAllowedNumber");
                            return Json(new { Result = responese, Resources = true }, JsonRequestBehavior.AllowGet);
                        }
                        else if (resultCountSupplement.Results.Count()>0 && resultCountSupplement.Results.FirstOrDefault().SupplementTypeId==MasterDataId.LateDuration)
                        {                           
                            validations.Add(new ValidationModel { ColumnName = "model.MissingTime", ErrorMessage = _localizationService.GetResources("Workingday.ErrorLateDuration") + " " + resultCountSupplement.Results.FirstOrDefault().OutValue + " " + _localizationService.GetResources("Workingday.Unit.Minute") });
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }
                        else if (resultCountSupplement.Results.Count() > 0 && resultCountSupplement.Results.FirstOrDefault().SupplementTypeId == MasterDataId.EarlyDuration)
                        {                         
                            validations.Add(new ValidationModel { ColumnName = "model.MissingTime", ErrorMessage = _localizationService.GetResources("Workingday.ErrorEarlyDuration") + " " + resultCountSupplement.Results.FirstOrDefault().OutValue + " " + _localizationService.GetResources("Workingday.Unit.Minute")});
                            return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            var response = _workingdayService.SaveListWorkingDaySupplement(listType, CurrentUser.UserId, isApproval);
            var responeseResources = string.Empty;
            HrmResultModel<WorkingdaySupplementModel> result = new HrmResultModel<WorkingdaySupplementModel>();
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {

                    if (model.Id == 0)
                    {
                        if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id == 0 || result.Results.Count() == 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                            result.Success = true;
                        }
                        else if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                            result.Success = false;

                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                            result.Success = false;
                        }
                    }
                    else
                    {
                        if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id == 0 || result.Results.Count() == 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                            result.Success = true;
                        }
                        else if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id != 0)
                        {
                            var stringError = string.Empty;
                            foreach (var item in result.Results)
                            {
                                if (stringError == string.Empty)
                                {
                                    stringError = _localizationService.GetLocalizedData(item.RequestStatusId.ToString() + ";" + DataType.MasterData + ";Name").ToString();
                                }
                                else
                                {
                                    stringError += "," + _localizationService.GetLocalizedData(item.RequestStatusId.ToString() + ";" + DataType.MasterData + ";Name").ToString();
                                }
                            }
                            responeseResources = _localizationService.GetResources("Workingday.NoticationFailEdit") + " " + stringError;
                            result.Success = false;
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                            result.Success = false;
                        }
                    }

                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditWorkingdaySupplement(WorkingdaySupplementModel data, long id = 0,bool isView=true,bool isSendApproval=false)
        {
            var listGroup = new List<LongTypeModel>();
            AddWorkingdaySupplementViewModel addWorkingdaySupplement_vm = new AddWorkingdaySupplementViewModel();
            addWorkingdaySupplement_vm.IsView = isView;
            addWorkingdaySupplement_vm.isSendApproval = isSendApproval;
            if (id != 0)
            {
                var response = _workingdayService.GetWorkingdaySupplementById(id);
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdaySupplement_vm.WorkingdaySupplement = result.Results.FirstOrDefault();
                        addWorkingdaySupplement_vm.SupplementTypeIdSelected = addWorkingdaySupplement_vm.WorkingdaySupplement.SupplementTypeId;
                    }
                }
            }
            else if(data.Id != 0)
            {
                var response = _workingdayService.GetWorkingdaySupplementById(data.Id);
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdaySupplement_vm.WorkingdaySupplement = result.Results.FirstOrDefault();
                        addWorkingdaySupplement_vm.SupplementTypeIdSelected = addWorkingdaySupplement_vm.WorkingdaySupplement.SupplementTypeId;
                    }
                }
            }
            else if (data != null)
            {
                addWorkingdaySupplement_vm.WorkingdaySupplement = data;
                if (data.SupplementTypeId == 0)
                {
                    addWorkingdaySupplement_vm.SupplementTypeIdSelected = MasterDataId.LateDuration;
                    addWorkingdaySupplement_vm.isClickByDate = true;
                    addWorkingdaySupplement_vm.Date = data.Date;
                }
                else
                {
                    addWorkingdaySupplement_vm.SupplementTypeIdSelected = data.SupplementTypeId;
                }
            }
            else
            {
                addWorkingdaySupplement_vm.WorkingdaySupplement = new WorkingdaySupplementModel();
                addWorkingdaySupplement_vm.SupplementTypeIdSelected = MasterDataId.LateDuration;

            }

            //get master data
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.ReasonType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SupplementType
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Coefficient
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
                addWorkingdaySupplement_vm.DataDropdownReasonType = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.ReasonType).ToList();
                addWorkingdaySupplement_vm.DataDropdownSupplementType = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SupplementType).ToList();
                addWorkingdaySupplement_vm.DataDropdownOverTimeRate = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Coefficient).ToList();

            }
            return PartialView(UrlHelpers.View("~/Views/Workingday/_AddWorkingdaySupplement.cshtml"), addWorkingdaySupplement_vm);
        }
        public ActionResult SaveSubmitApproval(List<WorkingdaySupplementApprovalModel> listModel, bool isApproval)
        {
            var type = MapperHelper.MapList<WorkingdaySupplementApprovalModel, WorkingdaySupplementApprovalType>(listModel);
            var response = _workingdayService.SaveSubmitApproval(type, CurrentUser.UserId, CurrentUser.RoleId, isApproval);
            var responeseResources = string.Empty;
            HrmResultModel<WorkingdaySupplementModel> result = new HrmResultModel<WorkingdaySupplementModel>();
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {

                    if (result.Results.Count() > 0)
                    {
                        if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id != 0)
                        {
                            if (result.Results.FirstOrDefault().RequestStatusId==10)
                            {
                                responeseResources = _localizationService.GetResources("Workingday.ErrorSendApprovalByNoHasPermission");
                            }
                            else if (isApproval == false)
                            {
                                responeseResources = _localizationService.GetResources("Workingday.ErrorSendApproval");
                            }
                            else
                            {
                                responeseResources = _localizationService.GetResources("Workingday.ErrorApproval");

                            }
                           
                            result.Success = false;
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Successful");
                            result.Success = true;
                        }

                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Successful");
                        result.Success = true;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchWorkingdaySupplement(int activeTab, int month, int year, int staffId, string searchKey)
        {
            int filterTypeSupplement = 0;
            if (activeTab != 1)
            {
                filterTypeSupplement = 1;
            }
            WorkingdaySupplementViewModel workingdaySupplement_vm = new WorkingdaySupplementViewModel();
            workingdaySupplement_vm.WorkingdaySupplements = new List<WorkingdaySupplementModel>();
            workingdaySupplement_vm.longTypes = new List<LongType>();
            DateTime date = new DateTime(year, month, DateTime.Now.Day);
            var response = _workingdayService.SearchWorkingdaySupplement(staffId, CurrentUser.UserId, CurrentUser.RoleId, date, 1, filterTypeSupplement, searchKey);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    workingdaySupplement_vm.WorkingdaySupplements = result.Results;
                }
            }
            var listGroups = new List<LongType>();

            listGroups.Add(new LongType() { Value = MasterDataId.RequestPending });
            if (activeTab == 1)
            {
                listGroups.Add(new LongType() { Value = MasterDataId.RequestDraff });
                listGroups.Add(new LongType() { Value = MasterDataId.RequestApprove });
                listGroups.Add(new LongType() { Value = MasterDataId.RequestReject });

            }
            workingdaySupplement_vm.longTypes = listGroups;
            if (activeTab != 1)
            {
                return View("~/Views/Workingday/_ListNeedApproveWorkingdaySupplenment.cshtml", workingdaySupplement_vm);

            }
            else
            {
                return View("~/Views/Workingday/_ListWorkingdaySupplenment.cshtml", workingdaySupplement_vm);

            }
        }
        public ActionResult FormNoteSaveApproval(bool isApproval, long id = 0)
        {
            WorkingdaySupplementViewModel workingdaySupplement_vm = new WorkingdaySupplementViewModel();
            workingdaySupplement_vm.IsApproval = isApproval;
            workingdaySupplement_vm.Id = id;
            return PartialView("~/Views/Workingday/_FormNoteSaveApproval.cshtml", workingdaySupplement_vm);

        }
        public ActionResult CheckNoteApproval(WorkingdaySupplementApprovalModel data)
        {
            if (data != null)
            {
                var validations = ValidationHelper.Validation(data, "");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteWorkingDaySupplement(long id)
        {
            HrmResultModel<bool> result = new HrmResultModel<bool>();
            var responeseResources = string.Empty;
            var response = _workingdayService.DeleteWorkingDaySupplement(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.UnSuccessful");
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteListWorkingDaySupplement(List<int> listId)
        {
            var listStringId = string.Join(",", listId);
            HrmResultModel<WorkingdaySupplementModel> result = new HrmResultModel<WorkingdaySupplementModel>();
            var responeseResources = string.Empty;

            var response = _workingdayService.DeleteListWorkingDaySupplement(listStringId, CurrentUser.UserId);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count() == 1 && result.Results.FirstOrDefault().Id == 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        result.Success = true;
                    }
                    else if (result.Results.Count() > 0 && result.Results.FirstOrDefault().Id != 0)
                    {
                        var stringError = string.Empty;
                        foreach (var item in result.Results)
                        {
                            if (stringError == string.Empty)
                            {
                                stringError = _localizationService.GetLocalizedData(item.RequestStatusId.ToString() + ";" + DataType.MasterData + ";Name").ToString();
                            }
                            else
                            {
                                stringError += "," + _localizationService.GetLocalizedData(item.RequestStatusId.ToString() + ";" + DataType.MasterData + ";Name").ToString();
                            }
                        }
                        responeseResources = _localizationService.GetResources("Workingday.NoticationFail") + " " + stringError;
                        result.Success = false;
                    }
                    else if (result.Results.Count() == 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                        result.Success = true;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.UnSuccessful");
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }       
        public ActionResult FormWorkingdaySupplenmentAndSummaryDetail(long staffId,DateTime date,int tab=0)
        {
            FormWorkingdaySupplenmentAndSummaryDetailViewModel viewModel = new FormWorkingdaySupplenmentAndSummaryDetailViewModel();
            viewModel.StaffId = staffId;
            viewModel.Date = date;
            viewModel.ActiveTap = tab;
            if (staffId==0)
            {
                staffId = CurrentUser.UserId;
            }
            var responseWorkingdaySummary = _workingdayService.GetWorkingdaySummaryByStaffAndDate(staffId, date);
            if (responseWorkingdaySummary!=null)
            {
                var resultWorkingdaySummary = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayModel>>(responseWorkingdaySummary);
                if (!CheckPermission(resultWorkingdaySummary))
                {
                    //return to Access Denied
                }
                else
                {
                    viewModel.WorkingdaySummary = resultWorkingdaySummary.Results.FirstOrDefault();
                }
            }

            var responeseWorkingdayCheckInOut = _workingdayService.GetWorkingCheckInOutByDate(date,staffId);
            if (responeseWorkingdayCheckInOut!=null)
            {
                var resultWorkingdayCheckInOut = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayCheckInOutModel>>(responeseWorkingdayCheckInOut);
                if (!CheckPermission(resultWorkingdayCheckInOut))
                {
                    //return to Access Denied
                }
                else
                {
                    viewModel.WorkingdayCheckInOuts = resultWorkingdayCheckInOut.Results;
                }
            }
            var responseSupplement = _workingdayService.GetWorkingdaySupplementByStaffAndDate(staffId,CurrentUser.RoleId,CurrentUser.UserId, date);
            if (responseSupplement!=null)
            {
                var resultSupplement= JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(responseSupplement);
                if (!CheckPermission(resultSupplement))
                {
                    //return to Access Denied
                }
                else
                {
                    viewModel.WorkingdaySupplements = resultSupplement.Results;
                }
            }
            var listGroups = new List<LongType>();
            listGroups.Add(new LongType() { Value = MasterDataId.RequestDraff });
            listGroups.Add(new LongType() { Value = MasterDataId.RequestPending });
            listGroups.Add(new LongType() { Value = MasterDataId.RequestReject });
            listGroups.Add(new LongType() { Value = MasterDataId.RequestApprove });
            viewModel.longTypes = listGroups;
            return PartialView("~/Views/Workingday/_FormWorkingdaySumaryAndSupplenmentDetail.cshtml", viewModel);
        }     
        public ActionResult GetSummaryByDate(DateTime date,long staffId = 0)
        {           
            List<WorkingdayCheckInOutModel> lists = new List<WorkingdayCheckInOutModel>();
            var responeseWorkingdayCheckInOut = _workingdayService.GetWorkingCheckInOutByDate(date, staffId);
            if (responeseWorkingdayCheckInOut != null)
            {
                var resultWorkingdayCheckInOut = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayCheckInOutModel>>(responeseWorkingdayCheckInOut);
                if (!CheckPermission(resultWorkingdayCheckInOut))
                {
                    //return to Access Denied
                }
                else
                {
                    lists = resultWorkingdayCheckInOut.Results;
                }
            }
            return PartialView("~/Views/Workingday/_ContentTooltipHistory.cshtml", lists);
        }
        public ActionResult GetSupplementByDate(DateTime date,long staffId = 0)
        {
            List<WorkingdaySupplementModel> lists = new List<WorkingdaySupplementModel>();
            var responseSupplement = _workingdayService.GetWorkingdaySupplementByStaffAndDate(staffId, CurrentUser.RoleId, CurrentUser.UserId, date);
            if (responseSupplement != null)
            {
                var resultSupplement = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(responseSupplement);
                if (!CheckPermission(resultSupplement))
                {
                    //return to Access Denied
                }
                else
                {
                    lists = resultSupplement.Results;
                }
            }
            return PartialView("~/Views/Workingday/_ContentTooltipSupplement.cshtml", lists);
        }

        public ActionResult ReloadDropdownStaff(long id,List<StaffModel> listStaffs,int tab=3)
        {
            DropdownListModel dropdown = new DropdownListModel();
            dropdown.ValueField = "Id";
            dropdown.DisplayField = "Name";
            if (tab==3)
            {
                dropdown.Name = "staff-summary-furlong";
            }
            else
            {
                dropdown.Name = "workingday-all-staff-select";
            }
            dropdown.SearchField = true;
            dropdown.LabelName = _localizationService.GetResources("Dropdown.Label.Staff").ToString();
            if (listStaffs != null && listStaffs.Count()>0)
            {
                if (id==0)
                {
                    dropdown.Data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listStaffs));
                }
                else
                {
                    var dataSearch = listStaffs.Where(s => s.OrganizationId == id).ToList();
                    dropdown.Data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(dataSearch));
                }
              
            }
            else
            {
                dropdown.Data = new List<dynamic>();
            }
            return PartialView("~/Views/Shared/Template/FloatingLabel/_Dropdown.cshtml", dropdown);

        }
        #region RenderTable
        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
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

            var response = GetData(type, param, out outTotalJson,out totalRecord);         
            result.CurrentPage = param.PageNumber;
            result.ItemsPerPage = param.PageSize;
            result.TotalRecord = totalRecord;
            result.TableDataUrl = TableUrl.WorkingdayGetDataUrl;
            if (outTotalJson!=string.Empty && outTotalJson!=null && outTotalJson!="")
            {
                result.Total = JsonConvert.DeserializeObject<dynamic>(outTotalJson);
            }
            if (type == TableName.Furlough)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayFurlough>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.Furlough;
                result.TableConfigName = TableConfig.WorkingdayFurlough;

            }
            else if (type == TableName.WorkingdayAllStaff)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAllStaffModel>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.WorkingdayAllStaff;
                result.TableConfigName = TableConfig.WorkingdayAllStaff;
            }

            else if (type == TableName.TableWorkingDaySupplement)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.TableWorkingDaySupplement;
                result.TableConfigName = TableConfig.WorkingDaySupplement;
            }
            else if (type == TableName.WorkingDaySupplementNeedApproval)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.WorkingDaySupplementNeedApproval;
                result.TableConfigName = TableConfig.WorkingDaySupplementApproval;
            }
            else if (type == TableName.TableWorkingdaySummaryFinal)
            {
                if (response != null)
                {
                    var resultData = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayModel>>(response);
                    if (!CheckPermission(resultData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        result.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultData.Results));
                    }
                }
                result.TableName = TableName.TableWorkingdaySummaryFinal;
                result.TableConfigName = TableConfig.WorkingdaySummaryFinal;
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
            int year = 0;
            int month = 0;
            long staffId = 0;            
            if (!string.IsNullOrEmpty(param.StringJson) && param.StringJson!=null)
            {
                var dynamic= JsonConvert.DeserializeObject<dynamic>(param.StringJson);
                month = Convert.ToInt32(dynamic["Month"]);
                year = Convert.ToInt32(dynamic["Year"]);
                staffId = Convert.ToInt32(dynamic["StaffId"]);
                if (staffId==0)
                {
                    staffId = CurrentUser.UserId;
                }
            }

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (tableName == TableName.Furlough)
            {
                outTotalJson = string.Empty;
                return this._workingdayService.GetWorkingdayFurlough(paramEntity, _userId, param.ReferenceId, out totalRecord);
            }
            if (tableName == TableName.WorkingdayAllStaff)
            {
                outTotalJson = string.Empty;
                return this._workingdayService.GetWorkingdayAllStaff(paramEntity,staffId,month,year, param.ReferenceId, out totalRecord);
            }
            if (tableName == TableName.TableWorkingDaySupplement)
            {
                //if (paramEntity.FilterField == null)
                //{
                //    paramEntity.FilterField = "StaffId = " + staffId + " AND MONTH(Date) = " + month + " AND YEAR(Date) = " + year;
                //}
                outTotalJson = string.Empty;
                return _workingdayService.GetTableWorkingdaySupplementByStaffAndMonth(paramEntity, staffId,month,year,out totalRecord);
            }

            if (tableName == TableName.WorkingDaySupplementNeedApproval)
            {
            //    if (paramEntity.FilterField == null)
            //    {
            //        paramEntity.FilterField = "AND MONTH(Date) = " + month + " AND YEAR(Date) = " + year;
            //    }
                outTotalJson = string.Empty;
                return _workingdayService.GetTableNeedApproveWorkingdaySupplement(paramEntity, staffId,month,year, out totalRecord);
            }
            if (tableName == TableName.TableWorkingdaySummaryFinal)
            {
                if (staffId==0)
                {
                    staffId = CurrentUser.UserId;
                }
                return _workingdayService.GetSummaryFinal(paramEntity, staffId, year,month,out outTotalJson, out totalRecord);
            }
            totalRecord = 0;
            outTotalJson = string.Empty;
            return string.Empty;
        }
        #endregion       
    }
}