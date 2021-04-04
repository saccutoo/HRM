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
    public class StaffRoleRepository : CommonRepository, IStaffRoleRepository
    {
        public HrmResultEntity<StaffRoleEntity> GetRoleByStaff(BasicParamType param, long staffId, out int totalRecord)
        {
            var par = new DynamicParameters();
            par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
            par.Add("@StaffId", staffId);
            par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
            var result = ListProcedure<StaffRoleEntity>("Role_Get_GetRoleByStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Role_Get_GetRoleByStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;
            return result;
        }
    }
}
