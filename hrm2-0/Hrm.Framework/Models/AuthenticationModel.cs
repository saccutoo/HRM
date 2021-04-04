using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class AuthenticationModel
    {
        public List<dynamic> Languages { get; set; }
        public UserModel User { get; set; }
    }
}