using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;

namespace Hrm.Web.ViewModels
{
    public class StaffViewModel:BaseViewModel
    {
        public StaffViewModel()
        {
            Table = new TableViewModel();
        }
        public TableViewModel Table { get; set; }
    }
}
