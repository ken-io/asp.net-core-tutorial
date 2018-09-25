using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ken.Tutorial.Web.Common
{
    public class NameRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string name = values["name"]?.ToString();
            if (name == null) return true;
            if (name.Length > 5 && name.Contains(",")) return false;
            return true;
        }
    }
}
