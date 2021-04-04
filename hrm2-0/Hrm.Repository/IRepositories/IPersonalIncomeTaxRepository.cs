using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public partial interface IPersonalIncomeTaxRepository
    {
        HrmResultEntity<PersonalIncomeTaxEntity> GetCurrentPersonalIncomeTax(BasicParamType param);
        HrmResultEntity<PersonalIncomeTaxDetailEntity> GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(BasicParamType param, long personalIncomeTaxId);
        HrmResultEntity<PersonalIncomeTaxEntity> GetPersonalIncomeTaxHistory(BasicParamType param);
    }
}
