using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Coneixement.SuggestionsForUserModule.Interfaces;
using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using System.Threading;
using System;
using System.Threading.Tasks;
namespace Coneixement.SuggestionsForUser
{
    /// <summary>
    /// Interaction logic for SuggestionsForUserUserControl.xaml
    /// </summary>
    public partial class SuggestionsControl : UserControl, ISugesstionView
    {
        public IViewModel ViewModel
        {
            get
            {
                return (IViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
        public SuggestionsControl()
        {
            InitializeComponent();
            ViewModel = (IViewModel)new SuggestionsViewModel(this);
        }
        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                Concept selectedconecpt = item.DataContext as Concept;
                Dispatcher.BeginInvoke(new Action(() =>
              {
                  (this.ViewModel as SuggestionsViewModel).NotifyConceptChanged(selectedconecpt);
              }));
            };
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (this.ViewModel as SuggestionsViewModel).ShowPreviousView();
        }
    }
}
