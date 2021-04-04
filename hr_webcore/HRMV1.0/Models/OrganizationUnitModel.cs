using AutoMapper;
using HRM.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class OrganizationUnitModel
    {
        public int OrganizationUnitID { get; set; }
        public string OrganizationUnitCode { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public int ParentID { get; set; }
        public int DS_CompanyID { get; set; }
        public int DS_UnitID { get; set; }
        public int DS_BranchID { get; set; }
        public int DS_BUID { get; set; }
        public int OrderNo { get; set; }
        public string Status { get; set; }
        public bool IsImplementationReport { get; set; }
        public int Type { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float CommitmentBudgetLimit { get; set; }
        public float MarginMultiplierRate { get; set; }
        public int CompanyType { get; set; }
        public int CurrencyTypeID { get; set; }
        public bool HasChild { get; set; }
        public string StatusName { get; set; }
        private OrganizationUnitModel _instance;
        public OrganizationUnitModel()
        {
            _instance = this;
        }
        public OrganizationUnit GetEntity()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrganizationUnitModel, OrganizationUnit>();
            });

            OrganizationUnit entity = configMapper.CreateMapper().Map<OrganizationUnitModel, OrganizationUnit>(this);

            return entity;
        }
        public OrganizationUnitModel(OrganizationUnit entity, bool encodeHtml = false)
        {
            if (entity != null)
            {
                var configMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrganizationUnit, OrganizationUnitModel>();
                });
                configMapper.CreateMapper().Map(entity, this);

            }
        }
    }
}
