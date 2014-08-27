using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Commands;
using Coneixement.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using Coneixement.DataImporter.Interfaces;
using System.Windows.Input;
using Coneixement.DataImporter.Views;
using System.Reflection;
using System.IO;
using System.Windows;
using Coneixement.Infrastructure.Modals;
using System.Xml.Serialization;
using System.Threading;
using System.Runtime.InteropServices;
namespace Coneixement.DataImporter.ViewModal
{
    class DataImportViewModal : INotifyPropertyChanged, IDataImportViewModal
    {
        bool IsDVDDriveAvailable;
        int cycle = 0;
        TaskScheduler UIContxt;
        CancellationTokenSource CanclationSource;
        CancellationToken token;
        public FileInfo CategoriesFile
        {
            get;
            set;
        }
        string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }
        IEventAggregator _eventAggrigator;
        IUnityContainer _container;
        IRegionManager _regionManager;
        public event PropertyChangedEventHandler PropertyChanged;
        string _DataImportPath;
        public string DataImportPath
        {
            get
            {
                return _DataImportPath;
            }
            set
            {
                _DataImportPath = value;
                RaisePropertyChanged("DataImportPath");
            }
        }
        string ParentDirectoryPath
        {
            get;
            set;
        }
        string DataRepositoryPath
        {
            get;
            set;
        }
        string SettingsRepositoryPath
        {
            get;
            set;
        }
        public List<Category> CategoriesList
        {
            get;
            set;
        }
        public ICommand ImportCommand
        {
            get;
            internal set;
        }
        private bool CanExecuteImportCommand()
        {
            return (!(string.IsNullOrWhiteSpace(DataImportPath))) && Directory.Exists(DataImportPath);
        }
        private void CreateImportCommand()
        {
            ImportCommand = new RelayCommand(param => PerformDataImport(), param => CanExecuteImportCommand());
        }
        public  void PerformDataImport()
        {
            ReadCategories();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                ManageList();
            };
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Message = "Data Menifest has been Saved";
            BackgroundWorker workercopyDvd1 = new BackgroundWorker();
            workercopyDvd1.WorkerReportsProgress = true;
            workercopyDvd1.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                cycle = 1;
                DirectoryCopy(DataImportPath, DataRepositoryPath, true);
            };
            workercopyDvd1.ProgressChanged += workercopyDvd1_ProgressChanged;
            workercopyDvd1.RunWorkerAsync();
            workercopyDvd1.RunWorkerCompleted += workercopyDvd1_RunWorkerCompleted;
        }
        void workercopyDvd1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
         void workercopyDvd1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxResult result=MessageBoxResult.OK;
            while( ( Directory.Exists(DataImportPath) && (File.Exists(Path.Combine(DataImportPath,"Categories.cnx")))))
            {
                result = GetResult();
                if (result == MessageBoxResult.Cancel)
                    break;
            }
            if (result != MessageBoxResult.Cancel)
            {
                BackgroundWorker workercopyDvd2 = new BackgroundWorker();
                workercopyDvd2.DoWork += delegate(object s, DoWorkEventArgs args)
                {
                    DirectoryCopy(DataImportPath, DataRepositoryPath, true);
                };
                workercopyDvd2.RunWorkerAsync();
                workercopyDvd2.RunWorkerCompleted += workercopyDvd2_RunWorkerCompleted;
            }
            else
                _eventAggrigator.GetEvent<DataImportComplted>().Publish(new DirectoryInfo(DataRepositoryPath));
        }
         public MessageBoxResult GetResult()
         {
             if(IsDVDDriveAvailable)
             EjectMedia.Eject(new DirectoryInfo(DataImportPath).Parent.FullName);
             MessageBoxResult result = MessageBox.Show("Insert Another DVD That is Provided with the Package and Press OK Button Then", "Information", MessageBoxButton.OKCancel, MessageBoxImage.Information);
             return result;
         }
         void workercopyDvd2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _eventAggrigator.GetEvent<DataImportComplted>().Publish(new DirectoryInfo(DataRepositoryPath));
        }
        private void ManageList()
        {
            var t = Task.Factory.StartNew(() =>
            {
                Message = "Preparing Data Menifest";
                var Categories = CategoriesList;
                Parallel.ForEach(Categories, x =>
                {
                    x.CategoryDataPath = Path.Combine(DataRepositoryPath, x.CategoryDataPath);
                    if (x.Title.ToLower() == "Complete Description".ToLower() || x.Title.ToLower() == "Notes".ToLower())
                    {
                        foreach (var item in x.Subjects)
                        {
                            item.SubjectDataPath = Path.Combine(DataRepositoryPath, item.SubjectDataPath);
                            foreach (var concept in item.Concepts)
                            {
                                concept.DataPath = Path.Combine(DataRepositoryPath, concept.DataPath);
                            }
                        }
                    }
                    else
                    {
                        if (x.Title.ToLower() == "Videos".ToLower())
                        {
                            foreach (var item in x.Subjects)
                            {
                                item.SubjectDataPath = Path.Combine(DataRepositoryPath, item.SubjectDataPath);
                                foreach (var concept in item.Concepts)
                                {
                                    concept.DataPath = Path.Combine(DataRepositoryPath, concept.DataPath);
                                    foreach (var topic in concept.Topics)
                                    {
                                        topic.DataPath = Path.Combine(DataRepositoryPath, topic.DataPath);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var subcategory in x.SubCategories)
                            {
                                subcategory.CategoryDataPath = Path.Combine(DataRepositoryPath, subcategory.CategoryDataPath);
                                if (subcategory.Title.ToLower() == "Chapter wise Mock Test".ToLower())
                                {
                                    foreach (var subject in subcategory.Subjects)
                                    {
                                        subject.SubjectDataPath = Path.Combine(DataRepositoryPath, subject.SubjectDataPath);
                                        foreach (var concept in subject.Concepts)
                                        {
                                            concept.DataPath = Path.Combine(DataRepositoryPath, concept.DataPath);
                                            foreach (var topic in concept.Topics)
                                            {
                                                topic.DataPath = Path.Combine(DataRepositoryPath, topic.DataPath);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var examtype in subcategory.RelatedExaminationsTypes)
                                    {
                                        examtype.CategoryDataPath = Path.Combine(DataRepositoryPath, examtype.CategoryDataPath);
                                        foreach (var exam in examtype.Examinations)
                                        {
                                            exam.CategoryDataPath = Path.Combine(DataRepositoryPath, exam.CategoryDataPath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
                KeepList();
                Message = "Menifest has been saved.";
            });
        }
        private void KeepList()
        {
            if (!Directory.Exists(Path.Combine(DataRepositoryPath, "Settings")))
                Directory.CreateDirectory(Path.Combine(DataRepositoryPath, "Settings"));
            var s = new XmlSerializer(typeof(List<Category>));
            using (var stream = new FileStream(Path.Combine(DataRepositoryPath, "Settings", "manifest.cnx"), FileMode.Create))
            {
                s.Serialize(stream, CategoriesList);
                stream.Close();
            }
        }
        public IView View
        {
            get;
            set;
        }
        private SubscriptionToken sb;
        Visibility displayProgressbar;
        public Visibility DisplayProgressBar
        {
            get
            {
                return displayProgressbar;
            }
            set
            {
                displayProgressbar = value;
                RaisePropertyChanged("DisplayProgressBar");
            }
        }
        public DataImportViewModal()
        {
            UIContxt = TaskScheduler.FromCurrentSynchronizationContext();
            CanclationSource = new CancellationTokenSource();
            token = CanclationSource.Token;
            _container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            _eventAggrigator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            View = new DataImporteView(this);
            ImportCompleted();
            CreateImportCommand();
            ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            DataRepositoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IkonTechnology");
            if (!Directory.Exists(DataRepositoryPath))
                Directory.CreateDirectory(DataRepositoryPath);
            SettingsRepositoryPath = Path.Combine(ParentDirectoryPath, "Settings");
            var drive = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom).FirstOrDefault();
            if (drive != null)
            {
                IsDVDDriveAvailable = true;
                DataImportPath = Path.Combine(DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom).FirstOrDefault().Name, "StudyMaterial");
            }
            else
            {
                IsDVDDriveAvailable = false;
                DataImportPath = Path.Combine(DriveInfo.GetDrives().FirstOrDefault().Name, "StudyMaterial");
            }
            DisplayMessage = "Initilizing";
        }
        public void ImportCompleted()
        {
            if (_eventAggrigator != null)
            {
                if (sb == null)
                {
                    LoginStatusChangedEvent ev = _eventAggrigator.GetEvent<LoginStatusChangedEvent>();
                    sb = ev.Subscribe(ShowDataImportWindow, ThreadOption.PublisherThread, false);
                }
            }
        }
        private void ShowDataImportWindow(User obj)
        {
            if (obj.LastLoginStatus == LastLoginStatus.Success)
            {
                if (!File.Exists(Path.Combine(DataRepositoryPath, "Settings", "manifest.cnx")))
                {
                    IRegion ActionRegion = _regionManager.Regions[RegionNames.ActionRegion];
                    if (ActionRegion.Views.OfType<DataImporteView>().Count() > 0)
                    {
                        this.View = ActionRegion.Views.OfType<DataImporteView>().First();
                        ActionRegion.Remove(this.View);
                    }
                    IRegion SecondryRegion = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.SecondaryRegion];
                    IRegionManager detailsRegionManager = SecondryRegion.Add(this.View, null, true);
                    SecondryRegion.Activate(this.View);
                }
                else
                {
                    _eventAggrigator.GetEvent<DataImportComplted>().Publish(new DirectoryInfo(DataRepositoryPath));
                }
            }
        }
        private void RaisePropertyChanged(string caller)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        string _displayMessage;
        public string DisplayMessage
        {
            get
            {
                return _displayMessage;
            }
            set
            {
                _displayMessage = value;
                RaisePropertyChanged("DisplayMessage");
            }
        }
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool parallel)
        {
            Message = "Processing" + new DirectoryInfo(sourceDirName).Name;
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                DisplayMessage = "Processing" + dir.Parent.Name + @"\" + dir.Name;
            }
            // Get the files in the directory and copy them to the new location.
            var files = dir.EnumerateFiles();
            Parallel.ForEach(files, newPath =>
            {
                string temppath = Path.Combine(destDirName, newPath.Name);
                newPath.CopyTo(temppath, false);
            });
            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                Parallel.ForEach(dirs, newPath =>
                {
                    string temppath = Path.Combine(destDirName, newPath.Name);
                    DirectoryCopy(newPath.FullName, temppath, copySubDirs);
                });
            }
        }
        private  void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            Message = "Processing data for"+new DirectoryInfo(sourceDirName).Name;
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
                File.SetAttributes(temppath, File.GetAttributes(temppath) & ~FileAttributes.ReadOnly);
            }
            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
            DirectoryInfo d = new DirectoryInfo(destDirName);
            d.Attributes &= ~FileAttributes.ReadOnly;
        }
        private void ReadCategories()
        {
            CategoriesFile = new FileInfo(Path.Combine(DataImportPath, "Categories.cnx"));
            using (StreamReader SRCategory = new StreamReader(CategoriesFile.FullName))
            {
                XmlSerializer xmlser = new XmlSerializer(typeof(List<Category>));
                CategoriesList = (List<Category>)xmlser.Deserialize(SRCategory);
            }
        }
    }
    class EjectMedia
    {
        // Constants used in DLL methods
        const uint GENERICREAD = 0x80000000;
        const uint OPENEXISTING = 3;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 2967560;
        const int INVALID_HANDLE = -1;
        // File Handle
        private static IntPtr fileHandle;
        private static uint returnedBytes;
        // Use Kernel32 via interop to access required methods
        // Get a File Handle
        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr CreateFile(string fileName,
        uint desiredAccess,
        uint shareMode,
        IntPtr attributes,
        uint creationDisposition,
        uint flagsAndAttributes,
        IntPtr templateFile);
        [DllImport("kernel32", SetLastError = true)]
        static extern int CloseHandle(IntPtr driveHandle);
        [DllImport("kernel32", SetLastError = true)]
        static extern bool DeviceIoControl(IntPtr driveHandle,
        uint IoControlCode,
        IntPtr lpInBuffer,
        uint inBufferSize,
        IntPtr lpOutBuffer,
        uint outBufferSize,
        ref uint lpBytesReturned,
                 IntPtr lpOverlapped);
        public static void Eject(string driveLetter)
        {
            try
            {
                // Create an handle to the drive
                fileHandle = CreateFile(driveLetter,
                 GENERICREAD,
                 0,
                 IntPtr.Zero,
                 OPENEXISTING,
                 0,
                  IntPtr.Zero);
                if ((int)fileHandle != INVALID_HANDLE)
                {
                    // Eject the disk
                    DeviceIoControl(fileHandle,
                     IOCTL_STORAGE_EJECT_MEDIA,
                     IntPtr.Zero, 0,
                     IntPtr.Zero, 0,
                     ref returnedBytes,
                            IntPtr.Zero);
                }
            }
            catch
            {
                throw new Exception(Marshal.GetLastWin32Error().ToString());
            }
            finally
            {
                // Close Drive Handle
                CloseHandle(fileHandle);
                fileHandle = IntPtr.Zero;
            }
        }
    }
}
