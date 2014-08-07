using Coneixement.Infrastructure;
using Coneixement.ShowSubjects.Intefaces;
using Coneixement.ShowSubjects.ViewModal;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Coneixement.ShowSubjects.Views
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class SubjectsView : UserControl, ISubjectsView
    {

        public SubjectsView()
        {
             InitializeComponent();
             ViewModel = (IViewModel)new SubjectsViewModal(this);
             
           
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
                var a = (e.OriginalSource as Button).Tag;
               // myTextBox.Text += (((System.Xml.XmlElement)(((e.OriginalSource as Button).Tag)))).InnerText;
            }
            catch (Exception) { }
        }

   
    }
}
