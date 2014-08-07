using Coneixement.Infrastructure;
using Coneixement.SplashScreen.Interfaces;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
namespace Coneixement.SplashScreen
{
  public partial class SplashWindow : Window, ISplashView
    {
        Thread loadingThread;
        Storyboard Showboard;
        Storyboard Hideboard;
        public IViewModel ViewModel
        {
            get
            {
                return (IViewModel)this.DataContext ;
            }
            set
            {
                this.DataContext = value;
            }
        }
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();
        ShowDelegate showDelegate;
        HideDelegate hideDelegate;
        public SplashWindow()
        {
            InitializeComponent();
            showDelegate = new ShowDelegate(this.showText);
            hideDelegate = new HideDelegate(this.hideText);
            Showboard = this.Resources["showStoryBoard"] as Storyboard;
            Hideboard = this.Resources["HideStoryBoard"] as Storyboard;
            this.ViewModel = new SplashScreenViewModal(this);
            this.Closed += SplashWindow_Closed;
        }
        void SplashWindow_Closed(object sender, EventArgs e)
        {
            (ViewModel as SplashScreenViewModal).RequestNextView();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (this.ViewModel as SplashScreenViewModal).PublishIntiationInformation();
            loadingThread = new Thread(load);
            loadingThread.Start();
        }
        private void load()
        {
            this.Dispatcher.Invoke(showDelegate, "Validating Configurations.....");
            Thread.Sleep(4000);
            this.Dispatcher.Invoke(hideDelegate);
            this.Dispatcher.Invoke(showDelegate, "Preparing Modules....");
            Thread.Sleep(4000);
            this.Dispatcher.Invoke(hideDelegate);      
            this.Dispatcher.Invoke(showDelegate, "Loading Data....");
            Thread.Sleep(4000);
            this.Dispatcher.Invoke(hideDelegate);            
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action)delegate() { 
                    Close();
                });
        }
        private void showText(string txt)
        {
            txtLoading.Text = txt;
            BeginStoryboard(Showboard);
        }
        private void hideText()
        {
            BeginStoryboard(Hideboard);
        }
    }
}
