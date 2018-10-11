using System;
using System.Collections.Generic;
using Ken.Tutorial.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class ParamsMappingTestController : Controller
    {
        public IActionResult GetId(int id)
        {
            return Content($"Action params mapping test by ken.io, id:{id}");
        }

        public IActionResult GetArray(string[] id)
        {
            var message = "Action params mapping test by ken.io,id:";
            if (id != null)
            {
                message += string.Join(",", id);
            }
            return Content(message);
        }

        public IActionResult GetPerson(Person person)
        {
            return Json(new { Message = "Action params mapping test by ken.io", Data = person });
        }

        public IActionResult GetPersonList(List<Person> person)
        {
            return Json(new { Message = "Action params mapping test by ken.io", Data = person });
        }

        public IActionResult GetPersonJson([FromBody]Person person)
        {
            return Json(new { Message = "Action params mapping test by ken.io", Data = person });
        }

        public IActionResult GetByHand()
        {
            return Json(new
            {
                Id = RouteData.Values["id"],
                Name = Request.Query["name"]
            });
        }

    }
}
