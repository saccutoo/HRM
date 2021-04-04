using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IPersonalIncomeTaxService : IBaseService
    {
        string GetCurrentPersonalIncomeTax(BasicParamType param);
        string GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(BasicParamType param, long personalIncomeTaxId);
        string GetPersonalIncomeTaxHistory(BasicParamType param);
    }
}
