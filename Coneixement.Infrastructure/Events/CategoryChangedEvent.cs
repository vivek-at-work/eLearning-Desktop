using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
namespace Coneixement.Infrastructure.Events
{
    public class CategoryChangeCompleted : CompositePresentationEvent<Category>
    {
    }
    public class CategoryChangeInitiated : CompositePresentationEvent<Category>
    {
    }
}
