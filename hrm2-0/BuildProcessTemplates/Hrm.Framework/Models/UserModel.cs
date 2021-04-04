using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public long UserType { get; set; }
        public long Status { get; set; }
        public long LanguageId { get; set; }
        public bool IsActivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string ErrMess { get; set; } = string.Empty;
        public long UserId { get; set; }
        public long CurrencyId { get; set; }
        public long RoleId { get; set; }

    }
}