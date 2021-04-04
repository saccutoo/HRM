//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM.DataAccess.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_Table_Column
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sys_Table_Column()
        {
            this.Sys_Table_Column_Role = new HashSet<Sys_Table_Column_Role>();
        }
    
        public int Id { get; set; }
        public int TableId { get; set; }
        public int Type { get; set; }
        public string ColumnName { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameEn { get; set; }
        public bool isVisible { get; set; }
        public bool isFilter { get; set; }
        public bool isQuickFilter { get; set; }
        
        public string Class { get; set; }
        public string ShowTotal { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public bool isActive { get; set; }
        public double Width { get; set; }
        public string Css { get; set; }
        public string CustomHTML { get; set; }
        public string Param { get; set; }
        public string Benchmark { get; set; }
        public int OrderNo { get; set; }
        public string DataFomat { get; set; }
        public string SQLvalue { get; set; }
        public bool isFixed { get; set; }

        public int Order { get; set; }

        public virtual Sys_Table Sys_Table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sys_Table_Column_Role> Sys_Table_Column_Role { get; set; }
    }
}
