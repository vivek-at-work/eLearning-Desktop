using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Coneixement.Infrastructure
{
  public  interface IViewModel
    {
      IView View
      {
          get;
          set;
      }
    }
}
