using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitcher.Models;
using Pitcher.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pitcher.Models.TeamViewModels;
using System;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Pitcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _context;

        public HomeController(TeamContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
               string accessToken = await HttpContext.GetTokenAsync("access_token");
    
                // if you need to check the Access Token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                DateTime accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"), 
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
                    
                string idToken = await HttpContext.GetTokenAsync("id_token");

                // Now you can use them. For more info on when and how to use the
                // Access Token and ID Token, see https://auth0.com/docs/tokens
            }
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
        
        //TO DO: Group user count by Job title with current deadline and problem count.
        //GET Users.
        //Get Job Title
        //SELECT all users where ID = tblRegistration.JobID and tblProblem = Problem.ID and tblRegistration.Job.JobDeadline = tblJobs.JobID.
        //GROUP users by tblRegistration.Job.JobID.
        //COUNT Users.
        //DISPLAY Deadlines.
        //COUNT Problems.        
        public async Task<ActionResult> About()
        {
            IQueryable<ProjectTotalsGroup> data = 
                from registrations in _context.Registrations
                group registrations by registrations.Job.JobTitle /*+ registrations.Job.JobDeadline*/ into projectGroup
                select new ProjectTotalsGroup()
                {
                    JobName = projectGroup.Key,
                    UserCount = projectGroup.Count(),
                    //DeadlineDate = projectGroup,
                };
            return View(await data.AsNoTracking().ToListAsync());  
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
