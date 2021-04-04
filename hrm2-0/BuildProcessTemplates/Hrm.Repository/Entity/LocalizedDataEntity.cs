using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class LocalizedDataEntity:BaseEntity
    {
        public long DataId { get; set; }
        public string DataType { get; set; }
        public long LanguageId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public bool IsDefault { get; set; }

    }
}
