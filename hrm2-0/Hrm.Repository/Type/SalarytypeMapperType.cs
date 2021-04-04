using Hrm.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Type
{
    public class SalarytypeMapperType : IUserDefinedType
    {
        public long Id { get; set; }
        public long SalarytypeId { get; set; }
        public long TypeId { get; set; }
        public long DataId { get; set; }

    }
}
