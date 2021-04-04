using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IOrganizationService : IBaseService
    {
        string GetOrganization(BasicParamType param, out int totalRecord);
        string SaveOrganization(OrganizationEntity data,BasicParamType param);
        string GetOrganizationById(long id);
        string savePersonnelTransfer(WorkingProcessEntity data, BasicParamType param);
        string SaveMergerOrganization(OrganizationEntity data, List<ListDataSelectedIdType> listData, BasicParamType param);
        string GetParentOrganization(BasicParamType param, long id);
        string GetAllOrganizationForDropDown(BasicParamType param);
        string SearchOrganization(long languageId, string searchKey);
        string DeleteOrganization(long id,long userId,long roleId);
        string GetAllOrganization();
        string CheckOrganizationType(List<ListDataSelectedIdType> listData);
        string GetOrganizationByParentId(long parentId);
    }
}
