using Coneixement.Examination.Interfaces;
using Coneixement.Examination.Views;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.IO;
namespace Coneixement.Examination.ViewModals
{
    public class ExaminationViewModal : INotifyPropertyChanged, IExaminationViewModal
    {
        public event PropertyChangedEventHandler PropertyChanged;
        SubscriptionToken sb;
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        public IView View
        {
            get;
            set;
        }
        QuestionPaper _QuestionPaper;
        public QuestionPaper QuestionPaper
        {
            get
            {
                return _QuestionPaper;
            }
            set
            {
                _QuestionPaper = value;
                RaisePropertyChangedEvent("QuestionPaper");
            }
        }
        DirectoryInfo _QuestionPaperDirectory;
        public DirectoryInfo QuestionPaperDirectory
        {
            get
            {
                return _QuestionPaperDirectory;
            }
            set
            {
                _QuestionPaperDirectory = value;
                RaisePropertyChangedEvent("QuestionPaperDirectory");
            }
        }
        public ExaminationViewModal(IView view)
        {
            View = view;
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            ImportCompleted();
        }
        DispatcherTimer dt;
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    SelectedQuestionPaperChanged ev = _eventAggrigator.GetEvent<SelectedQuestionPaperChanged>();
                    sb = ev.Subscribe(OnSelectedQuestionPaperChanged, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void OnSelectedQuestionPaperChanged(Concept entity)
        {
            if (entity != null)
            {
                QuestionPaperDirectory = new DirectoryInfo(entity.DataPath);
                Task GetQuestionPaper = new Task(() => RefreshQuestionPaper());
                GetQuestionPaper.Start();
                IRegion MainRegion = _regionManager.Regions[RegionNames.ActionRegion];
                if (MainRegion.Views.OfType<Views.Examination>().Count() > 0)
                    MainRegion.Remove(this.View);
                IRegion detailsRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                var a = detailsRegion.Views.FirstOrDefault(x => x == this.View);
                if (a == null)
                {
                    IRegionManager detailsRegionManager = detailsRegion.Add(this.View, null, true);
                }
                detailsRegion.Activate(this.View);
                while (!GetQuestionPaper.IsCompleted)
                {
                }
                Countdown((int)QuestionPaper.Duration, TimeSpan.FromSeconds(1), cur => QuestionPaper.RemainingTime = new DateTime(TimeSpan.FromSeconds(cur).Ticks).ToString("HH:mm:ss").ToString());
            }
        }
        private void RefreshQuestionPaper()
        {
            QuestionPaper = new QuestionPaper(QuestionPaperDirectory.FullName);
        }
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            if (dt != null)
            {
                dt.Stop();
            }
            dt = new System.Windows.Threading.DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (count-- == 0)
                {
                    dt.Stop();
                    if (this.QuestionPaper != null && this.QuestionPaper.p != null && this.QuestionPaper.p.Count() > 0)
                    {
                        MessageBox.Show("Time Up!! heading towards the review", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                        Review();
                    }
                }
                else
                    ts(count);
            };
            ts(count);
            dt.Start();
        }
        public void StopTimer()
        {
            if (dt != null)
            dt.Stop();
        }
        internal void Review()
        {
            QuestionPaper.Next();
            QuestionPaper.currentIndex = 0;
            QuestionPaper.CurrentQuestion = 0;
            ObservableCollection<Review> review = null;
            if (QuestionPaper.p != null)
            {
                IRegion detailsRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                detailsRegion.Deactivate(this.View);
                review = new ObservableCollection<Review>();
                review.Add(new Review()
                {
                    Title = "Total Questions",
                    Comment = QuestionPaper.p.Count().ToString()
                });
                review.Add(new Review()
                {
                    Title = "Total Attempted Questions",
                    Comment = QuestionPaper.p.ToList().FindAll(x => x.UsersAnswer != "" && x.UsersAnswer != null).Count().ToString()
                });
                review.Add(new Review()
                {
                    Title = "Total Skipped Questions",
                    Comment = QuestionPaper.p.ToList().FindAll(x => x.UsersAnswer == "" || x.UsersAnswer == null).Count().ToString()
                });
                review.Add(new Review()
                {
                    Title = "Total Correct Answers",
                    Comment = (QuestionPaper.p.ToList().FindAll(x => x.UsersAnswer != "" && x.UsersAnswer != null).ToList()).FindAll(x => x.UsersAnswer.ToUpper() == x.CorrectAnswer.ToUpper()).Count().ToString()
                });
            }
            StopTimer();
            ReviewUserControl r = new ReviewUserControl(review, QuestionPaper);
            IRegion SecondryRegion = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.SecondaryRegion];
            IRegionManager detailsRegionManager = SecondryRegion.Add(r, null, true);
            SecondryRegion.Activate(r);
            Application.Current.MainWindow.Show();
        }
    }
}
