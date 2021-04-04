using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Type;
using Hrm.Service.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial class PersonalIncomeTaxService : IPersonalIncomeTaxService
    {
        IPersonalIncomeTaxRepository _personalIncomeTaxRepository;
        private string _dbName;
        public PersonalIncomeTaxService(IPersonalIncomeTaxRepository personalIncomeTaxRepository)
        {
            this._personalIncomeTaxRepository = personalIncomeTaxRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetCurrentPersonalIncomeTax(BasicParamType param)
        {
            var taxResponse = this._personalIncomeTaxRepository.GetCurrentPersonalIncomeTax(param);
            return JsonConvert.SerializeObject(taxResponse);
        }
        public string GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(BasicParamType param, long personalIncomeTaxId)
        {
            var taxResponse = this._personalIncomeTaxRepository.GetPersonalIncomeTaxDetailByPersonalIncomeTaxId(param, personalIncomeTaxId);
            return JsonConvert.SerializeObject(taxResponse);
        }
        public string GetPersonalIncomeTaxHistory(BasicParamType param)
        {
            var taxResponse = this._personalIncomeTaxRepository.GetPersonalIncomeTaxHistory(param);
            return JsonConvert.SerializeObject(taxResponse);
        }
    }
}
