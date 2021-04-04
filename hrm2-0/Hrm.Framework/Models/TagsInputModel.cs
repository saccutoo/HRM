using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class TagsInputModel : BaseModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<dynamic> Data { get; set; }
    }
}