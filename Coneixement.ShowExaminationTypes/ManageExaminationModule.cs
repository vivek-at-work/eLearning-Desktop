using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using Coneixement.ShowExaminationTypes.Intefaces;
using Coneixement.ShowExaminationTypes.Views;
using Coneixement.ShowExaminationTypes.ViewModals;
namespace Coneixement.ShowExaminationTypes
{
    public class ManageExaminationModule : BaseModule
    {
        public ManageExaminationModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            Container.RegisterType<IShowPreviuosYearPaperTypesViewModal, ShowPreviuosYearPaperTypesViewModal>();
            Container.RegisterType<IShowTestSeriesSubjectsViewModal, ShowTestSeriesSubjectsViewModal>();
            Container.RegisterType<IShowTestSeriesConceptViewModal, ShowTestSeriesConceptsViewModal>();
            Container.RegisterType<IShowTestSeriesTypesViewModal, ShowTestSeriesTypesViewModal>();
            Container.RegisterType<IShowExaminationsViewModal, ShowExaminationsViewModal>();
            Container.RegisterType<IShowPreviuosYearPaperTypesView, ShowPreviuosYearPaperTypes>();
            Container.RegisterType<IShowTestSeriesSubjectsView, ShowTestSeriesSubjects>();
            Container.RegisterType<IShowTestSeriesTopicsView, ShowTestSeriesConcepts>();
            Container.RegisterType<IShowTestSeriesTypesView, ShowTestSeriesTypes>();
            Container.RegisterType<IShowExaminationsView, ShowExaminations>();
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(ShowTestSeriesTypes));
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(ShowTestSeriesSubjects));
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(ShowTestSeriesConcepts));
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(ShowPreviuosYearPaperTypes));
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(ShowExaminations));
        }
    }
}
