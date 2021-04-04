using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrm.Service
{
    public partial class LocalizationService : ILocalizationService
    {
        ILocalizationRepository _localizationRepository;
        private string _dbName;
        public LocalizationService(ILocalizationRepository localizationRepository)
        {
            this._localizationRepository = localizationRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetResources(string key)
        {
            var languageId = Common.CurrentUser.LanguageId;
            var result = new List<LocalizationEntity>();
            var response = this._localizationRepository.GetResources(languageId, _dbName);
            var resource = response.Results.ToList().FirstOrDefault(x => x.ResourceName == key);
            if (resource != null) return resource.ResourceValue;
            //return key;
            return string.Empty;
        }
        public string GetBaseResources(string key)
        {
            var languageId = Common.CurrentUser.LanguageId;
            var result = new List<LocalizationEntity>();
            var response = this._localizationRepository.GetResources(languageId, Constant.SystemDbName);
            var resource = response.Results.ToList().FirstOrDefault(x => x.ResourceName == key);
            if (resource != null) return resource.ResourceValue;
            //return key;
            return string.Empty;
        }
        public string GetLanguage()
        {
            var response = this._localizationRepository.GetLanguage(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetLocalizedData(string format)
        {
            var languageId = CurrentUser.LanguageId;
            var localizedDataResponse = this._localizationRepository.GetLocalizedData(languageId, _dbName);
            if (localizedDataResponse != null && localizedDataResponse.Results.Count > 0)
            {
                if (format.Contains(';'))
                {
                    var data = localizedDataResponse.Results;
                    var listKey = format.Split(';');
                    var resource = data.FirstOrDefault(x => x.DataId.ToString() == listKey[0] && x.DataType == listKey[1] && x.PropertyName == listKey[2]);
                    if (resource != null) return resource.PropertyValue;
                }
            }
            return string.Empty;
        }
        public HrmResultEntity<string> GetMultipleLanguageConfiguration(string dataType)
        {
            var result = new List<string>();
            return _localizationRepository.GetMultipleLanguageConfiguration(dataType, _dbName);
        }
        public string GetLocalizedDataByDataId(long dataId,string dataType)
        {
            var response = this._localizationRepository.GetLocalizedDataByDataId(dataId, dataType,_dbName);
            return JsonConvert.SerializeObject(response);
        }
    }
}
