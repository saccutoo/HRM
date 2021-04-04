using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class ColumnValidationEntity:BaseEntity
    {
        public long ColumnId { get; set; }
        public long TableId { get; set; }
        public string ColumnName { get; set; }
        public bool IsRequired { get; set; }
        public int MaxLength { get; set; }
        public long BasicValidationTypeId { get; set; }
    }
}
