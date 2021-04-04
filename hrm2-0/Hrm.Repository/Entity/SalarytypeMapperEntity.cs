using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class SalarytypeMapperEntity : BaseEntity
    {
        public long SalarytypeId { get; set; }
        public long TypeId { get; set; }
        public long DataId { get; set; }
    }
}
