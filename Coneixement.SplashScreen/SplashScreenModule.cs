using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure.Events;
using Coneixement.SplashScreen.Interfaces;
namespace Coneixement.SplashScreen
{
    public class SplashScreenModule : BaseModule
    {
        public SplashScreenModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            Container.RegisterType<ISplashScreenViewModal, SplashScreenViewModal>();
            Container.RegisterType<ISplashView, SplashWindow>();
            SplashWindow view = new SplashWindow();
            view.Show();
        }
    }
}
