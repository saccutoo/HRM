using Hrm.Common;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Models;
using Hrm.Repository.Type;
using Hrm.Service;
using Hrm.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrm.Web.Controllers
{
    public class DashboardController : BaseController
    {
        #region Fields
        private IDashboardService _dashboardService;
        private IWorkingdayService _workingdayService;
        private IStaffService _staffService;
        private long _languageId;
        private long _userId;
        private long _roleId;
        #endregion Fields
        #region Constructors
        public DashboardController(IDashboardService dashboardService, IWorkingdayService workingdayService, IStaffService staffService)
        {
            this._dashboardService = dashboardService;
            this._workingdayService = workingdayService;
            this._staffService = staffService;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._userId = CurrentUser.UserId;
                this._languageId = CurrentUser.LanguageId;
                this._roleId = 1;
            }

        }
        #endregion
        // GET: Dashboard
        public ActionResult Detail(int? tab)
        {
            var model = new DashboardViewModel();
            model.ActiveTab = tab ?? 0;
            switch (model.ActiveTab)
            {
                case 0: // tab chung
                    DateTime currentDate = DateTime.Now;
                    DateTime periodDate = DateTime.Now.AddMonths(-1);
                    var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    var firstDayOfMonthPeriod = new DateTime(periodDate.Year, periodDate.Month, 1);
                    var lastDayOfMonthPeriod = firstDayOfMonthPeriod.AddMonths(1).AddDays(-1);
                    var response = _dashboardService.GetSummary(firstDayOfMonth, lastDayOfMonth, firstDayOfMonthPeriod, lastDayOfMonthPeriod);
                    if (response != null)
                    {
                        var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardSummaryModel>>(response);
                        if (!CheckPermission(resultDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (resultDetail.Results.Count > 0)
                            {
                                model.DashboardSummary = resultDetail.Results.FirstOrDefault();
                            }
                        }
                    }
                    var dbEvent = _dashboardService.GetStaffOnboardOrBirthdayByParentId();
                    if (dbEvent != null)
                    {
                        var dbEventDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardEventViewModel>>(dbEvent);
                        if (!CheckPermission(dbEventDetail))
                        {
                            //return to Access Denied
                        }
                        else
                        {
                            if (dbEventDetail.Results.Count > 0)
                            {
                                model.DashboardEvent = dbEventDetail.Results;
                            }
                        }
                    }
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
                        model.Staffs = staffResult.Results;
                    }

                    var supplementNeedApprovalResponse = _workingdayService.GetNeedApproveWorkingdaySupplement(CurrentUser.UserId, CurrentUser.UserId, CurrentUser.RoleId, DateTime.Now, 1);
                    var supplementNeedApprovalResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementNeedApprovalResponse);
                    if (!CheckPermission(supplementNeedApprovalResult))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.WorkingdaySupplements = supplementNeedApprovalResult.Results;
                    }
                    break;
                case 1: // tab cá nhân
                    model.DashboardPersonal.StaffId = this._userId;
                   List<WorkingdayModel> wkdayResult = GetWorkingDays(_userId, DateTime.UtcNow.Month, DateTime.UtcNow.Year);
                    if (wkdayResult.Count > 0 && wkdayResult.FirstOrDefault(x => x.Date.ToString("dd/MM/yyyy") == DateTime.UtcNow.ToString("dd/MM/yyyy")) != null)
                    {
                        model.DashboardPersonal.TimeIn = wkdayResult.FirstOrDefault(x => x.Date.ToString("dd/MM/yyyy") == DateTime.UtcNow.ToString("dd/MM/yyyy")).StartTime;
                        model.DashboardPersonal.TimeOut = wkdayResult.FirstOrDefault(x => x.Date.ToString("dd/MM/yyyy") == DateTime.UtcNow.ToString("dd/MM/yyyy")).EndTime;
                    }
                    var lstStaff = _staffService.GetStaffByParentId(CurrentUser.UserId);
                    var lstStaffResult = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(lstStaff);
                    if (!CheckPermission(lstStaffResult))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.DashboardPersonal.StaffMangers = lstStaffResult.Results.ToList();

                    }
                    break;
            }

            return View(model);
        }
        public ActionResult GetSummary(DateTime startDate, DateTime endDate, DateTime startPeriod, DateTime endPeriod)
        {
            var result = new DashboardSummaryModel();
            var response = _dashboardService.GetSummary(startDate, endDate, startPeriod, endPeriod);
            if (response != null)
            {
                var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardSummaryModel>>(response);
                if (!CheckPermission(resultDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDetail.Results.Count > 0)
                    {
                        result = resultDetail.Results.FirstOrDefault();
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTurnoverrate(DateTime? startDate, DateTime? endDate, int? viewType)
        {
            var result = new List<DashboardTurnoverRateModel>();
            var response = _dashboardService.GetTurnoverrate(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue, viewType ?? 0);
            if (response != null)
            {
                var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardTurnoverRateModel>>(response);
                if (!CheckPermission(resultDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDetail.Results.Count > 0)
                    {
                        result = resultDetail.Results;
                    }
                }
            }
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetImplementation(DateTime? startDate, DateTime? endDate, int? viewType)
        {
            var result = new List<DashboardImplementationModel>();
            var response = _dashboardService.GetImplementation(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue, viewType ?? 0);
            if (response != null)
            {
                var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardImplementationModel>>(response);
                if (!CheckPermission(resultDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDetail.Results.Count > 0)
                    {
                        result = resultDetail.Results;
                    }
                }
            }
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private List<WorkingdayModel> GetWorkingDays(long staffId, int month, int year)
        {
            List<WorkingdayModel> wk_VM = new List<WorkingdayModel>();
            var response= _workingdayService.GetWorkingdayByStaffAndMonth(staffId, month, year);
            var result = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                wk_VM = result.Results.ToList();
            }
            return wk_VM;
        }
        private List<WorkingdaySupplementModel> GetWorkingdaySupplements(long staffId, int month, int year)
        {
            List<WorkingdaySupplementModel> wk_SupplementVM = new List<WorkingdaySupplementModel>();
            var supplementResponse = _workingdayService.GetWorkingdaySupplementByStaffAndMonth(staffId, month, year, "");
            var supplementResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdaySupplementModel>>(supplementResponse);
            if (!CheckPermission(supplementResult))
            {
                //return to Access Denied
            }
            else
            {
                wk_SupplementVM = supplementResult.Results;
            }
            return wk_SupplementVM;
        }
        public ActionResult ReloadDataWorkingSummary(int month, int year)
        {
            var model = new DashboardViewModel();
            model.DashboardPersonal.Workingdays = GetWorkingDays(_userId, month, year);
            model.DashboardPersonal.WorkingdaySupplements = GetWorkingdaySupplements(_userId, month, year);

            return PartialView(UrlHelpers.View("~/Views/Dashboard/_WorkingSummary.cshtml"), model.DashboardPersonal);
        }
        public ActionResult GetWorkingDayChart(int type, string year, long staffId)
        {
            var result = new List<DashboardWorkingdayChartModel>();
            var response = _dashboardService.GetWorkingDayChart(type, year, staffId);
            if (response != null)
            {
                var resultDetail = JsonConvert.DeserializeObject<HrmResultModel<DashboardWorkingdayChartModel>>(response);
                if (!CheckPermission(resultDetail))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDetail.Results.Count > 0)
                    {
                        result = resultDetail.Results;
                    }
                }
            }
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult ReloadNotePieChart(DashboardSummaryModel model)
        {
            return PartialView(UrlHelpers.View("~/Views/Dashboard/_Note_PieChart.cshtml"), model);
        }

    }

}