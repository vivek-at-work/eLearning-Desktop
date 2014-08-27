using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowExaminationTypes.Intefaces;
using Coneixement.ShowExaminationTypes.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
namespace Coneixement.ShowExaminationTypes.ViewModals
{
   public class ShowExaminationsViewModal:IShowExaminationsViewModal,INotifyPropertyChanged
    {
        public IView View
        {
            get;
            set;
        }
        ExaminationType _SelectedExaminationType;
        ObservableCollection<Examination> _examinations;
        public ObservableCollection<Examination> Examinations
        {
            get
            {
                return _examinations;
            }
            set
            {
                _examinations = value;
                RaisePropertyChangedEvent("Examinations");
            }
        }
        Category _category;
        public Category SelectedCategory
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                RaisePropertyChangedEvent("SelectedCategory");
            }
        }
        public ExaminationType SelectedExaminationType{
            get
            {
                return _SelectedExaminationType;
            }
            set
            {
                _SelectedExaminationType = value;
                RaisePropertyChangedEvent("SelectedExaminationType");
            }
        }
        public Category SelectedTestSeriesType { get; set; }
        private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
        private SubscriptionToken sb;
        public event PropertyChangedEventHandler PropertyChanged;
        internal ShowExaminationsViewModal(IView view)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            View = view;
            Examinations = new ObservableCollection<Examination>();
            ImportCompleted();
        }
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    ExaminationTypeChangeCompleted ev = _eventAggrigator.GetEvent<ExaminationTypeChangeCompleted>();
                    sb = ev.Subscribe(OnExaminationTypeChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnExaminationTypeChangeCompleted(Category obj)
        {
            if (obj != null)
            {
                SelectedCategory = obj;
                Examinations.Clear();
                SelectedTestSeriesType = null;
                if (SelectedCategory.Title.ToLower() == "TEST SERIES".ToLower())
                {
                    SelectedCategory.SubCategories.ForEach((x) =>
                        {
                            if ((x.Title.ToLower() == "Last Year Test Papers".ToLower() && x.IsSelected))
                            {
                                x.IsSelected = true;
                                SelectedTestSeriesType = x;
                            }
                            else
                            {
                                x.IsSelected = false;
                            }
                            x.Subjects.ForEach((y) => y.IsSelected = false);
                        });
                    SelectedTestSeriesType.RelatedExaminationsTypes.ForEach(x =>
                        {
                            if (x.IsSelected)
                            {
                                SelectedExaminationType = x;
                            }
                        });
                    foreach (var item in SelectedExaminationType.Examinations)
                    {
                        Examinations.Add(item);
                    }
                    Application.Current.MainWindow.Visibility = Visibility.Visible;
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                    Application.Current.MainWindow.Show();
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    if (ActionRegion.Views.Contains(View))
                        ActionRegion.Remove(View);
                    IRegion SecondaryRegion = _regionManager.Regions[RegionNames.MainRegion];
                    IRegionManager SecondaryRegionManager = null;
                    if (!SecondaryRegion.Views.Contains(View))
                        SecondaryRegionManager = SecondaryRegion.Add(View, null, true);
                    SecondaryRegion.Activate(View);
                    (View as ShowPreviuosYearPaperTypes).itemsControl.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void NotifyExaminationChange(Examination examination)
        {
            
            _eventAggrigator.GetEvent<SelectedQuestionPaperChanged>().Publish(new Concept
                {
                     DataPath=examination.CategoryDataPath,
                      IconPath= examination.IconPath,
                       Title= examination.Title,
                });
        }
        private void CloseView()
        {
           this._regionManager.Regions[RegionNames.MainRegion].Deactivate(View);
           Application.Current.MainWindow.Hide();
        }
        public void ShowPreviousView()
        {
            CloseView();
            _eventAggrigator.GetEvent<PrviousYearPaperTypesRequested>().Publish(SelectedCategory);
        }
    }
}
