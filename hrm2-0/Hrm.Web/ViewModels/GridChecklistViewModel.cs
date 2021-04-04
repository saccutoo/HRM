using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;

namespace Hrm.Web.ViewModels
{
    public class GridChecklistViewModel : BaseViewModel
    {
        public bool IsSendChecklist { get; set; }
        public int TotalCurrentChecklistChild { get; set; }
        public int TotalCurrentCheckListDetailCompleted { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string OfficePositionId { get; set; }
    }
}
