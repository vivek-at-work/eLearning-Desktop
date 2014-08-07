using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Commands;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Helpers;
using Coneixement.Infrastructure.Modals;
using Coneixement.Login.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
namespace Coneixement.Login
{
    public class LoginViewModel : ILoginViewModel, INotifyPropertyChanged
    {
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        public event PropertyChangedEventHandler PropertyChanged;
        User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }
        string ParentDirectoryPath { get; set; }
        string LoginHistoryPath
        {
            get;
            set;
        }
        public IView View
        {
            get;
            set;
        }
        public bool IsValidUser
        {
            get;
            set;
        }
        private SubscriptionToken sb;
        public LoginViewModel()
        {
            CurrentUser = new User();
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            LoginHistoryPath = Path.Combine(ParentDirectoryPath,"Settings", "LoginInfo.cnx");
            CreateLoginCommand();
            ImportCompleted();
        }
        public void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    LicenceValidationCompleted ev = _eventAggrigator.GetEvent<LicenceValidationCompleted>();
                    sb = ev.Subscribe(OnLicenceValidationCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnLicenceValidationCompleted(LicencingHistory obj)
        {
            if (obj.IsValidLicence)
            {
                IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                if (ActionRegion.Views.OfType<Views.LoginControl>().Count() > 0)
                {
                    this.View = ActionRegion.Views.OfType<Views.LoginControl>().First();
                    ActionRegion.Remove(this.View);
                }
                IRegion SecondryRegion = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.SecondaryRegion];
                if (SecondryRegion.Views.OfType<Views.LoginControl>().Count() == 0)
                    SecondryRegion.Add(this.View, null, true);
                SecondryRegion.Activate(this.View);
            }
            else
            {
            }
        }
        public ICommand LoginCommand
        {
            get;
            internal set;
        }
        private bool CanExecuteLoginCommand()
        {
            return !(string.IsNullOrWhiteSpace(CurrentUser.UserName) && string.IsNullOrWhiteSpace(CurrentUser.Password));
        }
        private void CreateLoginCommand()
        {
            LoginCommand = new RelayCommand(param => PerformAuthentication(), param => CanExecuteLoginCommand());
        }
        private bool IsValidationRequired()
        {
            return true;
        }
        public User GetValidUser()
        {
            User user = null;
            var s = new XmlSerializer(typeof(User));
            using (var stream = new FileStream(LoginHistoryPath, FileMode.Open))
            {
              user=(User)  s.Deserialize(stream);
                stream.Close();
            }
            return user;
        }
        public void PerformAuthentication()
        {
            User ValidUser = GetValidUser();
            var s = new XmlSerializer(typeof(User));
                 var appSettings = ConfigurationManager.AppSettings;
                if (CurrentUser.UserName.Trim() == ValidUser.UserName.Trim() && CurrentUser.Password.Trim() == ValidUser.Password.Trim())
                {
                    _regionManager.Regions[RegionNames.MainRegion].Activate(_regionManager.Regions[RegionNames.MainRegion].Views.First());
                    _regionManager.Regions[RegionNames.SecondaryRegion].Deactivate(this.View);
                    CurrentUser.LastLoginStatus = LastLoginStatus.Success;
                    CurrentUser.LastLoginOn = DateTime.Now;
                    _eventAggrigator.GetEvent<LoginStatusChangedEvent>().Publish(CurrentUser);
                }
                else
                {
                    CurrentUser.LastLoginStatus = LastLoginStatus.Failure;
                    MessageBox.Show("Incorrect Username or Password!!" + Environment.NewLine + "Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
        }
        private void RaisePropertyChanged(string caller)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
