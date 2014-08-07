using Coneixement.SplashScreen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Helpers.Configuration;
using System.Configuration;
using System.Windows;
using System.ComponentModel;
namespace Coneixement.SplashScreen
{
    public class SplashScreenViewModal : ISplashScreenViewModal,INotifyPropertyChanged
    {
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        public event PropertyChangedEventHandler PropertyChanged;
        public SplashScreenViewModal(IView view)
        {
            View = view;
            var appSettings = ConfigurationManager.AppSettings;
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            ApplicationName = appSettings["ApplicationName"];
            ApplicationVersion = appSettings["ApplicationVersion"];
            ApplicationPublisherName = appSettings["PublisherName"];
            LicenceValidationServicePath = appSettings["LicenceValidationSerivePath"];
            IsTrialVersion= (appSettings["IsTrialVersion"]=="True")?Visibility.Visible:Visibility.Collapsed;
        }
        public void PublishIntiationInformation()
        {
            _eventAggrigator.GetEvent<SplashScreenInitiated>().Publish(null);
        }
        Visibility _IsTrialVersion;
        public Visibility IsTrialVersion
        {
            get
            {
                return _IsTrialVersion;
            }
            set
            {
                _IsTrialVersion = value;
                RaisePropertyChanged("IsTrialVersion");
            }
        }
        public string LicenceValidationServicePath
        {
            get;
            set;
        }
        public string ApplicationName
        {
            get;
            set;
        }
        public string ApplicationVersion
        {
            get;
            set;
        }
        public string ApplicationPublisherName
        {
            get;
            set;
        }
        public void RequestNextView()
        {
            _eventAggrigator.GetEvent<SplashScreenClosed>().Publish(null);
        }
        public IView View
        {
            get;
            set;
        }
        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
