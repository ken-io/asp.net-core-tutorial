using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ken.Tutorial.Web.Middlewares
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenCheckMiddleware(RequestDelegate requestDelegate)
        {
            this._next = requestDelegate;
        }

        public Task Invoke(HttpContext context)
        {
            //先从Url取token，如果取不到就从Form表单中取token
            var token = context.Request.Query["token"].ToString() ?? context.Request.Form["token"].ToString();
            if (string.IsNullOrWhiteSpace(token))
            {
                //如果没有获取到token信息，那么久返回token missing
                return context.Response.WriteAsync("token missing");
            }
            //获取前1分钟和当前的分钟
            var minute0 = DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm");
            var minute = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //当token和前一分钟或当前分钟任一时间字符串的MD5哈希一致，就认为是合法请求
            if (token == MD5Hash(minute) || token == MD5Hash(minute0))
            {
                return _next.Invoke(context);
            }
            //如果token未验证通过返回token error
            return context.Response.WriteAsync("token error");
        }

        public string MD5Hash(string value)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}