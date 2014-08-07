using Coneixement.Infrastructure.Commands;
using Coneixement.Infrastructure.Events;
using Coneixement.Infrastructure.Modals;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Coneixement.Infrastructure;
using System.Windows;
using System.Threading;
using Coneixement.Infrastructure.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
using Coneixement.Infrastructure.Helpers;
using System.Xml.Serialization;
namespace Coneixement.QuestioncatelogGenrator.ViewModals
{
    class CatalogGeneratorViewModal : ICatalogGeneratorViewModal, INotifyPropertyChanged
    {
        SubscriptionToken sb;
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        private Views.CatalogGeneratorView catalogGeneratorView;
        List<Question> Questions;
        string imagefolder;
        string answerkeyfile;
        public string ImageFolder
        {
            get
            {
                return imagefolder;
            }
            set
            {
                imagefolder = value;
                RaisePropertyChangedEvent("ImageFolder");
            }
        }
        public string AnswerKeyFile
        {
            get
            {
                return answerkeyfile;
            }
            set
            {
                answerkeyfile = value;
                RaisePropertyChangedEvent("AnswerKeyFile");
            }
        }
        public string ChapterName
        {
            get;
            set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public CatalogGeneratorViewModal(Views.CatalogGeneratorView catalogGeneratorView)
        {
            this.View = catalogGeneratorView;
            this.catalogGeneratorView = catalogGeneratorView;
            Questions = new List<Question>();
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>(); ;
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            ImportCompleted();
        }
        private void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    SupprtModeInitiatedEvent ev = _eventAggrigator.GetEvent<SupprtModeInitiatedEvent>();
                    sb = ev.Subscribe(OnSupprtModeInitiatedEvent, ThreadOption.PublisherThread, false);
                }
            }
            //
        }
        private void OnSupprtModeInitiatedEvent(User obj)
        {
            if (obj != null)
            {
                IRegion MainRegion = _regionManager.Regions[RegionNames.ActionRegion];
                if (MainRegion.Views.OfType<Views.CatalogGeneratorView>().Count() > 0)
                    MainRegion.Remove(this.View);
                IRegion detailsRegion = _regionManager.Regions[RegionNames.SecondaryRegion];
                var a = detailsRegion.Views.FirstOrDefault(x => x == this.View);
                if (a == null)
                {
                    IRegionManager detailsRegionManager = detailsRegion.Add(this.View, null, true);
                }
                detailsRegion.Activate(this.View);
            }
        }
        public IView View
        {
            get;
            set;
        }
        public void GenerateCatalog()
        {
            var d = Directory.GetDirectories(ImageFolder);
            foreach( var item in d)
            {
                var c = Directory.GetDirectories(item);
                for (int i = 0; i < c.Count(); i++)
                {
                    try
                    {
                        Questions = new List<Question>();
                        ChapterName = new DirectoryInfo(c[i]).Name;
                        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook workbook;
                        Microsoft.Office.Interop.Excel.Worksheet worksheet;
                        Microsoft.Office.Interop.Excel.Range range;
                        workbook = excelApp.Workbooks.Open(Path.Combine(new DirectoryInfo(c[i]).FullName, "catalog.xlsx"));
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets.get_Item(1);
                        if (workbook != null)
                        {
                            int column = 0;
                            int row = 0;
                            range = worksheet.UsedRange;
                            for (row = 2; row <= range.Rows.Count; row++)
                            {
                                if(new DirectoryInfo(item).Name=="MATHS")
                                Questions.Add(new Question()
                                  {
                                      ImagePath = (range.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() + ".jpg",
                                      CorrectAnswer = (range.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range).Value2.ToString().ToUpper(),
                                      ProjectionID = Convert.ToInt32((range.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range).Value2.ToString()),
                                      QutstionID = Questions.Count(),
                                      SolutionImage = (range.Cells[row, 4] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() + ".jpg",
                                  });
                                else
                                    Questions.Add(new Question()
                                    {
                                        ImagePath = (range.Cells[row, 1] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() + ".jpg",
                                        CorrectAnswer = (range.Cells[row, 2] as Microsoft.Office.Interop.Excel.Range).Value2.ToString().ToUpper(),
                                        ProjectionID = Convert.ToInt32((range.Cells[row, 3] as Microsoft.Office.Interop.Excel.Range).Value2.ToString()),
                                        QutstionID = Questions.Count(),
                                    });
                            }
                            Questions = Questions.OrderBy(x => x.ProjectionID).ToList();
                            var s = new XmlSerializer(typeof(List<Question>));
                            using (var stream = new FileStream(Path.Combine(c[i], "catalog.cnx"), FileMode.Create))
                            {
                                s.Serialize(stream, Questions);
                                stream.Close();
                            }
                        }
                        workbook.Close(true, Missing.Value, Missing.Value);
                        excelApp.Quit();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
    }
}
