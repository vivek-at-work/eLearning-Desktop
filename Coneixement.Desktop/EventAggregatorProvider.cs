using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace Coneixement.Desktop
{
    public static class ServicesFactory
    {
        // Singleton instance of the EventAggregator service
        private static EventAggregator eventService = null;

        // Lock (sync) object
        private static object _syncRoot = new object();

        // Factory method
        public static EventAggregator EventService
        {
            get
            {
                // Lock execution thread in case of multi-threaded
                // (concurrent) access.
                lock (_syncRoot)
                {
                    if (null == eventService)
                    {
                        eventService = new EventAggregator();
                    }
                    // Return singleton instance
                    return eventService;
                } // lock
            }
        }
    }
    public class GenericEvent<tvalue /> <TValue>: CompositeWpfEvent<EventParameters<TValue>>{ }
    public class EventParameters<TValue>
    {
        public string Topic { get; private set; }
        public TValue Value { get; private set; }
    }
}
