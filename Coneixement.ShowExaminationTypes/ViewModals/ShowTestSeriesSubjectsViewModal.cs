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
    public class ShowTestSeriesSubjectsViewModal : IShowTestSeriesSubjectsViewModal, INotifyPropertyChanged
    {
          public IView View
        {
            get;
            set;
        }
        ObservableCollection<Subject> _subjects;
        public ObservableCollection<Subject> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
                RaisePropertyChangedEvent("Subjects");
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
        public Subject SelectedSubject { get; set; }
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
        internal ShowTestSeriesSubjectsViewModal(IShowTestSeriesSubjectsView view)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            View = view;
            Subjects = new ObservableCollection<Subject>();
            ImportCompleted();
        }
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    TestSeriesTypeChangeCompleted ev = _eventAggrigator.GetEvent<TestSeriesTypeChangeCompleted>();
                    sb = ev.Subscribe(OnExaminationTypeChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnExaminationTypeChangeCompleted(Category obj)
        {
            if (obj != null)
            {
                SelectedCategory = obj;              
                Subjects.Clear();
                SelectedTestSeriesType = null;
                if (SelectedCategory.Title.ToLower() == "TEST SERIES".ToLower())
                {
                    SelectedCategory.SubCategories.ForEach((x) =>
                        {
                            if ((x.Title.ToLower() == "Chapter wise Mock Test".ToLower() && x.IsSelected))
                            {
                                x.IsSelected = true;
                                SelectedTestSeriesType = x;
                            }
                            x.Subjects.ForEach((y) => y.IsSelected = false);
                        });
                    if (SelectedTestSeriesType != null)
                    {
                        foreach (var item in SelectedTestSeriesType.Subjects)
                        {
                            Subjects.Add(item);
                        }
                        IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                        if (ActionRegion.Views.Contains(View))
                            ActionRegion.Remove(View);
                        IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                        IRegionManager SecondaryRegionManager = null;
                        if (!SecondaryRegion.Views.Contains(View))
                            SecondaryRegionManager = SecondaryRegion.Add(View, null, true);
                        SecondaryRegion.Activate(View);
                        (View as ShowTestSeriesSubjects).itemsControl.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        _eventAggrigator.GetEvent<PrviousYearPaperTypesRequested>().Publish(SelectedCategory);
                    }
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
        internal void NotifySubjectChange(Subject selectedsubject)
        {
            SelectedSubject = selectedsubject;
            CloseView();
            SelectedTestSeriesType.Subjects.ForEach(x => x.IsSelected = false);
            var a = SelectedTestSeriesType.Subjects.FindIndex(x => x.Title == SelectedSubject.Title);
            SelectedTestSeriesType.Subjects[a].IsSelected = true;
            _eventAggrigator.GetEvent<TestSeriesSubjectChangeCompleted>().Publish(SelectedCategory);
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
