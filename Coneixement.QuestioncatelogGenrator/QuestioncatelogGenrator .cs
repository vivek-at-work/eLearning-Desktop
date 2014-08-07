using Coneixement.Infrastructure.Base;
using Coneixement.QuestioncatelogGenrator.ViewModals;
using Coneixement.QuestioncatelogGenrator.Views;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Coneixement.QuestioncatelogGenrator
{
    public class QuestionCatelogGenrator  : BaseModule
    {
        public QuestionCatelogGenrator(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<ICatalogGeneratorViewModal, CatalogGeneratorViewModal>();
            this.Container.RegisterType<ICatalogGeneratorView, CatalogGeneratorView>();
            this.RegionManager.RegisterViewWithRegion(Coneixement.Infrastructure.RegionNames.ActionRegion, typeof(CatalogGeneratorView));
        }
    }
}
