using Hrm.Repository.Entity;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface ILocalizationService : IBaseService
    {
        string GetResources(string key);
        string GetLanguage();
        string GetLocalizedData(string format);
        HrmResultEntity<string> GetMultipleLanguageConfiguration(string dataType);
        string GetLocalizedDataByDataId(long dataId,string DataType);
        string GetBaseResources(string key);

    }
}
