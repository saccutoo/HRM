using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;

namespace Hrm.Web.ViewModels
{
    public class PayrollViewModel : BaseViewModel
    {
        public PayrollViewModel()
        {

            Table = new TableViewModel();
        }
        public TableViewModel Table { get; set; }
    }
}