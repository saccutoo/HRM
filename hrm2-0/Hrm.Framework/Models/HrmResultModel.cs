using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class HrmResultModel<T>
    {
        public List<T> Results { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
    }
}