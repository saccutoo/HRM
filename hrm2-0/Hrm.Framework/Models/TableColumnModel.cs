using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class TableColumnModel : BaseModel
    {
        public string TableId { get; set; }
        public string ColumnName { get; set; }
        public string SqlAlias { get; set; }

        public int ColumnDataTypeId { get; set; }
        public string SqlData { get; set; }
        public int OrderNo { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsFilter { get; set; }
        public long OperatorId { get; set; }
        public string FilterValue { get; set; }
        public bool IsLocked { get; set; }
        public bool IsVisible { get; set; }
        public bool IsChecked { get; set; }
        public string ColumnValue { get; set; }
        public long ColumnDataId { get; set; }
        public string OriginalColumnName { get; set; }
        public string OriginalAliasTableName { get; set; }
    }
}
