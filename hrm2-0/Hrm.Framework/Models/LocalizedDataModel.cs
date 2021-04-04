using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class LocalizedDataModel:BaseModel
    {
        public long DataId { get; set; }
        public long LanguageId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public bool IsDefault { get; set; }
    }
}
