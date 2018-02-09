using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Common
{
    public class FileWatcher 
    {
        public static FileSystemWatcher FileSystemWatcherProp { get;set;}

        public class FileChangedEvent
        {
            public string FullPath { get; private set; }
            public bool IsFileDeleted { get; private set; }
            public string Reason { get; private set; }
            public RenamedEventArgs RenEvArgs { get; private set; }

            public FileChangedEvent(string path, string reason, bool isFileDeleted = false, RenamedEventArgs renEvArgs = null)
            {
                FullPath = path;
                IsFileDeleted = isFileDeleted;
                Reason = reason;
                RenEvArgs = renEvArgs;
            }
        }

        public static IObservable<FileChangedEvent> ObserveFolderChanges(string path, string filter, TimeSpan throttle)
        {
            FileSystemWatcherProp = new FileSystemWatcher(path, filter) { EnableRaisingEvents = true, IncludeSubdirectories = true };

            

            return Observable.Using(
            () => FileSystemWatcherProp,
            fileSystemWatcher => CreateSources(fileSystemWatcher)
                .Merge()
                .GroupBy(c => c.FullPath)
                .SelectMany(fileEvents => fileEvents
                    .Throttle(throttle)
                    .Where(e => !e.IsFileDeleted)));
        }

        private static IObservable<FileChangedEvent>[] CreateSources(FileSystemWatcher fileWatcher)
        {
            return new[]
            {
            Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs >(handler => fileWatcher.Created += handler, handler => fileWatcher.Created -= handler)
                        .Select(ev => new FileChangedEvent(ev.EventArgs.FullPath, ev.EventArgs.ChangeType.ToString())),

            Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs >(handler => fileWatcher.Deleted += handler, handler => fileWatcher.Deleted -= handler)
                        .Select(ev => new FileChangedEvent(ev.EventArgs.FullPath, ev.EventArgs.ChangeType.ToString())),

            Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs >(handler => fileWatcher.Changed += handler, handler => fileWatcher.Changed -= handler)
                        .Select(ev => new FileChangedEvent(ev.EventArgs.FullPath, ev.EventArgs.ChangeType.ToString())),

            //The rename source needs to send a delete event for the old file name.
            Observable.Create<FileChangedEvent>(nameChangedObserver =>
            {
                return Observable.FromEventPattern<RenamedEventHandler, RenamedEventArgs>(handler => fileWatcher.Renamed += handler, handler => fileWatcher.Renamed -= handler)
                    .Subscribe(ev =>
                    {
                        nameChangedObserver.OnNext(new FileChangedEvent(ev.EventArgs.FullPath, ev.EventArgs.ChangeType.ToString(), false, ev.EventArgs));
                        nameChangedObserver.OnNext(new FileChangedEvent(ev.EventArgs.OldFullPath, ev.EventArgs.ChangeType.ToString(), false, ev.EventArgs));
                    });
            }),

            Observable.FromEventPattern<ErrorEventHandler, ErrorEventArgs >(handler => fileWatcher.Error += handler, handler => fileWatcher.Error -= handler)
                        .SelectMany(ev => Observable.Throw<FileChangedEvent>(ev.EventArgs.GetException()))
        };
        }
    }
}
