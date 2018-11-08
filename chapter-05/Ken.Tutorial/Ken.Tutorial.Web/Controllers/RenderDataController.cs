using System;
using System.Collections.Generic;
using System.Dynamic;
using Ken.Tutorial.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class RenderDataController : Controller
    {
        public IActionResult ViewDataDemo()
        {
            ViewData["title"] = "ViewData传值示例";
            ViewData["name"] = "ken";
            ViewData["birthday"] = new DateTime(2000, 1, 1);
            ViewData["hobby"] = new string[] { "跑步", "阅读", "Coding" };
            return View();
        }

        public IActionResult ViewBagDemo()
        {
            ViewBag.Title = "ViewBag传值示例";
            ViewBag.Name = "ken";
            ViewBag.Birthday = new DateTime(2000, 1, 1);
            ViewBag.Hobby = new string[] { "跑步", "阅读", "Coding" };
            return View();
        }

        public IActionResult ViewModelDemo()
        {
            ViewBag.Title = "ViewModel传值示例";
            var person = new Person
            {
                Name = "ken",
                Birthday = new DateTime(2000, 1, 1),
                Hobby = new string[] { "跑步", "阅读", "Coding" }
            };
            //等同于 return View("ViewModelDemo", person);
            return View(person);
        }
    }
}
