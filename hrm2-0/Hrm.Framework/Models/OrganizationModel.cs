using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class OrganizationModel :BaseModel
    {
        public string OrganizationName { get; set; }
        public string OrganizationCode { get; set; }
        public long ParentId { get; set; }
        public long? OrganizationType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long CurrencyTypeId { get; set; }
        public long OrderNo { get; set; }
        public long Branch { get; set; }
        public long Status { get; set; }
        public string Note { get; set; }
        public int NumberOfChild { get; set; }
        public string DecisionNo { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long ToOrganizationaID { get; set; }


    }
}