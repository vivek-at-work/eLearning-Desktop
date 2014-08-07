using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
namespace Coneixement.Infrastructure.Base
{
    public abstract class BaseModule : IModule
    {
        /// <summary>
        /// Abstract method to register types
        /// </summary>
        protected abstract void RegisterTypes();
        /// <summary>
        /// Gets or set Unity container
        /// </summary>
        protected IUnityContainer Container { get; set; }
        /// <summary>
        /// Gets or sets region manager
        /// </summary>
        protected IRegionManager RegionManager { get; set; }
        /// <summary>
        ///  Initializes a new instance of the <see cref="BaseModule" /> class.
        /// </summary>
        /// <param name="container">unity container</param>
        /// <param name="regionManager">region manager</param>
        public BaseModule(IUnityContainer container , IRegionManager regionManager)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }
        /// <summary>
        /// Method to initialize the module
        /// </summary>
        public void Initialize()
        {
            this.RegisterTypes();
        }
    }
}
