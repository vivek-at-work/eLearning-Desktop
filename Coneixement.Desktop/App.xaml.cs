using System;
using System.Windows;
using System.IO;
using Coneixement.Infrastructure.Helpers;
namespace Coneixement.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            foreach (string file in files)
            {
                File.SetAttributes(file , FileAttributes.Normal);
                File.Delete(file);
            }
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }
            Directory.Delete(target_dir , false);
        }
        private void Application_Startup(object sender , StartupEventArgs e)
        {
            if (Directory.Exists(StorageHelper.DecryptedDatabase))
            {
                try
                {
                    var collection = Directory.GetDirectories(StorageHelper.DecryptedDatabase);
                    foreach (var item in collection)
                    {
                        DeleteDirectory(item);
                    }
                }
                catch
                {
                }
            }
            RunInDebugMode();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (Directory.Exists(StorageHelper.DecryptedDatabase))
            {
                try
                {
                    var collection = Directory.GetDirectories(StorageHelper.DecryptedDatabase);
                    foreach (var item in collection)
                    {
                        DeleteDirectory(item);
                    }
                }
                catch
                {
                }
            }
            base.OnExit(e);
        }
        private static void AppDomainUnhandledException(object sender , UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }
        private static void RunInDebugMode()
        {
            BootStrapper bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }
        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                BootStrapper bootstrapper = new BootStrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;
        }
    }
}
