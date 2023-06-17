using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitcher.Data;
using Pitcher.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Example.Auth0.AuthenticationApi.Services;


namespace Pitcher.Controllers
{

    public class UsersController : Controller
    {
        private readonly TeamContext _context;
        private readonly IUserService _userService;
        public IPagedList<Auth0.ManagementApi.Models.User> Users { get; private set; }

        public UsersController(TeamContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() {RedirectUri = returnUrl});
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the
                // **Allowed Logout URLs** settings for the app.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }
        
        [Authorize]        
        /// <summary>
        /// /// Displays user profile.
        /// </summary>
        /// <returns>Info about user who logged in</returns>
        public IActionResult Profile()
        {            
            return View(new AuthenticationServiceUserProfile()
            {
                UserName = User.Identity.Name,
                UserEmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                UserProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
    
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();            
        }

        public IActionResult GetAllUsers()
        {
            return Json(_context.Users.ToList());
        }

        public async Task <IActionResult> GetAllAuth0Users(CancellationToken cancellationToken)
        {
            // var apiClient = new ManagementApiClient(_Auth0Token.strAuthToken, new Uri ("https://dev-dgdfgfdgf324.au.auth0.com/api/v2/"));
            // var allUsers = await apiClient.Users.GetAllAsync(new Auth0.ManagementApi.Models.GetUsersRequest(), new Auth0.ManagementApi.Paging.PaginationInfo());
            var allUsers = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
            var renderedUsers = allUsers.Select(u => new Pitcher.Models.User
            {                
                UserFirstName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[0] : "no space",
                UserLastName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[1] : "no space",
                UserContactEmail = u.Email
            }).ToList();
            return Json(renderedUsers);
        }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Users = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
        }

        // GET: Users/Details/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.  
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                //Include navigational properties for Registrations and Jobs.
                .Include(u => u.Registrations)
                    .ThenInclude(r => r.Job)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserLastName,UserFirstName,UserIsLeader,UserContactEmail,UserPhoneNumber,UserAddress,UserPostCode,UserCountry,UserMobileNumber,UserState,UserLogInName,UserPassword")] Pitcher.Models.User user)
        {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
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

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (await TryUpdateModelAsync<Pitcher.Models.User>(
                userToUpdate,
                //Empty string is a prefix for form field names.
                "",
                //TOD DO: bind image to database.
                u => u.UserFirstName, u => u.UserLastName, u => u.UserFullname,
                u => u.UserContactEmail, u => u.UserPhoneNumber, u => u.UserAddress,
                u => u.UserPostCode, u => u.UserCountry, u => u.UserMobileNumber,
                u => u.UserState))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        // Catches delete failures and allows for retry.
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(user);
        }

        // POST: Users/Delete/5
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS.
        //Catches delete failures and allows for retry.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Users.Remove(user);
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
