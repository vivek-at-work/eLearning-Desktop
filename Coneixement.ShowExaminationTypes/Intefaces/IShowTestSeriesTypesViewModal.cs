using Coneixement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Coneixement.ShowExaminationTypes.Intefaces
{
  public  interface IShowTestSeriesTypesViewModal :IViewModel
    {
    }
  public interface IShowTestSeriesConceptViewModal : IViewModel
    {
    }
  public interface IShowTestSeriesSubjectsViewModal : IViewModel
    {
    }
  public interface IShowPreviuosYearPaperTypesViewModal : IViewModel
    {
    }
  public interface IShowExaminationsViewModal : IViewModel
  {
  }
  public interface IShowTestSeriesTypesView : IView
  {
  }
  public interface IShowTestSeriesTopicsView : IView
  {
  }
  public interface IShowTestSeriesSubjectsView: IView
  {
  }
  public interface IShowPreviuosYearPaperTypesView : IView
  {
  }
  public interface IShowExaminationsView: IView
  {
  }
}
