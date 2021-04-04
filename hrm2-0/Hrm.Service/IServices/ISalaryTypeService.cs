using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface ISalaryTypeService : IBaseService
    {
        string GetSalaryType(BasicParamType param, out int totalRecord);
        string SaveSalaryType(SalaryTypeEntity data, List<SalaryElementType> listData, List<SalarytypeMapperType> listSalarytypeMapper);
        string GetSalaryTypeById(long id);
        string GetSalarytypeMapperBySalaryTypeId(long id);
        string DeleteSalaryType(long id);
    }
}
