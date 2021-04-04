using AutoMapper;
using HRM.Common;
using HRM.DataAccess.Entity;
using HRM.Utils;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using HRM.Constants;
namespace HRM.Models
{
    public class Sec_UserModel : BaseModel
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
        public bool IsLock { get; set; }
        public bool IsActive { get; set; }
        public DateTime LockedDate { get; set; }
        public string LockedBy { get; set; }
        public DateTime ActivatedDate { get; set; }
        public int ActivatedBy { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int UseridV1 { get; set; }
        public int CurrencyTypeID { get; set; }
        public string PasswordPrevious { get; set; }
        public int temp { get; set; }
        public int AutoAdsID { get; set; }
        public int ImportDataID { get; set; }
        public string DepartmentName { get; set; }
        public string Capcha { get; set; }

        public string RedirectUrl { get; set; }
        public int hdLanguage { get; set; }
        public DateTime? PasswordLastChange { get; set; }
        private Sec_UserModel _instance;
        public Sec_UserModel()
        {
            _instance = this;
        }
        public Sec_User GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sec_UserModel, Sec_User>();
            });

            Sec_User entity = configMapper.CreateMapper().Map<Sec_UserModel, Sec_User>(this);

            return entity;
        }
        public Sec_UserModel(Sec_User entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Sec_User, Sec_UserModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }

    public class Sec_UserListModel : BaseListModel
    {
        public Sec_UserListModel()
        {
            LstData = new List<Sec_UserFullInfoModel>();
        }
        public List<Sec_UserFullInfoModel> LstData { get; set; }

        public string Email { get; set; }
        public int UserType { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsLock { get; set; }
        public int Status { get; set; } 
        public List<EnumModel> StatusList
        {
            get
            {
                var lstResult = new List<EnumModel>();

                var lstEnumStatus = Enum.GetValues(typeof(Global.EUserStatus)).Cast<Global.EUserStatus>().ToList();

                if (Global.CurrentLanguage == (int)Constant.numLanguage.VN)
                {
                    lstResult.Add(new EnumModel { Value = 0, Name = "Tất cả trạng thái" });
                }
                else
                {
                    lstResult.Add(new EnumModel { Value = 0, Name = "All status" });
                }

                foreach (var e in lstEnumStatus)
                {
                    var name = EnumUtils.GetAttributeDescription(e).Split(',');
                    if (Global.CurrentLanguage == (int)Constant.numLanguage.VN)
                    {
                        lstResult.Add(new EnumModel { Name = name[0], Value = e.GetHashCode() });
                    }
                    else
                    {
                        lstResult.Add(new EnumModel { Name = name[1], Value = e.GetHashCode() });
                    }
                }

                return lstResult;
            }
        }
    }

    public partial class Sec_UserFullInfoModel : BaseModel
    {
        public Sec_UserFullInfoModel()
        {
            Status = 0;
            PasswordExpiredDate = DateTime.Now.AddDays(1);
        }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CurrentLanguageID { get; set; }
        public int UserType { get; set; }
        public string DisplayName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public int Status { get; set; }
        public string StatusName
        {
            get
            {
                if (Status == 0)
                {
                    return "N/A";
                }
                else
                {
                    var statusName = EnumUtils.GetAttributeDescription((Global.EUserStatus)Status).Split(',');
                    if (Global.CurrentLanguage == (int)Constant.numLanguage.VN)
                    {
                        return statusName[0];
                    }
                    else
                    {
                        return statusName[1];
                    }
                }
            }

        }
        public DateTime PasswordExpiredDate { get; set; }

        public List<EnumModel> StatusList
        {
            get
            {
                var lstResult = new List<EnumModel>();

                var lstEnumStatus = Enum.GetValues(typeof(Global.EUserStatus)).Cast<Global.EUserStatus>().ToList();

                if (Global.CurrentLanguage == (int)Constant.numLanguage.VN)
                {
                    lstResult.Add(new EnumModel { Value = 0, Name = "Tất cả trạng thái" });
                }
                else
                {
                    lstResult.Add(new EnumModel { Value = 0, Name = "All status" });
                }

                foreach (var e in lstEnumStatus)
                {
                    var name = EnumUtils.GetAttributeDescription(e).Split(',');
                    if (Global.CurrentLanguage == (int)Constant.numLanguage.VN)
                    {
                        lstResult.Add(new EnumModel { Name = name[0], Value = e.GetHashCode() });
                    }
                    else
                    {
                        lstResult.Add(new EnumModel { Name = name[1], Value = e.GetHashCode() });
                    }
                }

                return lstResult;
            }
        }
    } 

    public class Sec_UserRoleModel : BaseModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public List<int> Roles { get; set; }
        public string SelectedRoleIds { get; set; }
        public List<RoleModel> AllRole { get; set; }
    }

    public class AddSwitchModel : BaseModel
    {
        //from acc
        public int UserID { get; set; }
        //to acc
        public string Email { get; set; }
    }
}