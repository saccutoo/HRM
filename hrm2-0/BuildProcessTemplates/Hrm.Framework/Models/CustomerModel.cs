using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class CustomerModel: BaseModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DbName { get; set; }
        public DateTime StartedDate { get; set; }
        public string Theme { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public bool IsDuplicate{ get; set; }
        public string UserName { get; set; }
    }
}
