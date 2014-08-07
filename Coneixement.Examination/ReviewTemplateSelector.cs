using Coneixement.Infrastructure.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace Coneixement.Examination
{
    public class ReviewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WithSolutionTemplate { get; set; }
        public DataTemplate WithoutSolutionTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Question path = (Question)item;       
            if (path.SolutionImage!=null)
                return WithSolutionTemplate;
            return WithoutSolutionTemplate;
        }
    }
    public class AnswerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FinalAnswerT { get; set; }
        public DataTemplate CorrectAnswerT { get; set; }
        public DataTemplate OptionT { get; set; }
        public override DataTemplate SelectTemplate(object item , DependencyObject container)
        {
            ReviewHelper path = (ReviewHelper)item;
            if (path.Type ==  AnswerType.CorrectAnswer)
                return CorrectAnswerT;
            if (path.Type == AnswerType.FinalAnswer)
            return FinalAnswerT;
            return OptionT;
        }
    }
}
