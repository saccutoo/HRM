using Hrm.Common;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Models;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hrm.Framework.Helpers
{
    public static class ValidationHelper
    {
        public static List<ValidationModel> ListValidation<T>(List<T> models, string modelName, string dbName = "")
        {
            var result = new List<ValidationModel>();
            List<ColumnValidationModel> validationsR = new List<ColumnValidationModel>();
            if (models != null && models.Count > 0)
            {
                var validationService = EngineContext.Current.Resolve<IColumnValidationService>();
                var validationResponse = validationService.GetColumnValidationByTable(models[0].GetType().Name.Replace("Model", string.Empty),dbName);
                var validationResult = JsonConvert.DeserializeObject<Models.HrmResultModel<ColumnValidationModel>>(validationResponse);
                if (validationResult.StatusCode == StatusCode.AccessDenied)
                {
                    //return access denied
                }
                else
                {
                    validationsR = validationResult.Results;
                }
                var index = -1;
                if (models.Any())
                {
                    foreach (var model in models)
                    {
                        index++;
                        result.AddRange(Validation(model, modelName, index, validationsR));
                    }
                }
            }
            return result;
        }
        public static List<ValidationModel> Validation<T>(T model, string modelName, int index = -1, List<ColumnValidationModel> validationsR = null, string dbName = "")
        {
            var result = new List<ValidationModel>();
            var nameOfModel = modelName;
            if (validationsR == null)
            {
                var validationService = EngineContext.Current.Resolve<IColumnValidationService>();
                var validationResponse = validationService.GetColumnValidationByTable(model.GetType().Name.Replace("Model", string.Empty),dbName);
                var validationResult = JsonConvert.DeserializeObject<Models.HrmResultModel<ColumnValidationModel>>(validationResponse);
                if (validationResult.StatusCode == StatusCode.AccessDenied)
                {
                    //return access denied
                }
                else
                {
                    validationsR = validationResult.Results;
                }
            }

            #region column validation
            var properties = model.GetType().GetProperties();
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            foreach (var prop in properties)
            {
                var columnName = prop.Name;
                var columnValue = string.Empty;
                if (prop.GetValue(model) != null)
                    columnValue = prop.GetValue(model).ToString();
                var validations = validationsR.Where(x => x.ColumnName == columnName).ToList();
                if (validations.Any())
                {
                    foreach (var validation in validations)
                    {
                        if (validation != null)
                        {
                            if (validation.IsRequired)
                            {
                                if (string.IsNullOrEmpty(columnValue) || ((prop.PropertyType.Name.ToLower() == "decimal"
                                                                            || prop.PropertyType.Name.ToLower() == "float"
                                                                            || prop.PropertyType.Name.ToLower() == "double"
                                                                            || prop.PropertyType.Name.ToLower() == "int16"
                                                                            || prop.PropertyType.Name.ToLower() == "int32"
                                                                            || prop.PropertyType.Name.ToLower() == "int64"
                                                                            || prop.PropertyType.Name.ToLower() == "single"
                                                                           ) && columnValue == "0"))
                                {
                                    result.Add(new ValidationModel
                                    {
                                        ColumnName = nameOfModel + (index != -1 ? "[" + index.ToString() + "]." : nameOfModel==""?"":".") + columnName,
                                        IsError = false,
                                        ErrorMessage = localizationService.GetResources("ErrorMessage.Validation.Required")
                                    });
                                    goto Finish;
                                }
                                else
                                {
                                    goto MaxLength;
                                }
                            }
                            MaxLength:
                            if (validation.MaxLength != 0)
                            {   if(prop.PropertyType.Name.ToLower() == "decimal"
                                                                            || prop.PropertyType.Name.ToLower() == "float"
                                                                            || prop.PropertyType.Name.ToLower() == "double"
                                                                            || prop.PropertyType.Name.ToLower() == "single"
                                                                           )
                                {
                                    columnValue = Convert.ToInt64(Math.Floor(Convert.ToDouble(columnValue))).ToString();
                                }
                                if (columnName != null && columnValue.Length > validation.MaxLength)
                                {
                                    result.Add(new ValidationModel
                                    {
                                        ColumnName = nameOfModel + (index != -1 ? "[" + index.ToString() + "]." : nameOfModel == "" ? "" : ".") + columnName,
                                        IsError = false,
                                        ErrorMessage = string.Format(localizationService.GetResources("ErrorMessage.Validation.MaxLength"), "{0}", validation.MaxLength)
                                    });
                                    goto Finish;
                                }
                                else
                                {
                                    goto Error339;
                                }
                            }
                            Error339:
                            if (validation.BasicValidationTypeId == 339 && !string.IsNullOrEmpty(columnValue))
                            {
                                var emailReg = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,4})+)$";
                                if (!Regex.IsMatch(columnValue, emailReg))
                                {
                                    result.Add(new ValidationModel
                                    {
                                        ColumnName = nameOfModel + (index != -1 ? "[" + index.ToString() + "]." : nameOfModel == "" ? "" : ".") + columnName,
                                        IsError = false,
                                        ErrorMessage = localizationService.GetResources("ErrorMessage.Validation.EmailFormat")
                                    });
                                    goto Finish;
                                }
                                else
                                {
                                    goto Error340;
                                }

                            }
                            Error340:
                            if (validation.BasicValidationTypeId == 340 && !string.IsNullOrEmpty(columnValue))
                            {
                                var phoneReg = @"^\d{9,15}$";
                                if (!Regex.IsMatch(columnValue, phoneReg))
                                {
                                    result.Add(new ValidationModel
                                    {
                                        ColumnName = nameOfModel + (index != -1 ? "[" + index.ToString() + "]." : nameOfModel == "" ? "" : ".") + columnName,
                                        IsError = false,
                                        ErrorMessage = localizationService.GetResources("ErrorMessage.Validation.PhoneFormat")
                                    });
                                    goto Finish;
                                }
                                else
                                {
                                    goto Finish;
                                }
                            }
                            Finish:
                            continue;
                        }
                    }
                }
                #endregion
            }
            return result;
        }
    }
}
