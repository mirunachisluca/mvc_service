using Microsoft.AspNetCore.Identity;
using service_auto_web.Data;
using service_auto_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_auto_web.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Appointment> appointmentRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public GenericRepository<Appointment> AppointmentRepository
        {
            get
            {

                if (this.appointmentRepository == null)
                {
                    this.appointmentRepository = new GenericRepository<Appointment>(_context);
                }
                return appointmentRepository;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
