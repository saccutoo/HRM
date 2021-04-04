using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public interface IConfigInsuranceDetailService : IBaseService
    {
        string GetCurrentConfigInsuranceDetail(BasicParamType param, long insuranceId, out int totalRecord);
    }
}
