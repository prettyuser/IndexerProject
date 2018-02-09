using IndexerProject.Indexers.AbstractIndexers;
using IndexerProject.Indexers.DTO;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.IO;
using System.Linq;

namespace IndexerProject.Indexers.CustomIndexers
{
    public class TXTIndexer : AbstractIndexer
    {
        public override string ExceptionInfo { get; set; }

        public override JObject CreateCustomDescriptionInfo(string path)
        {
            try
            {
                //TODO: Реализовать свой маппер
                var txtObjectInfo = (JObject)JToken.FromObject(new TxtDto()
                {
                    FileType = this.ToString(),
                    NumberChars = File.ReadAllText(path).Count(),
                    NumberWords = File.ReadAllText(path)
                                    .Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Count()
                });

                var baseObject = base.CreateBaseDescriptionInfo(path);

                txtObjectInfo.Merge(baseObject);

                return txtObjectInfo;
            }
            catch (Exception ex)
            {
                ExceptionInfo = ex.ToString();
                return (JObject)JToken.FromObject(ExceptionInfo);
            }
        }

        public override string ToString()
        {
            return "Текстовый файл";
        }
    }
}
