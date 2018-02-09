using IndexerProject.Indexers.AbstractIndexers;
using IndexerProject.Indexers.CustomIndexers;
using IndexerProject.Util;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Indexers
{
    public class IndexerGetter
    {
        IKernel kernel = null;

        public IndexerGetter(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public AbstractIndexer GetIndexer(string fileExtension)
        {

            switch (fileExtension)
            {
                case ".txt":
                    return kernel.Get<TXTIndexer>();

                case ".jpeg":
                case ".jpg":
                    return kernel.Get<JPEGIndexer>();

                default:
                    return kernel.Get<CommonIndexer>();
            }
        }
    }
}
