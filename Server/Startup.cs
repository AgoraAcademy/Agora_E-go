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
    /// <summary>
    /// 启动类，在<see cref="Program.CreateHostBuilder(string[])"/>中被使用
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">依赖注入的配置文件</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 依赖注入的配置文件
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 将被运行时调用，被用于将服务添加到容器。
        /// </summary>
        /// <param name="services">服务集合</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 于此处添加数据库连接，添加时应于SQL Server对象资源管理器中添加表
            //AddDbContext<TContext>(services);
            // ...

            // 添加所有控制器
            services.AddControllers();
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <typeparam name="TContext">数据库上下文类型</typeparam>
        /// <param name="services">服务集合</param>
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

        /// <summary>
        /// 将被运行时调用，被用于配置HTTP请求管道。
        /// </summary>
        /// <param name="app">应用建造器</param>
        /// <param name="env">托管环境</param>
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
