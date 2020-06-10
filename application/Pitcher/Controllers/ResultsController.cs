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
    public class ResultsController : Controller
    {
        private readonly TeamContext _context;

        public ResultsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["JobTitleSortParam"] = sortOrder == "jobTitle" ? "jobTitle_desc" : "jobTitle";
            ViewData["ProblemTitleSortParam"] = sortOrder == "problemTitle" ? "jobTitle_desc" : "problemTitle";
            ViewData["CurrentFilter"] = searchString;
            IQueryable <Result> results = _context.Results.Include(r => r.Job).Include(r => r.Problem);
            
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
                results = results.Where(r => r.Job.ToString().Contains(searchString)
                                    || r.Problem.ToString().Contains(searchString));
            }
            
           switch (sortOrder)
            {
                case "problemTitle":
                    results = results.OrderBy(r => r.Problem);
                    break;
                case "problemTitle_desc":
                    results = results.OrderByDescending(r => r.Problem);
                    break;
                case "jobTitle_desc":
                    results = results.OrderByDescending(r => r.Job);
                    break;
                //By default FullName is in ascending order when entity is loaded. 
                default:
                    results = results.OrderBy(r => r.Job);
                    break;   
                    
            }
                int pageSize = 20;
                return View(await  PaginatedList<Result>.CreateAsync(results.AsNoTracking(), pageNumber ?? 1, pageSize));   
        }

        // GET: Results/Details/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Job)
                .Include(r => r.Problem)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle");
            ViewData["ProblemID"] = new SelectList(_context.Problems, "ID", "ProblemTitle");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobID,ProblemID")] Result result)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(result);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", result.JobID);
                ViewData["ProblemID"] = new SelectList(_context.Problems, "ID", "ProblemTitle", result.ProblemID);
            }
            catch (DbUpdateException /* ex */)
            {                
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", result.JobID);
            ViewData["ProblemID"] = new SelectList(_context.Problems, "ID", "ProblemTitle", result.ProblemID);
            return View(result);
        }

        // POST: Results/Edit/5
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

            var resultToUpdate = await _context.Results.FirstOrDefaultAsync(u => u.ID == id);
            if (await TryUpdateModelAsync<Result>(
                resultToUpdate,
                //Empty string is a prefix for form field names.
                "",
                r => r.JobID, r => r.ProblemID))
            {
                try
                {
                    await _context.SaveChangesAsync();                    
                    ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "JobTitle", resultToUpdate.JobID);
                    ViewData["ProblemID"] = new SelectList(_context.Problems, "ID", "ProblemTitle", resultToUpdate.ProblemID);
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
            return View(resultToUpdate);  
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .AsNoTracking()
                .Include(r => r.Job)
                .Include(r => r.Problem)
                .FirstOrDefaultAsync(m => m.JobID == id);
            if (result == null)
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if(result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {

                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new {id = id, saveChangesError = true});
            }
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ID == id);
        }
    }
}
