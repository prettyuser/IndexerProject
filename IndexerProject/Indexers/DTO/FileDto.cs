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
    public class FileDto
    {
        [DataMember(Name = "File name:")]
        public string Name { get; set; }

        [DataMember(Name = "Full path:")]
        public string FullName { get; set; }

        [DataMember(Name = "File extension:")]
        public string Extension { get; set; }

        [DataMember(Name = "Creation time:")]
        public DateTime CreationTime { get; set; }

        [DataMember(Name = "Last access time:")]
        public DateTime LastAccessTime { get; set; }

        [DataMember(Name = "Last write time:")]
        public DateTime LastWriteTime { get; set; }

        [DataMember(Name = "Size, bytes:")]
        public long Length { get; set; }

        [DataMember(Name = "Is read-only:")]
        public bool IsReadOnly { get; set; }
    }
}
