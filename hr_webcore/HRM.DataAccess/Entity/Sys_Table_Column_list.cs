namespace HRM.DataAccess.Entity
{
    using System;
    using System.Collections.Generic;

    public partial class Sys_Table_Column_list
    {
        public int id { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameEn { get; set; }
    }
}

