using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
namespace Coneixement.Infrastructure.Events
{
   public class ExaminationTypeChangeCompleted : CompositePresentationEvent<Category>
    {
    }
    public class ExaminationTypeChangeInitiated : CompositePresentationEvent<Category>
    {
    }
    public class TestSeriesTypeChangeCompleted : CompositePresentationEvent<Category>
    {
    }
    public class TestSeriesTypeChangeInitiated : CompositePresentationEvent<Category>
    {
    }
}
