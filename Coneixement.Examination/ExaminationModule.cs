using Microsoft.Practices.Prism.Regions;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Unity;
using Coneixement.Examination.ViewModals;
using Coneixement.Examination.Interfaces;
namespace Coneixement.Examination
{
    public class ExaminationModule : BaseModule
    {
        public ExaminationModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            this.Container.RegisterType<IExaminationViewModal, ExaminationViewModal>();
            this.Container.RegisterType<IExamination, Examination.Views.Examination>();
            this.RegionManager.RegisterViewWithRegion(Coneixement.Infrastructure.RegionNames.ActionRegion, typeof(Examination.Views.Examination));
        }
    }
}