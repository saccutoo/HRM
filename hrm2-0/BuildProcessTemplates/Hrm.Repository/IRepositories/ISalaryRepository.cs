using Hrm.Common;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface ISalaryRepository
    {
        HrmResultEntity<SalaryEntity> GetSalary(BasicParamType param, int month, int year, long staffId, out string outTotalJson, out int totalRecord);
    }
}
