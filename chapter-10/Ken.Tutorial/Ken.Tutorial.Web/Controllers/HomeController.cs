using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ken.Tutorial.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogTrace("index:trace log");
            _logger.LogInformation("------\r\nindex：hello world\r\n------");
            return Content("Hello World ！ -ken.io");
        }

        public IActionResult CheckPhone(string phone)
        {
            _logger.LogInformation($"------\r\ncheck phone：{phone}\r\n------");
            var result = true;
            var message = "pass";
            if (string.IsNullOrWhiteSpace(phone))
            {
                result = false;
                message = "phone number is empty";
                _logger.LogError($"------\r\ncheck phone：{message}\r\n------");
            }
            else if (phone.Length != 11)
            {
                result = false;
                message = "wrong phone number length";
                _logger.LogWarning($"------\r\ncheck phone：{message}\r\n------");
            }
            return Json(new { Result = result, Phone = phone, Message = message });
        }

        public IActionResult TestLog()
        {
            var logger = NLog.LogManager.GetLogger("testlog");
            logger.Trace("这是Trace日志");
            logger.Debug("这是Debug日志");
            logger.Info("这是Info日志");
            logger.Warn("这是警告日志");
            logger.Error("这是错误日志");
            return Content("ok");
        }

        public IActionResult TestLogMany()
        {
            var logger = NLog.LogManager.GetLogger("logmany");
            for (int i = 0; i <= 30000; i++)
            {
                logger.Info("ASP.NET Core入门教程，这里是日志内容，测试NLog的日志归档功能，ken的杂谈(https://ken.io)");
            }
            return Content("ok");
        }

        public IActionResult Time()
        {
            //将当前服务器时间放入ViewBag中
            ViewBag.ServerTime = DateTime.Now;
            return View("Time");
        }
    }
}
