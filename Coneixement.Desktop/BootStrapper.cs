using System;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Coneixement.Infrastructure;
using Microsoft.Practices.Prism.Logging;
namespace Coneixement.Desktop
{
    class BootStrapper : UnityBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter _logger = new EnterpriseLibraryLoggerAdapter();
        public BootStrapper()
        {
            _logger.Log("Application Initialting" , Category.Info , Priority.High);
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
        }
        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }
        protected override DependencyObject CreateShell()
        {
            ShellWindow Shell = new ShellWindow();
            Shell.Dispatcher.BeginInvoke((Action)delegate
            {
                Shell.Show();
                Shell.Visibility = Visibility.Collapsed;
            });
            _logger.Log("Shell Window Created" , Category.Info , Priority.High);
            return Shell;
        }
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    DirectoryModuleCatalog catalog = new DirectoryModuleCatalog();
        //    catalog.ModulePath = @"Modules";
        //    return catalog;
        //}
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog catalog = (ModuleCatalog)this.ModuleCatalog;
            catalog.AddModule(typeof(Coneixement.ShowExaminationTypes.ManageExaminationModule));
            catalog.AddModule(typeof(Coneixement.ShowCategories.Module));
            catalog.AddModule(typeof(Coneixement.SuggestionsForUserModule.SuggestionsForUserModule));
            catalog.AddModule(typeof(Coneixement.ShowSubjects.ShowSubjectsModule));
            catalog.AddModule(typeof(Coneixement.ImageViewer.ImageViewerModule));
            catalog.AddModule(typeof(Coneixement.Login.LoginModule));
            catalog.AddModule(typeof(Coneixement.SplashScreen.SplashScreenModule));
            catalog.AddModule(typeof(Coneixement.VideoPlayer.VideoPlayerModule));
            catalog.AddModule(typeof(Coneixement.Examination.ExaminationModule));
            catalog.AddModule(typeof(Coneixement.QuestioncatelogGenrator.QuestionCatelogGenrator));
            catalog.AddModule(typeof(Coneixement.LicencingModule.LicencingModule));
            catalog.AddModule(typeof(Coneixement.DataImporter.DataImporteModule));
        }
    }
}
