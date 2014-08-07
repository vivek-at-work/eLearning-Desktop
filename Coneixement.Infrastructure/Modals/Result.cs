using System.Collections.ObjectModel;
namespace Coneixement.Infrastructure.Modals
{
    public class Result
    {
        public ObservableCollection<Review> Reviews
        {
            get;
            set;
        }
        public QuestionPaper QuestionPaper
        {
            get;
            set;
        }
    }
}
