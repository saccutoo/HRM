using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class Sec_User
    {
        public int OriginalUserId { get; set; }
        public int LoginUserId { get; set; }
        public int RoleId { get; set; }
        public int StaffLevelID { get; set; }
        public int OrganizationUnitID { get; set; }
        public string OrganizationUnitName { get; set; }
        public string MCCs { get; set; }
        public string BMs { get; set; }
        public int BranchId { get; set; }
        public int OfficePositionID { get; set; }
        public int? CompanyType { get; set; }
        public double ClientLimit { get; set; }

    }

    public class Sec_UserLogin
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public int StaffLevelID { get; set; }
        public string Email { get; set; }
        public int LockedBy { get; set; }
        public bool IsActivated { get; set; }
        public int BranchId { get; set; }
        public int OriginalUserId { get; set; }
        public int LoginUserId { get; set; }
        public string DisplayName { get; set; }
        public bool IsLocked { get; set; }
        public int OrganizationUnitID { get; set; }
        public string OrganizationUnitName { get; set; }
        public string MCCs { get; set; }
        public string BMs { get; set; }
        //    public int OfficePositionID { get; set; }
        public int OfficePositionID { get; set; }
        public string Password { get; set; }
        public int CurrentLanguageID { get; set; }

        public int? CompanyType { get; set; }
        public int CurrencyTypeID { get; set; }
        //   public int RoleId { get; set; }
        public double ClientLimit { get; set; }
        public bool NeedChangePassword { get; set; }
    }
}
