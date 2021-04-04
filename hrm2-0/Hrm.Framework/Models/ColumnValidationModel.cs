using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ColumnValidationModel
    {
        public long ColumnId { get; set; }
        public long TableId { get; set; }
        public string ColumnName { get; set; }
        public bool IsRequired { get; set; }
        public int MaxLength { get; set; }
        public long BasicValidationTypeId { get; set; }
    }
}
