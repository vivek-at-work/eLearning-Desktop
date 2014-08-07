using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Coneixement.LicencingModule.ViewModals;
using Coneixement.LicencingModule.Views;
namespace Coneixement.LicencingModule
{
   public class LicencingModule :BaseModule
    {
       public LicencingModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<ILicencingViewModal, LicencingViewModal>();
            this.Container.RegisterType<ILicenceContolView, LicencingControl>();
            this.RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(LicencingControl));
        }
    }
}