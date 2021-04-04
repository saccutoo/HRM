using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hrm.Repository
{
    public partial class AnnualLeaveRepository : CommonRepository, IAnnualLeaveRepository
    {
        public HrmResultEntity<AnnualLeaveEntity> GetAnnualLeave(string dbName)
        {
          var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<AnnualLeaveEntity>("System_Get_GetAnnualLeave", par, dbName: dbName);
        }       
    }
}
