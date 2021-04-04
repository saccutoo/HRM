using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Sys_Table_Column_User
    {
        public int AutoId { get; set; }
        public int TableId { get; set; }
        public int TableColumnId { get; set; }
        public int UserId { get; set; }
        public bool? Visible { get; set; }
        public double? Width { get; set; }
        public int OrderNo { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameEn { get; set; }
        public string DataFomat { get; set; }
    }

}
