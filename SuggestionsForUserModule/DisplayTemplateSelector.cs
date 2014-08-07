using Coneixement.Infrastructure.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace Coneixement.SuggestionsForUser
{
    public class DisplayTemplateSelector : DataTemplateSelector
    {
      public  DataTemplate WithTopic { get; set; }
      public  DataTemplate WithOutTopic { get; set; }
      public override DataTemplate SelectTemplate(object item , DependencyObject container)
        {
            Concept path = (Concept)item;
           // if (path.Topics!=null && path.Topics.Count>0)
              // return WithTopic;
            return WithOutTopic;
        }
    }
}
