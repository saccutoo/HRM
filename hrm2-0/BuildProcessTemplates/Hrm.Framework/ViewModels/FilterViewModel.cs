using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Framework.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel()
        {
            Columns = new List<TableColumnModel>();
            Operators = new List<MasterDataModel>();
        }
        public List<TableColumnModel> Columns { get; set; }
        public List<MasterDataModel> Operators { get; set; }
        public string IsFilter { get; set; } // use for filter or show hide column
        public string GroupId { get; set; }// use for general-category
        public string TableUrl { get; set; }
        public string TableName { get; set; }
        public bool IsLocked { get; set; }
    }
}
