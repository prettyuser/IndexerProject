using IndexerProject.Common;
using IndexerProject.Indexers;
using IndexerProject.Indexers.AbstractIndexers;
using IndexerProject.Indexers.CustomIndexers;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<Watcher>().ToSelf().InSingletonScope();
            Bind<IndexerGetter>().ToSelf().InSingletonScope();
            Bind<RecursivelyIndexing>().ToSelf().InSingletonScope();
            Bind<AbstractIndexer>().To<TXTIndexer>().InSingletonScope();
        }
    }
}
