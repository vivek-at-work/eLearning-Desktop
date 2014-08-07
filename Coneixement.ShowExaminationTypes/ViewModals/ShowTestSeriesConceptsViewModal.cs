using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coneixement.ShowExaminationTypes.Intefaces;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using System.Collections.ObjectModel;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowExaminationTypes.Views;
using System.IO;
using System.Reflection;
using System.Windows;
namespace Coneixement.ShowExaminationTypes.ViewModals
{
    public class ShowTestSeriesConceptsViewModal : IShowTestSeriesConceptViewModal,INotifyPropertyChanged
    {
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
        Category _SelectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                RaisePropertyChangedEvent("SelectedCategory");
            }
        }
        Category _SelectedTestType;
        public Category SelectedTestType
        {
            get
            {
                return _SelectedTestType;
            }
            set
            {
                _SelectedTestType = value;
                RaisePropertyChangedEvent("SelectedTestType");
            }
        }
        Subject _SelectedSubject;
        public Subject SelectedSubject
        {
            get
            {
                return _SelectedSubject;
            }
            set
            {
                _SelectedSubject = value;
                RaisePropertyChangedEvent("SelectedSubject");
            }
        }
        private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private SubscriptionToken sb;
        string ParentDirectory { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<Concept> _concepts;
        public ObservableCollection<Concept> Concepts
        {
            get
            {
                return _concepts;
            }
            set
            {
                _concepts = value;
                RaisePropertyChangedEvent("Concepts");
            }
        }
        private IView view;
        public IView View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
            }
        }
        public ShowTestSeriesConceptsViewModal(IView view)
        {
            ParentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            View = view;
            Concepts = new ObservableCollection<Concept>();
            ImportCompleted();
        }
        public void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    TestSeriesSubjectChangeCompleted ev = _eventAggrigator.GetEvent<TestSeriesSubjectChangeCompleted>();
                    sb = ev.Subscribe(OnTestSeriesSubjectChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        Concept SelectedConcept
        {
            get;
            set;
        }
        public void NotifyConceptChanged(Concept concept)
         {
             var subject=SelectedTestType.Subjects.Find(x => x.IsSelected);
             foreach (var item in subject.Concepts)
             {
                 if (item.Title.Trim().ToLower() == concept.Title.ToLower())
                 {
                     item.IsSelecetd = true;
                     SelectedConcept = item;
                 }
                 else
                 {
                     item.IsSelecetd = false;
                 }
             }
             _eventAggrigator.GetEvent<SelectedQuestionPaperChanged>().Publish(SelectedConcept);
             (this.view as Coneixement.ShowExaminationTypes.Views.ShowTestSeriesConcepts).ConceptListView.SelectedIndex = -1;
         }
        private void OnTestSeriesSubjectChangeCompleted(Category obj)
        {
            SelectedCategory = obj;
            SelectedTestType = obj.SubCategories.FirstOrDefault(x => x.IsSelected);
            SelectedSubject = SelectedTestType.Subjects.FirstOrDefault(x => x.IsSelected);
            IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
            if (ActionRegion.Views.Contains(View))
                ActionRegion.Remove(View);
            IRegion MainRegion = _regionManager.Regions[RegionNames.MainRegion];
            if (!MainRegion.Views.Contains(View))
                MainRegion.Add(View,null,true);
            _regionManager.Regions[RegionNames.MainRegion].Activate(View);
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            Application.Current.MainWindow.Show();
            Concepts.Clear();
            AddItems();
        }
        public void AddItems()
        {
            if (_SelectedCategory != null)
            {
                var concepts = SelectedSubject.Concepts;
                foreach (var item in concepts)
                {
                    Concepts.Add(item);
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
        private void CloseView()
        {
           this._regionManager.Regions[RegionNames.MainRegion].Deactivate(View);
            Application.Current.MainWindow.Visibility = Visibility.Collapsed;
        }
        public void ShowPreviousView()
        {
             CloseView();
             _eventAggrigator.GetEvent<TestSeriesTypeChangeCompleted>().Publish(_SelectedCategory);
        }
    }
}
