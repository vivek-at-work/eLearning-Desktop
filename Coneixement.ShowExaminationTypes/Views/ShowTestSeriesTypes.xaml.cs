using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowExaminationTypes.Intefaces;
using Coneixement.ShowExaminationTypes.ViewModals;
using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Animation;
namespace Coneixement.ShowExaminationTypes.Views
{
    /// <summary>
    /// Interaction logic for ShowTestSeriesTypes.xaml
    /// </summary>
    public partial class ShowTestSeriesTypes : UserControl, IShowTestSeriesTypesView
    {
        public ShowTestSeriesTypes()
        {
            InitializeComponent();
            ViewModel = new ShowTestSeriesTypesViewModal(this);
        }
        public Infrastructure.IViewModel ViewModel
        {
            get
            {
                return this.DataContext as IViewModel;
            }
            set{
                this.DataContext = value;
            }
        }
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            (ViewModel as ShowTestSeriesTypesViewModal).ShowPreviousView();
        }
        private void itemsControl_IsVisibleChanged_1(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = this.FindResource("InTransition") as Storyboard;
            sb.Begin();
        }
        private void ItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Category selectedtestseriestype = (Category)(e.OriginalSource as Button).Tag;
                (ViewModel as ShowTestSeriesTypesViewModal).NotifyTestSeriesChange(selectedtestseriestype);
            }
            catch (Exception) { }
        }
    }
}
