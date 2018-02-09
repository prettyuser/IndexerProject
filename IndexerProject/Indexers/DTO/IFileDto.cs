using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IndexerProject.Indexers.DTO
{
    public interface IFileDto
    {
        [DataMember(Name = "File type:")]
        string FileType { get; set; }
    }
}
