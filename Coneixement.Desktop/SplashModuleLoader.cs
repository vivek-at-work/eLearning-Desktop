using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Events;

namespace Coneixement.Desktop
{
    class SplashModuleLoader
    {
        public class SplashModuleLoader : ModuleLoader
        {
            private readonly IContainerFacade container;

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="containerFacade"></param>
            /// <param name="loggerFacade"></param>
            public SplashModuleLoader(IContainerFacade containerFacade, ILoggerFacade loggerFacade)
                : base(containerFacade, loggerFacade)
            {
                this.container = containerFacade;
            }

            /// <summary>
            /// Creates an IModule using the container.
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            protected override IModule CreateModule(Type type)
            {
                IModule module = container.Resolve(type) as IModule;
                OnModuleInitializing(type);
                return module;
            }

            /// <summary>
            /// Raises ModuleInitializing event when a
            /// module is being initialized.
            /// </summary>
            /// <param name="type"></param>
            protected virtual void OnModuleInitializing(Type type)
            {
                if (ModuleInitializing != null)
                    ModuleInitializing(this, new DataEventArgs<Type>(type));
            }

            /// <summary>
            /// Event raised when a module is being initialized.
            /// </summary>
            public event EventHandler<DataEventArgs<Type>> ModuleInitializing;
        }
    }
}
