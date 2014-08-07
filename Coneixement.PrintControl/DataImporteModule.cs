using Coneixement.Infrastructure.Base;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using Coneixement.DataImporter.Interfaces;
using Coneixement.DataImporter.ViewModal;
using Coneixement.DataImporter.Views;
namespace Coneixement.DataImporter
{
    public class DataImporteModule : BaseModule
    {
        public DataImporteModule(IRegionManager regionManager, IUnityContainer container)
            : base(container, regionManager) { }
        protected override void RegisterTypes()
        {
            Container.RegisterType<IDataImportViewModal , DataImportViewModal>();
            Container.RegisterType<IDataImportView, DataImporteView>();
            RegionManager.RegisterViewWithRegion(RegionNames.ActionRegion, typeof(DataImporteView));
        }
    }
}
