using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public interface IConfigInsuranceService : IBaseService
    {
        string GetCurrentConfigInsurance(BasicParamType param);
        string GetCurrentConfigInsuranceDetailByInsurance(BasicParamType param, int insuranceId);
    }
}
