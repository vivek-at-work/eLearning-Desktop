using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using Coneixement.ShowSubjects.Intefaces;
using Coneixement.ShowSubjects.ViewModal;
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
namespace Coneixement.ShowSubjects.Views
{
    /// <summary>
    /// Interaction logic for SubjectView.xaml
    /// </summary>
    public partial class SubjectView : UserControl, ISubjectsView
    {
        public SubjectView()
        {
            InitializeComponent();
            ViewModel = new SubjectsViewModal(this);
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
                Subject selectedsubject = (Subject)(e.OriginalSource as Button).Tag;              
                (ViewModel as SubjectsViewModal).NotifySubjectChange(selectedsubject);
            }
            catch (Exception) { }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (ViewModel as SubjectsViewModal).ShowPreviousView();
        }
        private void itemsControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard sb = this.FindResource("InTransition") as Storyboard;
            sb.Begin();
        }
    }
}
