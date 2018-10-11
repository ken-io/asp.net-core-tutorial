using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class ActionResultTestController : Controller
    {
        public IActionResult ContentTest()
        {
            return Content("ContentResult Test by ken.io");
        }

        public IActionResult JsonTest()
        {
            return Json(new { Message = "JsonResult Test", Author = "ken.io" });
        }

        public IActionResult FileTest()
        {
            var bytes = Encoding.Default.GetBytes("FileResult Test by ken.io");
            return File(bytes, "application/text", "filetest.txt");
        }

        public IActionResult RedirectTest()
        {
            return Redirect("https://ken.io");
        }

        public IActionResult RedirectToActionTest()
        {
            return RedirectToAction("jsontest");
        }

        public IActionResult RedirectToRouteTest()
        {
            return RedirectToRoute("Default", new { Controller = "home", Action = "index" });
        }
    }
}
