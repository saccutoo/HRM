using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using Hrm.Common;
using Hrm.Service;
using Hrm.Core.Infrastructure;
using System.Reflection;

namespace Hrm.Framework.Helper
{
    public static class FunctionHelpers
    {
        public static List<dynamic> Sort(List<dynamic> input, string property, string direction)
        {
            if (direction == Sorting.Desc)
                return input.Where(p => p[property] != null).OrderByDescending(p => p[property]).Concat(input.Where(p => p[property] == null)).ToList();
            else
                return input.Where(p => p[property] != null).OrderBy(p => p[property]).Concat(input.Where(p => p[property] == null)).ToList();
        }
        public static List<LocalizedDataModel> ConvertToLanguage<T>(T model, string dataType, List<string> properties)
        {
            var result = new List<LocalizedDataModel>();
            if (properties != null && properties.Count() > 0)
            {
                foreach (var prop in properties)
                {
                    long id = 0;
                    long.TryParse(model.GetType().GetProperty("Id").GetValue(model, null).ToString(), out id);
                    long languageId = 0;
                    long.TryParse(model.GetType().GetProperty("LanguageId").GetValue(model, null).ToString(), out languageId);
                    var propVal = string.Empty;
                    try
                    {
                        propVal = model.GetType().GetProperty(prop).GetValue(model, null).ToString();
                        result.Add(new LocalizedDataModel
                        {
                            DataId = id,
                            DataType = dataType,
                            LanguageId = languageId,
                            PropertyName = prop,
                            PropertyValue = propVal
                        });
                    }
                    catch (Exception)
                    {
                        //todo catch
                    }
                }
            }
            return result;
        }
        public static List<LocalizedDataModel> ConvertToLanguageFromModel<T>(T model, string dataType)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var listProp = localizationService.GetMultipleLanguageConfiguration(dataType);
            return ConvertToLanguage(model, dataType, listProp.Results);
        }
        public static List<LocalizedDataModel> ConvertToLanguageFromListModel<T>(List<T> listModel, string dataType)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var listProp = localizationService.GetMultipleLanguageConfiguration(dataType);
            var result = new List<LocalizedDataModel>();
            foreach (var model in listModel)
            {
                result.AddRange(ConvertToLanguage(model, dataType, listProp.Results));
            }
            return result;
        }
        public static bool ControlPermission(string action, string target)
        {
            var authenService = EngineContext.Current.Resolve<IAuthenticationService>();
            return authenService.ControlPermission(action, target);
        }
        public static T ConvertLanguageToModel<T>(List<LocalizedDataModel> LocalizedData, T modelResult, List<string> properties)
        {
            if (properties != null && properties.Count() > 0)
            {
                foreach (var item in properties)
                {
                    var propVal = string.Empty;
                    try
                    {
                        if (LocalizedData != null)
                        {
                            foreach (var data in LocalizedData)
                            {
                                if (item == data.PropertyName)
                                {
                                    propVal = data.PropertyValue;
                                    PropertyInfo propertyInfo = modelResult.GetType().GetTypeInfo().GetDeclaredProperty(item);
                                    propertyInfo.SetValue(modelResult, Convert.ChangeType(propVal, propertyInfo.PropertyType));
                                }

                            }
                        }


                    }
                    catch (Exception)
                    {
                        //todo catch
                    }
                }
            }

            return modelResult;
        }

    }
}