using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Events;
namespace Coneixement.Infrastructure.Events
{
    public class LicenceValidationCompleted : CompositePresentationEvent<LicencingHistory>
    {
    }
    public class LicenceValidationInitiated : CompositePresentationEvent<LicencingHistory>
    {
    }
}
