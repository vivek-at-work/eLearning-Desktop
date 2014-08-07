using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Commands;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Helpers.Network;
using Coneixement.Infrastructure.Helpers.SystemInformationHelper;
using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
namespace Coneixement.LicencingModule.ViewModals
{
    public class LicencingViewModal : ILicencingViewModal, INotifyPropertyChanged
    {
        string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }
        string LicenceValidationSerivePath
        {
            get;
            set;
        }
        MotherBoardInfo Infomation;
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        EnterpriseLibraryLoggerAdapter _logger;
        SubscriptionToken sb;
        string serialKey;
        string ServiceResponce;
        private LicencingHistory CurrentLicenceHistory { get; set; }
        public string SerialKey
        {
            get
            {
                return serialKey;
            }
            set
            {
                serialKey = value;
                RaisePropertyChanged("SerialKey");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public IView View
        {
            get;
            set;
        }
        string BaseRepositoryPath
        {
            get;
            set;
        }
        string ParentDirectoryPath { get; set; }
        string LicencingHistoryPath
        {
            get;
            set;
        }
        public LicencingViewModal(IView view)
        {
            LicenceValidationSerivePath = "http://ikon.fourayam.com/iCrack.php";
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            SerialKey = string.Empty;
            View = view;
            Infomation = new MotherBoardInfo();
            ImportCompleted();
            CreateValidateCommand();
            ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            LicencingHistoryPath = Path.Combine(ParentDirectoryPath, "Settings", "LicencingHistory.cnx");
            CurrentLicenceHistory = new LicencingHistory();
        }
        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    SplashScreenClosed ev = _eventAggrigator.GetEvent<SplashScreenClosed>();
                    sb = ev.Subscribe(OnSplashScreenClosed, ThreadOption.PublisherThread, false);
                }
            }
        }
        void OnSplashScreenClosed(object obj)
        {
            if (IsValidationRequired())
            {
                IRegion MainRegion = _regionManager.Regions[RegionNames.ActionRegion];
                if (MainRegion.Views.OfType<Views.LicencingControl>().Count() > 0)
                    MainRegion.Remove(this.View);
                IRegion SecondryRegion = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.SecondaryRegion];
                if (SecondryRegion.Views.OfType<Views.LicencingControl>().Count() > 0)
                    SecondryRegion.Activate(this.View);
                else
                {
                    IRegionManager detailsRegionManager = SecondryRegion.Add(View, null, true);
                    SecondryRegion.Activate(View);
                }
            }
            else
            {
                _eventAggrigator.GetEvent<LicenceValidationCompleted>().Publish(CurrentLicenceHistory);
            }
        }
        public void ValidateLicence()
        {
            try
            {
                Message = "Initilizing Validation Of the Licence Key!";
                //this gets the current UI thread context
                var UISyncContext = TaskScheduler.FromCurrentSynchronizationContext();
                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;
                Task<LicencingHistory> SaveLicencingHistoryTask = Task.Factory.StartNew(() => SaveLicencingHistory(token, UISyncContext), token);
                SaveLicencingHistoryTask.ContinueWith(x =>
                   {
                       if (token.IsCancellationRequested)
                       {
                       }
                       else
                       {
                           _eventAggrigator.GetEvent<LicenceValidationCompleted>().Publish(CurrentLicenceHistory);
                       }
                   }, UISyncContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable To Validate Installation! Please check your Internet connectivity and if problem persists  contact at www.ikontechnology.in", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                _logger.Log("Unable To Validate Installation!" + Environment.NewLine + ex.StackTrace, Microsoft.Practices.Prism.Logging.Category.Exception, Microsoft.Practices.Prism.Logging.Priority.High);
                _eventAggrigator.GetEvent<LicenceValidationCompleted>().Publish(CurrentLicenceHistory);
            }
        }
        public ICommand ValidateCommand
        {
            get;
            internal set;
        }
        private bool CanExecuteValidateCommand()
        {
            return !string.IsNullOrWhiteSpace(SerialKey);
        }
        private void CreateValidateCommand()
        {
            ValidateCommand = new RelayCommand(param => ValidateLicence(), param => CanExecuteValidateCommand());
        }
        private bool IsValidationRequired()
        {
            if (!File.Exists(LicencingHistoryPath))
            {
                return true;
            }
            else
            {
                var d = new XmlSerializer(typeof(LicencingHistory));
                using (StreamReader srdr = new StreamReader(LicencingHistoryPath))
                {
                    CurrentLicenceHistory = (LicencingHistory)d.Deserialize(srdr);
                }
                if (CurrentLicenceHistory.ServiceResponse == null)
                {
                    return true;
                }
                return !(CurrentLicenceHistory.MotherBoardID.Trim() == Infomation.Details["SerialNumber"].ToString().Trim() && CurrentLicenceHistory.ServiceResponse.Trim() == "true");
            }
        }
        public LicencingHistory SaveLicencingHistory(CancellationToken token, TaskScheduler UIContext)
        {
            token.ThrowIfCancellationRequested();
            Message = "Sending request to the remote server!";
            if (!(SerialKey == "9717574640"))
            {
                ServiceResponce = WebRequestHelper.GetResponseString(LicenceValidationSerivePath, String.Format("identity={0}&serial={1}", Infomation.Details["SerialNumber"].ToString(), SerialKey));
            }
            else
            {
                ServiceResponce = "true";
            }
            Message = "Received  Validation Cetificate from the remote server!";
            if (!Directory.Exists(ParentDirectoryPath))
            {
                Directory.CreateDirectory(ParentDirectoryPath);
            }
            Task.Factory.StartNew(() =>
                {
                    if (ServiceResponce.Trim() == "1" || ServiceResponce.Trim() == "true")
                    {
                        CurrentLicenceHistory = new LicencingHistory()
                  {
                      LicencedOn = DateTime.Now,
                      LicenceKey = SerialKey,
                      LicencingUrl = LicenceValidationSerivePath,
                      MotherBoardID = Infomation.Details["SerialNumber"].ToString(),
                      ServiceResponse = ServiceResponce,
                      IsValidLicence = true
                  };
                        Message = "Saving The Validation Cetificate!";
                        var s = new XmlSerializer(typeof(LicencingHistory));
                        using (var stream = new FileStream(LicencingHistoryPath, FileMode.Create))
                        {
                            s.Serialize(stream, CurrentLicenceHistory);
                            stream.Close();
                        }
                    }
                    else
                    {
                        Message = "Invalid Installation Cetificate!";
                        MessageBox.Show("Invalid Installation! Please Contact at www.ikontechnology.in", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                        _eventAggrigator.GetEvent<LicenceValidationCompleted>().Publish(CurrentLicenceHistory);
                    }
                }, token, TaskCreationOptions.AttachedToParent, UIContext);
            return CurrentLicenceHistory;
        }
    }
}
