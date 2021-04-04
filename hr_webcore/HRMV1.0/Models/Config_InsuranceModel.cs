using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Config_InsuranceModel
    {

        public int AutoID { get; set; }
        public int InsuranceTypeID { get; set; }
        public string InsuranceTypeName { get; set; }
        public string DecisionCode { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ApplyDate { get; set; }

        public double RateCompany { get; set; }
        public double RatePerson { get; set; }

        public double Total { get; set; }
        private Config_InsuranceModel _instance;
        public Config_InsuranceModel()
        {
            _instance = this;
        }
        public Config_Insurance GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Config_InsuranceModel, Config_Insurance>();
            });

            Config_Insurance entity = configMapper.CreateMapper().Map<Config_InsuranceModel, Config_Insurance>(this);

            return entity;
        }
        public Config_InsuranceModel(Config_Insurance entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Config_Insurance, Config_InsuranceModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
