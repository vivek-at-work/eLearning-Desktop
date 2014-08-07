using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Coneixement.Infrastructure
{
  public interface IView
    {
      IViewModel ViewModel
      {
          get;
          set;
      }
    }
}
