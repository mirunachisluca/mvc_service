using service_auto_web.DAL;
using service_auto_web.Data;
using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.Services
{
    public class AppointmentService
    {
        private readonly ApplicationDbContext _context;
        private UnitOfWork unitOfWork;
        private ExportFactory exporter;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(context);
        }
        
        public void addAppointment(Appointment appointment)
        {
            unitOfWork.AppointmentRepository.Insert(appointment);
            unitOfWork.Save();
        }

        public IEnumerable<Appointment> listAppointments()
        {
            IEnumerable<Appointment> appointments = unitOfWork.AppointmentRepository.Get();
            return appointments;
        }

        public IEnumerable<Appointment> getByNameAppointments(string name)
        {
            IEnumerable<Appointment> appointments = unitOfWork.AppointmentRepository.Get(appointment => appointment.Client == name);
            return appointments;
        }

        public IEnumerable<Appointment> getByDate(string date)
        {
            DateTime date1 = DateTime.Parse(date);
            date1 = date1.Date;
            IEnumerable<Appointment> appointments = unitOfWork.AppointmentRepository.Get(appointment => appointment.Date.Date.Equals(date1));
            return appointments;
        }

        public IEnumerable<Appointment> getBetweenDates(string from, string to)
        {
            DateTime date1 = DateTime.Parse(from);
            date1 = date1.Date;
            DateTime date2 = DateTime.Parse(to);
            date2 = date2.Date;
            IEnumerable<Appointment> appointments = unitOfWork.AppointmentRepository.Get(appointment => appointment.Date >= date1 && appointment.Date <= date2);
            return appointments;
        }

        public Appointment GetByID(int? id)
        {
            return unitOfWork.AppointmentRepository.GetByID(id);
        }

        public void Update(int? id)
        {
            Appointment app = unitOfWork.AppointmentRepository.GetByID(id);
            
        }

        public void Update(Appointment appointment)
        {
            unitOfWork.AppointmentRepository.Update(appointment);
            unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            unitOfWork.AppointmentRepository.Delete(GetByID(id));
            unitOfWork.Save();
        }

        public bool appointmentExists(int id)
        {
            if (GetByID(id) != null)
                return true;
            else
                return false;
        }

        public IEnumerable<Appointment> getByDate(DateTime date)
        {
            return unitOfWork.AppointmentRepository.Get(app => app.Date.Equals(date));
        }

        public bool AppointmentExists(DateTime date)
        {
            IEnumerable<Appointment> app = getByDate(date);
            if (app.ToList().Count != 0)
                return false;
            else
                return true;
        }

        //public byte[]  export(String format)
        //{
        //    exporter = new ExportFactory(format);
        //    return exporter.export(listAppointments());
        //}


    }
}
