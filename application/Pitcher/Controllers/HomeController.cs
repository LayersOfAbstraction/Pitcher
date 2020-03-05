using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitcher.Models;
using Pitcher.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Pitcher.Models.TeamViewModels;
namespace Pitcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _context;

        public HomeController(TeamContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        //TO DO: GROUP UserCount by JobName. 
        public async Task<ActionResult> About()
        {
            IQueryable<ProjectTotalsGroup> data = 
                from jobs in _context.Jobs
                group jobs by jobs.JobTitle into projectGroup
                select new ProjectTotalsGroup()
                {
                    JobName = projectGroup.Key,
                    UserCount = projectGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());  
        }
    }
}
