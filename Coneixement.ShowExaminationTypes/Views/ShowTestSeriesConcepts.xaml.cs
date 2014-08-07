using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowExaminationTypes.Intefaces;
using Coneixement.ShowExaminationTypes.ViewModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Coneixement.ShowExaminationTypes.Views
{
    /// <summary>
    /// Interaction logic for ShowTestSeriesTopics.xaml
    /// </summary>
    public partial class ShowTestSeriesConcepts : UserControl, IShowTestSeriesTopicsView
    {
        public ShowTestSeriesConcepts()
        {
            InitializeComponent();
            ViewModel = new ShowTestSeriesConceptsViewModal(this);
        }
        public Infrastructure.IViewModel ViewModel
        {
            get
            {
                return this.DataContext as IViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                (this.ViewModel as ShowTestSeriesConceptsViewModal).NotifyConceptChanged(item.DataContext as Concept);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (this.ViewModel as ShowTestSeriesConceptsViewModal).ShowPreviousView();
        }
    }
}
