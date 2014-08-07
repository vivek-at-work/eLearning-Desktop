using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Helpers;
using Coneixement.Infrastructure.Modals;
using Coneixement.VideoPlayer.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
namespace Coneixement.VideoPlayer.ViewModel
{
    public class MediaPlayerViewModel : IVideoPlayerViewModel, INotifyPropertyChanged
    {
        Task DecryptMediaFileTask;
        CancellationTokenSource tokenSource;
        CancellationToken token;
        Category SelectedCategory;
        Concept _selectedConcept;
        public Concept SelectedConcept
        {
            get
            {
                return _selectedConcept;
            }
            set
            {
                _selectedConcept = value;
                RaisePropertyChangedEvent("SelectedConcept");
            }
        }
        SubscriptionToken sb;
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        Visibility _showOverlay;
        public Visibility ShowOverlay
        {
            get
            {
                return _showOverlay;
            }
            set
            {
                _showOverlay = value;
                if (_showOverlay == Visibility.Collapsed)
                {
                    ShowPlayer = Visibility.Visible;
                }
                else
                {
                    ShowPlayer = Visibility.Collapsed;
                }
                RaisePropertyChangedEvent("ShowOverlay");
            }
        }
        Visibility _showPlayer;
        public Visibility ShowPlayer
        {
            get
            {
                return _showPlayer;
            }
            set
            {
                _showPlayer = value;
                RaisePropertyChangedEvent("ShowPlayer");
            }
        }
        ObservableCollection<Topic> playList;
        public ObservableCollection<Topic> PlayList
        {
            get
            {
                return playList;
            }
            set
            {
                playList = value;
                RaisePropertyChangedEvent("PlayList");
            }
        }
        Topic _SelectedTopic;
        public Topic SelectedTopic
        {
            get
            {
                return _SelectedTopic;
            }
            set
            {
                _SelectedTopic = value;
                RaisePropertyChangedEvent("SelectedTopic");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        string _DecryptedMediaPath;
        public string DecryptedMediaPath
        {
            get
            {
                return _DecryptedMediaPath;
            }
            set
            {
                if (File.Exists(value.ToString()))
                {
                    _DecryptedMediaPath = value;
                    RaisePropertyChangedEvent("DecryptedMediaPath");
                }
            }
        }
        public MediaPlayerViewModel(IView view)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<Microsoft.Practices.Prism.Events.IEventAggregator>();
            View = view;
            ImportCompleted();
        }
        public void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    ConceptChangeCompleted ev = _eventAggrigator.GetEvent<ConceptChangeCompleted>();
                    sb = ev.Subscribe(OnConceptChangeCompleted, ThreadOption.UIThread, false);
                }
            }
        }
        private void OnConceptChangeCompleted(Category category)
        {
            IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
            if (SecondaryRegion.Views.Contains(View))
            {
                SecondaryRegion.Deactivate(View);
            }
            ShowOverlay = Visibility.Visible;
            SelectedCategory = category;
            SelectedConcept = category.Subjects.Find(x => x.IsSelected == true).Concepts[category.Subjects.Find(x => x.IsSelected == true).Concepts.FindIndex(x => x.IsSelecetd == true)];
            if (null != (SelectedCategory))
            {
                if (PlayList == null)
                    PlayList = new ObservableCollection<Topic>();
                PlayList.Clear();
                foreach (var item in SelectedConcept.Topics)
                {
                    PlayList.Add(item);
                }
                IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                if (ActionRegion.Views.Contains(View))
                    ActionRegion.Remove(View);
                SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                IRegionManager detailsRegionManager = null;
                if (!SecondaryRegion.Views.Contains(View))
                {
                    detailsRegionManager = SecondaryRegion.Add(View, null, true);
                }
                if (PlayList.Count > 0)
                {
                    NotifyTopicChanged(PlayList[0]);
                    (View as VideoPlayer.Views.VideoPlayer).TopicListView.SelectedIndex = 0;
                    if (DecryptMediaFileTask != null)
                        DecryptMediaFileTask.Wait();
                    SecondaryRegion.Activate(View);
                    (View as VideoPlayer.Views.VideoPlayer).videoElement.Play();
                }
            }
            ShowOverlay = Visibility.Collapsed;
        }
        public IView View
        {
            get;
            set;
        }
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        internal void NotifyTopicChanged(Topic topic)
        {
            ShowPlayer = Visibility.Hidden;
            ShowOverlay = Visibility.Visible;
            SelectedTopic = topic;
            decryptMedia();
            ShowPlayer = Visibility.Visible;
            ShowOverlay = Visibility.Collapsed;
        }
        private void decryptMedia()
        {
            if (tokenSource != null && DecryptMediaFileTask.Status == TaskStatus.Running)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            DecryptMediaFileTask = Task.Factory.StartNew(() => DecrptMediaFile(1, token), token);
            // DecryptMediaFileTask.Start();
             DecryptMediaFileTask.Wait();
        }
        private void DecrptMediaFile(int taskNum, CancellationToken ct)
        {
            // Was cancellation already requested?  
            if (ct.IsCancellationRequested == true)
            {
                ct.ThrowIfCancellationRequested();
            }
            FileInfo f = new FileInfo(SelectedTopic.DataPath);
            string path = Path.Combine(Path.Combine(StorageHelper.DecryptedDatabase, f.Directory.Name));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileEncryption.DecryptFile(SelectedTopic.DataPath, Path.Combine(path, f.Name));
            DecryptedMediaPath = Path.Combine(path, f.Name);
            if (ct.IsCancellationRequested)
            {
            }
        }
        internal bool MoveNextInPlayList()
        {
            if (PlayList.Count < (PlayList.IndexOf(_SelectedTopic)-1))
            {
                NotifyTopicChanged(PlayList[PlayList.IndexOf(_SelectedTopic) + 1]);
                return true;
            }
            return false;
        }
    }
}
