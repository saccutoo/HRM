using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface ISalaryService : IBaseService
    {
        string GetSalary(BasicParamType param, int month, int year, long staffId, out string outTotalJson, out int totalRecord);
    }
}
