using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowCategories.Intefaces;
using Coneixement.ShowCategories.ViewModal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
namespace Coneixement.ShowCategories.Views
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl, ICategoriesView
    {
        public CategoriesView()
        {
             InitializeComponent();
             ViewModel = (IViewModel)new CategoriesViewModal(this);
        }
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
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            itemsControl.Visibility = Visibility.Visible;
        }
        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Category selectedcategory =(Category) (e.OriginalSource as Button).Tag;
                (ViewModel as CategoriesViewModal).NotifyCategoryChange(selectedcategory);
            }
            catch (Exception) { }
        }
        private void itemsControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = this.FindResource("InTransition") as Storyboard;         
            sb.Begin();
        }
    }
}
