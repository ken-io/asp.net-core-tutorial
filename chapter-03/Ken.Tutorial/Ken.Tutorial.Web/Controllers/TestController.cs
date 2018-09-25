using System;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    [Route("/test")]
    public class TestController : Controller
    {
        [Route("")]
        [Route("/test/home")]
        public IActionResult Index()
        {
            return Content("ASP.NET Core RouteAttribute test by ken from ken.io");
        }

        [Route("servertime")]
        [Route("/t/t")]
        public IActionResult Time(){
            return Content($"ServerTime：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - ken.io");
        }


        [Route("welcome/{name:name}")]
        public IActionResult Welcome(string name){
            return Content($"Welcome {name} !");
        }
    }
}
