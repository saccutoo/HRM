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
    public partial interface ISalaryElemenRepository
    {
        HrmResultEntity<SalaryElementEntity> GetSalaryElement(BasicParamType param, out int totalRecord);
        HrmResultEntity<SalaryElementEntity> SaveSalaryElement(SalaryElementEntity data, List<LocalizedDataEntity> listData, string dbName);
        HrmResultEntity<SalaryElementEntity> GetSalaryElementById(long Id, string dbName);

        HrmResultEntity<bool> GetResultSFormular(string sFormularstr, string dbName, out string resultFormularstr, out float resultValue);
    }
}
