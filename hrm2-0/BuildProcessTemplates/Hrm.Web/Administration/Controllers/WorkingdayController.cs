using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
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
    public partial class WorkingdayController : BaseController
    {
        #region Fields
        private IMasterDataService _masterDataService;
        private ITableConfigService _tableConfigService;
        private IWorkingdayService _workingdayService;
        private ILocalizationService _localizationService;
        private ITableColumnService _tableColumnService;
        private IAnnualLeaveService _annualLeaveService;
        private IStaffService _staffService;
        private long _languageId;
        private int _totalRecord = 0;
        #endregion Fields
        #region Constructors
        public WorkingdayController(IMasterDataService masterDataService, ITableConfigService tableConfigService, ITableColumnService tableColumnService, IWorkingdayService workingdayService, ILocalizationService localizationService, IAnnualLeaveService annualLeaveService, IStaffService staffService)
        {
            _tableConfigService = tableConfigService;
            _workingdayService = workingdayService;
            _localizationService = localizationService;
            _masterDataService = masterDataService;
            _tableColumnService = tableColumnService;
            _annualLeaveService = annualLeaveService;
            _staffService = staffService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion

        #region holiday
        public ActionResult Holiday()
        {
            WorkingdayHolidayViewModel workingdayHoliday_vm = new WorkingdayHolidayViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayHoliday);
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
                    dataTableConfig.TableConfigName = TableConfig.WorkingdayHoliday;
                    dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TableWorkingdayReloadUrl;
                    workingdayHoliday_vm.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdayHoliday);
                }
            }
            return View(workingdayHoliday_vm);
        }

        public ActionResult ShowFormAddWorkingdayHoliday(long id = 0)
        {
            AddWorkingdayHolidayViewModel addWorkingdayHoliday = new AddWorkingdayHolidayViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.TypeOfWorking
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SalaryRegime
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.DesistRegime
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
                addWorkingdayHoliday.Classifys = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.TypeOfWorking).ToList();
                addWorkingdayHoliday.SalaryRegimes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SalaryRegime).ToList();
                addWorkingdayHoliday.DesistRegimes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.DesistRegime).ToList();
            }
            var responseWorkingdayShift = _workingdayService.GetListDropdownWorkingdayShift();
            if (responseWorkingdayShift!=null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftModel>>(responseWorkingdayShift);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    addWorkingdayHoliday.WorkingdayShifts = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            if (id != 0)
            {
                var responseHoliday = _workingdayService.GetWorkingDayHolidayById(id);
                if (responseHoliday != null)
                {
                    var resultHoliday = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayModel>>(responseHoliday);
                    if (!CheckPermission(resultHoliday))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayHoliday.WorkingdayHoliday = resultHoliday.Results.FirstOrDefault();
                    }
                }
                var responseWorkingdayHolidayShift = _workingdayService.GetWorkingdayHolidayShiftId(id);
                if (responseWorkingdayHolidayShift!=null)
                {
                    var resultWorkingdayHolidayShift = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayShiftModel>>(responseWorkingdayHolidayShift);
                    if (!CheckPermission(resultWorkingdayHolidayShift))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (resultWorkingdayHolidayShift.Results.Count()>0)
                        {
                            foreach (var item in resultWorkingdayHolidayShift.Results)
                            {
                                addWorkingdayHoliday.WorkingdayHoliday.ListShift.Add(item.ShiftId.ToString());
                            }
                        }
                    }
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_AddWorkingdayHoliday.cshtml"), addWorkingdayHoliday);
        }
        //public ActionResult WorkingdayHolidayBody(WorkingdayHolidayModel model)
        //{
        //    AddWorkingdayHolidayViewModel addWorkingdayHoliday = new AddWorkingdayHolidayViewModel();
        //    DateTime FromDate = new DateTime();
        //    DateTime ToDate = new DateTime();
        //    //return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_AddWorkingdayHoliday.cshtml"), addWorkingdayHoliday);
        //    if (model.IsAnnualLeave)
        //    {
        //        if (model.Id != 0)
        //        {
        //            var responseHolidayMapper = _workingdayService.GetWorkingDayHolidayMapperByHolidayId(model.Id);
        //            if (responseHolidayMapper != null)
        //            {
        //                var resultHolidayMapper = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayMapperModel>>(responseHolidayMapper);
        //                if (!CheckPermission(resultHolidayMapper))
        //                {
        //                    //return to Access Denied
        //                }
        //                else
        //                {
        //                    addWorkingdayHoliday.WorkingHolidayMappers = resultHolidayMapper.Results;
        //                }
        //            }
        //        }
        //        if (model.FromDate != null && model.ToDate != null)
        //        {
        //            FromDate = Convert.ToDateTime(model.FromDate);
        //            ToDate = Convert.ToDateTime(model.ToDate);
        //            for (DateTime date = FromDate; date <= ToDate; date = date.AddDays(1))
        //            {
        //                var filter = addWorkingdayHoliday.WorkingHolidayMappers.Where(s => s.Date == date).ToList();
        //                if (filter == null || filter.Count == 0)
        //                {
        //                    WorkingdayHolidayMapperModel dataNew = new WorkingdayHolidayMapperModel()
        //                    {
        //                        Date = date,
        //                    };

        //                    addWorkingdayHoliday.WorkingHolidayMappers.Add(dataNew);
        //                }
        //            }
        //        }
        //        else if ((model.FromDate != null && model.ToDate == null) || (model.FromDate == null && model.ToDate != null))
        //        {
        //            if (model.FromDate != null)
        //            {
        //                model.Date = Convert.ToDateTime(model.FromDate);
        //            }
        //            if (model.ToDate != null)
        //            {
        //                model.Date = Convert.ToDateTime(model.ToDate);
        //            }
        //            var filter = addWorkingdayHoliday.WorkingHolidayMappers.Where(s => s.Date == model.Date).ToList();
        //            if (filter == null || filter.Count == 0)
        //            {
        //                WorkingdayHolidayMapperModel dataNew = new WorkingdayHolidayMapperModel()
        //                {
        //                    Date = model.Date,
        //                };
        //                addWorkingdayHoliday.WorkingHolidayMappers.Add(dataNew);
        //            }

        //        }
        //        addWorkingdayHoliday.WorkingHolidayMappers = addWorkingdayHoliday.WorkingHolidayMappers.Where(s => s.Date >= FromDate && s.Date <= ToDate).ToList();
        //        var listGroup = new List<LongTypeModel>();
        //        listGroup.Add(new LongTypeModel()
        //        {
        //            Value = MasterDataId.DesistRegime
        //        });
        //        var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);
        //        var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
        //        var responseMasterDataDetail = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
        //        if (!CheckPermission(responseMasterDataDetail))
        //        {
        //            //return to Access Denied
        //        }
        //        else
        //        {
        //            addWorkingdayHoliday.DesistRegimes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.DesistRegime).ToList();
        //        }
        //    }
        //    return PartialView("~/Administration/Views/Workingday/_AddWorkingdayHolidayBody.cshtml", addWorkingdayHoliday);

        //}

        public ActionResult SaveWorkingdayHoliday(WorkingdayHolidayModel model )
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            List<WorkingdayHolidayMapperModel> listWorkingdayHolidayMapper = new List<WorkingdayHolidayMapperModel>();
            
            var entity = MapperHelper.Map<WorkingdayHolidayModel, WorkingdayHolidayEntity>(model);
            entity.CreatedBy = CurrentUser.UserId;
            entity.UpdatedBy = CurrentUser.UserId;

            var listData = new List<BaseModel>();
            for (int i = 0; i < model.ListShift.Count(); i++)
            {
                listData.Add(new BaseModel
                {
                    Id = long.Parse(model.ListShift[i])
                });
            }
            var listType = MapperHelper.MapList<BaseModel, ListDataSelectedIdType>(listData);
            string responeseResources = string.Empty;
           var result = new HrmResultModel<WorkingdayHolidayModel>();
            var response = this._workingdayService.SaveWorkingdayHoliday(entity, listType);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayModel>>(response);
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

        public ActionResult DeleteWorkingdayHoliday(long id)
        {
            var result = new HrmResultModel<WorkingdayHolidayModel>();
            string responeseResources = string.Empty;
            var response = _workingdayService.DeleteWorkingdayHoliday(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayModel>>(response);
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

            if (type == TableName.TableWorkingdayHoliday)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayHolidayModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdayHoliday;
                tableData.TableDataUrl = TableUrl.TableWorkingdayHolidayUrl;
                tableData.TableConfigName = TableConfig.WorkingdayHoliday;

            }
            else if (type == TableName.TableWorkingdayShift)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdayShift;
                tableData.TableDataUrl = TableUrl.TableWorkingdayShiftUrl;
                tableData.TableConfigName = TableConfig.WorkingdayShift;

            }
            else if (type == TableName.TableWorkingdayCalculationPeriod)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingDayCalculationPeriodModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdayCalculationPeriod;
                tableData.TableDataUrl = TableUrl.TableWorkingdayCalculationPeriodUrl;
                tableData.TableConfigName = TableConfig.WorkingdayDayCalculationPeriod;
            }
            else if (type == TableName.TableWorkingdayAnnualLeave)
            {
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAnnualLeaveModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        tableData.TableData = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                    }
                }
                tableData.TableName = TableName.TableWorkingdayAnnualLeave;
                tableData.TableDataUrl = TableUrl.TableWorkingdayAnnualLeaveUrl;
                tableData.TableConfigName = TableConfig.WorkingdayAnnualLeave;
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
            if (type == TableName.TableWorkingdayHoliday)
            {
                return this._workingdayService.GetWorkingDayHoliday(paramEntity, out totalRecord);
            }
            if (type == TableName.TableWorkingdayShift)
            {
                return this._workingdayService.GetWorkingdayShift(paramEntity, out totalRecord);
            }
            if (type == TableName.TableWorkingdayCalculationPeriod)
            {
                return this._workingdayService.GetWorkingdayPeriod(paramEntity, out totalRecord);
            }
            if (type == TableName.TableWorkingdayAnnualLeave)
            {
                return this._workingdayService.GetWorkingdayAnnualLeave(paramEntity, out totalRecord);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion

        #region Workingday-shift

        public ActionResult Shift()
        {
            WorkingdayShiftViewModel workingdayShift_vm = new WorkingdayShiftViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayShift);
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
                    dataTableConfig.TableConfigName = TableConfig.WorkingdayShift;
                    workingdayShift_vm.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdayShift);
                }
            }
            return View(workingdayShift_vm);
        }

        public ActionResult AddShift(long Id = 0)
        {
            AddWorkingdayShiftViewModel addWorkingdayShift = new AddWorkingdayShiftViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Status
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.TypeWork
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
                addWorkingdayShift.Status = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
                addWorkingdayShift.Works = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.TypeWork).ToList();
                addWorkingdayShift.TableContentAddShift.ListOvertimeRate = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Coefficient).ToList();
            }
            var param = new BasicParamModel()
            {
                FilterField = "",
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                DbName = CurrentUser.DbName,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId
            };
            int total = 0;
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responsePeriod = _workingdayService.GetWorkingdayPeriod(paramEntity, out total);
            if (responsePeriod != null)
            {
                var resultPeriod = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayPeriodModel>>(responsePeriod);
                if (!CheckPermission(resultPeriod))
                {
                    //return to Access Denied
                }
                else
                {
                    addWorkingdayShift.Periods = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(resultPeriod.Results));
                }
            }
            if (Id != 0)
            {
                var responseWorkingdayShift = _workingdayService.GetWorkingdayShiftById(Id);
                if (responseWorkingdayShift != null)
                {
                    var resultWorkingdayShift = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftModel>>(responseWorkingdayShift);
                    if (!CheckPermission(resultWorkingdayShift))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayShift.WorkingdayShift = resultWorkingdayShift.Results.FirstOrDefault();
                    }
                }
                var responseWorkingdayShiftDetail = _workingdayService.GetWorkingdayShiftDetailByShiftId(Id);
                if (responseWorkingdayShiftDetail != null)
                {
                    var resultWorkingdayShiftDetail = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftDetailModel>>(responseWorkingdayShiftDetail);
                    if (!CheckPermission(resultWorkingdayShiftDetail))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayShift.TableContentAddShift.WorkingdayShiftDetails = resultWorkingdayShiftDetail.Results;
                        if (addWorkingdayShift.WorkingdayShiftDetails.Count > 0)
                        {
                            foreach (var item in addWorkingdayShift.WorkingdayShiftDetails)
                            {
                                item.Index = Convert.ToInt32(item.Id);
                            }
                        }
                    }
                }
            }
            return View(addWorkingdayShift);
        }

        public ActionResult AddRowShiftDetail(List<WorkingdayShiftDetailModel> listModel)
        {
            TableContentAddShiftViewModel TableContentAddShift_vm = new TableContentAddShiftViewModel();
            var listGroup = new List<LongTypeModel>();
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
                TableContentAddShift_vm.ListOvertimeRate = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Coefficient).ToList();
            }
            if (listModel == null)
            {
                listModel = new List<WorkingdayShiftDetailModel>();
                listModel.Add(new WorkingdayShiftDetailModel { Index = 1 });
                TableContentAddShift_vm.WorkingdayShiftDetails = listModel;
            }
            else
            {
                int index = listModel.Count() - 1;
                listModel.Add(new WorkingdayShiftDetailModel { Index = listModel[index].Index + 1 });
                TableContentAddShift_vm.WorkingdayShiftDetails = listModel;


            }
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_tableContentListShiftDetail.cshtml"), TableContentAddShift_vm);

        }
        public ActionResult RemoveRowShiftDetail(List<WorkingdayShiftDetailModel> listModel, int index)
        {
            
             TableContentAddShiftViewModel TableContentAddShift_vm = new TableContentAddShiftViewModel();
            var itemToTemove = listModel.Single(r => r.Index == index);
            listModel.Remove(itemToTemove);
            TableContentAddShift_vm.WorkingdayShiftDetails = listModel;
            var listGroup = new List<LongTypeModel>();
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
                TableContentAddShift_vm.ListOvertimeRate = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Coefficient).ToList();
            }
            

            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_tableContentListShiftDetail.cshtml"), TableContentAddShift_vm);
        }

        public ActionResult SaveWorkingdayShift(WorkingdayShiftModel model, List<WorkingdayShiftDetailModel> listModel)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            var entity = MapperHelper.Map<WorkingdayShiftModel, WorkingdayShiftEntity>(model);
            entity.CreatedBy = CurrentUser.UserId;
            entity.UpdatedBy = CurrentUser.UserId;
            var listType = MapperHelper.MapList<WorkingdayShiftDetailModel, WorkingdayShiftDetailType>(listModel);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<WorkingdayShiftModel>();
            var response = this._workingdayService.SaveWorkingdayShift(entity, listType);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if ((result.Results.Count == 1) && result.Results.FirstOrDefault().Id == 0)
                    {
                        if (model.ShiftId != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                        result.Success = true;
                    }
                    else if (result.Results.Count == 0)
                    {
                        if (model.ShiftId != 0)
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
                        if (model.ShiftId != 0)
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

        public ActionResult ReloadTableWorkingShiftDetail(WorkingdayShiftModel model, List<WorkingdayShiftDetailModel> listModel)
        {
            //foreach (var item in listModel)
            //{
            //    decimal totalTime = 0;
            //    if (item.StartTime != null && item.EndTime != null)
            //    {
            //        TimeSpan time = new TimeSpan();
            //        time = item.EndTime.Value.Subtract(item.StartTime.Value);
            //        totalTime = Convert.ToDecimal(time.TotalHours);
            //    }
            //    if (model.WorkId == MasterDataId.WorkingDay)
            //    {
            //        item.TotalTime = totalTime;
            //    }
            //    else
            //    {
            //        item.TotalTime = 0;
            //    }
            //}
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_tableContentListShiftDetail.cshtml"), listModel);
        }

        public ActionResult DeleteWorkingdayShift(long id)
        {

            var result = new HrmResultModel<WorkingdayShiftModel>();
            string responeseResources = string.Empty;
            var response = _workingdayService.DeleteWorkingdayShift(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayShiftModel>>(response);
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
        #region workingday-calculation-Period
        public ActionResult WorkingDayCalculationPeriod()
        {
            WorkingDayCalculationPeriodViewModel workingDayCalculationPeriod = new WorkingDayCalculationPeriodViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayDayCalculationPeriod);
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
                    //for reload and filter case
                    dataTableConfig.TableConfigName = TableConfig.WorkingdayDayCalculationPeriod;
                    // for show hide column case
                    dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TableWorkingdayReloadUrl;
                    workingDayCalculationPeriod.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdayCalculationPeriod);
                }

            }
            return View(workingDayCalculationPeriod);
        }
        public ActionResult AddWorkingDayCalculationPeriod(long id = 0)
        {
            AddWorkingDayCalculationPeriodViewModel addWorkingDayCalculationPeriod = new AddWorkingDayCalculationPeriodViewModel();
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Status
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.OrderWeekOfMonth
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
                addWorkingDayCalculationPeriod.ListStatus = responseMasterDataDetail.Results.Where(x => x.GroupId == MasterDataId.Status).ToList();
                addWorkingDayCalculationPeriod.ListOrderWeekOfMonth = responseMasterDataDetail.Results.Where(x => x.GroupId == MasterDataId.OrderWeekOfMonth).ToList();
            }
            if (id != 0)
            {
                var response = _workingdayService.GetWorkDayByPeroidId(id);
                if (response != null)
                {

                    var result = JsonConvert.DeserializeObject<HrmResultModel<WorkDayModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingDayCalculationPeriod.WorkDays = result.Results.ToList();
                    }
                }
                var responseCalculationPeriod = _workingdayService.GetWorkingDayCalculationPeriodById(id);
                if (responseCalculationPeriod != null)
                {

                    var resultCalculationPeriod = JsonConvert.DeserializeObject<HrmResultModel<WorkingDayCalculationPeriodModel>>(responseCalculationPeriod);
                    if (!CheckPermission(resultCalculationPeriod))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingDayCalculationPeriod.WorkingDayCalculationPeriod = resultCalculationPeriod.Results.FirstOrDefault();
                    }
                }
            }
            return View(addWorkingDayCalculationPeriod);
        }
        public ActionResult SaveWorkingdayCalculationPeriod(WorkingDayCalculationPeriodModel model, List<WorkDayModel> listWorkDay)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            var entity = MapperHelper.Map<WorkingDayCalculationPeriodModel, WorkingdayPeriodEntity>(model);
            entity.CreatedBy = CurrentUser.UserId;
            entity.UpdatedBy = CurrentUser.UserId;
            string responeseResources = string.Empty;
            var result = new HrmResultModel<WorkingDayCalculationPeriodModel>();
            var listWorkday = MapperHelper.MapList<WorkDayModel, WorkDayType>(listWorkDay);
            var response = this._workingdayService.SaveWorkingdayCalculationPeriod(entity, listWorkday);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingDayCalculationPeriodModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results.Count == 0 || result.Results == null)
                    {
                        if (model.AutoId != 0)
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
                        if (model.AutoId != 0)
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
        public ActionResult DeleteWorkingdayCalculationPeroid(long id)
        {
            var result = new HrmResultModel<WorkingDayCalculationPeriodModel>();
            string responeseResources = string.Empty;
            var response = _workingdayService.DeleteWorkingdayCalculationPeroid(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingDayCalculationPeriodModel>>(response);
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

        #region Workingday-AnnualLeave
        public ActionResult AnnualLeave()
        {
            WorkingdayAnnualLeaveViewModel workingdayAnnualLeave_vm = new WorkingdayAnnualLeaveViewModel();
            workingdayAnnualLeave_vm.ActiveTab = 0;
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.WorkingdayAnnualLeave);
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
                    //for reload and filter case
                    dataTableConfig.TableConfigName = TableConfig.WorkingdayAnnualLeave;
                    // for show hide column case
                    dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TableWorkingdayReloadUrl;
                    workingdayAnnualLeave_vm.Table = RenderTable(dataTableConfig, param, TableName.TableWorkingdayAnnualLeave);
                }
            }
            var responseAnnualLeave = _annualLeaveService.GetAnnualLeave();
            if (responseAnnualLeave != null)
            {
                var resultAnnualLeave = JsonConvert.DeserializeObject<HrmResultModel<AnnualLeaveModel>>(responseAnnualLeave);
                if (!CheckPermission(resultAnnualLeave))
                {
                    //return to Access Denied
                }
                else
                {
                    workingdayAnnualLeave_vm.AnnualLeave = resultAnnualLeave.Results.FirstOrDefault();
                }
            }
            return View(workingdayAnnualLeave_vm);
        }

        public ActionResult SaveAnnualLeave(AnnualLeaveModel model)
        {
            var entity = MapperHelper.Map<AnnualLeaveModel, AnnualLeaveEntity>(model);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<AnnualLeaveModel>();
            var response = _workingdayService.SaveAnnualLeave(entity, 0);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<AnnualLeaveModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        result.Success = true;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowFormAddWorkingdayAnnualLeave(long id = 0)
        {
            AddWorkingdayAnnualLeaveViewModel addWorkingdayAnnualLeave = new AddWorkingdayAnnualLeaveViewModel();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = 10,
                LanguageId = _languageId,
                RoleId = CurrentUser.RoleId,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
            if (responseStaff != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    addWorkingdayAnnualLeave.Staffs = result.Results;
                }
            }
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Status
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.SalaryRegime
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.PeriodApply
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
                addWorkingdayAnnualLeave.Status = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.Status).ToList();
                addWorkingdayAnnualLeave.PeriodApplys = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.PeriodApply).ToList();
                addWorkingdayAnnualLeave.Regimes = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.SalaryRegime).ToList();
            }
            if (id != 0)
            {
                var responseWorkingdayAnnualLeave = _workingdayService.GetWorkingdayAnnualLeaveById(id);
                if (responseWorkingdayAnnualLeave != null)
                {
                    var resultWorkingdayAnnualLeave = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAnnualLeaveModel>>(responseWorkingdayAnnualLeave);
                    if (!CheckPermission(resultWorkingdayAnnualLeave))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayAnnualLeave.WorkingdayAnnualLeave = resultWorkingdayAnnualLeave.Results.FirstOrDefault();
                    }
                }

                var responseWorkingdayAnnualLeaveStaffMapper = _workingdayService.GetWorkingdayAnnualLeaveStaffMapperByAnnualLeaveId(id);
                if (responseWorkingdayAnnualLeaveStaffMapper != null)
                {
                    var resultWorkingdayAnnualLeaveStaffMapper = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAnnualLeaveModel>>(responseWorkingdayAnnualLeaveStaffMapper);
                    if (!CheckPermission(resultWorkingdayAnnualLeaveStaffMapper))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        if (resultWorkingdayAnnualLeaveStaffMapper.Results.Count > 0)
                        {
                            for (int i = 0; i < resultWorkingdayAnnualLeaveStaffMapper.Results.Count; i++)
                            {
                                addWorkingdayAnnualLeave.ListCheckbox.Add(Convert.ToInt32(resultWorkingdayAnnualLeaveStaffMapper.Results[i].StaffId));
                            }
                        }

                    }
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_AddAnnualLeave.cshtml"), addWorkingdayAnnualLeave);
        }

        public ActionResult SaveWorkingdayAnnualLeave(WorkingdayAnnualLeaveModel model, List<WorkingdayAnnualLeaveStaffMapperModel> list)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            var entity = MapperHelper.Map<WorkingdayAnnualLeaveModel, WorkingdayAnnualLeaveEntity>(model);
            entity.CreatedBy = CurrentUser.UserId;
            var listType = MapperHelper.MapList<WorkingdayAnnualLeaveStaffMapperModel, WorkingdayAnnualLeaveStaffMapperType>(list);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<WorkingdayAnnualLeaveModel>();
            var response = _workingdayService.SaveWorkingdayAnnualLeave(entity, listType);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAnnualLeaveModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
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

        public ActionResult DeleteWorkingdayAnnualLeave(long id)
        {
            var result = new HrmResultModel<WorkingdayAnnualLeaveModel>();
            string responeseResources = string.Empty;
            var response = _workingdayService.DeleteWorkingdayAnnualLeaveById(id);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayAnnualLeaveModel>>(response);
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
        public ActionResult SearchStaff(string searchKey, List<int> listStaffId)
        {
            AddWorkingdayAnnualLeaveViewModel addWorkingdayAnnualLeave = new AddWorkingdayAnnualLeaveViewModel();
            if (searchKey == "" || searchKey == string.Empty)
            {
                var param = new BasicParamModel()
                {
                    FilterField = string.Empty,
                    PageNumber = 1,
                    PageSize = 10,
                    LanguageId = _languageId,
                    RoleId = CurrentUser.RoleId,
                    UserId = CurrentUser.UserId,
                    DbName = CurrentUser.DbName
                };
                var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
                var responseStaff = _staffService.GetStaff(paramEntity, out _totalRecord);
                if (responseStaff != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayAnnualLeave.Staffs = result.Results;
                    }
                }
            }
            else
            {
                var response = _staffService.SearchPermissionStaff(searchKey);
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        addWorkingdayAnnualLeave.Staffs = result.Results;
                    }
                }
            }

            if (listStaffId == null)
            {
                listStaffId = new List<int>();
            }
            addWorkingdayAnnualLeave.ListCheckbox = listStaffId;
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_ListStaffSelectCheckbox.cshtml"), addWorkingdayAnnualLeave);
        }

        public ActionResult ShowFormEditAnnualLeave()
        {
            EditAnnualLeaveViewModel annualLeave_vm = new EditAnnualLeaveViewModel();
            var responseAnnualLeave = _annualLeaveService.GetAnnualLeave();
            if (responseAnnualLeave != null)
            {
                var resultAnnualLeave = JsonConvert.DeserializeObject<HrmResultModel<AnnualLeaveModel>>(responseAnnualLeave);
                if (!CheckPermission(resultAnnualLeave))
                {
                    //return to Access Denied
                }
                else
                {
                    annualLeave_vm.AnnualLeave = resultAnnualLeave.Results.FirstOrDefault();
                }
            }
            var listGroup = new List<LongTypeModel>();
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.HandlingSurplus
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
                annualLeave_vm.HandlingAnnualLeaves = responseMasterDataDetail.Results.Where(m => m.GroupId == MasterDataId.HandlingSurplus).ToList();

            }
            return PartialView(UrlHelpers.TemplateAdmin("Workingday", "_EditAnnualLeave.cshtml"), annualLeave_vm);
        }


        public ActionResult EditAnnualLeave(AnnualLeaveModel model)
        {
            if (model != null)
            {
                var validations = ValidationHelper.Validation(model, "model");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            var entity = MapperHelper.Map<AnnualLeaveModel, AnnualLeaveEntity>(model);
            string responeseResources = string.Empty;
            var result = new HrmResultModel<AnnualLeaveModel>();
            var response = _workingdayService.SaveAnnualLeave(entity, 1);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<AnnualLeaveModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        result.Success = false;
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        result.Success = true;
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }

        #endregion
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
                TableViewModel tableDataResult = RenderTable(tableData, param, tableData.TableName);
                tableDataResult.TableDataUrl = tableData.TableDataUrl;
                tableDataResult.TableReloadConfigUrl = tableData.TableReloadConfigUrl;
                tableDataResult.TableConfigName = tableData.TableConfigName;
                tableDataResult.TableName = tableData.TableName;
                return View(UrlHelpers.Template("_TableContent.cshtml"), tableDataResult);
            }
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
        #endregion
    }
}