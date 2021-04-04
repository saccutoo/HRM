using AutoMapper;
using ERP.Framework.App_LocalResources;
using HRM.Common;
using HRM.DataAccess.Entity;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRM.Constants;

namespace HRM.Models
{
    public class Sec_MenuModel : BaseModel
    {
        public Sec_MenuModel()
        {
            SubMenu = new List<Sec_MenuModel>();
        }
        public int MenuID { get; set; }
        [Display(Name = "PrPhone", ResourceType = typeof(AppRes))]
        [Required(ErrorMessageResourceName = "ErrorMessageNull", ErrorMessageResourceType = typeof(AppRes))]
        //[RegularExpression("^[0-9]*$", ErrorMessageResourceName = "ErrorPhone", ErrorMessageResourceType = typeof(AppRes))] dùng số
        public string ActionName { get; set; }
        public string MenuName { get; set; }
        public string MenuName_EN { get; set; }
        public int ParentId { get; set; }
        public int OrderNo { get; set; }
        public string CssIconClass { get; set; }
        
        public List<Sec_MenuModel> SubMenu { get; set; }
        public string MenuNameByLanguage
        {
            get
            {
                if (Global.CurrentLanguage == (int)Constant.numLanguage.EN)
                    return MenuName_EN;
                return MenuName;
            }
        }

        public string Url { get; set; }
        public string Param { get; set; }
        public bool IsActive { get; set; }
        public List<Models.Sec_MenuModel> ParentList{get; set;}      
    
        //public List<Sec_Action> ActionList
        //{
        //    get
        //    {
        //        return null;
        //        var repon = new ActionReponsitory();
        //        return repon.GetAll();
        //    }
        //}

        public string ControllerAction { get; set; }
        public Nullable<int> ControllerID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public bool IncludeMenu { get; set; }
        public int MenuPositionID { get; set; }
        public int ParentID { get; set; }

        public int TreeLevel { get; set; }
        public bool HasChild { get; set; }

        private Sec_MenuModel _instance;
        //public Sec_MenuModel()
        //{
        //    _instance = this;
        //}
        public Sec_Menu GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sec_MenuModel, Sec_Menu>();
            });

            Sec_Menu entity = configMapper.CreateMapper().Map<Sec_MenuModel, Sec_Menu>(this);

            return entity;
        }
        public Sec_MenuModel(Sec_Menu entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Sec_Menu, Sec_MenuModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}