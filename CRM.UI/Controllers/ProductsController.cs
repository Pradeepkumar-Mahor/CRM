using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult View()
        {
            return View();
        }

        [Authorize(Roles = "Administrator,SuperAdmin")]
        public IActionResult Create()
        { return View(); }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult SpecialFeature()
        {
            return View();
        }
    }
}