using Coneixement.Infrastructure.Modals;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace Coneixement.Examination.Views
{
    /// <summary>
    /// Interaction logic for ReviewUserControl.xaml
    /// </summary>
    public partial class ReviewUserControl : UserControl
    {
        public ReviewUserControl(ObservableCollection<Review> Reviews , QuestionPaper paper)
        {
            InitializeComponent();
            paper.Questions.Clear();
            paper.CurrentQuestion = 0;
            paper.currentIndex = 0;
            paper.NextReview();
            this.DataContext = new Result()
            {
                Reviews = Reviews ,
                QuestionPaper = paper
            };
        }
        private void Prev_Click(object sender , RoutedEventArgs e)
        {
            (this.DataContext as Result).QuestionPaper.PreviousReview();
        }
        private void Next_Click(object sender , RoutedEventArgs e)
        {
            (this.DataContext as Result).QuestionPaper.NextReview();
        }
    }
}
