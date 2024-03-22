using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            return View();
        }


#region File System Upload functions

        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, int? id, string title, 
            string description, DateTime dateTime, int severity)
        {
            foreach (var file in files)
            {
                //Get the base Path, i.e, The Current Directory of the application + /Files/.
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");

                //Checks if the base path directory exists, else creates it.
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                //Gets the file name without the extension.
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //Combines the base path with the file name.
                var filePath = Path.Combine(basePath, file.FileName);
                //If the file doesnt exist in the generated path, we use a filestream object, and create a new file, and then copy the contents to it.
                var extension = Path.GetExtension(file.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    //Create a new Problem object with required values.
                    var problem = await _context.Problems.FindAsync(id);
                    problem = new Problem
                    {
                        ProblemFileAttachments = filePath,
                        ProblemTitle = title,
                        ProblemDescription = description,
                        ProblemStartDate = dateTime,
                        ProblemSeverity = severity
                    };
                    //Inserts this model to the db via the context instance of EF Core.
                    _context.Problems.Add(problem);
                    _context.SaveChanges();
                }
            }
            //Loads all the File data to an object and sets a message in the TempData.
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {

            var file = await _context.Problems.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(file.ProblemFileAttachments, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, file.ProblemFileAttachments);
        }
        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {

            var file = await _context.Problems.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (file == null) return null;
            if (System.IO.File.Exists(file.ProblemFileAttachments))
            {
                System.IO.File.Delete(file.ProblemFileAttachments);
            }
            _context.Remove(file);
            _context.SaveChanges();
            TempData["Message"] = $"Removed {file.ProblemFileAttachments} successfully from File System.";
            return RedirectToAction("Index");
        }
#endregion File System Upload functions

        public IActionResult GetAllProblems()
        {
            return Json(_context.Problems.ToList());
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProblemTitle,ProblemDescription,ProblemStartDate,ProblemFileAttachments,ProblemSeverity,ProblemComplete")] Problem problem, ProblemInputModel probInputModel)
        {      
            //Used for file attachment upload.

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
                    //Upload or update any attachments user inserted. 
                    await UploadToFileSystem(probInputModel.ProblemFileAttachments ,problem.ID, problem.ProblemTitle, problem.ProblemDescription,
                        problem.ProblemStartDate, problem.ProblemSeverity);
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
