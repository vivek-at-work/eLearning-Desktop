using Coneixement.SuggestionsForUserModule.Interfaces;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Coneixement.SuggestionsForUser;
using Coneixement.Infrastructure.Modals;
using Coneixement.Infrastructure;

namespace SuggestionsForUserModule.Views
{
    /// <summary>
    /// Interaction logic for SelectCategory.xaml
    /// </summary>
    public partial class SelectCategory : UserControl, ISugesstionView
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
    
        public SelectCategory()
        {
            InitializeComponent();
            ViewModel = (IViewModel)SuggestionsViewModel.GetInstance(this);
            this.Loaded += SelectCategory_Loaded;
          
        }
        void btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        void SelectCategory_Loaded(object sender, RoutedEventArgs e)
        {
           // CategoriesListView.SelectionChanged += Category_Selection_Changed;
        }

        private void Category_Selection_Changed(object sender, SelectionChangedEventArgs e)
        {
            (ViewModel as SuggestionsViewModel).SelectedCategory = (sender as ListView).SelectedItem as Category;
            (ViewModel as SuggestionsViewModel).ViewSubjects();
           
        }

        private void CompleteDescription_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Ellipse).Name == "CompleteDescription")
            {
                (ViewModel as SuggestionsViewModel).SelectedCategory = (ViewModel as SuggestionsViewModel).Categories.First(x => x.Title.ToUpper() == "Complete Description".ToUpper());
            }
            if ((sender as Ellipse).Name == "Notes")
            {
                (ViewModel as SuggestionsViewModel).SelectedCategory = (ViewModel as SuggestionsViewModel).Categories.First(x => x.Title.ToUpper() == "Notes".ToUpper());
            }
            if ((sender as Ellipse).Name == "TestSeries")
            {
                (ViewModel as SuggestionsViewModel).SelectedCategory = (ViewModel as SuggestionsViewModel).Categories.First(x => x.Title.ToUpper() == "Test Series".ToUpper());
            }
            if ((sender as Ellipse).Name == "Videos")
            {
                (ViewModel as SuggestionsViewModel).SelectedCategory = (ViewModel as SuggestionsViewModel).Categories.First(x => x.Title.ToUpper() == "Videos".ToUpper());
            }
            Application.Current.MainWindow.Visibility = System.Windows.Visibility.Collapsed;
            (ViewModel as SuggestionsViewModel).ViewSubjects();

        }
}
}
