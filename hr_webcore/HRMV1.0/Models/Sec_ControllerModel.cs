using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
     public class Sec_ControllerModel
    {
        public int ControllerID { get; set; }
        public string ControllerName { get; set; }
        private Sec_ControllerModel _instance;
        public Sec_ControllerModel()
        {
            _instance = this;
        }
        public Sec_Controller GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sec_ControllerModel, Sec_Controller>();
            });

            Sec_Controller entity = configMapper.CreateMapper().Map<Sec_ControllerModel, Sec_Controller>(this);

            return entity;
        }
        public Sec_ControllerModel(Sec_Controller entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Sec_Controller, Sec_ControllerModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
