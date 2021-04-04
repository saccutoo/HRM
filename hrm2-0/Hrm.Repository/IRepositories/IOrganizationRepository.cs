using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Repository
{
    public partial interface IOrganizationRepository
    {

        HrmResultEntity<OrganizationEntity> GetOrganization(BasicParamType param, out int totalRecord);
        HrmResultEntity<OrganizationEntity> SaveOrganization(OrganizationEntity data,BasicParamType param);
        HrmResultEntity<OrganizationEntity> GetOrganizationById(long id, string dbName);
        HrmResultEntity<bool> savePersonnelTransfer(WorkingProcessEntity data, BasicParamType param);
        HrmResultEntity<OrganizationEntity> SaveMergerOrganization(OrganizationEntity data, List<ListDataSelectedIdType> listData, BasicParamType param, string dbName);
        HrmResultEntity<OrganizationEntity> GetParentOrganization(BasicParamType param, long id);
        HrmResultEntity<OrganizationEntity> GetAllOrganizationForDropDown(BasicParamType param);     
        HrmResultEntity<OrganizationEntity> SearchOrganization(long languageId, string searchKey, string dbName);
        HrmResultEntity<OrganizationEntity> DeleteOrganization(long id, long userId, long roleId, string dbName);
        HrmResultEntity<OrganizationEntity> GetAllOrganization(string dbName);
        HrmResultEntity<OrganizationEntity> CheckOrganizationType(List<ListDataSelectedIdType> listData, string dbName);
        HrmResultEntity<OrganizationEntity> GetOrganizationByParentId(long parentId, string dbName);
    }
}
