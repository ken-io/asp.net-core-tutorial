using System;
using Ken.Tutorial.Web.Models;
using Ken.Tutorial.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ken.Tutorial.Web.Controllers
{
    public class EfCoreWithSqlController : Controller
    {
        public TutorialWithSqlRepository Repository { get; }

        public EfCoreWithSqlController(TutorialWithSqlRepository repository)
        {
            this.Repository = repository;
        }

        public IActionResult Add(UserEntity user)
        {
            var message = Repository.Add(user) > 0 ? "success" : "failed";
            return Json(new { Message = message, User = user });
        }

        public IActionResult Delete(int id)
        {
            var message = Repository.Delete(id) > 0 ? "success" : "failed";
            return Json(new { Message = message });
        }

        public IActionResult Update(UserEntity user)
        {
            var message = Repository.Update(user) > 0 ? "success" : "failed";
            return Json(new { Message = message, User = user });
        }

        public IActionResult QueryById(int id)
        {
            var user = Repository.QueryById(id);
            return Json(new { User = user });
        }

        public IActionResult QueryByAge(int age)
        {
            var users = Repository.QueryByAge(age);
            return Json(new { Users = users });
        }

        public IActionResult QueryNameByAge(int age)
        {
            var users = Repository.QueryNameByAge(age);
            return Json(new { Users = users });
        }

        public IActionResult QueryUserPaging(int pageSize, int page)
        {
            var users = Repository.QueryUserPaging(pageSize, page);
            return Json(new { Users = users });
        }

        public IActionResult FixAge()
        {
            var count = Repository.FixAge();
            return Json(new { FixCount = count });
        }

    }
}
