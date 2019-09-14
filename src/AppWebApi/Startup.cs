
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppWebApi.Services;
using AppWebApi.Interfaces;


namespace AppWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //請參考https://ithelp.ithome.com.tw/articles/10193172
            //Service 生命週期
            //註冊在 DI 容器的 Service 有分三種生命週期：
            //Transient
            //每次注入時，都重新 new 一個新的實例。
            //Scoped
            //每個 Request 都重新 new 一個新的實例，同一個 Request 不管經過多少個 Pipeline 都是用同一個實例。上例所使用的就是 Scoped。
            //Singleton
            //被實例化後就不會消失，程式運行期間只會有一個實例。


            //將Service注入給容器使用
            services.AddScoped<IToDo, ToDoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }


    }
}
