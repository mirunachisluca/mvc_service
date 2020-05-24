using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using service_auto_web.Models;

namespace service_auto_web.Services
{
    public class JsonExporter : Exporter
    {

        public JsonExporter(string contentType, string fileDownloadName, IEnumerable<Appointment> content) : base(contentType, fileDownloadName, content)
        {

        }

        protected override void export(IEnumerable<Appointment> content)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(content, options);

            _exportBytes = System.Text.Encoding.UTF8.GetBytes(json);

        }

    }
}
