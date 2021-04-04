using Autofac;
using Hrm.Core.Infrastructure;
using Hrm.Core.Infrastructure.DependencyManagement;
using Hrm.Framework.Controllers;
using Hrm.Repository;
using Hrm.Service;

namespace Hrm.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            //repositories

            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationRepository>().As<ILocalizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ColumnValidationRepository>().As<IColumnValidationRepository>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<ColumnValidationService>().As<IColumnValidationService>().InstancePerLifetimeScope();
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterType<BaseController>().InstancePerLifetimeScope();
            builder.RegisterType<SearchController>().InstancePerLifetimeScope();
            builder.RegisterType<FilterController>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}