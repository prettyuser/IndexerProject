using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Indexers.DTO
{
    [DataContract]
    public class ImageFileDto : IFileDto
    {
        public string FileType { get; set; }

        [DataMember(Name = "Dimensions (W x H), px:")]
        public string Dimensions { get; set; }

        [DataMember(Name = "DPI (X x Y):")]
        public string DPI { get; set; }
    }
}
