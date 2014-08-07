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
using Coneixement.SuggestionsForUserModule.Interfaces;
using Microsoft.Practices.Unity;
using Coneixement.SuggestionsForUser;
using SuggestionsForUserModule.Interfaces;
using Coneixement.Infrastructure;
namespace Coneixement.SuggestionsForUserModule
{
    public class SuggestionsForUserModule:BaseModule
    {
        public SuggestionsForUserModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<ISugesstionView, SuggestionsControl>();
            this.Container.RegisterType<ISuggestionsViewModel, SuggestionsViewModel>();
            this.RegionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(SuggestionsControl));
        }
    }
}
