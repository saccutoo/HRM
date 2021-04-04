using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class SecUserModel
    {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime PasswordExpiredDate { get; set; }
            public DateTime CreatedDate { get; set; }
            public int CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public int? ModifiedBy { get; set; }
            public int CurrentLanguageID { get; set; }
            public int Status { get; set; }
            public int UserType { get; set; }
            public string DisplayName { get; set; }
            public bool IsLocked { get; set; }
            public bool IsActivated { get; set; }
            public DateTime? LockedDate { get; set; }
            public int? LockedBy { get; set; }
            public DateTime? ActivatedDate { get; set; }
            public int? ActivatedBy { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public int? UseridV1 { get; set; }
            public int? CurrencyTypeID { get; set; }
            public string PasswordPrevious { get; set; }
            public int? temp { get; set; }
            public int? AutoAdsID { get; set; }
            public int? ImportDataID { get; set; }
       
    }
}
