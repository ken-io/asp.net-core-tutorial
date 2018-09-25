using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

using Ken.Tutorial.Web.Common;

namespace Ken.Tutorial.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //引入MVC模块
            services.AddMvc();

            //引入自定义路由约束
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("name", typeof(NameRouteConstraint));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                //带自定义约束的路由
                routes.MapRoute(
                    name: "TutorialDiyConstraintRoute",
                    template: "diy/{name}",
                    defaults: new { controller = "Tutorial", action = "Welcome" },
                    constraints: new { name = new NameRouteConstraint() }
                );

                routes.MapRoute(
                    name: "TutorialDiyConstraintRoute2",
                    template: "diy2/{name:name}",
                    defaults: new { controller = "Tutorial", action = "Welcome" }
                );

                //带正则约束的路由
                routes.MapRoute(
                    name: "TutorialRegexRoute",
                    template: "welcome/{name}",
                    defaults: new { controller = "Tutorial", action = "Welcome" },
                    constraints: new { name = @"k[a-z]*" }
                );

                //带约束的路由
                routes.MapRoute(
                    name: "TutorialLengthRoute",
                    template: "hello/{name}/{age?}",
                    defaults: new { controller = "Tutorial", action = "Welcome", name = "ken" },
                    constraints: new
                    {
                        name = new MaxLengthRouteConstraint(5),
                        age = new CompositeRouteConstraint(new IRouteConstraint[] {
                                            new IntRouteConstraint(),
                                            new MinRouteConstraint(1),
                                            new MaxRouteConstraint(150) })
                    }
                );

                //带约束的路由
                routes.MapRoute(
                    name: "TutorialLengthRoute2",
                    template: "hello2/{name:length(1,5)}/{age:range(1,150)?}",
                    defaults: new { controller = "Tutorial", action = "Welcome", name = "ken" }
                );

                //固定前缀路由
                routes.MapRoute(
                    name: "TutorialPrefixRoute",
                    template: "jiaocheng/{action}",
                    defaults: new { controller = "Tutorial" }
                );

                //固定后缀路由
                routes.MapRoute(
                    name: "TutorialSuffixRoute",
                    template: "{controller}/{action}.html"
                );

                //路径参数路由
                routes.MapRoute(
                    name: "TutorialPathValueRoute",
                    template: "{controller}/{action}/{name}/{age?}"
                );

                //配置默认路由
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });
        }
    }
}
