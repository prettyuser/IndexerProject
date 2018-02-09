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
    public class TxtDto : IFileDto
    {
        public string FileType { get; set; }

        [DataMember(Name = "Number of words:")]
        public int NumberWords { get; set; }

        [DataMember(Name = "Number of chars:")]
        public int NumberChars { get; set; }
    }
}
