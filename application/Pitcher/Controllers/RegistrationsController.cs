using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pitcher.Data;
using Pitcher.Models;

namespace Pitcher.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly TeamContext _context;

        public RegistrationsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Registrations
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS BOUND TO COMPOSITE TABLES. Enables sorting.
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["FullNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "fullName_desc" : "";
            ViewData["JobTitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "jobTitle_desc" : "";
            ViewData["JobTitleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "jobTitle" : "";
            ViewData["RegDateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            IQueryable <Registration> registrations = _context.Registrations.Include(r => r.Job).Include(r => r.User);
            if (!String.IsNullOrEmpty(searchString))
            {
                registrations = registrations.Where(r => r.User.ToString().Contains(searchString)
                                    || r.Job.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fullName_desc":
                    registrations = registrations.OrderByDescending(r => r.User);
                    break;
                case "jobTitle_desc":
                    registrations = registrations.OrderByDescending(r => r.Job);
                    break;
                case "jobTitle":
                registrations = registrations.OrderBy(r => r.Job);
                    break;
                case "Date":
                registrations = registrations.OrderBy(r => r.RegistrationDate);
                    break;
                case "date_desc":
                registrations = registrations.OrderByDescending(r => r.RegistrationDate);
                    break;
                //By default FullName is in ascending order when entity is loaded. 
                default:
                    registrations = registrations.OrderBy(r => r.User);
                    break;
            }
            
            return View(await registrations.ToListAsync());
        }

        // GET: Registrations/Details/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Job)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserFullname", null);
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,JobID,RegistrationDate")] Registration registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", registration.JobID);
                ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserFullname", registration.UserID);
            }
            catch (DbUpdateException /* ex */)
            {                
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", registration.JobID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserFullname", registration.UserID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS. Prevents overposting.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrationToUpdate = await _context.Registrations.FirstOrDefaultAsync(u => u.ID == id);
            if(await TryUpdateModelAsync<Registration>(
                registrationToUpdate,
                //Empty string is a prefix for form field names.
                "",
                r => r.UserID, u => u.JobID, u => u.RegistrationDate))
            {
                try
                {                    
                    await _context.SaveChangesAsync();
                    ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", registrationToUpdate.JobID);
                    ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserFullname", registrationToUpdate.UserID);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }            
            return View(registrationToUpdate);       
        }

        // GET: Registrations/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        // Catches delete failures and allows for retry.
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .AsNoTracking()
                .Include(r => r.Job)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (registration == null)
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        //Catches delete failures and allows for retry.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if(registration == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new {id = id, saveChangesError = true});
            }
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.ID == id);
        }
    }
}
