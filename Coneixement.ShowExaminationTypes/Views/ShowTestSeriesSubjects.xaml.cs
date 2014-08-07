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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Coneixement.ShowExaminationTypes.Views
{
    /// <summary>
    /// Interaction logic for ShowTestSeriesSubjects.xaml
    /// </summary>
    public partial class ShowTestSeriesSubjects : UserControl, IShowTestSeriesSubjectsView
    {
        public ShowTestSeriesSubjects()
        {
            InitializeComponent();
            ViewModel = new ShowTestSeriesSubjectsViewModal(this);
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (ViewModel as ShowTestSeriesSubjectsViewModal).ShowPreviousView();
        }
        private void itemsControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = this.FindResource("InTransition") as Storyboard;
            sb.Begin();
        }
        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
                 try
            {
                Subject selectedsubject = (Subject)(e.OriginalSource as Button).Tag;              
                (ViewModel as ShowTestSeriesSubjectsViewModal).NotifySubjectChange(selectedsubject);
            }
            catch (Exception) { }
        }
    }
}
