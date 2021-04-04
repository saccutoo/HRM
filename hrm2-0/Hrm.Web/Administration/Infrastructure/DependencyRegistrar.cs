using Autofac;
using Hrm.Core.Infrastructure;
using Hrm.Core.Infrastructure.DependencyManagement;
using Hrm.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Common;
using Hrm.Service;
using Hrm.Repository;
using Hrm.Repository.IRepositories;
using Hrm.Service.IServices;
using Hrm.Service.Services;

namespace Hrm.Admin.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //repositories
            builder.RegisterType<CommonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MasterDataRepository>().As<IMasterDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PipelineRepository>().As<IPipelineRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmailRepository>().As<IEmailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChecklistRepository>().As<IChecklistRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissonRepository>().As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChecklistDetailRepository>().As<IChecklistDetailRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TableColumnRepository>().As<ITableColumnRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRepository>().As<IStaffRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AttachmentRepository>().As<IAttachmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdayRepository>().As<IWorkingdayRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingDaySupplementConfigurationRepository>().As<IWorkingDaySupplementConfigurationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingDaySupplementConfigurationExceptionRepository>().As<IWorkingDaySupplementConfigurationExceptionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AnnualLeaveRepository>().As<IAnnualLeaveRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryElementRepository>().As<ISalaryElementRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryTypeRepository>().As<ISalaryTypeRepository>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<MasterDataService>().As<IMasterDataService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope();
            builder.RegisterType<PipelineService>().As<IPipelineService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<ChecklistService>().As<IChecklistService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<ChecklistDetailService>().As<IChecklistDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<TableColumnService>().As<ITableColumnService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffService>().As<IStaffService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<AttachmentService>().As<IAttachmentService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdayService>().As<IWorkingdayService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingDaySupplementConfigurationService>().As<IWorkingDaySupplementConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingDaySupplementConfigurationExceptionService>().As<IWorkingDaySupplementConfigurationExceptionService>().InstancePerLifetimeScope();
            builder.RegisterType<AnnualLeaveService>().As<IAnnualLeaveService>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryElementService>().As<ISalaryElementService>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryTypeService>().As<ISalaryTypeService>().InstancePerLifetimeScope();

            //controller
            builder.RegisterType<HomeController>();
            builder.RegisterType<SettingController>().InstancePerLifetimeScope();
            builder.RegisterType<MasterDataController>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationController>().InstancePerLifetimeScope();
            builder.RegisterType<PipelineController>().InstancePerLifetimeScope();
            builder.RegisterType<EmailController>().InstancePerLifetimeScope();
            builder.RegisterType<ChecklistController>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionController>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdayController>().InstancePerLifetimeScope();
            builder.RegisterType<WorkingdaySupplementConfigurationController>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryElementController>().InstancePerLifetimeScope();
            builder.RegisterType<SalaryTypeController>().InstancePerLifetimeScope();



        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }
}