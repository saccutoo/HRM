using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class HrmResultEntity<T> : BaseEntity
    {
        public List<T> Results { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
    }
}
