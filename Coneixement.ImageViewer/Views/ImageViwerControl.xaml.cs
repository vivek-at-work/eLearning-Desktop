using Coneixement.ImageViewer.Interfaces;
using Coneixement.ImageViewer.ViewModals;
using Coneixement.Infrastructure;
using System.Windows.Controls;
namespace Coneixement.ImageViewer.Views
{
    /// <summary>
    /// Interaction logic for ImageViwerControl.xaml
    /// </summary>
    public partial class ImageViwerControl : UserControl, IImageViewerView
    {
        public ImageViwerControl()
        {
            InitializeComponent();
            this.ViewModel = (IViewModel)new ImageViewerViewModal(this);
        }
        public IViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
            get
            {
                return (IViewModel)this.DataContext;
            }
        }
    }
}
