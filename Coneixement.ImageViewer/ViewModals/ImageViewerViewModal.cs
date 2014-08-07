using Coneixement.ImageViewer.Interfaces;
using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Commands;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace Coneixement.ImageViewer.ViewModals
{
    public class ImageViewerViewModal : IImageViewerViewModal, INotifyPropertyChanged
    {
        FlowDocument document;
        public FlowDocument Document
        {
            get
            {
                return document;
            }
            set
            {
                document = value;
                RaisePropertyChangedEvent("Document");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        SubscriptionToken sb;
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        Visibility _showPrintButton;
        public Visibility ShowPrintOption
        {
            get
            {
                return _showPrintButton;
            }
            set
            {
                _showPrintButton = value;
                RaisePropertyChangedEvent("ShowPrintOption");
            }
        }
        ObservableCollection<Coneixement.Infrastructure.Modals.Image> _Images;
        public ObservableCollection<Coneixement.Infrastructure.Modals.Image> Images
        {
            get
            {
                return _Images;
            }
            set
            {
                _Images = value;
                RaisePropertyChangedEvent("Images");
            }
        }
        public IView View
        {
            get;
            set;
        }
        Category _SelectedCategory;
        Concept _SelectedConcept;
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
        public Concept SelectedConcept
        {
            get
            {
                return _SelectedConcept;
            }
            set
            {
                _SelectedConcept = value;
                RaisePropertyChangedEvent("SelectedConcept");
            }
        }
        public ICommand PrintCommand
        {
            get;
            internal set;
        }
        private bool CanExecutePrintCommand()
        {
            if (SelectedCategory != null)
                return (SelectedCategory.Title.ToLower() == "notes".ToLower());
            return false;
        }
        private void CreatePrintCommand()
        {
            PrintCommand = new RelayCommand(param => PrintAction(), param => CanExecutePrintCommand());
        }
        public ImageViewerViewModal(IView view)
        {
            View = view;
            ShowPrintOption = Visibility.Collapsed;
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<Microsoft.Practices.Prism.Events.IEventAggregator>();
            ImportCompleted();
            CreatePrintCommand();
        }
        SubscriptionToken ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    ConceptChangeCompleted ev = _eventAggrigator.GetEvent<ConceptChangeCompleted>();
                    sb = ev.Subscribe(OnConceptChangeCompleted, ThreadOption.UIThread, false);
                }
            }
            return sb;
        }
        void OnConceptChangeCompleted(Category category)
        {
            SelectedCategory = category;
            SelectedConcept = category.Subjects.Find(x => x.IsSelected == true).Concepts[category.Subjects.Find(x => x.IsSelected == true).Concepts.FindIndex(x => x.IsSelecetd == true)];
            if (SelectedConcept.Type == ContentType.Image)
            {
                var entity = category.Subjects.Find(x => x.IsSelected == true).Concepts.FirstOrDefault(x => x.IsSelecetd = true).DataPath;
                if (entity != null && Directory.Exists(entity))
                {
                    IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                    if (SecondaryRegion.Views.Contains(View))
                    {
                        SecondaryRegion.Deactivate(View);
                    }
                    var ImageDirectory = new DirectoryInfo(entity);
                    Task ReadImagesFromDirectory = new Task(() => GetImages());
                    ReadImagesFromDirectory.Start();
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    if (ActionRegion.Views.Contains(View))
                        ActionRegion.Remove(View);
                    if (!SecondaryRegion.Views.Contains(View))
                    {
                        IRegionManager detailsRegionManager = SecondaryRegion.Add(View, null, true);
                    }
                     ReadImagesFromDirectory.Wait();
                    SecondaryRegion.Activate(View);
                }
            }
        }
        void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                if (propertyName == "SelectedCategory")
                {
                    if (SelectedCategory.Title.ToLower() == "Notes".ToLower())
                    {
                        ShowPrintOption = Visibility.Visible;
                    }
                    else
                    {
                        ShowPrintOption = Visibility.Collapsed;
                    }
                }
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        ObservableCollection<Infrastructure.Modals.Image> GetImages()
        {
            var imageCollection = new ObservableCollection<Infrastructure.Modals.Image>();
            var dircetoryinfo = new DirectoryInfo(SelectedConcept.DataPath);
            var currentDirectoryFiles = dircetoryinfo.GetFiles();
            foreach (var item in currentDirectoryFiles)
            {
                imageCollection.Add(new Coneixement.Infrastructure.Modals.Image()
                {
                    Path = new Coneixement.Infrastructure.Convertors.EncryptedDataConvertor().Convert(item.FullName, null, null, null).ToString(),
                    Title = item.Name
                });
            }
            Images = imageCollection;
            return imageCollection;
        }
        public void PrintAction()
        {
            FlowDocument document = new FlowDocument();
            foreach (var item in Images)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                BitmapImage bimg = new BitmapImage();
                bimg.BeginInit();
                bimg.UriSource = new Uri(new Coneixement.Infrastructure.Convertors.EncryptedDataConvertor().Convert(item.Path, null, null, null).ToString(), UriKind.Absolute);
                bimg.EndInit();
                image.Source = bimg;
                document.Blocks.Add(new BlockUIContainer(image));
            }
            IDocumentPaginatorSource idpSource = document;
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;
            document.PageHeight = pd.PrintableAreaHeight;
            document.PageWidth = pd.PrintableAreaWidth;
            IDocumentPaginatorSource idocument = document as IDocumentPaginatorSource;
            pd.PrintDocument(idocument.DocumentPaginator, "Printing Notes On " + SelectedConcept.Title);
        }
    }
}
