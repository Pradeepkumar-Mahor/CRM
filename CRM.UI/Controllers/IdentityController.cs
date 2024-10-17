using CRM.UI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.UI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            var model = new SignUpViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    ModelState.AddModelError("SignUp", string.Join("", result.Errors.Select(x => x.Description)));
                    return View(model);

                    //await _userManager.CreateAsync()
                }
            }
            return View(model);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}