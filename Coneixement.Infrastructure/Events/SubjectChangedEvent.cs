using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
namespace Coneixement.Infrastructure.Events
{
    public class SubjectChangeCompleted : CompositePresentationEvent<Category>
    {
    }
    public class SubjectChangeInitiated : CompositePresentationEvent<Category>
    {
    }
    public class SelectedImageGallaryChanged : CompositePresentationEvent<string>
    {
    }
    public class SelectedQuestionPaperChanged : CompositePresentationEvent<Concept>
    {
    }
    public class SupprtModeInitiatedEvent : CompositePresentationEvent<User>
    {
    }
}
