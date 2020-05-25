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
    public class ProblemsController : Controller
    {
        private readonly TeamContext _context;

        public ProblemsController(TeamContext context)
        {
            _context = context;
        }

        // GET: Problems
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProblemIDSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ProblemID_desc" : "";
            ViewData["ProblemTitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ProblemTitle_desc" : "";
            ViewData["ProblemStartDateSortParm"] = sortOrder == "ProblemStartDate" ? "ProblemStartDate_desc" : "ProblemStartDate";
            ViewData["ProblemSeveritySortParm"] = sortOrder == "ProblemSeverity" ? "ProblemSeverity_desc" : "ProblemSeverity";
            ViewData["ProblemCompleteSortParm"] = sortOrder == "ProblemComplete" ? "ProblemComplete_desc" : "ProblemComplete";            
            ViewData["CurrentFilter"] = searchString;
            var problems = from p in _context.Problems
                            //.Include(p => p.Result)
                            select p;
                        if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if(!String.IsNullOrEmpty(searchString))
            {
                problems = problems.Where(p => p.ProblemTitle.Contains(searchString)
                                            || p.ProblemDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ProblemID_desc":
                    problems = problems.OrderByDescending(p => p.ID);
                    break;
                case "ProblemTitle_desc":
                    problems = problems.OrderByDescending(p => p.ProblemTitle);
                    break;
                case "ProblemStartDate":
                    problems = problems.OrderBy(p => p.ProblemStartDate);                    
                    break;
                case "ProblemStartDate_desc":
                    problems = problems.OrderBy(p => p.ProblemStartDate);                    
                    break;
                case "ProblemSeverity":
                    problems = problems.OrderBy(p => p.ProblemSeverity);
                    break;
                case "ProblemSeverity_desc":
                    problems = problems.OrderByDescending(p => p.ProblemSeverity);
                    break;   
                case "ProblemCompleteSortParm":
                    problems = problems.OrderBy(p => p.ProblemComplete);
                    break;
                case "ProblemCompleteSortParm_desc":
                    problems = problems.OrderByDescending(p => p.ProblemComplete);
                    break; 
                default:
                    problems = problems.OrderBy(p => p.ProblemTitle);
                    break;                 
            }

            int pageSize = 20;            
            return View(await PaginatedList<Problem>.CreateAsync(problems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Problems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProblemTitle,ProblemDescription,ProblemStartDate,ProblemFileAttachments,ProblemSeverity,ProblemComplete")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problem);
        }

        // GET: Problems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            return View(problem);
        }

        // POST: Problems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProblemTitle,ProblemDescription,ProblemStartDate,ProblemFileAttachments,ProblemSeverity,ProblemComplete")] Problem problem)
        {
            if (id != problem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.ID))
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
            return View(problem);
        }

        // GET: Problems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.ID == id);
        }
    }
}
