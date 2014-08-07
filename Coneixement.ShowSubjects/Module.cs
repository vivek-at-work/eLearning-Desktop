using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using Coneixement.ShowSubjects.Intefaces;
using Coneixement.ShowSubjects.Views;
using Coneixement.ShowSubjects.ViewModal;
namespace Coneixement.ShowSubjects
{
   public class ShowSubjectsModule:BaseModule
    {
       public ShowSubjectsModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<ISubjectsView, SubjectView>();
            this.Container.RegisterType<ISubjectsViewModal, SubjectsViewModal>();
            this.RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(SubjectView));
        }
    }
}
