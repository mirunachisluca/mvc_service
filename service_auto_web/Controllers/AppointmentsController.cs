﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using service_auto_web.Data;
using service_auto_web.Models;
using service_auto_web.Services;

namespace service_auto_web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private AppointmentService appService;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
            appService = new AppointmentService(_context);
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string clientName, string date1, string date2)
        {
            if (!String.IsNullOrEmpty(clientName))
                return View(appService.getByNameAppointments(clientName));
            else
                if (!String.IsNullOrEmpty(date1) && String.IsNullOrEmpty(date2))
                return View(appService.getByDate(date1));
            else
                if (!String.IsNullOrEmpty(date1) && !String.IsNullOrEmpty(date2))
                return View(appService.getBetweenDates(date1, date2));
            else
                return View(appService.listAppointments().ToList());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View(appService.GetByID(id));
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,Client,Phone,Car,Problem,Status")] Appointment appointment)
        {
            try
            {
                if (ModelState.IsValid && AppointmentExists(appointment.Date))
                {
                    appService.addAppointment(appointment);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is already an apointment with this specific date!");
                }
            }
            catch (System.Data.DataException  /*dex*/ )
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = appService.GetByID(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Client,Phone,Car,Problem,Status")] Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appService.Update(appointment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = appService.GetByID(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            appService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return appService.appointmentExists(id);
        }

        private bool AppointmentExists(DateTime date)
        {
            IEnumerable<Appointment> app = appService.getByDate(date);
            if (app.ToList().Count != 0)
                return false;
            else 
                return true;
        }
    }
}