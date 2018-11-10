using System;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult SectionDemo()
        {
            return View();
        }
    }
}
