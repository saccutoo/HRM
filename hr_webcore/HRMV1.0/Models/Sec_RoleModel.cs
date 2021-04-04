using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Sec_RoleModel
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        private Sec_RoleModel _instance;
        public Sec_RoleModel()
        {
            _instance = this;
        }
        public Sec_Role GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sec_RoleModel, Sec_Role>();
            });

            Sec_Role entity = configMapper.CreateMapper().Map<Sec_RoleModel, Sec_Role>(this);

            return entity;
        }
        public Sec_RoleModel(Sec_Role entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Sec_Role, Sec_RoleModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
