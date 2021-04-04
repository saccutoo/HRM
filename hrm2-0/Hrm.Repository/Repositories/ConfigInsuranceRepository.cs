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
    public class ConfigInsuranceRepository : CommonRepository, IConfigInsuranceRepository
    {
        public HrmResultEntity<ConfigInsuranceEntity> GetCurrentConfigInsurance(BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<ConfigInsuranceEntity>("ConfigInsurance_Get_GetCurrentConfigInsurance", par, dbName: param.DbName);
            return result;
        }
        public HrmResultEntity<ConfigInsuranceDetailEntity> GetCurrentConfigInsuranceDetailByInsurance(BasicParamType param, int insuranceId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@InsuranceId", insuranceId);
            var result = ListProcedure<ConfigInsuranceDetailEntity>("ConfigInsurance_Get_GetCurrentConfigInsuranceDetailByInsurance", par, dbName: param.DbName);
            return result;
        }
    }
}
