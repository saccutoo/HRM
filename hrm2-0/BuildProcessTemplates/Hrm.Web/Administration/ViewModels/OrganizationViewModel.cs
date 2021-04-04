using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class OrganizationViewModel
    {
        public List<dynamic> Organizations { get; set; } = new List<dynamic>();
        public List<OrganizationModel> OrganizationsTree { get; set; } = new List<OrganizationModel>();
        public TableViewModel Table { get; set; } = new TableViewModel();
        public List<dynamic> MasterDataStatus { get; set; } = new List<dynamic>();
        public List<dynamic> MasterDataCurrency { get; set; } = new List<dynamic>();
        public List<dynamic> MasterDataCategoryCompany { get; set; } = new List<dynamic>();
        public List<dynamic> MasterDataBrand { get; set; } = new List<dynamic>();
        public long ParentId { get; set; }
        public long CheckRadio { get; set; } = 1;
        public OrganizationModel Organization { get; set; } = new OrganizationModel();
        public bool IsOrganizationOld { get; set; } = true;
        public long OrganizationType { get; set; }

    }
}