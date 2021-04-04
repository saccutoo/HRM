using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class BaseModel : ICloneable
    {      
        public long Id { get; set; }
        public string AngularModel { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string DataType { get; set; }
        public bool IsDeleted { get; set;}
        public string ValueCommon { get; set; }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}