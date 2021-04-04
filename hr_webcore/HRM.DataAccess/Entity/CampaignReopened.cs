using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class CampaignReopened
    {
        public string CampaignId { get; set; }
        public string Reason { get; set; }
        public string ReopenBy { get; set; }
        public string OrginizationUnit { get; set; }
        public string Requester { get; set; }
        public DateTime ReopenDate { get; set; }
    }
}
