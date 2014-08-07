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
namespace Coneixement.ShowExaminationTypes.ViewModals
{
    public class ShowPreviuosYearPaperTypesViewModal : IShowPreviuosYearPaperTypesViewModal,INotifyPropertyChanged
    {
        public IView View
        {
            get;
            set;
        }
        ObservableCollection<ExaminationType> _examinationTypes;
        public ObservableCollection<ExaminationType> ExaminationTypes
        {
            get
            {
                return _examinationTypes;
            }
            set
            {
                _examinationTypes = value;
                RaisePropertyChangedEvent("ExaminationTypes");
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
        public ExaminationType SelectedExaminationType{ get; set; }
        Category _SelectedTestSeriesType;
        public Category SelectedTestSeriesType
        {
            get
            {
                return _SelectedTestSeriesType;
            }
            set
            {
                _SelectedTestSeriesType = value;
                RaisePropertyChangedEvent("SelectedTestSeriesType");
            }
        }
        private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
        private SubscriptionToken sb;
        public event PropertyChangedEventHandler PropertyChanged;
        internal ShowPreviuosYearPaperTypesViewModal(IView view)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            View = view;
            ExaminationTypes = new ObservableCollection<ExaminationType>();
            ImportCompleted();
        }
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    PrviousYearPaperTypesRequested ev = _eventAggrigator.GetEvent<PrviousYearPaperTypesRequested>();
                    sb = ev.Subscribe(OnExaminationTypeChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnExaminationTypeChangeCompleted(Category obj)
        {
            if (obj != null)
            {
                SelectedCategory = obj;
                ExaminationTypes.Clear();
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
                    foreach (var item in SelectedTestSeriesType.RelatedExaminationsTypes)
                    {
                        ExaminationTypes.Add(item);
                    }
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    if (ActionRegion.Views.Contains(View))
                        ActionRegion.Remove(View);
                    IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
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
        internal void NotifySubjectChange(ExaminationType selectedsubject)
        {
            SelectedExaminationType = selectedsubject;
            CloseView();
            SelectedTestSeriesType.RelatedExaminationsTypes.ForEach(x => x.IsSelected = false);
            var a = SelectedTestSeriesType.RelatedExaminationsTypes.FindIndex(x => x.Title == SelectedExaminationType.Title);
            SelectedTestSeriesType.RelatedExaminationsTypes[a].IsSelected = true;
            _eventAggrigator.GetEvent<ExaminationTypeChangeCompleted>().Publish(SelectedCategory);
        }
        private void CloseView()
        {
            this._regionManager.Regions[RegionNames.SecondaryRegion].Deactivate(View);
        }
        public void ShowPreviousView()
        {
            CloseView();
            _eventAggrigator.GetEvent<CategoryChangeCompleted>().Publish(SelectedCategory);
        }
    }
}
