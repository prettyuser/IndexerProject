using AutoMapper;
using IndexerProject.Indexers.DTO;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace IndexerProject.Indexers.AbstractIndexers
{
    public abstract class AbstractIndexer
    {
        public abstract string ExceptionInfo { get; set; }

        internal virtual JObject CreateBaseDescriptionInfo(string path)
        {
            try
            {
                var oFileInfo = new FileInfo(path);

                //Comment: stuck
                //Mapper.Initialize(cfg => cfg.CreateMap<FileInfo, CommonFileDto>());
                //var commonFileInfoObject = Mapper.Map<FileInfo, CommonFileDto>(oFileInfo);

                //TODO: Реализовать свой маппер
                var commonFileInfoObject = new FileDto()
                {
                    Name = oFileInfo.Name,
                    FullName = oFileInfo.FullName,
                    Extension = oFileInfo.Extension,
                    Length = oFileInfo.Length,
                    CreationTime = oFileInfo.CreationTime,
                    LastWriteTime = oFileInfo.LastWriteTime,
                    LastAccessTime = oFileInfo.LastAccessTime,
                    IsReadOnly = oFileInfo.IsReadOnly,
                };

                return (JObject)JToken.FromObject(commonFileInfoObject);
            }
            catch (Exception ex)
            {
                ExceptionInfo = ex.ToString();
                return (JObject)JToken.FromObject(ExceptionInfo);
            }
            
        }

        public abstract JObject CreateCustomDescriptionInfo(string path);
    }
}
