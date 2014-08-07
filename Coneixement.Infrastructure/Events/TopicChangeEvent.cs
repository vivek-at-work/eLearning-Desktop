using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
namespace Coneixement.Infrastructure.Events
{
    public class ConceptChangeInitiated : CompositePresentationEvent<Category>
    {
    }
    public class ConceptChangeCompleted : CompositePresentationEvent<Category>
    {
    }
}
