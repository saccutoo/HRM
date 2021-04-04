using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class DepartmentModel
    {
        public int id { get; set; }
        public string label { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public int ParentID { get; set; }
        public bool? collapsed { get; set; }
        public List<DepartmentModel> children { get; set; }
        private DepartmentModel _instance;
        public DepartmentModel()
        {
            _instance = this;
        }
        public Department GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DepartmentModel, Department>();
            });

            Department entity = configMapper.CreateMapper().Map<DepartmentModel, Department>(this);

            return entity;
        }
        public DepartmentModel(Department entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Department, DepartmentModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
