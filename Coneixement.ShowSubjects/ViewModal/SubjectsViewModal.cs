using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowSubjects.Intefaces;
using Coneixement.ShowSubjects.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Coneixement.ShowSubjects.ViewModal
{
    public class SubjectsViewModal : ISubjectsViewModal, INotifyPropertyChanged
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
        private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
        private SubscriptionToken sb;
        public event PropertyChangedEventHandler PropertyChanged;
        internal SubjectsViewModal(ISubjectsView view)
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
                    CategoryChangeCompleted ev = _eventAggrigator.GetEvent<CategoryChangeCompleted>();
                    sb = ev.Subscribe(OnCategoryChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnCategoryChangeCompleted(Category obj)
        {
            if (obj != null)
            {
                SelectedCategory = obj;
                Subjects.Clear();
                if (SelectedCategory.Title.ToLower() != "TEST SERIES".ToLower())
                {
                    foreach (var item in SelectedCategory.Subjects)
                    {
                        Subjects.Add(item);
                    }
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    IRegionManager SecondaryRegionManager = null;
                    if (ActionRegion.Views.Contains(View))
                        ActionRegion.Remove(View);
                    IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                    if (!SecondaryRegion.Views.Contains(View))
                        SecondaryRegionManager = SecondaryRegion.Add(View, null, true);
                    SecondaryRegion.Activate(View);
                    (View as SubjectView).itemsControl.Visibility = System.Windows.Visibility.Visible;
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
            SelectedCategory.Subjects.ForEach(x => x.IsSelected = false);
            var a= SelectedCategory.Subjects.FindIndex(x => x.Title == SelectedSubject.Title);
            SelectedCategory.Subjects[a].IsSelected = true;
            _eventAggrigator.GetEvent<SubjectChangeCompleted>().Publish(SelectedCategory);
        }
        private void CloseView()
        {
            this._regionManager.Regions[RegionNames.SecondaryRegion].Deactivate(View);
        }
        public void ShowPreviousView()
        {
            CloseView();
            _eventAggrigator.GetEvent<DataImportComplted>().Publish(null);
        }
    }
}
