using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgoraAcademy.AgoraEgo.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 将被运行时调用，被用于将服务添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {
            // 于此处添加数据库连接，添加时应于SQL Server对象资源管理器中添加表
            //AddDbContext<TContext>(services);
            // ...

            // 添加所有控制器
            services.AddControllers();
        }
        // 添加数据库上下文
        private void AddDbContext<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>((context) =>
            {
                // 连接数据库，应在Visual Studio内Server项目的Connected Services页面连接本地SQL Server，
                // 然后选择将连接关键字记录在secrets.json中，
                // 最后把appsettings.json中的DbConnectionString对应的值设为连接关键字
                context.UseSqlServer(Configuration.GetConnectionString(Configuration["DbConnectionString"]));
            });
        }

        // 将被运行时调用，被用于配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // 如果想要添加参数以应对实际生产需求，细节见 https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
