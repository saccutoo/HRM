using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hrm.Admin.Controllers
{
    public partial class PermissionController : Hrm.Framework.Controllers.OrganizationController
    {
        #region Fields
        private IPermissionService _permissionService;
        private IOrganizationService _organizationService;
        private IMenuService _menuService;
        private IStaffService _staffService;
        private ILocalizationService _localizationService;
        private IRoleService _roleService;

        private long _languageId;
        private long _roleId;
        private long _staffId;

        #endregion Fields
        #region Constructors
        public PermissionController(IPermissionService permissionService, IOrganizationService organizationService, IMenuService menuService, IStaffService staffService, ILocalizationService localizationService, IRoleService roleService)
        {
            _permissionService = permissionService;
            _organizationService = organizationService;
            _menuService = menuService;
            _staffService = staffService;
            _localizationService = localizationService;
            _roleService = roleService;
            _languageId = CurrentUser.LanguageId;
            _roleId = CurrentUser.RoleId;
            _staffId = CurrentUser.UserId;
        }
        #endregion
        public ActionResult PermissionRole()
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<RoleModel> listRole = new List<RoleModel>();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            List<MenuModel> listMenu = new List<MenuModel>();

            var responeRoles = _roleService.GetRole();
            if (responeRoles!=null)
            {
                var resultRoles = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(responeRoles);
                if (!CheckPermission(resultRoles))
                {
                    //return to Access Denied
                }
                else
                {
                    listRole = resultRoles.Results;
                }
            }
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }

            if (listRole.Count>0)
            {
                var responeRole = _roleService.GetRoleById(listRole[0].Id);
                if (responeRole != null)
                {
                    var resultRole = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(responeRole);
                    if (!CheckPermission(resultRole))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        menuPermissionView.Role = resultRole.Results[0];
                    }
                }
                var responeMenuPermission = _permissionService.GetPermissionByRoleId(listRole[0].Id);
                if (responeMenuPermission != null)
                {
                    var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                    if (!CheckPermission(resultMenuPermission))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        listMenuPermission = resultMenuPermission.Results;
                    }
                }
            }

            menuPermissionView.isView = true;
            menuPermissionView.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listRole));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            return View(menuPermissionView);
        }
        public ActionResult AddPermissionRole(long id = 0)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<RoleModel> listRole = new List<RoleModel>();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            List<MenuModel> listMenu = new List<MenuModel>();
            menuPermissionView.Role = new RoleModel();          
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }
            var responeMenuPermission = _permissionService.GetPermissionByRoleId(id);
            if (responeMenuPermission != null)
            {
                var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                if (!CheckPermission(resultMenuPermission))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenuPermission = resultMenuPermission.Results;
                }
            }
            if (id!=0)
            {
                var responeRole = _roleService.GetRoleById(id);
                if (responeRole != null)
                {
                    var resultRole = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(responeRole);
                    if (!CheckPermission(resultRole))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        menuPermissionView.Role = resultRole.Results[0];
                    }
                }
            }
            menuPermissionView.isView = false;
            menuPermissionView.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listRole));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            return View(menuPermissionView);
        }

        public ActionResult PermissionUser()
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            List<MenuModel> listMenu = new List<MenuModel>();
            List<StaffModel> listStaff = new List<StaffModel>();
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _staffId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaffs = _staffService.GetAllStaffForDropDown(paramEntity);
            if (responseStaffs != null)
            {
                var resultStaffs = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaffs);
                if (!CheckPermission(resultStaffs))
                {
                    //return to Access Denied
                }
                else
                {
                    listStaff = resultStaffs.Results;
                }
            }
            if (listStaff.Count>0)
            {
                var responeMenuPermission = _permissionService.GetPermissionByStaffId(listStaff[0].Id);
                if (responeMenuPermission != null)
                {
                    var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                    if (!CheckPermission(resultMenuPermission))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        listMenuPermission = resultMenuPermission.Results;
                    }
                }
                var responeStaff = _staffService.GetStaffInformationById(listStaff[0].Id);
                if (responeStaff != null)
                {
                    var resultStaff = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responeStaff);
                    if (!CheckPermission(resultStaff))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        menuPermissionView.Staff = resultStaff.Results[0];
                    }
                }
            }
           
            menuPermissionView.isView = true;
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            menuPermissionView.Staffs = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listStaff));
            return View(menuPermissionView);
        }
        public ActionResult AddPermissionUser(long id=0)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            List<MenuModel> listMenu = new List<MenuModel>();
            List<StaffModel> listStaff = new List<StaffModel>();
            var responeMenuPermission = _permissionService.GetPermissionByStaffId(id);
            if (responeMenuPermission != null)
            {
                var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                if (!CheckPermission(resultMenuPermission))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenuPermission = resultMenuPermission.Results;
                }
            }
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = _languageId,
                RoleId = _roleId,
                UserId = _staffId,
                DbName = CurrentUser.DbName
            };
            var paramEntity = MapperHelper.Map<BasicParamModel, BasicParamType>(param);
            var responseStaff = _staffService.GetAllStaffForDropDown(paramEntity);
            if (responseStaff != null)
            {
                var resultStaff = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responseStaff);
                if (!CheckPermission(resultStaff))
                {
                    //return to Access Denied
                }
                else
                {
                    listStaff = resultStaff.Results;
                }
            }
            menuPermissionView.Staff = new StaffModel();
            menuPermissionView.Staff.Id = id;
            menuPermissionView.isView = false;
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            menuPermissionView.Staffs = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listStaff));

            return View(menuPermissionView);
        }
        public ActionResult SavePermission(StaffRolePermissionModel staffRolePermisson, List<MenuPermissionModel> menuPermission)
        {
            if (staffRolePermisson != null)
            {
                if (staffRolePermisson.DataId==0 && staffRolePermisson.DataTypeName == DataType.Role)
                {
                    staffRolePermisson.DataId = -1;
                }
                var validations = ValidationHelper.Validation(staffRolePermisson, "staffRolePermisson");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            if (staffRolePermisson.DataId < 0)
            {
                staffRolePermisson.DataId = 0;
            }
            foreach (var item in menuPermission)
            {
                item.CreatedBy = _staffId;
                item.UpdatedBy = _staffId;
            }
            var staffRolePermissonEntity = MapperHelper.Map<StaffRolePermissionModel, StaffRolePermissionEntity>(staffRolePermisson);
            var menuPermissionType = MapperHelper.MapList<MenuPermissionModel, MenuPermissionType>(menuPermission);
            var responeseResources = string.Empty;
            var result = new HrmResultModel<bool>();
            var response = _permissionService.SavePermission(staffRolePermissonEntity, menuPermissionType);
            if (response != null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {

                    if (staffRolePermisson.DataId == 0)
                    {
                        if (result.Success == true)
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UpdateFail");
                        }
                    }
                    else
                    {
                        if (result.Success == true)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.Successful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                    }                    
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPermissionByStaffId(long id, bool isView)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<MenuModel> listMenu = new List<MenuModel>();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }
            var responeMenuPermission = _permissionService.GetPermissionByStaffId(id);
            if (responeMenuPermission != null)
            {
                var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                if (!CheckPermission(resultMenuPermission))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenuPermission = resultMenuPermission.Results;
                }
            }
            var responeStaff = _staffService.GetStaffInformationById(id);
            if (responeStaff != null)
            {
                var resultStaff = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(responeStaff);
                if (!CheckPermission(resultStaff))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultStaff.Results.Count>0)
                    {
                        menuPermissionView.Staff = resultStaff.Results[0];
                    }
                }
            }
            menuPermissionView.isView = isView;
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            return PartialView(UrlHelpers.TemplateAdmin("Permission", "_ListPermission.cshtml"), menuPermissionView);
        }
        public ActionResult DeletePermissionByDataId(long dataId, string dataTypeName)
        {
            var responeseResources = string.Empty;
            var result = new HrmResultModel<bool>();
            var response = _permissionService.DeletePermissionByDataId(dataId, dataTypeName);
            if (response!=null)
            {
                result = JsonConvert.DeserializeObject<HrmResultModel<bool>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    if (result.Success==true)
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.Successful");
                    }
                    else
                    {
                        responeseResources = _localizationService.GetResources("Message.Delete.UnSuccessful");
                    }
                }
            }
            return Json(new { result, responeseResources }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPermissionByRoleId(long id, bool isView)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            List<RoleModel> listRole = new List<RoleModel>();
            List<MenuPermissionModel> listMenuPermission = new List<MenuPermissionModel>();
            List<MenuModel> listMenu = new List<MenuModel>();

           
            var responseMenu = _menuService.GetMenuByParentId(0);
            if (responseMenu != null)
            {
                var resultMenu = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(responseMenu);
                if (!CheckPermission(resultMenu))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenu = resultMenu.Results;
                }
            }
            var responeMenuPermission = _permissionService.GetPermissionByRoleId(id);
            if (responeMenuPermission != null)
            {
                var resultMenuPermission = JsonConvert.DeserializeObject<HrmResultModel<MenuPermissionModel>>(responeMenuPermission);
                if (!CheckPermission(resultMenuPermission))
                {
                    //return to Access Denied
                }
                else
                {
                    listMenuPermission = resultMenuPermission.Results;
                }
            }
            var responeRole = _roleService.GetRoleById(id);
            if (responeRole != null)
            {
                var resultRole = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(responeRole);
                if (!CheckPermission(resultRole))
                {
                    //return to Access Denied
                }
                else
                {
                    menuPermissionView.Role = resultRole.Results[0];
                }
            }
            menuPermissionView.isView = isView;
            menuPermissionView.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listRole));
            menuPermissionView.Menus = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenu));
            menuPermissionView.MenuPermissions = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(listMenuPermission));
            return PartialView(UrlHelpers.TemplateAdmin("Permission", "_ListPermission.cshtml"), menuPermissionView);
        }
        public ActionResult SearchPermissionStaff(string searchKey)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            menuPermissionView.Staffs = new List<dynamic>();
            var response = _staffService.SearchPermissionStaff(searchKey);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<StaffModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    menuPermissionView.Staffs = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Permission", "_ListUser.cshtml"), menuPermissionView);

        }
        public ActionResult SearchRole(string searchKey)
        {
            MenuPermissionViewModel menuPermissionView = new MenuPermissionViewModel();
            menuPermissionView.Roles = new List<dynamic>();
            var response = _roleService.SearchRole(searchKey,CurrentUser.LanguageId);
            if (response != null)
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<RoleModel>>(response);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    menuPermissionView.Roles = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(result.Results));
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Permission", "_ListRole.cshtml"), menuPermissionView);

        }

    }
}
