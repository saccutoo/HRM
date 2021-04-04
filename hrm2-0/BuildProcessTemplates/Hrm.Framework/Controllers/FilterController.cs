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
using System.Globalization;

namespace Hrm.Framework.Controllers
{
    public class FilterController : BaseController
    {
        public ActionResult ShowDialog(ITableColumnService tableColumnService, IMasterDataService masterDataService, string tableName, TableViewModel tableViewModel, string filterConfig, string tableUrl, string isFilter, string groupId)
        {
            var model = new FilterViewModel();
            model.TableName = tableName;
            model.TableUrl = tableUrl;
            model.IsFilter = isFilter;
            model.GroupId = groupId;

            var responseColumn = tableColumnService.GetTableColumn(tableName, false, 0, int.Parse(groupId));
            if (responseColumn != null)
            {
                var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<TableColumnModel>>(responseColumn);
                if (!CheckPermission(resultColumn))
                {
                    //return to Access Denied
                }
                else
                {
                    model.Columns = resultColumn.Results;
                    foreach (var field in resultColumn.Results)
                    {
                        model.Columns.Where(x => x.ColumnName == field.SqlAlias).ToList().ForEach(x =>
                        {
                            x.SqlAlias = field.SqlAlias;
                        });
                    }

                    if (model.IsFilter == "1")
                    {
                        for (int i = 0; i < model.Columns.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(model.Columns[i].OriginalColumnName))
                            {
                                model.Columns[i].ColumnName = model.Columns[i].OriginalColumnName;
                            }
                            if (!string.IsNullOrEmpty(model.Columns[i].OriginalAliasTableName))
                            {
                                model.Columns[i].SqlAlias = model.Columns[i].OriginalAliasTableName;
                            }
                        }
                        if (!string.IsNullOrEmpty(filterConfig))
                        {
                            var filter = JsonConvert.DeserializeObject<List<TableColumnModel>>(filterConfig);
                            foreach (var col in filter)
                            {
                                model.Columns.Where(x => x.Id == col.Id).ToList().ForEach(x =>
                                {
                                    x.OperatorId = col.OperatorId;
                                    x.FilterValue = col.FilterValue;
                                    x.IsChecked = true;
                                });
                            }
                        }
                        var responseOperator = masterDataService.GetAllMasterDataByGroupId(474);
                        if (responseOperator != null)
                        {
                            var resultOperator = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseOperator);
                            if (!CheckPermission(resultOperator))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                model.Operators = resultOperator.Results;
                            }
                        }
                    }
                    else
                    {
                        var fields = tableViewModel.Fields;
                        foreach (var field in fields)
                        {
                            model.Columns.Where(x => x.ColumnName == field.FieldName).ToList().ForEach(x =>
                            {
                                x.IsDisplay = field.Visible;
                            });
                        }
                    }
                }
            }

            return PartialView(UrlHelpers.View("~/Views/Shared/_Filter.cshtml"), model);
        }
        public ActionResult FilterValueSelector(ITableColumnService tableColumnService, TableColumnModel column)
        {
            if (column.ColumnDataTypeId == 473)
            {
                var model = new DropdownListModel()
                {
                    IsAnimationLabel = false,
                    IsTagsInput = true,
                    IsMultipleLanguage = false,
                    ValueField = "Id",
                    DisplayField = "Name",
                    Name = "filter-value-" + column.Id,
                };
                if (column.IsChecked)
                {
                    model.ValueMultiSelect = JsonConvert.DeserializeObject<List<string>>(column.FilterValue.ToString());
                }
                var responseColumn = tableColumnService.ExcuteSqlString(column.SqlData);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.Data = resultColumn.Results;
                    }
                }

                return PartialView(UrlHelpers.FloatingTemplate("_Dropdown.cshtml"), model);
            }
            else
                if (column.ColumnDataTypeId == 472)
            {
                var model = new DatePickerModel()
                {
                    Name = "filter-value-" + column.Id,
                    DateFormat = "dd/MM/yyyy",
                    Vertical = "top"
                };
                if (column.IsChecked)
                {
                    model.Value = DateTime.ParseExact(column.FilterValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                return PartialView(UrlHelpers.FloatingTemplate("_DatePicker.cshtml"), model);
            }
            else
            if (column.ColumnDataTypeId == 470 || column.ColumnDataTypeId == 471)
            {
                var model = new TagsInputModel()
                {
                    Name = "filter-value-" + column.Id
                };
                if (column.IsChecked)
                {
                    model.Value = column.FilterValue ?? string.Empty;
                }
                return PartialView(UrlHelpers.FloatingTemplate("_MultiSelect.cshtml"), model);
            }
            return null;
        }
        public ActionResult ShowInlineEditing(ITableColumnService tableColumnService, ILocalizationService localizationService, TableColumnModel column, int index, string tableName)
        {
            if (column.ColumnDataTypeId == 470 || column.ColumnDataTypeId == 471)
            {
                var model = new TextEditorModel()
                {
                    LabelName = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName"),
                    Name = tableName + "[" + index.ToString() + "]." + column.ColumnName,
                    Value = column.ColumnValue,
                    PlaceHolder = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName")

                };
                if (column.ColumnDataTypeId == 471)
                {
                    model.Type = "Number";
                }
                return PartialView(UrlHelpers.FloatingTemplate("_TextEditor.cshtml"), model);
            }
            else
            if (column.ColumnDataTypeId == 472)
            {
                var model = new DatePickerModel()
                {
                    LabelName = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName"),
                    Name = tableName + "[" + index.ToString() + "]." + column.ColumnName,
                    DateFormat = "dd/MM/yyyy",
                    Vertical = "top",
                    PlaceHolder = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName")

                };
                if (column.ColumnValue != null && !string.IsNullOrEmpty(column.ColumnValue))
                {
                    model.Value = DateTime.Parse(column.ColumnValue);
                }
                else
                {
                    model.Value = null;
                }
                return PartialView(UrlHelpers.FloatingTemplate("_DatePicker.cshtml"), model);
            }
            else
            if (column.ColumnDataTypeId == 473)
            {
                var model = new DropdownListModel()
                {
                    LabelName = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName"),
                    DataType = DataType.MasterData,
                    PropertyName = "Name",
                    IsAnimationLabel = false,
                    IsTagsInput = false,
                    IsMultipleLanguage = false,
                    ValueField = "Id",
                    DisplayField = "Name",
                    Name = tableName + "[" + index.ToString() + "]." + column.ColumnName,
                    Value = column.ColumnValue
                };
                var responseColumn = tableColumnService.ExcuteSqlString(column.SqlData);
                if (responseColumn != null)
                {
                    var resultColumn = JsonConvert.DeserializeObject<HrmResultModel<dynamic>>(responseColumn);
                    if (!CheckPermission(resultColumn))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        model.Data = resultColumn.Results;
                    }
                }

                return PartialView(UrlHelpers.FloatingTemplate("_Dropdown.cshtml"), model);
            }
            else
            if (column.ColumnDataTypeId == 10519)
            {
                var model = new CheckboxModel()
                {
                    LabelName = localizationService.GetLocalizedData(column.Id.ToString() + ";" + DataType.TableName + ";ColumnName"),
                    Name = tableName + "[" + index.ToString() + "]." + column.ColumnName,
                    Value = column.ColumnValue,
                };
                return PartialView(UrlHelpers.FloatingTemplate("_Checkbox.cshtml"), model);
            }
            else
            {
                return Content("<div class=\"hrm-v2-td-template\"><span id=\"original-row-column-\"" + column.Id + ">" + column.ColumnValue + "</span></div>");
            }
        }
    }
}