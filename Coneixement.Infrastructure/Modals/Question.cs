using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
namespace Coneixement.Infrastructure.Modals
{
    public class ReviewHelper
    {
        public string Value
        {
            get;
            set;
        }
        public AnswerType Type
        {
            get;
            set;
        }
    }
    public enum AnswerType
    {
        Option ,
        FinalAnswer ,
        CorrectAnswer ,
    }
    [Serializable]
    [XmlRoot(ElementName = "Question")]
    public class Question
    {
        public IEnumerable<string> Options
        {
            get
            {
                yield return "A";
                yield return "B";
                yield return "C";
                yield return "D";
            }
        }
        public int QutstionID { get; set; }
        public int ProjectionID { get; set; }
        public String ImagePath { get; set; }
        public String CorrectAnswer { get; set; }
        public String UsersAnswer { get; set; }
        public String SolutionImage { get; set; }
        public string Remarks
        {
            get;
            set;
        }
        public ObservableCollection<ReviewHelper> Answers
        {
            get
            {
                ObservableCollection<ReviewHelper> an = new ObservableCollection<ReviewHelper>();
                foreach (var item in Options)
                {
                    an.Add(new ReviewHelper
                    {
                        Type = AnswerType.Option ,
                        Value = item
                    });
                }
                if (UsersAnswer != null)
                {
                    ReviewHelper a = (ReviewHelper)an.First(x => x.Value == UsersAnswer);
                    if (a != null)
                    {
                        a.Type = AnswerType.FinalAnswer;
                    }
                }
                    if (CorrectAnswer != null)
                    {
                        if (CorrectAnswer.Length > 0)
                        {
                            var r = an.First(c => c.Value == CorrectAnswer.Trim()[0].ToString());
                            if (r != null)
                                r.Type = AnswerType.CorrectAnswer;
                        }
                    }
                                 return an;
            }
        }
        public override string ToString()
        {
            return "UserAnswer->" + UsersAnswer + Environment.NewLine + "CorrectAnswer" + CorrectAnswer;
        }
    }
}
