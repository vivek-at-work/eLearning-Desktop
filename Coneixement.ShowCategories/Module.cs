using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using Coneixement.ShowCategories.Intefaces;
using Coneixement.ShowCategories.Views;
using Coneixement.ShowCategories.ViewModal;
namespace Coneixement.ShowCategories
{
   public class Module:BaseModule
    {
       public Module(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<ICategoriesView, CategoriesView>();
            this.Container.RegisterType<ICategoriesViewModal, CategoriesViewModal>();
            this.RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(CategoriesView));
        }
    }
}
