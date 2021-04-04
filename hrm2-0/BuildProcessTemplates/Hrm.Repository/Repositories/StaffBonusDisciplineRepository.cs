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
    public class StaffBonusDisciplineRepository : CommonRepository, IStaffBonusDisciplineRepository
    {
        public HrmResultEntity<StaffBonusDisciplineEntity> GetBonusDisciplineByStaff(BasicParamType param, long staffId, long type, out int totalRecord)
        {
           var par = new DynamicParameters();
                par.Add("@BaseParam", param.ToUserDefinedDataTable(), DbType.Object);
                par.Add("@StaffId", staffId, DbType.Int32);
                par.Add("@Type", type, DbType.Int32);
                par.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.InputOutput);
                var result = ListProcedure<StaffBonusDisciplineEntity>("Staff_Get_GetBonusDisciplineByStaff", par, dbName: param.DbName);
            if (result.StatusCode != StatusCode.AccessDenied)
            {
                par = HttpRuntime.Cache.Get(param.DbName + "-Staff_Get_GetBonusDisciplineByStaff-" + GetKeyFromParam(par as object) + "-" + CurrentUser.UserId + "-output") as DynamicParameters;
                totalRecord = par.GetDataOutput<int>("@TotalRecord");
            }
            else
                totalRecord = 0;

            return result;
        }
        public HrmResultEntity<StaffBonusDisciplineEntity> GetStaffBonusDisciplineById(BasicParamType param, long id)
        {
            var par = new DynamicParameters();
            par.Add("@Id", id);
            par.Add("@DbName", param.DbName);
            par.Add("@UserID", param.UserId);
            par.Add("@RoleID", param.RoleId);
            return ListProcedure<StaffBonusDisciplineEntity>("Staff_Get_GetStaffBonusDisciplineById", par, dbName: param.DbName);
        }
    }
}
