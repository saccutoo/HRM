using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using HRM.DataAccess.Entity;
namespace HRM.Models
{
    public class GlobalListModel
    {
        private GlobalListModel _instance;
        public int GlobalListID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Value { get; set; }
        public string ValueEN { get; set; }
        public string IsActive { get; set; }
        public string Descriptions { get; set; }
        public int ParentID { get; set; }
        public int TreeLevel { get; set; }
        public bool HasChild { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> idOld { get; set; }
        public string ListChildID { get; set; }

        public string DisplayName { get; set; }
        public GlobalListModel()
        {
            _instance = this;
        }
        public GlobalList GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GlobalListModel, GlobalList>();
            });

            GlobalList entity = configMapper.CreateMapper().Map<GlobalListModel, GlobalList>(this);

            return entity;
        }
        public GlobalListModel(GlobalList entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GlobalList, GlobalListModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }

}
