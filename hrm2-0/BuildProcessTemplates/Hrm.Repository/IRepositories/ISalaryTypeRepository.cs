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
    public partial interface ISalaryTypeRepository
    {
        HrmResultEntity<SalaryTypeEntity> GetSalaryType(BasicParamType param, out int totalRecord);
        HrmResultEntity<SalaryTypeEntity> SaveSalaryType(SalaryTypeEntity data, List<SalaryElementType> listData, List<SalarytypeMapperType> listSalarytypeMapper, string dbName);
        HrmResultEntity<SalaryTypeEntity> GetSalaryTypeById(long id, string dbName);
        HrmResultEntity<SalarytypeMapperEntity> GetSalarytypeMapperBySalaryTypeId(long id, string dbName);
        HrmResultEntity<SalaryTypeEntity> DeleteSalaryType(long id, string dbName);
    }
}
