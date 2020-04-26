using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Phone { get; set; }
        public string Car { get; set; }
        public string Problem { get; set; }
        public bool Status { get; set; }
    }
}
