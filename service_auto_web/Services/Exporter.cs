using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.Services
{
    public abstract class Exporter:IExporter
    {
        protected string _contentType;
        protected string _fileDownloadName;
        protected IEnumerable<Appointment> _content;
        protected byte[] _exportBytes;

        public Exporter(string contentType, string fileDownloadName, IEnumerable<Appointment> content)
        {
            _contentType = contentType;
            _fileDownloadName = fileDownloadName;
            _content = content;
            export(content);
        }

        public string GetContentType()
        {
            return _contentType;
        }

        public byte[] GetExportBytes()
        {
            return _exportBytes;
        }

        public string GetFileDownloadName()
        {
            return _fileDownloadName;
        }

        protected abstract void export(IEnumerable<Appointment> content);
    }
}
