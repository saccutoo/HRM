using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository
{
    public interface IConfigInsuranceRepository
    {
        HrmResultEntity<ConfigInsuranceEntity> GetCurrentConfigInsurance(BasicParamType param);
        HrmResultEntity<ConfigInsuranceDetailEntity> GetCurrentConfigInsuranceDetailByInsurance(BasicParamType param, int insuranceId);
    }
}
