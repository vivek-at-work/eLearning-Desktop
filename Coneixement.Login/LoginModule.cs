using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Coneixement.Login.Interfaces;
using Coneixement.Login.Views;
using Coneixement.Infrastructure;
namespace Coneixement.Login
{
    public class LoginModule : BaseModule
    {
        public LoginModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            Container.RegisterType<ILoginViewModel, LoginViewModel>();
            Container.RegisterType<ILoginView, LoginControl>();
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(LoginControl));
        }
    }
}
