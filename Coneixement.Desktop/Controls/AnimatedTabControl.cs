using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace Coneixement.Desktop.Controls
{
    class AnimatedTabControl : TabControl
    {
        //Routed events are the events that travel through the parent/ Child container of a control with raising  their events .The movement logic known as Routing Strategy 
        // So events can also be captured at parent or child container instead of the particular control which invokes it.
        // WPF UI is a composite architecture based  , means a control can be consists of several other controls or resource.
        //  Routed events are events which navigate up or down the visual tree acording to their RoutingStrategy. The routing strategy can be bubble, tunnel or direct. You can hook up event handlers on the element that raises the event or also on other elements above or below it by using the attached event syntax: Button.Click="Button_Click".
        //Routed events normally appear as pair. The first is a tunneling event called PreviewMouseDown and the second is
        //the bubbling called MouseDown. They don't stop routing if the reach an event handler. 
        //To stop routing then you have to set
        //    e.Handled = true;
        //Tunneling The event is raised on the root element and navigates down to the visual tree until it reaches the source element or 
        //    until the tunneling is stopped by marking the event as handeld. By naming convention it is called Preview... 
        //        and appears before corresponding bubbling event.
        //Bubbling The event is raised on the source element and navigates up to the visual tree until it reaches the root element or 
        //    until the bubbling is stopped by marking the event as handled. The bubbling event is raised after the tunneling event.
        //Direct The event is raised on the source element and must be handled on the source element itself. This behavior is the same 
        //    as normal .NET events.
        public static readonly RoutedEvent SelectionChangingEvent = EventManager.RegisterRoutedEvent(
          "SelectionChanging", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AnimatedTabControl));
        //If your calling code is time consuming, then simple put your code in Task (if you have knowledge about TPL.) 
        //Here is sample application that will help you.
        // DispatcherTimer Works on the same thread as of dispacher
        //  DispatcherTimer is the regular timer. It fires its Tick event on the UI thread, you can do anything you want with the UI. System.Timers.Timer is an asynchronous timer, its Elapsed event runs on a thread pool thread. You have to be very careful in your event handler, you are not allowed to touch any UI component. And you'll need to use the lock statement where ever you access class members that are also used on the UI thread.
        //    In the linked answer, the Timer class was more appropriate because the OP was trying to run code asynchronously on purpose.
        private DispatcherTimer timer;
        public AnimatedTabControl()
        {
            //The style key. To work correctly as part of theme style lookup,
            //this value is expected to be the Type of the control being styled.
            DefaultStyleKey = typeof(AnimatedTabControl);
        }
        public event RoutedEventHandler SelectionChanging
        {
            add { AddHandler(SelectionChangingEvent, value); }
            remove { RemoveHandler(SelectionChangingEvent, value); }
        }
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(
                (Action)delegate
                {
                    this.RaiseSelectionChangingEvent();
                    this.StopTimer();
                    this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 500) };
                    EventHandler handler = null;
                    handler = (sender, args) =>
                    {
                        this.StopTimer();
                        base.OnSelectionChanged(e);
                    };
                    this.timer.Tick += handler;
                    this.timer.Start();
                });
        }
        // This method raises the Tap event
        private void RaiseSelectionChangingEvent()
        {
            var args = new RoutedEventArgs(SelectionChangingEvent);
            RaiseEvent(args);
        }
        private void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer = null;
            }
        }
    }
}
