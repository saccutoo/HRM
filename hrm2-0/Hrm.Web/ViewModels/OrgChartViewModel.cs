using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Web.ViewModels
{
    public class OrgChartViewModel:BaseModel
    {
        public List<StaffByLevel> StaffsByLevel { get; set; }
    }
    public class StaffByLevel
    {
        public StaffModel Staff { get; set; }
        public int Level { get; set; }
    }
}
