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
namespace Coneixement.ShowExaminationTypes.ViewModals
{
    public class ShowTestSeriesTypesViewModal : IShowTestSeriesTypesViewModal,INotifyPropertyChanged
    {
        ObservableCollection<Category> _testseriesTypes;
        public ObservableCollection<Category> TestSeriesTypes
        {
            get
            {
                return _testseriesTypes;
            }
            set
            {
                _testseriesTypes = value;
                RaisePropertyChangedEvent("TestSeriesTypes");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        Category _category;
        private IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        public Category SelectedTestSeriesType { get; set; }
        private IEventAggregator _eventAggrigator
        {
            get;
            set;
        }
        private SubscriptionToken sb;
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
        public ShowTestSeriesTypesViewModal(IView showTestSeriesTypes)
        {
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
             View = showTestSeriesTypes;
            TestSeriesTypes = new ObservableCollection<Category>();
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
        private void OnCategoryChangeCompleted(Infrastructure.Modals.Category obj)
        {
            if (obj != null)
            {
                SelectedCategory = obj;
            if (SelectedCategory.SubCategories != null && SelectedCategory.SubCategories.Count > 0)
            {
                if (SelectedCategory.Title.ToLower() == "TEST SERIES".ToLower())
                {
                    SelectedCategory.SubCategories.ForEach((x) =>
                        {
                            x.IsSelected = false;
                        });
                    TestSeriesTypes.Clear();
                    foreach (var item in SelectedCategory.SubCategories)
                    {
                        TestSeriesTypes.Add(item);
                    }
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    if (ActionRegion.Views.Contains(View))
                        ActionRegion.Remove(View);
                    IRegion SecondaryRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                    IRegionManager SecondaryRegionManager = null;
                    if (!SecondaryRegion.Views.Contains(View))
                        SecondaryRegionManager = SecondaryRegion.Add(View, null, true);
                    SecondaryRegion.Activate(View);
                    (View as ShowTestSeriesTypes).itemsControl.Visibility = System.Windows.Visibility.Visible;
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
        public IView View
        {
            get;
            set;
        }
        internal void NotifyTestSeriesChange(Category selectedtestseriestype)
        {
            SelectedTestSeriesType = selectedtestseriestype;
            SelectedCategory.SubCategories.ForEach((x) => x.IsSelected = false);
            SelectedCategory.SubCategories.First(x => x.Title.ToLower() == SelectedTestSeriesType.Title.ToLower()).IsSelected = true;
            _eventAggrigator.GetEvent<TestSeriesTypeChangeCompleted>().Publish(SelectedCategory);
        }
        internal void ShowPreviousView()
        {
            CloseView();
            _eventAggrigator.GetEvent<DataImportComplted>().Publish(null);
        }
        private void CloseView()
        {
            this._regionManager.Regions[RegionNames.SecondaryRegion].Deactivate(View);
        }
    }
}
