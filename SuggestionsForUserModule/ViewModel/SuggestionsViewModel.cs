using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using SuggestionsForUserModule.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
namespace Coneixement.SuggestionsForUser
{
    public class SuggestionsViewModel : INotifyPropertyChanged, ISuggestionsViewModel
    {
        Category _SelectedCategory;
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
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
        Subject selectedSubject;
        public Subject SelectedSubject
        {
            get
            {
                return selectedSubject;
            }
            set
            {
                selectedSubject = value;
                RaisePropertyChangedEvent("SelectedSubject");
            }
        }
        public SuggestionsViewModel(IView view)
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
                    SubjectChangeCompleted ev = _eventAggrigator.GetEvent<SubjectChangeCompleted>();
                    sb = ev.Subscribe(OnSubjectChangeCompleted, ThreadOption.PublisherThread, false);
                }
            }
        }
        Concept SelectedConcept
        {
            get;
            set;
        }
        public  void NotifyConceptChanged(Concept concept)
         {
             var canceilationsource = new CancellationTokenSource();
             var token = canceilationsource.Token;
             var UIContext = TaskScheduler.FromCurrentSynchronizationContext();
             Task<Concept> t = Task.Factory.StartNew(() =>
                 {
                     foreach (var item in SelectedSubject.Concepts)
                     {
                         if (item.Title.Trim().ToLower() == concept.Title.ToLower())
                         {
                             item.IsSelecetd = true;
                         }
                         else
                         {
                             item.IsSelecetd = false;
                         }
                     }
                     _eventAggrigator.GetEvent<ConceptChangeCompleted>().Publish(SelectedCategory);
                     return concept;
                 },token,TaskCreationOptions.AttachedToParent,UIContext);
         }
        private void OnSubjectChangeCompleted(Category obj)
        {
            SelectedCategory = obj;
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
            if (SelectedCategory != null)
            {
                SelectedSubject = SelectedCategory.Subjects.FirstOrDefault(x => x.IsSelected == true);
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
             _eventAggrigator.GetEvent<CategoryChangeCompleted>().Publish(SelectedCategory);
        }
    }
}
