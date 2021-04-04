using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class LocalizationRepository : CommonRepository, ILocalizationRepository
    {
        public HrmResultEntity<LocalizationEntity> GetResources(long languageId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@DbName", dbName);
            return ListProcedure<LocalizationEntity>("System_Full_GetResources", par, useCache:true, dbName:dbName);
        }
        public HrmResultEntity<LanguageEntity> GetLanguage(string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<LanguageEntity>("System_Full_GetLanguage", par, useCache: true, dbName: dbName);            
        }
        public HrmResultEntity<LocalizedDataEntity> GetLocalizedData(long languageId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@LanguageId", languageId);
            par.Add("@DbName", dbName);
            return ListProcedure<LocalizedDataEntity>("System_Get_GetLocalizedData", par);           
        }
        public HrmResultEntity<string> GetMultipleLanguageConfiguration(string dataType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DataType", dataType);
            par.Add("@DbName", dbName);
            return ListProcedure<string>("System_Get_GetMultipleLanguageConfiguration", par, dbName: dbName);
        }
        public HrmResultEntity<LocalizedDataEntity> GetLocalizedDataByDataId(long dataId,string dataType, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DataId", dataId);
            par.Add("@DataType", dataType);
            par.Add("@DbName", dbName);
            return ListProcedure<LocalizedDataEntity>("System_Get_GetLocalizedDataByDataId", par);
        }
    }
}
