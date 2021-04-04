using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IWorkingDaySupplementConfigurationService : IBaseService
    {
        string GetSupplementConfigurationByRoleId(BasicParamType param, out int totalRecord);
        string SaveSupplementConfiguration(WorkingdaySupplementConfigurationEntity entity);
        string DeleteSupplementConfiguration(long id);
        string GetSupplementConfigurationById(long id);
    }
}
