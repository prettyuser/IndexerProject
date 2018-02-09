using IndexerProject.Indexers;
using IndexerProject.Indexers.CustomIndexers;
using IndexerProject.Util;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Common
{
    public class RecursivelyIndexing
    {
        IKernel kernel;

        public RecursivelyIndexing(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public void DirectoryIndexing(string sourceDirName, bool indexingSubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                FileInfo oFileInfo = new FileInfo(file.FullName);

                using (FileStream fs = File.Create(oFileInfo.DirectoryName + "\\" + $"{oFileInfo.Name}~index"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(kernel.Get<IndexerGetter>().GetIndexer(file.Extension).CreateCustomDescriptionInfo(file.FullName).ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (indexingSubDirs)
            {
                foreach (DirectoryInfo subdir in dirs.Where(ss => !ss.Extension.Contains("~index")))
                {
                    DirectoryIndexing(subdir.FullName, indexingSubDirs);
                }
            }
        }

        public void DeleteIndexing(string sourceDirName, bool indexingSubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Extension.Contains("~index"))
                {
                    file.Delete();
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (indexingSubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    DeleteIndexing(subdir.FullName, indexingSubDirs);
                }
            }
        }
    }
}
