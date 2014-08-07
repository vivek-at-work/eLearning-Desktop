using Coneixement.Examination.Interfaces;
using Coneixement.Examination.ViewModals;
using Coneixement.Infrastructure;
using System.Windows;
using System.Windows.Controls;
namespace Coneixement.Examination.Views
{
    /// <summary>
    /// Interaction logic for Examination.xaml
    /// </summary>
    public partial class Examination : UserControl , IExamination
    {
        public Examination()
        {
            InitializeComponent();
            ViewModel = (IViewModel)new ExaminationViewModal(this);
        }
        public IViewModel ViewModel
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
        private void Prev_Click(object sender , RoutedEventArgs e)
        {
            (this.ViewModel as ExaminationViewModal).QuestionPaper.Previous();
        }
        private void Next_Click(object sender , RoutedEventArgs e)
        {
            (this.ViewModel as ExaminationViewModal).QuestionPaper.Next();
        }
        private void submit_Click(object sender , RoutedEventArgs e)
        {
            (this.ViewModel as ExaminationViewModal).Review();
        }
        private void UserControl_Unloaded(object sender , RoutedEventArgs e)
        {
            (this.ViewModel as ExaminationViewModal).StopTimer();
        }
    }
}
