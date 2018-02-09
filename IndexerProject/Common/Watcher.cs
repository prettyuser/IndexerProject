using IndexerProject.Indexers;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static IndexerProject.Common.FileWatcher;

namespace IndexerProject.Common
{
    public class Watcher : INotifyPropertyChanged
    {
        #region Fields

        IDisposable fileWatcher = null;

        private string _currentDirectoryName = null;
        private string _watchDirectoryName = null;
        public object synchro = new object();
        IKernel kernel = null;

        #endregion Fields

        #region FieldsEvents

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyPathChanged;

        #endregion FieldsEvents

        #region Properties

        public List<string> ListOfFiles { get; set; }

        public string WatchDirectoryName
        {
            get
            {
                return _watchDirectoryName;
            }
            set
            {
                SetField(ref _watchDirectoryName, value, "WatchDirectoryName");
            } 
        }
        
        public string CurrentDirectoryName
        {
            get
            {
                return _currentDirectoryName;
            }

            set
            {
                SetField(ref _currentDirectoryName, value, "CurrentDirectoryName");
            }
        }

        #endregion Properties

        #region NotifyPropertyChangedHandlers

        protected virtual void OnWatcherPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPathPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyPathChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            //if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;

            if (propertyName == "WatchDirectoryName")
                OnPathPropertyChanged(propertyName);
            else
            {
                OnWatcherPropertyChanged(propertyName);
            }

            return true;
        }

        #endregion NotifyPropertyChangedHandlers

        #region Constructor

        public Watcher()
        {

        }

        #endregion Constructor

        #region Methods

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void RunReactive(IKernel kernel)
        {
            this.kernel = kernel;

            var pathToWatch = WatchDirectoryName;

            ListOfFiles = new List<string>();

            kernel.Get<RecursivelyIndexing>(new ConstructorArgument("kernel", kernel)).DeleteIndexing(pathToWatch, true);
            kernel.Get<RecursivelyIndexing>(new ConstructorArgument("kernel", kernel)).DirectoryIndexing(pathToWatch, true);

            fileWatcher =
            FileWatcher.ObserveFolderChanges(pathToWatch, "*.*", TimeSpan.FromSeconds(0.1))
                        .Subscribe(fce => OnChangedReactivePlus(fce), e => Console.WriteLine("____" + e));
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void StopReactive()
        {
            ListOfFiles = null;
            FileWatcher.FileSystemWatcherProp.Dispose();
            FileWatcher.FileSystemWatcherProp = null;
            fileWatcher.Dispose();
            fileWatcher = null;
        }

        #endregion Methods

        #region Handlers

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void OnChangedReactivePlus(FileChangedEvent fce)
        {
            if (fce != null && fce.FullPath!=null)
            {
                if(synchro == null)
                {
                    lock (synchro)
                    {
                        CurrentDirectoryName = fce.Reason + ": " + fce.FullPath;
                        ListOfFiles.Add(CurrentDirectoryName);
                    }
                }
                
                if (fce.Reason == "Deleted")
                {
                    FileWatcher.FileSystemWatcherProp.EnableRaisingEvents = false;

                    if (!fce.FullPath.Contains("~index") && Directory.Exists(Path.GetDirectoryName(fce.FullPath)))
                    {
                        var fileName = Path.GetFileName(fce.FullPath);
                        var directoryPath = Path.GetDirectoryName(fce.FullPath);
                        DirectoryInfo dir = new DirectoryInfo(directoryPath);

                        FileInfo file = dir.GetFiles().FirstOrDefault(f => f.FullName.Contains(fileName));

                        if (file != null && file.Extension.Contains("~index"))
                        {
                            file.Delete();
                        }
                        
                    }
                    else if (fce.FullPath.Contains("~index") && File.Exists(fce.FullPath.Replace("~index", "")))
                    {
                        FileInfo oFileInfo = new FileInfo(fce.FullPath.Replace("~index", ""));

                        using (FileStream fs = File.Create(oFileInfo.DirectoryName + "\\" + $"{oFileInfo.Name}~index"))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(kernel.Get<IndexerGetter>().GetIndexer(oFileInfo.Extension).CreateCustomDescriptionInfo(oFileInfo.FullName).ToString());
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                            
                        }
                    }
                    FileWatcher.FileSystemWatcherProp.EnableRaisingEvents = true;
                }

                else if (File.Exists(fce.FullPath) && (fce.Reason == "Created" || fce.Reason == "Changed" || fce.Reason == "Renamed"))
                {
                    FileWatcher.FileSystemWatcherProp.EnableRaisingEvents = false;

                    FileInfo oFileInfo = new FileInfo(fce.FullPath);

                    if (fce.Reason == "Renamed")
                    {
                        foreach (var filePath in Directory.GetFiles(oFileInfo.DirectoryName))
                        {
                            if (filePath.Contains(fce.RenEvArgs.OldFullPath) && filePath.Contains("~index"))
                                File.Delete(filePath);
                        }
                    }

                    if(!fce.FullPath.Contains("~index"))
                        using (FileStream fs = File.Create(oFileInfo.DirectoryName + "\\" + $"{oFileInfo.Name}~index"))
                        {
                            Byte[] info = new UTF8Encoding(true).GetBytes(kernel.Get<IndexerGetter>().GetIndexer(oFileInfo.Extension).CreateCustomDescriptionInfo(oFileInfo.FullName).ToString());
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    else if (fce.FullPath.Contains("~index") && !File.Exists(fce.FullPath.Replace("~index", "")))
                    {
                        File.Delete(fce.FullPath);
                    }

                    FileWatcher.FileSystemWatcherProp.EnableRaisingEvents = true;
                }
            } 
        }

        #endregion Handlers

        #region IDisposable

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (rwlock != null)
        //        {
        //            rwlock.Dispose();
        //            rwlock = null;
        //        }
        //        if (watcher != null)
        //        {
        //            watcher.EnableRaisingEvents = false;
        //            watcher.Dispose();
        //            watcher = null;
        //        }
        //    }
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        #endregion IDisposable
    }
}
