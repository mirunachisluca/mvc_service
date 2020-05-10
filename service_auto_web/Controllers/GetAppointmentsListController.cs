using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using service_auto_web.Data;
using service_auto_web.Models;
using service_auto_web.Services;

namespace service_auto_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAppointmentsListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private AppointmentService appointmentService;

        public GetAppointmentsListController(ApplicationDbContext context)
        {
            _context = context;
            appointmentService = new AppointmentService(_context);
        }

        // GET: api/GetAppointmentsList
        [HttpGet]
        public string GetList()
        {
            var appointments = appointmentService.listAppointments();
            appointments = appointments.OrderBy(appointment => appointment.Date);
            string text = "";
            foreach (Appointment app in appointments)
            {
                string jsonString = JsonSerializer.Serialize(app);
                text += jsonString;
                text += "\n";
            }
            return text;
        }
    }
}
