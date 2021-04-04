using System;
using System.Linq;
using System.Reflection;
using Autofac;
using System.IO;
using System.Collections.Generic;
using Hrm.Core.Infrastructure;
using Hrm.Core.Infrastructure.DependencyManagement;

namespace Hrm.Core.Infrastructure
{
    public class HrmStarter
    {
        private readonly object _locker = new object();
        private bool _configured;
        private IContainer _container;

        public IContainer BuildContainer()
        {
            lock (_locker)
            {
                if (_configured)
                    return _container;

                var builder = new ContainerBuilder();
                var typeFinder = new WebAppTypeFinder();
                builder.Register(c => typeFinder);
                //find IDependencyRegistar implementations
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                foreach (var t in drTypes)
                {
                    dynamic dependencyRegistar = Activator.CreateInstance(t);
                    dependencyRegistar.Register(builder, typeFinder);
                }
                _container = builder.Build();
                _configured = true;
                return _container;
            }
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        public void ExecuteStartUpTasks()
        {
            var startUpTaskTypes = _container.Resolve<ITypeFinder>().FindClassesOfType<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
            {
                var startUpTask = ((IStartupTask)Activator.CreateInstance(startUpTaskType));
                startUpTask.Execute();
            }
        }

    }
}
