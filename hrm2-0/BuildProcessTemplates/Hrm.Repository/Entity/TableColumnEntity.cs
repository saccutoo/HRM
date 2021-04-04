using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class TableColumnEntity : BaseEntity
    {
        public string TableId { get; set; }
        public string ColumnName { get; set; }
        public int ColumnDataTypeId { get; set; }
        public string SqlData { get; set; }
        public int OrderNo { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsFilter { get; set; }
        public string SqlAlias { get; set; }
        public string OriginalColumnName { get; set; }

        public string OriginalAliasTableName { get; set; }

    }
}
