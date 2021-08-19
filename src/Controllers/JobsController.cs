using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pitcher.Data;
using Pitcher.Models;
using Pitcher.Models.TeamViewModels;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Pitcher.Controllers
{
    public class JobsController : Controller
    {
        private readonly TeamContext _context;

        public JobsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Jobs
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS. Enables sorting. 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllJobs()
        {
            return Json(_context.Jobs.ToList());
        }
        
        public IActionResult GetAllProblemByJobId(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }            
            var problemlist = _context.Results.Where(r => r.JobID == ID).Select(r => r.Problem).ToList();
            return Json(problemlist);
        }

        public IActionResult GetAllUsersByJobId(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }                    
            var userlist = _context.Registrations.Where(r => r.JobID == ID).Select(r => r.User).ToList();
            return Json(userlist);
        }
        
#region User Registrations
        /// <summary>
        /// Creates a Registration to assign user to a currently displayed job.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="JobID"></param>
        /// <returns>The newly assigned user registration</returns>
        [HttpGet]
        public async Task<IActionResult> AssignUserRegistration(int UserID, int JobID)
        {
            Registration registration = new Registration{UserID = UserID, JobID = JobID, RegistrationDate = DateTime.Now};
            _context.Add(registration);
            await _context.SaveChangesAsync();
            //ID is JobID. Now refresh the page by redirecting to it again.
            return RedirectToAction(nameof(UserAssignments), new{ID = JobID});
        }

        /// <summary>
        /// Action that displays table showing unassigned users
        /// to currently selected job.
        /// </summary>
        /// <returns>A list of unassigned users for currently selected Job</returns>
        public IActionResult GetUnassignedUsers()
        {
            _context.Jobs.OrderByDescending(j => j.ID).FirstOrDefault();       
            var userlist = _context.Users.Where(u => !u.Registrations.Any());
            return Json(userlist);
        }

        /// <summary>
        /// Action that displays table showing assigned users
        /// </summary>
        /// <returns>A list of users assigned to currently selected Job</returns>
        public IActionResult GetAssignedUsers()
        {
            try
            {
                _context.Jobs.OrderByDescending(j => j.ID).FirstOrDefault();       
                
            }
            
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log).
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "See your system administrator.");
            }
            var userlist = _context.Users.Where(u => u.Registrations.Any());
            return Json(userlist);
        }

        //TO DO: Fix method below.

        /// <summary>
        /// NullReferenceException: Object reference not set to an instance of an object.
        /// This method unassign the registration in memory for the currently selected Job.
        /// </summary>
        /// <param name="RegistrationID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UnassignUserRegistration(int RegistrationID)
        {
            var user = _context.Users.Include(c => c.Registrations).FirstOrDefault(c => c.ID == RegistrationID);
            foreach (var item in user.Registrations)
            {
                if (user!=null)
                {
                     _context.Entry(item).State = EntityState.Deleted; //delete items from the Join table(registrations).  
                                 //_context.Entry(user).State = EntityState.Deleted; //delete the user //
                    await _context.SaveChangesAsync();             
                }               
            }
            return RedirectToAction(nameof(UserAssignments), new{ID = RegistrationID});
        }

        /// <summary>
        /// Opens up the UserAssignments view page, using the
        /// currently selected JobID in memory.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The currently selected Job in memory</returns>
        public IActionResult UserAssignments(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var job = _context.Jobs.Find(ID);
            return View(job);
        }
#endregion User Registrations

        // GET: Jobs/Details/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.  
        public IActionResult Details(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var job = _context.Jobs.Find(ID);
            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobTitle,JobDescription,JobStartDate,JobDeadline,JobIsComplete")] Job job)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    _context.Add(job);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index));
                }
                    ViewData["Jobs"] = _context.Jobs.ToList();
            }
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log).
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "See your system administrator.");
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
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

            var jobToUpdate = await _context.Jobs.FirstOrDefaultAsync(j => j.ID == id);
            if (await TryUpdateModelAsync<Job>(
                jobToUpdate,
                "",
                j => j.JobTitle, j => j.JobDescription, j => j.JobStartDate, j => j.JobDeadline, j => j.JobIsComplete)) 
            
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobToUpdate);
        }

        // GET: Jobs/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        // Catches delete failures and allows for retry.
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }
            
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(job);
        }

        // POST: Registrations/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        //Catches delete failures and allows for retry.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if(job == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new {id = id, saveChangesError = true});
            }
        }
        
        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.ID == id);
        }
    }
}
