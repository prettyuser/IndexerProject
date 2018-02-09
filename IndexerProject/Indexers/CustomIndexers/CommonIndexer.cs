using IndexerProject.Indexers.AbstractIndexers;
using IndexerProject.Indexers.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Indexers.CustomIndexers
{
    public class CommonIndexer : AbstractIndexer
    {
        public override string ExceptionInfo { get; set; }
       
        public override JObject CreateCustomDescriptionInfo(string path)
        {
            try
            {
                //TODO: Реализовать свой маппер
                var commonInfoObj = (JObject)JToken.FromObject(
                                    new UnknownFileDto()
                                    {
                                        FileType = this.ToString()
                                    });

                var baseObject = base.CreateBaseDescriptionInfo(path);

                commonInfoObj.Merge(baseObject);

                return commonInfoObj;
            }
            catch (Exception ex)
            {
                ExceptionInfo = ex.ToString();
                return (JObject)JToken.FromObject(ExceptionInfo);
            }
        }

        public override string ToString()
        {
            return "Неопределённый файл";
        }
    }
}
