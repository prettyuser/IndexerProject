using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IndexerProject.Indexers.DTO
{
    [DataContract]
    public class UnknownFileDto : IFileDto
    {
        public string FileType { get; set; }
    }
}
