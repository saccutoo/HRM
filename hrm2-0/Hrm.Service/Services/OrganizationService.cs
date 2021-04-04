using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial class OrganizationService : IOrganizationService
    {
        IOrganizationRepository _organizationRepository;
        private string _dbName;
        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            this._organizationRepository = organizationRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetOrganization(BasicParamType param, out int totalRecord)
        {
            var response = this._organizationRepository.GetOrganization(param, out totalRecord);
            return JsonConvert.SerializeObject(response);
        }

        public string SaveOrganization(OrganizationEntity data,BasicParamType param)
        {
            var response = this._organizationRepository.SaveOrganization(data, param);
            return JsonConvert.SerializeObject(response);
        }
        public string GetOrganizationById(long id)
        {
            var response = this._organizationRepository.GetOrganizationById(id, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string savePersonnelTransfer(WorkingProcessEntity data, BasicParamType param)
        {
            var response = this._organizationRepository.savePersonnelTransfer(data, param);
            return JsonConvert.SerializeObject(response);
        }
        public string SaveMergerOrganization(OrganizationEntity data, List<ListDataSelectedIdType> listData, BasicParamType param)
        {
            var response = this._organizationRepository.SaveMergerOrganization(data, listData, param,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetParentOrganization(BasicParamType param, long id)
        {
            var response = this._organizationRepository.GetParentOrganization(param, id);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAllOrganizationForDropDown(BasicParamType param)
        {
            var response = this._organizationRepository.GetAllOrganizationForDropDown(param);
            return JsonConvert.SerializeObject(response);
        }
        public string SearchOrganization(long languageId, string searchKey)
        {
            var response = this._organizationRepository.SearchOrganization(languageId, searchKey,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string DeleteOrganization(long id, long userId, long roleId)
        {
            var response = this._organizationRepository.DeleteOrganization(id,userId, roleId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetAllOrganization()
        {
            var response = this._organizationRepository.GetAllOrganization(_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string CheckOrganizationType(List<ListDataSelectedIdType> listData)
        {
            var response = this._organizationRepository.CheckOrganizationType(listData,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetOrganizationByParentId(long parentId)
        {
            var response = this._organizationRepository.GetOrganizationByParentId(parentId, _dbName);
            return JsonConvert.SerializeObject(response);
        }

    }
}
