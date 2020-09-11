using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pitcher.Data;
using Pitcher.Models;
using DataTables;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace Pitcher.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly TeamContext _context;

        private readonly IConfiguration _config;

        //Expose connection string in appsettings.json for DataTables Editor libraries.
        public RegistrationsController(TeamContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }      

        
        public IActionResult Index()
        {   
            return View();                 
        }
        
        //[Produces("application/json")]
        [HttpPost()]
        [HttpGet()]
        public ActionResult LeftJoinJobsAndUsersOntoRegistrations()
        {
            //DECLARE database connection.
            string connectionString = _config.GetConnectionString("DefaultConnection");            
            
            //CREATE datatabe instance.
            using (var db = new Database("sqlserver", connectionString))
            {
                //CREATE Editor instance with starting table.
                var response = new Editor(db, "tblRegistration")
                    //GET model of tblRegistration.
                    .Field(new Field("tblRegistration.RegistrationDate"))
                    //GET model of tblJob.
                    .Field(new Field("tblJob.JobTitle"))
                    //GET model of tblUser.
                    // .Field(new Field("tblUser.JobTitle"))
                    // //PRESENT selectable value list of JobTitles .
                     .Field(new Field("tblRegistration.JobID")
                         .Options("tblJob", "ID", "JobTitle")
                    //     .Validator(Validation.DbValues(new ValidationOpts {Empty = false}))
                    //     // .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                    //     // .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                     )
                    //JOIN from tblRegistration column JobID linked from tblJob column ID.   
                    .LeftJoin( "tblJob ", " tblJob.ID ", "=", " tblRegistration.JobID")
                    .Process(HttpContext.Request)
                    .Data();
                return Json(response);
            }        
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
