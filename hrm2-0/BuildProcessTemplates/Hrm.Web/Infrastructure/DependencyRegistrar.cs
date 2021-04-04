using Autofac;
using Hrm.Core.Infrastructure;
using Hrm.Core.Infrastructure.DependencyManagement;
using Hrm.Repository;
using Hrm.Service;
using Hrm.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            ////repositories
            builder.RegisterType<CommonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationRepository>().As<ILocalizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRepository>().As<IStaffRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingProcessRepository>().As<IWorkingProcessRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PipelineRepository>().As<IPipelineRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TableRepository>().As<ITableRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TableConfigRepository>().As<ITableConfigRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRelationShipsRepository>().As<IStaffRelationShipsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRoleRepository>().As<IStaffRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaffBonusDisciplineRepository>().As<IStaffBonusDisciplineRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdayRepository>().As<IWorkingdayRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DashboardRepository>().As<IDashboardRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryRepository>().As<ISalaryRepository>().InstancePerLifetimeScope();

            ////services
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffService>().As<IStaffService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingProcessService>().As<IWorkingProcessService>().InstancePerLifetimeScope();
            builder.RegisterType<TableService>().As<ITableService>().InstancePerLifetimeScope();
            builder.RegisterType<TableConfigService>().As<ITableConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<PipelineService>().As<IPipelineService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRelationShipsService>().As<IStaffRelationShipsService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRoleService>().As<IStaffRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffBonusDisciplineService>().As<IStaffBonusDisciplineService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdayService>().As<IWorkingdayService>().InstancePerLifetimeScope();
            builder.RegisterType<DashboardService>().As<IDashboardService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryService>().As<ISalaryService>().InstancePerLifetimeScope();

            //controller
            builder.RegisterType<CommonController>();
            builder.RegisterType<AuthenticationController>();
            builder.RegisterType<StaffController>();
            builder.RegisterType<MenuController>();
            builder.RegisterType<OrganizationController>();
            builder.RegisterType<WorkingProcessController>();
            builder.RegisterType<WorkingdayController>();
            builder.RegisterType<FilterController>();
            builder.RegisterType<DashboardController>();
            builder.RegisterType<AttachmentController>();
            builder.RegisterType<SalaryController>();

        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 3; }
        }
    }
}
