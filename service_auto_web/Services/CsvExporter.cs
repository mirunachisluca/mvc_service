using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.Text;


namespace service_auto_web.Services
{
    public class CsvExporter : Exporter
    {
        public CsvExporter(string contentType, string fileDownloadName, IEnumerable<Appointment> content) : base(contentType, fileDownloadName, content)
        {

        }

        protected override void export(IEnumerable<Appointment> content)
        {
            var text = CsvSerializer.SerializeToCsv(content);
            _exportBytes = System.Text.Encoding.UTF8.GetBytes(text);
        }
    }
}
