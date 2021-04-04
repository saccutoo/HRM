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

namespace Hrm.Repository
{
    public partial class PersonalIncomeTaxRepository : CommonRepository, IPersonalIncomeTaxRepository
    {
        public HrmResultEntity<PersonalIncomeTaxEntity> GetCurrentPersonalIncomeTax(BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<PersonalIncomeTaxEntity>("PersonalIncomeTax_Get_GetCurrentPersonalIncomeTax", par, dbName: param.DbName);
            return result;
        }
        public HrmResultEntity<PersonalIncomeTaxDetailEntity> GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(BasicParamType param, long personalIncomeTaxId)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@PersonalIncomeTaxId", personalIncomeTaxId);
            var result = ListProcedure<PersonalIncomeTaxDetailEntity>("PersonalIncomeTax_Get_GetPersonalIncomeTaxDetailByPersonalIncomeTaxId", par, dbName: param.DbName);
            return result;
        }
        public HrmResultEntity<PersonalIncomeTaxEntity> GetPersonalIncomeTaxHistory(BasicParamType param)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            var result = ListProcedure<PersonalIncomeTaxEntity>("PersonalIncomeTax_Get_GetPersonalIncomeTaxHistory", par, dbName: param.DbName);
            return result;
        }
    }
}
