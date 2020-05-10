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
            string text = "";
            foreach (Appointment app in content)
            {
                string jsonString = JsonSerializer.Serialize(app);
                text += jsonString;
                text += "\n";
            }

            _exportBytes = System.Text.Encoding.UTF8.GetBytes(text);

        }

    }
}
