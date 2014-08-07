using System;
using Coneixement.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using Coneixement.VideoPlayer.Interfaces;
using System.Windows.Threading;
using Coneixement.VideoPlayer.ViewModel;
using System.Windows.Input;
using System.Reflection;
using Coneixement.Infrastructure.Modals;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Coneixement.VideoPlayer.Views
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl, IVideoPlayerView
    {
        bool isDragging = false;
        private readonly DispatcherTimer _positionTimer;
        private readonly TimeSpan _timeSpanTick = new TimeSpan(0, 0, 0, 0, (int)((1.0 / 29.97) * 1000)); // time for 1 HD frame
        public VideoPlayer()
        {
            InitializeComponent();
            _positionTimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 100), DispatcherPriority.Loaded, DispatcherTimerTick, Dispatcher);
            timelineSlider.IsEnabled = true;
            volumeSlider.IsEnabled = true;
             ViewModel = new MediaPlayerViewModel(this);
            InitializePropertyValues();
            timelineSlider.ApplyTemplate();
            Thumb thumb = (timelineSlider.Template.FindName(
      "PART_Track", timelineSlider) as Track).Thumb;
            thumb.DragCompleted += thumb_DragCompleted;
            thumb.DragStarted += thumb_DragStarted;
            videoElement.MediaOpened += videoElement_MediaOpened;
            videoElement.MediaEnded += videoElement_MediaEnded;
        }
        void thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            isDragging = true;
        }
        private DependencyObject GetThumb(DependencyObject root)
        {
            if (root is Thumb)
                return root;
            DependencyObject thumb = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                thumb = GetThumb(VisualTreeHelper.GetChild(root, i));
                if (thumb is Thumb)
                    return thumb;
            }
            return thumb;
        }
        void thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (isDragging)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    int SliderValue = (int)timelineSlider.Value;
                    // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
                    // Create a TimeSpan with miliseconds equal to the slider value.
                    TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                    videoElement.Position = ts;
                    isDragging = false;
                    // videoElement.Play();
                }));
            }
        }
        void videoElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if(!(this.ViewModel as MediaPlayerViewModel).MoveNextInPlayList())
            videoElement.Stop();
        }
        void videoElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (videoElement.NaturalDuration.HasTimeSpan)
            {
                timelineSlider.Maximum = videoElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            }
            timelineSlider.IsEnabled = videoElement.IsLoaded;
            volumeSlider.IsEnabled = videoElement.IsLoaded;
            timelineSlider.IsEnabled = true;
            timelineSlider.Value = Convert.ToDouble(0.0);
            (ViewModel as MediaPlayerViewModel).ShowOverlay = Visibility.Collapsed;
        }
        void videoElement_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.PreviewKeyDown += VideoElementPreviewKeyDown;
            }
        }
        void VideoElementPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Right && e.Key != Key.Left)
                return;
            PauseVideoElement();
            if (e.Key == Key.Left)
                videoElement.Position -= _timeSpanTick;
            else
                videoElement.Position += _timeSpanTick;
            UpdateUserInterfaceByTimer();
        }
        void UpdateUserInterfaceByTimer()
        {
            if (videoElement != null)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (!isDragging)
                    {
                        timelineSlider.Value = videoElement.Position.TotalMilliseconds;
                        videoElementTime.Text = videoElement.Position.ToString();
                    }
                }));
            }
            else
            {
                timelineSlider.Value = 0.0;
            }
        }
        void DispatcherTimerTick(object sender, EventArgs e)
        {
            if(!isDragging)
            UpdateUserInterfaceByTimer();
        }
        void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            videoElement.Play();
        }
        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                (this.ViewModel as MediaPlayerViewModel).NotifyTopicChanged(item.DataContext as Topic);
            }
        }
        private void InitializePropertyValues()
        {
            videoElement.Position = new TimeSpan(0, 0, 0);
            timelineSlider.IsEnabled = true;
            timelineSlider.Value = Convert.ToDouble(0.0);
        }
        void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            PauseVideoElement();
        }
        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            videoElement.Stop();
        }
        void PauseVideoElement()
        {
            _positionTimer.Stop();
            videoElement.Pause();
        }
        void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoElement.SpeedRatio = speedRatioSlider.Value;
        }
        void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoElement.Volume = volumeSlider.Value;
        }
        void timelineSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
           // if (videoElement.NaturalDuration.HasTimeSpan)
               // timelineSlider.Maximum = videoElement.NaturalDuration.TimeSpan.TotalMilliseconds;
           // videoElement.Position = new TimeSpan(0, 0, 0, 0, (int)timelineSlider.Value);
        }
        void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                int SliderValue = (int)timelineSlider.Value;
                // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
                // Create a TimeSpan with miliseconds equal to the slider value.
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                videoElement.Position = ts;
               // videoElement.Play();
            }));
        }
        public IViewModel ViewModel
        {
            get
            {
                return (IViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
