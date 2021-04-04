using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Models;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hrm.Admin.Controllers
{
    public class PersonalIncomeTaxController : BaseController
    {
        private IPersonalIncomeTaxService _personalIncomeTaxService;
        private ITableService _tableService;
        private ITableConfigService _tableConfigService;
        private ITableColumnService _tableColumnService;
        private IMasterDataService _masterDataService;
        private long _languageId, _userId, _roleId;

        public PersonalIncomeTaxController(IPersonalIncomeTaxService personalIncomeTaxService,
                                            ITableService tableService,
                                            ITableConfigService tableConfigService,
                                            ITableColumnService tableColumnService,
                                            IMasterDataService masterDataService
                                          )
        {
            this._personalIncomeTaxService = personalIncomeTaxService;
            this._tableService = tableService;
            this._tableConfigService = tableConfigService;
            this._tableColumnService = tableColumnService;
            this._masterDataService = masterDataService;
            this._languageId = CurrentUser.LanguageId;
            this._userId = CurrentUser.UserId;
            this._roleId = CurrentUser.RoleId;
        }
        // GET: PersonalIncomeTax
        public ActionResult Index()
        {
            PersonalIncomeTaxViewModel model = new PersonalIncomeTaxViewModel();
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

            //var param = new BasicParamModel()
            //{
            //    FilterField = string.Empty,
            //    PageNumber = 1,
            //    PageSize = dataTableConfig.ItemsPerPage,
            //    LanguageId = _languageId,
            //    RoleId = _roleId,
            //    UserId = _userId,
            //    DbName = CurrentUser.DbName
            //};

            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var response = this._personalIncomeTaxService.GetCurrentPersonalIncomeTax(paramEntity);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<PersonalIncomeTaxModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.PersonalIncomeTax = result.Results.FirstOrDefault();
                    }
                }
            }

            var resultTableTaxDetailConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.PersonalIncomeTaxDetail);
            var resultConfigTaxDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableTaxDetailConfig);
            if (!CheckPermission(resultConfigTaxDetail))
            {
                //return to Access Denied
            }
            else
            {
                var Configresult = JsonConvert.DeserializeObject<TableViewModel>(resultConfigTaxDetail.Results.FirstOrDefault().ConfigData);
                Configresult.ShowFooter = false;
                Configresult.TableName = TableName.TablePersonalIncomeTaxDetail;
                Configresult.TableConfigName = TableConfig.PersonalIncomeTaxDetail;

                param.PageSize = Int32.MaxValue;
                param.ReferenceId = model.PersonalIncomeTax.Id;
                
                model.TablePersonalIncomeTaxDetails = RenderTable(Configresult, param, TableName.TablePersonalIncomeTaxDetail);
            }
            return View(model);
        }
        public ActionResult _SaveForm()
        {
            PersonalIncomeTaxViewModel model = new PersonalIncomeTaxViewModel();
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
                Value = MasterDataId.StatusAprove
            });
            listGroup.Add(new LongTypeModel()
            {
                Value = MasterDataId.Currency
            });
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var longTypeEntity = MapperHelper.MapList<LongTypeModel, LongType>(listGroup);

            //get dropdown value
            var resultMasterData = this._masterDataService.GetAllMasterDataByListGroupId(longTypeEntity);
            var response = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(resultMasterData);
            if (!CheckPermission(response))
            {
                //return to Access Denied
            }
            else
            {
                model.DataDropdownStatus = response.Results.Where(m => m.GroupId == MasterDataId.StatusAprove).ToList();
                model.DataDropdownCurrency = response.Results.Where(m => m.GroupId == MasterDataId.Currency).ToList();
            }
            // tax 
            var responseTax = this._personalIncomeTaxService.GetCurrentPersonalIncomeTax(paramEntity);
            if (responseTax != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<PersonalIncomeTaxModel>>(responseTax);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Results != null && result.Results.Count > 0)
                    {
                        model.PersonalIncomeTax = result.Results.FirstOrDefault();
                    }
                }
            }

            var resultTableTaxDetailConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.PersonalIncomeTaxDetailPopup);
            var resultConfigTaxDetailDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableTaxDetailConfig);
            if (!CheckPermission(resultConfigTaxDetailDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableTaxDetailConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigTaxDetailDetail.Results.FirstOrDefault().ConfigData);
                dataTableTaxDetailConfig.ShowFooter = false;
                dataTableTaxDetailConfig.TableName = TableName.TablePersonalIncomeTaxDetailPopup;
                dataTableTaxDetailConfig.TableConfigName = TableConfig.PersonalIncomeTaxDetailPopup;
                var param3 = new BasicParamModel()
                {
                    FilterField = "",
                    OrderBy = " Id ASC ",
                    PageNumber = 1,
                    PageSize = dataTableTaxDetailConfig.ItemsPerPage,
                    LanguageId = _languageId,
                    RoleId = _roleId,
                    UserId = _userId,
                    DbName = CurrentUser.DbName,
                    ReferenceId = model.PersonalIncomeTax.Id
                };
                model.TablePersonalIncomeTaxDetails = RenderTable(dataTableTaxDetailConfig, param3, TableName.TablePersonalIncomeTaxDetailPopup);
            }
            return PartialView(UrlHelpers.TemplateAdmin("PersonalIncomeTax", "_SaveForm.cshtml"), model);
        }
        public ActionResult _History()
        {
            TableViewModel model = new TableViewModel();
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
            
            // tax 

            var resultTableTaxDetailConfig = this._tableConfigService.GetTableConfigByTableName(TableConfig.PersonalIncomeTaxDetailHistory);
            var resultConfigTaxDetailDetail = JsonConvert.DeserializeObject<HrmResultModel<TableConfigModel>>(resultTableTaxDetailConfig);
            if (!CheckPermission(resultConfigTaxDetailDetail))
            {
                //return to Access Denied
            }
            else
            {
                var dataTableTaxDetailConfig = JsonConvert.DeserializeObject<TableViewModel>(resultConfigTaxDetailDetail.Results.FirstOrDefault().ConfigData);
                dataTableTaxDetailConfig.ShowFooter = false;
                dataTableTaxDetailConfig.TableName = TableName.TablePersonalIncomeTaxDetailHistory;
                dataTableTaxDetailConfig.TableConfigName = TableConfig.PersonalIncomeTaxDetailHistory;
                
                model = RenderTable(dataTableTaxDetailConfig, param, TableName.TablePersonalIncomeTaxDetailHistory);
            }
            return PartialView(UrlHelpers.TemplateAdmin("PersonalIncomeTax", "_History.cshtml"), model);
        }
        #region render table
        public TableViewModel RenderTable(TableViewModel tableData, BasicParamModel param, string type)
        {
            var result = new TableViewModel();
            result = JsonConvert.DeserializeObject<TableViewModel>(JsonConvert.SerializeObject(tableData.Clone()));
            //model param
            int totalRecord = 0;
            param.LanguageId = _languageId;
            param.UserId = _userId;
            param.RoleId = _roleId;
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
            if (type == TableName.TablePersonalIncomeTaxDetail || type == TableName.TablePersonalIncomeTaxDetailPopup)
            {
                totalRecord = 0;
                return this._personalIncomeTaxService.GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(paramEntity, param.ReferenceId);
            }
            if (type == TableName.TablePersonalIncomeTaxDetailHistory)
            {
                totalRecord = 0;
                return this._personalIncomeTaxService.GetPersonalIncomeTaxHistory(paramEntity);
            }
            totalRecord = 0;
            return string.Empty;
        }
        #endregion
    }
}