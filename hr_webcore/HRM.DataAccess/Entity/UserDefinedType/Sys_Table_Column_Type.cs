using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRM.DataAccess.Entity.UserDefinedType
{
    public class Sys_Table_Column_Type: IUserDefinedType
    {
        public int Id { get; set; }
        public bool isActive { get; set; }
        public int OrderNo { get; set; }
        public string ColumnName { get; set; }
        public int TableId { get; set; }
    }
}
