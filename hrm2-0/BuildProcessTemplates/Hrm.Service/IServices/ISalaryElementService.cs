using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface ISalaryElementService : IBaseService
    {
        string GetSalaryElement(BasicParamType param, out int totalRecord);
        string SaveSalaryElement(SalaryElementEntity data, List<LocalizedDataEntity> listData);
        string GetSalaryElementById(long id);
        string GetResultSFormular(string sFormularstr, out string resultFormularstr, out float resultValue);
        string DeleteSalaryElement(long id);
        string GetSalaryElementBySalaryTypeId(long id);
    }
}
