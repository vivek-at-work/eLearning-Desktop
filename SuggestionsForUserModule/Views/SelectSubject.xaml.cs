
using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Modals;
using Coneixement.SuggestionsForUser;
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

namespace SuggestionsForUserModule.Views
{
    /// <summary>
    /// Interaction logic for SelectSubject.xaml
    /// </summary>
    public partial class SelectSubject : UserControl ,ISugesstionView
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
        public SelectSubject()
        {
            InitializeComponent();
            ViewModel = (IViewModel)SuggestionsViewModel.GetInstance(this);
            if (SuggestionsViewModel.GetInstance(this).SelectedCategory.Title.ToUpper() == "Test Series".ToUpper())
            {
                _panelTestSeries.Visibility = System.Windows.Visibility.Visible;
                _panel.Visibility = Visibility.Collapsed;
            }
            else
            {
                _panelTestSeries.Visibility = System.Windows.Visibility.Collapsed;
                _panel.Visibility = Visibility.Visible;
            }
            this.Loaded += SelectSubject_Loaded;
        }

        void SelectSubject_Loaded(object sender, RoutedEventArgs e)
        {
           // SubjectsListView.SelectionChanged += SubjectsListView_SelectionChanged;
        }

        void SubjectsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (ViewModel as SuggestionsViewModel).SelectedSubject = (sender as ListView).SelectedItem as Subject;
        }

        private void Physics_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (ViewModel as SuggestionsViewModel).ViewConcepts();
            string Name = (sender as Ellipse).Name;
         
              if (Name == "Physics")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Physics".ToUpper());
              }
              if (Name == "Chemistry")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Chemistry".ToUpper());
              }
              if (Name == "Mathematics")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Mathematics".ToUpper());
              }
              if (Name == "Biology")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Biology".ToUpper());
              }
              if (Name == "ChapterwiseMockTest")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Chapter wise Mock Test".ToUpper());
                  Application.Current.MainWindow.Visibility = System.Windows.Visibility.Hidden;
              }
              if (Name == "LastYearTestPapers")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Last Year Test Papers".ToUpper());
                          
              }
              if (Name == "PrelimsandadvanceTestPaper")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Prelims and advance Test Paper".ToUpper());
                  Application.Current.MainWindow.Visibility = System.Windows.Visibility.Hidden;            
              }
              if (Name == "UnitTest")
              {
                  (ViewModel as SuggestionsViewModel).SelectedSubject = (ViewModel as SuggestionsViewModel).Subjects.FirstOrDefault(x => x.Title.ToUpper() == "Unit Test".ToUpper());
                  Application.Current.MainWindow.Visibility = System.Windows.Visibility.Hidden;              
              }
              
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SuggestionsViewModel.GetInstance(this).ShowPreviousView();

        }
     
    }
}
