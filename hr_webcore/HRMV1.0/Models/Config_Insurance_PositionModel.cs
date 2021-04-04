using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Config_Insurance_PositionModel
    {

        public int AutoID { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Note { get; set; }

        private Config_Insurance_PositionModel _instance;
        public Config_Insurance_PositionModel()
        {
            _instance = this;
        }
        public Config_Insurance_Position GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Config_Insurance_PositionModel, Config_Insurance_Position>();
            });

            Config_Insurance_Position entity = configMapper.CreateMapper().Map<Config_Insurance_PositionModel, Config_Insurance_Position>(this);

            return entity;
        }
        public Config_Insurance_PositionModel(Config_Insurance_Position entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Config_Insurance_Position, Config_Insurance_PositionModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
