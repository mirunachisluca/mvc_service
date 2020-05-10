using service_auto_web.DAL;
using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.Services
{
    public class ExportFactory
    {
    
        public static IExporter GetExporter(string format, IEnumerable<Appointment> content)
        {
            if (format.Equals("csv"))
            {
                return new CsvExporter("application/csv", "appointments.csv", content);
            }
            else if (format.Equals("json"))
            {
               return new JsonExporter("application/json", "appointments.json", content);
            }

            return null;
        }
    }
}
