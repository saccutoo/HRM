using Autofac;

namespace Hrm.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {

        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }
}
