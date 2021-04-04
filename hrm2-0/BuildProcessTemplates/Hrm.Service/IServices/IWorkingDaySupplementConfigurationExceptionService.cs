using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service.IServices
{
    public partial interface IWorkingDaySupplementConfigurationExceptionService : IBaseService
    {
        string GetSupplementConfigurationException(BasicParamType param, out int totalRecord);
        string GetApprovedSaff();
        string GetApprovedBySaff(string listApprovedSaffs);
        string SaveSupplementConfigurationException(WorkingdaySupplementConfigurationExceptionEntity entity);
    }
}
