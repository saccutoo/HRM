using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class TableConfigEntity
    {
        public long Id { get; set; }
        public long TableId { get; set; }
        public string ConfigData { get; set; }
        public string QueryData { get; set; }
        public string FilterData { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
