using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Common.Helpers;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Repository
{
    public class ConfigInsuranceDetailRepository : CommonRepository, IConfigInsuranceDetailRepository
    {
        public HrmResultEntity<ConfigInsuranceDetailEntity> GetCurrentConfigInsuranceDetail(BasicParamType param, long insuranceId, out int totalRecord)
        {
            //FilterField : 
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@InsuranceId", insuranceId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<ConfigInsuranceDetailEntity>("ConfigInsurance_Get_GetConfigInsuranceDetailByInsuranceId", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-ConfigInsurance_Get_GetConfigInsuranceDetailByInsuranceId-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }
    }
}
