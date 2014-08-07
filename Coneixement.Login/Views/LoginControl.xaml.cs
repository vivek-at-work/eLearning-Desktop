using Coneixement.Infrastructure;
using Coneixement.Login.Interfaces;
using System.Windows.Controls;
namespace Coneixement.Login.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, ILoginView
    {
        public LoginControl(ILoginViewModel viewModal)
        {
            InitializeComponent();
            ViewModel =(LoginViewModel) viewModal;
            ViewModel.View = this;
        }
        public IViewModel ViewModel
        {
            get
            {
                return (ILoginViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
