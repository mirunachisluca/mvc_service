using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.Services
{
    public interface IExporter
    {
        //public byte[] export(IEnumerable<Appointment> content);

        public string GetContentType();
        public string GetFileDownloadName();
        public byte[] GetExportBytes();
    }
}
