using System.IO;
using System.Reflection;
using Coneixement.Infrastructure.Modals;
using System.Windows.Controls;
using Coneixement.Infrastructure.Interfaces;
using Coneixement.ShowCategories.Intefaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using Coneixement.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Coneixement.Infrastructure.Events;
using Coneixement.ShowCategories.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;
namespace Coneixement.ShowCategories.ViewModal
{
    public class CategoriesViewModal : ICategoriesViewModal, INotifyPropertyChanged
    {
        public FileInfo CategoriesFile
        {
            get;
            set;
        }
        string ParentDirectoryPath { get; set; }
        public IView View
        {
            get;
            set;
        }
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                RaisePropertyChangedEvent("SelectedCategory");
            }
        }
        public void NotifyCategoryChange(Category selectedcategory)
        {
            SelectedCategory=selectedcategory;
           _eventAggrigator.GetEvent<CategoryChangeCompleted>().Publish(SelectedCategory);
           CloseView();
        }
        ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                RaisePropertyChangedEvent("Categories");
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
        string DataRepositoryPath
        {
            get;
            set;
        }
        public CategoriesViewModal(ICategoriesView view)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();          
            View = view;
            ImportCompleted();
            ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            DataRepositoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IkonTechnology");
            CategoriesFile = new FileInfo(Path.Combine(DataRepositoryPath, "Settings", "manifest.cnx"));
            Categories = new ObservableCollection<Category>();
        }
        private void ReadCategories()
        {
            using (StreamReader SRCategory = new StreamReader(CategoriesFile.FullName))
            {
                XmlSerializer xmlser = new XmlSerializer(typeof(List<Category>));
                var a = (List<Category>)xmlser.Deserialize(SRCategory);
                if (a != null)
                {
                    Categories.Clear();
                    for (int i = 0; i < a.Count; i++)
                    {
                        Categories.Add(a[i]);
                    }
                }
            }
        }
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    DataImportComplted ev = _eventAggrigator.GetEvent<DataImportComplted>();
                    sb = ev.Subscribe(OnDataImportComplted, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnDataImportComplted(object obj)
        {
            ReadCategories();
            IRegion actionRegion = _regionManager.Regions[RegionNames.ActionRegion];
            IRegionManager detailsRegionManager = null;
            if (actionRegion.Views.Contains(View))
                actionRegion.Remove(View);
            IRegion secondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
            if(!secondaryRegion.Views.Contains(View))
            detailsRegionManager = secondaryRegion.Add(this.View, null, true);
            secondaryRegion.Activate(this.View);
            (View as CategoriesView).itemsControl.Visibility = System.Windows.Visibility.Visible;
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
            this._regionManager.Regions[RegionNames.SecondaryRegion].Deactivate(View);
        }
    }
}
