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

namespace Hrm.Web.Controllers
{
    public class PayrollController : BaseController
    {
        #region Fields
        private IPayrollService _payrollService;
        private ITableService _tableService;
        private ITableConfigService _tableConfigService;
        private IMasterDataService _masterDataService;
        private IMenuService _menuService;
        private IRoleService _roleService;
        private IChecklistDetailService _checklistDetailService;
        private ITableColumnService _tableColumnService;
        private IAttachmentService _attachmentService;
        private IChecklistService _checklistService;
        private ILocalizationService _localizationService;
        private ISalaryTypeService _salaryTypeService;
        private ISalaryElementService _salaryElementService;
        private ISalaryService _salaryService;

        private long _languageId, _userId, _roleId;
        private int _totalRecord;
        #endregion Fields
        #region Constructors
        public PayrollController(IPayrollService payrollService, ITableService tableService, ITableConfigService tableConfigService, IMasterDataService masterDataService, IMenuService menuService, IRoleService roleService, IChecklistDetailService checklistDetailService, ITableColumnService tableColumnService, IAttachmentService attachmentService, IChecklistService checklistService, ILocalizationService localizationService, ISalaryTypeService salaryTypeService, ISalaryElementService salaryElementService, ISalaryService salaryService)
        {
            this._payrollService = payrollService;
            this._tableService = tableService;
            this._tableConfigService = tableConfigService;
            this._masterDataService = masterDataService;
            this._menuService = menuService;
            this._roleService = roleService;
            this._checklistDetailService = checklistDetailService;
            this._tableColumnService = tableColumnService;
            this._attachmentService = attachmentService;
            this._checklistService = checklistService;
            this._localizationService = localizationService;
            this._salaryTypeService = salaryTypeService;
            this._salaryElementService = salaryElementService;
            this._salaryService = salaryService;
            _languageId = CurrentUser.LanguageId;
            _userId = CurrentUser.UserId;
            this._roleId = CurrentUser.RoleId;
        }
        #endregion
        public ActionResult List()
        {
            PayrollViewModel payroll_vm = new PayrollViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Payroll);
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
                dataTableConfig.TableDataUrl = TableUrl.PayrollGetDataUrl;
                dataTableConfig.TableReloadConfigUrl = TableReloadUrl.TablePayrollReloadUrl;
                dataTableConfig.TableConfigName = TableConfig.Payroll;
                dataTableConfig.TableName = TableName.TablePayroll;
                payroll_vm.Table = RenderTable(dataTableConfig, param, TableName.TablePayroll);
            }
            return View(payroll_vm);
        }

        public ActionResult GetData(TableViewModel tableData, BasicParamModel param)
        {
            tableData = RenderTable(tableData, param, tableData.TableName);
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }

        #region RenderTable

        public TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.UserId = _userId;
            param.RoleId = _roleId;
            param.DbName = CurrentUser.DbName;

            //Gọi hàm lấy thông tin 
            string temp;
            var response = GetData(type, param, out temp, out totalRecord);
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

        private string GetData(string type, BasicParamModel param, out string outTotalJson, out int totalRecord)
        {
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            if (type == TableName.TablePayroll)
            {
                outTotalJson = string.Empty;
                return this._payrollService.GetPayroll(paramEntity, out totalRecord);
            }
            else if (type == TableName.TableSalary)
            {
                if (!String.IsNullOrEmpty(param.StringJson) && param.StringJson != null && param.StringJson != "")
                {
                    var dynamic = JsonConvert.DeserializeObject<dynamic>(param.StringJson);
                    int month = Convert.ToInt32(dynamic["Month"]);
                    int year = Convert.ToInt32(dynamic["Year"]);
                    int payrollId = Convert.ToInt32(dynamic["PayrollId"]);
                    int staffId = Convert.ToInt32(dynamic["StaffId"]);
                    return _salaryService.GetSalary(paramEntity, payrollId, month, year, staffId, out outTotalJson, out totalRecord);
                }
            }
            totalRecord = 0;
            outTotalJson = string.Empty;
            return string.Empty;
        }
        #endregion

        public ActionResult Add()
        {
            #region MyRegion
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
                Value = MasterDataId.PayrollStatus
            });
            #endregion

            var model = new PayrollDetailViewModel()
            {
                Payroll = new PayrollModel()
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
                model.DataDropdownPayrollStatus = response.Results.Where(m => m.GroupId == MasterDataId.PayrollStatus).ToList();
            }

            var resultSalaryType = this._salaryTypeService.GetAllSalaryTypeForDropDown(paramEntity);
            model.DataDropdownSalaryType = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultSalaryType).Results;

            return View(model);
        }

        public ActionResult Update(int id)
        {
            #region MyRegion
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
                Value = MasterDataId.PayrollStatus
            });
            #endregion
            var model = new PayrollDetailViewModel()
            {
                Payroll = new PayrollModel()
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var listGropuId = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);

            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(listGropuId);
            var responseMasterData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(responseMasterData))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownPayrollStatus = responseMasterData.Results.Where(m => m.GroupId == MasterDataId.PayrollStatus).ToList();
            }

            var resultSalaryType = this._salaryTypeService.GetAllSalaryTypeForDropDown(paramEntity);
            var responseSalaryType = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultSalaryType);
            if (!CheckPermission(responseSalaryType))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownSalaryType = responseSalaryType.Results;
            }

            var resultPayrollGetById = this._payrollService.GetPayrollById(id, paramEntity);
            var responsePayrollGetById = JsonConvert.DeserializeObject<HrmResultModel<PayrollModel>>(resultPayrollGetById);
            if (!CheckPermission(responsePayrollGetById))
            {
                //return to Access Denied
            }
            else
            {
                var resPayrolls = responsePayrollGetById.Results;
                model.Payroll = resPayrolls.FirstOrDefault();
            }
            return View(model);
        }

        //public ActionResult Detail(int id)
        //{
        //    return View();
        //}

        public ActionResult CheckPayroll(int id, int month, int year)
        {
            GeneralnformationAndSalaryViewModel generalnformationAndDetailSalary_vm = new GeneralnformationAndSalaryViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.Salary);
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
                        StringJson = "{Month:" + month + ",Year:" + year + ",PayrollId:" + id + ",StaffId:" + CurrentUser.UserId + "}",
                        PageSize = dataTableConfig.ItemsPerPage,
                        LanguageId = _languageId,
                        DbName = CurrentUser.DbName
                    };
                    dataTableConfig.TableConfigName = TableConfig.Salary;
                    dataTableConfig.TableDataUrl = TableUrl.TableSalary;
                    generalnformationAndDetailSalary_vm.PayrollId = id;
                    generalnformationAndDetailSalary_vm.Salary.Table = RenderTable(dataTableConfig, param, TableName.TableSalary);
                }
            }
            return View(generalnformationAndDetailSalary_vm);
        }

        public ActionResult Summary(int id)
        {
            #region MyRegion
            var payroll = new PayrollModel();
            var payrollDetail_vm = new PayrollDetailViewModel();
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };
            #endregion

            var paramType = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = _payrollService.GetPayrollById(id, paramType);
            var result = JsonConvert.DeserializeObject<HrmResultModel<PayrollModel>>(response);
            if (!CheckPermission(result))
            {
                //return to Access Denied
            }
            else
            {
                if (result.Results.Count > 0)
                {
                    payrollDetail_vm.Payroll = result.Results.FirstOrDefault();
                }
            }
            return View(payrollDetail_vm);
        }

        public ActionResult SavePayroll(PayrollModel model)
        {
            model = MapperHelper.ConvertModel(model);
            var validations = new List<ValidationModel>();
            validations.AddRange(ValidationHelper.Validation(model, ""));
            if (validations.Count > 0)
            {
                return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
            }
            var success = false;
            var msg = "";
            var payrollResult = new PayrollModel();
            var payroll = MapperHelper.Map<PayrollModel, PayrollEntity>(model);
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _userId,
                DbName = CurrentUser.DbName
            };

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

            var payrollResponse = this._payrollService.SavePayroll(payroll, paramEntity);

            if (payrollResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<PayrollModel>>(payrollResponse);
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
                        payrollResult = result.Results.FirstOrDefault();
                    }
                }
            }
            return Json(new { Result = payrollResult, Message = msg, Success = success }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFormSalaryPaySlip(int salaryTypeId)
        {
            SalaryPaySlipViewModel salaryPaySlip = new SalaryPaySlipViewModel();
            salaryPaySlip.IsViewOrder = false;
            salaryPaySlip.IsSave = false;
            var salaryElementResponse = this._salaryElementService.GetSalaryElementBySalaryTypeId(salaryTypeId);
            //var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(payrollResponse);
            if (salaryElementResponse != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<SalaryElementModel>>(salaryElementResponse);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        salaryPaySlip.SalaryElements = result.Results;
                    }
                }
            }
            return PartialView(UrlHelpers.View("~/Views/Shared/_SalaryPaySlipTemplate.cshtml"), salaryPaySlip);
        }

        //public ActionResult Latch(IEnumerable<string> modelLst)
        //{
        //    return Json(new { Result = staffResult, Message = msg, Success = success }, JsonRequestBehavior.AllowGet);
        //}
    }
}