using AutoMapper;
using IndexerProject.Indexers.AbstractIndexers;
using IndexerProject.Indexers.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace IndexerProject.Indexers.CustomIndexers
{
    public class JPEGIndexer : AbstractIndexer
    {
        public override string ExceptionInfo { get; set; }

        public override JObject CreateCustomDescriptionInfo(string path)
        {
            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                BitmapSource img = BitmapFrame.Create(fs as FileStream);

                //TODO: Реализовать свой маппер
                var imgObject = (JObject)JToken.FromObject(new ImageFileDto()
                {
                    FileType = this.ToString(),
                    Dimensions = img.PixelWidth + " x " + img.PixelHeight,
                    DPI = img.DpiX + " x " + img.DpiY,
                });

                var baseObject = base.CreateBaseDescriptionInfo(path);

                imgObject.Merge(baseObject);

                fs.Close();
                fs.Dispose();
                return imgObject;
            }
            catch (Exception ex)
            {
                ExceptionInfo = ex.ToString();
                return (JObject)JToken.FromObject(ExceptionInfo);
            }
        }

        public override string ToString()
        {
            return "Графический файл";
        }
    }
}
