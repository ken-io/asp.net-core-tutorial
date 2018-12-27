using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ken.Tutorial.Web.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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

            //配置DbContext注入
            services.AddTransient<TutorialDbContext>();

            //配置Repository注入
            services.AddTransient<TutorialRepository>();
            services.AddTransient<TutorialWithSqlRepository>();
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
