using Coneixement.Infrastructure;
using Coneixement.QuestioncatelogGenrator.ViewModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Coneixement.QuestioncatelogGenrator.Views
{
    /// <summary>
    /// Interaction logic for CatalogGeneratorView.xaml
    /// </summary>
    public partial class CatalogGeneratorView : UserControl, ICatalogGeneratorView
    {
        public CatalogGeneratorView()
        {
            InitializeComponent();
            this.ViewModel = new CatalogGeneratorViewModal(this);
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.ViewModel as CatalogGeneratorViewModal).GenerateCatalog();
        }
    }
}
