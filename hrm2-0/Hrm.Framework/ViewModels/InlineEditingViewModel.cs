using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Framework.ViewModels
{
    public class InlineEditingViewModel
    {
        public InlineEditingViewModel()
        {
            Columns = new List<TableColumnModel>();
            Fields = new List<FieldModel>();
        }
        public List<TableColumnModel> Columns { get; set; }
        public List<FieldModel> Fields { get; set; }
        public dynamic Data { get; set; }
        public int Index { get; set; }
        public string TableName { get; set; }
        public bool EditType { get; set; } = true;
    }
}
