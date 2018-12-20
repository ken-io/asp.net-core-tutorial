using System;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult Demo()
        {
            return View();
        }

        public IActionResult DemoWithParams()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View("_PartialViewTest");
        }
    }
}
