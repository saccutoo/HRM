using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
   public class EmployeeModel
    {
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public string StaffCode { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public string GenderID { get; set; }
        public int ProvinceID { get; set; }
        public int ContactCountryID { get; set; }
        public int ContactProvinceID { get; set; }
        public string OrganizationUnitName { get; set; }
        public string OfficePositionID { get; set; }
        public string Status { get; set; }
        private EmployeeModel _instance;
        public EmployeeModel()
        {
            _instance = this;
        }
        public Employee GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeModel, Employee>();
            });

            Employee entity = configMapper.CreateMapper().Map<EmployeeModel, Employee>(this);

            return entity;
        }
        public EmployeeModel(Employee entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Employee, EmployeeModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }

}
