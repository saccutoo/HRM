using Hrm.Common;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface ILocalizationRepository
    {
        HrmResultEntity<LocalizationEntity> GetResources(long languageId, string dbName);
        HrmResultEntity<LanguageEntity> GetLanguage(string dbName);
        HrmResultEntity<LocalizedDataEntity> GetLocalizedData(long languageId, string dbName);
        HrmResultEntity<string> GetMultipleLanguageConfiguration(string dataType, string dbName);
        HrmResultEntity<LocalizedDataEntity> GetLocalizedDataByDataId(long dataId,string dataType, string dbName);

    }
}
