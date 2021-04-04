using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IWorkingDaySupplementConfigurationRepository
    {
        HrmResultEntity<WorkingdaySupplementConfigurationEntity> GetSupplementConfigurationByRoleId(BasicParamType param, out int totalRecord);
        HrmResultEntity<WorkingdaySupplementConfigurationEntity> SaveSupplementConfiguration(WorkingdaySupplementConfigurationEntity entity, string dbName);
        HrmResultEntity<WorkingdaySupplementConfigurationEntity> DeleteSupplementConfiguration(long id, string dbName);
        HrmResultEntity<WorkingdaySupplementConfigurationEntity> GetSupplementConfigurationById(long id, string dbName);

    }
}
