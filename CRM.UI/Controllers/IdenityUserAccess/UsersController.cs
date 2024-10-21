using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var allUsersExceptCurrentUser =
                        await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
                return View(allUsersExceptCurrentUser);
            }
            catch (Exception xe)
            {
                throw;
            }
        }
    }
}