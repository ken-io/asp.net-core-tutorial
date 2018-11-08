using System;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello World ！ -ken.io");
        }

        public IActionResult Time()
        {
            //将当前服务器时间放入ViewBag中
            ViewBag.ServerTime = DateTime.Now;
            return View("Time");
        }

        public IActionResult Test(){
            return View();
        }
    }
}
