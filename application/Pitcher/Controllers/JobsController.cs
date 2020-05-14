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
    public class JobsController : Controller
    {
        private readonly TeamContext _context;

        public JobsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Jobs
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS. Enables sorting. 
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["JobTitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "jobTitle_desc" : "";
            ViewData["JobStartDateSortParm"] = sortOrder == "JobStartDate" ? "JobStart_date_desc" : "JobStartDate";
            ViewData["JobDeadlineDateSortParm"] = sortOrder == "JobDeadlineDate" ? "JobDeadline_date_desc" : "JobDeadlineDate";
            ViewData["CurrentFilter"] = searchString;
            var jobs = from j in _context.Jobs
                        select j;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(j => j.JobTitle.Contains(searchString)
                                    || j.JobDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "JobTitle_desc":
                    jobs = jobs.OrderByDescending(j => j.JobTitle);
                    break;
                case "JobStartDate":
                    jobs = jobs.OrderBy(j => j.JobStartDate);
                    break;
                case "JobStart_date_desc":
                    jobs = jobs.OrderByDescending(j => j.JobStartDate);
                    break;
                case "JobDeadline_date_desc":
                    jobs = jobs.OrderByDescending(j => j.JobDeadline);
                    break;
                case "JobDeadlineDate":
                    jobs = jobs.OrderBy(j => j.JobDeadline);
                    break;
                //By default JobTitle is in ascending order when entity is loaded. 
                default:
                    jobs = jobs.OrderBy(j => j.JobTitle);
                    break;                    
            } 

            int pageSize = 20;
            return View(await PaginatedList<Job>.CreateAsync(jobs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Jobs/Details/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.  
        public async Task<IActionResult> Details(int? id)
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
                    return RedirectToAction(nameof(Index));
                }
                
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

        // POST: Jobs/Delete/5
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
                //SQL DELETE command is generated
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
