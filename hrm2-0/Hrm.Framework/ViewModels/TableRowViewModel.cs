using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Framework.ViewModels
{
    public class TableRowViewModel
    {
        public TableRowViewModel()
        {
            TableViewModel = new TableViewModel();
        }
        public dynamic Data { get; set; }
        public TableViewModel TableViewModel { get; set; }
    }
}
