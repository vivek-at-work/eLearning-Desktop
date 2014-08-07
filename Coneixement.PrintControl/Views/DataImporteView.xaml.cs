using Coneixement.DataImporter.Interfaces;
using Coneixement.Infrastructure;
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
namespace Coneixement.DataImporter.Views
{
    /// <summary>
    /// Interaction logic for PrintControlView.xaml
    /// </summary>
    public partial class DataImporteView : UserControl,IDataImportView
    {
        public DataImporteView(IDataImportViewModal viewModal)
        {
            InitializeComponent();
            ViewModel = viewModal;
            ViewModel.View = this;
        }
        public IViewModel ViewModel
        {
            get
            {
                return (IDataImportViewModal)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
