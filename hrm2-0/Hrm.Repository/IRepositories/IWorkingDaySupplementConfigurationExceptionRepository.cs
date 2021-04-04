using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.IRepositories
{
    public partial interface IWorkingDaySupplementConfigurationExceptionRepository
    {
        HrmResultEntity<WorkingdaySupplementConfigurationExceptionEntity> GetSupplementConfigurationException(BasicParamType param, out int totalRecord);
        HrmResultEntity<StaffEntity> GetApprovedSaff(string dbName);
        HrmResultEntity<StaffEntity> GetApprovedBySaff(string dbName, string listApprovedSaffs);
        HrmResultEntity<WorkingdaySupplementConfigurationExceptionEntity> SaveSupplementConfigurationException(WorkingdaySupplementConfigurationExceptionEntity entity, string dbName);
    }
}
