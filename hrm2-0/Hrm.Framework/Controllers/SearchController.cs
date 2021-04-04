using Hrm.Framework.Context;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Hrm.Service;
using System.Web;
using System;
using Hrm.Common;
using Hrm.Framework;
using System.Web.Security;
using static Hrm.Common.Constant;
using Hrm.Framework.Helper;
using System.Collections.Generic;
using System.Linq;
using Hrm.Common.Helpers;
using Hrm.Core.Infrastructure;
using Hrm.Repository.Type;
using Hrm.Framework.ViewModels;

namespace Hrm.Framework.Controllers
{
    public class SearchController : BaseController
    {
        private IMasterDataService _masterDataService;
        private IOrganizationService _organizationService;
        private ITableConfigService _tableConfigService;
        private IStaffService _staffService;

        public SearchController(
            IMasterDataService masterDataService,
            IOrganizationService organizationService,
            ITableConfigService tableConfigService,
            IStaffService staffService
            )
        {
            _masterDataService = masterDataService;
            _organizationService = organizationService;
            _tableConfigService = tableConfigService;
            _staffService = staffService;
        }
        public ActionResult SearchMasterDataWithGroupId(string searchKey, bool isCategory)
        {
            var model = new List<MasterDataModel>();
            var response = this._masterDataService.SearchMasterData(searchKey, isCategory, CurrentUser.LanguageId);
            if (response != null)
            {
                var resultMasterData = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(response);
                if (!CheckPermission(resultMasterData))
                {
                    //return to Access Denied
                }
                else
                {
                    model = resultMasterData.Results;
                }
            }
            return View("~/Administration/Views/MasterData/_CategoryMasterData.cshtml", model);
        }
        public ActionResult SearchMasterDataCategory(string searchKey)
        {
            return SearchMasterDataWithGroupId(searchKey, true);
        }
        public ActionResult SearchOrganization(string searchKey)
        {
            var treeViewModel = new TreeViewModel();
            var model = new List<OrganizationModel>();
            HrmResultModel<OrganizationModel> result = new HrmResultModel<OrganizationModel>();
            var response = this._organizationService.SearchOrganization(CurrentUser.LanguageId, searchKey);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<OrganizationModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    model = result.Results;
                    if (model.Count > 0)
                    {

                        treeViewModel.MinParent = Convert.ToInt32(model[0].ParentId);
                        bool isOk = false;
                        foreach (var item in model)
                        {
                            int count = model.Where(s => s.ParentId == item.Id).ToList().Count();
                            if (count>0)
                            {
                                if (item.ParentId <= treeViewModel.MinParent)
                                {
                                    treeViewModel.MinParent = Convert.ToInt32(item.ParentId);
                                    isOk = true;
                                }
                            }
                           
                        }
                        if (isOk==false && model.Count>1)
                        {
                            foreach (var item in model)
                            {
                                if (item.ParentId < treeViewModel.MinParent)
                                {
                                    treeViewModel.MinParent = Convert.ToInt32(item.ParentId);                                }

                            }
                        }
                    }

                }
            }

            //var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(model));
            treeViewModel.TreeData = model;
            treeViewModel.DisplayField = "OrganizationName";
            treeViewModel.ValueField = "OrganizationCode";
            treeViewModel.Name = "tree-organization";
            treeViewModel.IsFilter = true;
            return View(UrlHelpers.Template("_TreeView.cshtml"), treeViewModel);
        }
        public ActionResult GlobalSearch(string searchKey, string menuSearch)
        {
            TableViewModel result_vm = new TableViewModel();
            var resultTableConfig = this._tableConfigService.GetTableConfigByTableName(menuSearch + "Search");
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
                    LanguageId = CurrentUser.LanguageId,
                    RoleId = CurrentUser.RoleId,
                    UserId = CurrentUser.UserId,
                    DbName = CurrentUser.DbName
                };
                result_vm = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(dataTableConfig.Clone()));

                //Gọi hàm lấy thông tin 
                var totalRecord = 0;
                var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

                var response = string.Empty;
                if (menuSearch == TableConfig.Staff)
                {
                    response = _staffService.SearchStaff(paramEntity, searchKey, out totalRecord);
                    result_vm.TableDataUrl = "Search/GetStaffData?searchkey=" + searchKey;
                }

                var resultData = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(response);
                if (!CheckPermission(resultData))
                {
                    //return to Access Denied
                }
                else
                {
                    result_vm.TableData = resultData.Results;
                }
                result_vm.CurrentPage = param.PageNumber;
                result_vm.ItemsPerPage = param.PageSize;
                result_vm.TotalRecord = totalRecord;

                var resultMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, CurrentUser.LanguageId);
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
                    result_vm.lstItemsPerPage = dataDropDownListDynamic;
                }
                result_vm.TableName = TableName.TableSearchStaff;
            }

            return View(UrlHelpers.View("~/Views/Search/_Result.cshtml"), result_vm);
        }        
        public ActionResult GetStaffData(TableViewModel tableData, BasicParamModel param)
        {
            var query = Request.Url.Query;
            var searchKey = HttpUtility.ParseQueryString(query).Get("searchkey");
            if (!string.IsNullOrEmpty(searchKey))
            {
                tableData = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(tableData.Clone()));
                //Gọi hàm lấy thông tin 
                var totalRecord = 0;
                var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);

                var response = string.Empty;

                response = _staffService.SearchStaff(paramEntity, searchKey, out totalRecord);
                tableData.TableDataUrl = "Search/GetStaffData?searchkey=" + searchKey;

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

                var resultMasterDataSelectList = this._masterDataService.GetAllMasterDataByName(MasterGroup.ItemsPerPage, CurrentUser.LanguageId);
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
                tableData.TableName = TableName.TableSearchStaff;
            }
            return View(UrlHelpers.Template("_TableContent.cshtml"), tableData);
        }
    }
}