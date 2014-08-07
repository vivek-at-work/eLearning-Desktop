using Coneixement.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
namespace Coneixement.Infrastructure.Modals
{
    public class QuestionPaper : INotifyPropertyChanged
    {
        string correntCorrectAnswer = string.Empty;
        int totalquestions;
        int currentquestion;
        public int TotalQuestions
        {
            get
            {
                return totalquestions;
            }
            set
            {
                totalquestions = value;
                RaisePropertyChangedEvent("TotalQuestions");
            }
        }
        public int CurrentQuestion
        {
            get
            {
                return currentquestion;
            }
            set
            {
                currentquestion = value;
                RaisePropertyChangedEvent("CurrentQuestion");
            }
        }
       public int currentIndex = 0;
        string Timeleft;
        public String RemainingTime
        {
            set
            {
                Timeleft = value;
                RaisePropertyChangedEvent("RemainingTime");
            }
            get
            {
                return Timeleft;
            }
        }
        public ObservableCollection<Question> p = null;
        public ObservableCollection<Question> QuestionPaperList
        {
            set
            {
                p = value;
                RaisePropertyChangedEvent("QuestionPaperList");
            }
            get{
                return p;
            }
        }
        public string Title
        {
            get;
            set;
        }
        DispatcherTimer dispatcherTimer = null;
        public int Duration
        {
            get;
            set;
        }
        ObservableCollection<Question> _question;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                RaisePropertyChangedEvent("Questions");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string QuestionPaperDirectory;
        public QuestionPaper(string QuestionPaperDirectory)
        {
             Title = new DirectoryInfo(QuestionPaperDirectory).Name;
            ObservableCollection<Question> q = new ObservableCollection<Question>();
            currentIndex = 0;
            this.QuestionPaperDirectory = QuestionPaperDirectory;
            Question item = null;
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Question>));
            if (File.Exists(Path.Combine(QuestionPaperDirectory, "catalog.cnx")))
            {
                string sourcepath = Path.Combine(QuestionPaperDirectory, "catalog.cnx");
                string destinationPath=Path.Combine(StorageHelper.AllUsersDataFolder, Assembly.GetExecutingAssembly().FullName, "temp");
                if(!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                string decpthcatlogparentfolder = Path.Combine(destinationPath, new DirectoryInfo(QuestionPaperDirectory).Name);
                if (!Directory.Exists(decpthcatlogparentfolder))
                {
                    Directory.CreateDirectory(decpthcatlogparentfolder);
                }
                string decrptedfilepath = Path.Combine(decpthcatlogparentfolder, "catalog.cnx");
                if(File.Exists(decrptedfilepath))
                {
                    File.Delete(decrptedfilepath);
                }
                Helpers.FileEncryption.DecryptFile(Path.Combine(QuestionPaperDirectory, "catalog.cnx"), decrptedfilepath);
                using (StreamReader srdr = new StreamReader(decrptedfilepath))
                {
                    QuestionPaperList = new ObservableCollection<Question>();
                   List<Question> plist = (List<Question>)xmlser.Deserialize(srdr);
                  foreach (Question qitem in plist)
                  {
                      qitem.ImagePath = Path.Combine(QuestionPaperDirectory, qitem.ImagePath);
                      if(qitem.SolutionImage!=null)
                      qitem.SolutionImage = Path.Combine(QuestionPaperDirectory , qitem.SolutionImage);
                          QuestionPaperList.Add(qitem);
                  }
                    srdr.Close();
                    item = QuestionPaperList[currentIndex];
                }
                q.Add(new Question()
                {
                    ImagePath = item.ImagePath,
                    CorrectAnswer = item.CorrectAnswer,
                    QutstionID = item.QutstionID,
                    SolutionImage = item.SolutionImage,
                    ProjectionID = item.ProjectionID,
                });
                Questions = q;
                TotalQuestions = QuestionPaperList.Count();
                CurrentQuestion = 1;
                Duration = ((int)QuestionPaperList.Count() + (int)Math.Ceiling((double)(QuestionPaperList.Count / 2)) * 60);
                RemainingTime = new DateTime(TimeSpan.FromSeconds(Duration).Ticks).ToString("HH:mm:ss").ToString();
            }
            else
            {
                MessageBox.Show("Catalog File Is Missing", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }
        public void Next()
        {
                if (QuestionPaperList != null)
                    if (currentIndex < QuestionPaperList.Count - 1 && QuestionPaperList.Count > 0)
                    {
                        QuestionPaperList[currentIndex].UsersAnswer= Questions[0].UsersAnswer;
                        currentIndex++;
                        CurrentQuestion++;
                        Questions.Clear();
                        Questions.Add(QuestionPaperList[currentIndex]);
                        Questions[0].ImagePath = Path.Combine(QuestionPaperDirectory , QuestionPaperList[currentIndex].ImagePath);
                    }
        }
        public void Previous()
        {
                if (currentIndex > 0)
                {
                    QuestionPaperList[currentIndex].UsersAnswer = Questions[0].UsersAnswer;
                    currentIndex--;
                    CurrentQuestion--;              
                    Questions.Clear();
                    Questions.Add(QuestionPaperList[currentIndex]);
                    Questions[0].ImagePath = Path.Combine(QuestionPaperDirectory , QuestionPaperList[currentIndex].ImagePath);
                }
        }
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void NextReview()
        {   
            if (QuestionPaperList != null)
                if (currentIndex < QuestionPaperList.Count - 1 && QuestionPaperList.Count > 0)
                {
                    currentIndex++;
                    CurrentQuestion++;
                    if (currentIndex > 1)
                    {
                        Questions[0].UsersAnswer = correntCorrectAnswer;
                    }
                    Questions.Clear();
                    Questions.Add(QuestionPaperList[currentIndex-1]);
                    correntCorrectAnswer = QuestionPaperList[currentIndex-1].UsersAnswer;
                    Questions[0].Remarks= GetRemarks(Questions[0]);
                }
        }
        public void PreviousReview()
        {
            if (currentIndex > 1)
            {
                QuestionPaperList[currentIndex-1].UsersAnswer = correntCorrectAnswer;
                currentIndex--;
                CurrentQuestion--;
                Questions.Clear();
                correntCorrectAnswer = QuestionPaperList[currentIndex-1].UsersAnswer;
                Questions.Add(QuestionPaperList[currentIndex-1]);
                Questions[0].Remarks= GetRemarks(Questions[0]);
            }
        }
        private string GetRemarks(Question item)
        {
                if (item.UsersAnswer != null)
                {
                    if (item.CorrectAnswer.ToUpper().IndexOf(correntCorrectAnswer.ToUpper()) > -1)
                    {
                        return "Correct Answer";
                    }
                    return "In-Correct Answer";
                }
                else
                {
                    return "Skipped";
                }
            }
        }
}
