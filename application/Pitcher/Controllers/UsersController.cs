using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitcher.Data;
using Pitcher.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;


namespace Pitcher.Controllers
{
    public class UsersController : Controller
    {
        private readonly TeamContext _context;

        public UsersController(TeamContext context)
        {
            _context = context;
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

        public IActionResult AccessDenied()
        {
            return View();
        }

        
        [Authorize]        
        /// <summary>
        /// Displays user profile.
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

        // GET: Users
        // COPY AND PASTE THIS METHOD CUSTOMIZATION INTO OTHER CONTROLLERS. Enables sorting.        
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["LastNameSortParm"] = sortOrder == "UserLastName" ? "LastName_desc" : "UserLastName";
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewData["UserEmailSortParm"] = sortOrder == "UserContactEmail" ? "UserContactEmail_desc" : "UserContactEmail";
            ViewData["UserPhoneNumberParm"] = sortOrder == "UserPhoneNumber" ? "UserPhoneNumber_desc" : "UserPhoneNumber";
            ViewData["UserAddressSortParm"] = sortOrder == "UserAddress" ? "UserAddress_desc" : "UserAddress";
            ViewData["UserPostCodeSortParm"] = sortOrder == "UserPostCode" ? "UserPostCode_desc" : "UserPostCode";
            ViewData["UserCountrySortParm"] = sortOrder == "UserCountry" ? "UserCountry_desc" : "UserCountry";
            ViewData["UserMobileNumberSortParm"] = sortOrder == "UserMobileNumber" ? "UserMobileNumber_desc" : "UserMobileNumber";
            ViewData["UserStateSortParm"] = sortOrder == "UserState" ? "UserState_desc" : "UserState";
            ViewData["CurrentFilter"] = searchString;
            var users = from u in _context.Users
                        select u;

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
                users = users.Where(u => u.UserLastName.Contains(searchString)
                                    || u.UserFirstName.Contains(searchString)
                                    || u.UserContactEmail.Contains(searchString)
                                    || u.UserPhoneNumber.Contains(searchString)
                                    || u.UserAddress.Contains(searchString)
                                    || u.UserPostCode.Contains(searchString)
                                    || u.UserCountry.Contains(searchString)
                                    || u.UserMobileNumber.Contains(searchString)
                                    || u.UserState.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "FirstName_desc":
                    users = users.OrderByDescending(u => u.UserFirstName);
                    break;
                case "LastName_desc":
                    users = users.OrderByDescending(u => u.UserLastName);
                    break;                
                case "UserLastName":
                    users = users.OrderBy(u => u.UserLastName);
                    break;
                case "UserContactEmail":
                    users = users.OrderBy(u => u.UserContactEmail);
                    break;
                case "UserContactEmail_desc":
                    users = users.OrderByDescending(u => u.UserContactEmail);
                    break;
                case "UserPhoneNumber":
                    users = users.OrderBy(u => u.UserPhoneNumber);
                    break;
                case "UserPhoneNumber_desc":
                    users = users.OrderByDescending(u => u.UserPhoneNumber);
                    break;           
                case "UserAddress":
                    users = users.OrderBy(u => u.UserAddress);
                    break;
                case "UserAddress_desc":
                    users = users.OrderByDescending(u => u.UserAddress);
                    break;
                case "UserPostCode":
                    users = users.OrderBy(u => u.UserPostCode);
                    break;
                case "UserPostCode_desc":
                    users = users.OrderByDescending(u => u.UserPostCode);
                    break;        
                case "UserCountry":
                    users = users.OrderBy(u => u.UserCountry);
                    break;
                case "UserCountry_desc":
                    users = users.OrderByDescending(u => u.UserCountry);
                    break; 
                case "UserMobileNumber":
                    users = users.OrderBy(u => u.UserMobileNumber);
                    break; 
                case "UserMobileNumber_desc":
                    users = users.OrderByDescending(u => u.UserMobileNumber);
                    break; 
                case "UserState":
                    users = users.OrderBy(u => u.UserState);
                    break;
                case "UserState_desc":
                    users = users.OrderByDescending(u => u.UserState);
                    break;
                default:
                    users = users.OrderBy(u => u.UserFirstName);
                    break;
            }            
            
            int pageSize = 20;
            return View(await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create([Bind("UserLastName,UserFirstName,UserIsLeader,UserContactEmail,UserPhoneNumber,UserAddress,UserPostCode,UserCountry,UserMobileNumber,UserState,UserLogInName,UserPassword")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
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
            if (await TryUpdateModelAsync<User>(
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
