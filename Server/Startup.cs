using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgoraAcademy.AgoraEgo.Server.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            // Auth0:Domain应记录于secrets.json中，并应与Auth0相应中的域相同
            Domain = $"https://{Configuration["Auth0:Domain"]}/";
        }

        /// <summary>
        /// 依赖注入的配置文件
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 完整Auth0域名
        /// </summary>
        private string Domain { get; }

        /// <summary>
        /// 将被运行时调用，被用于将服务添加到容器。
        /// </summary>
        /// <param name="services">服务集合</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 于此处注册数据库连接，添加时应于SQL Server对象资源管理器中添加表
            //AddDbContext<TContext>(services);
            // ...

            // 添加身份验证
            services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Domain;
                // Auth0:Audience应记录于secrets.json中，值应为Auth0内的登录api的identifier，
                // 应在该api的permissions页面注册所有scope，并在Auth0域内的User & Roles/Roles页面设置用户组具体的scope列表
                options.Audience = Configuration["Auth0:Audience"];
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // 将access_token作为claim记录到user，可使用Auth0的https://{Configuration["Auth0:Domain"]}/api/v2/userinfo来获取用户信息
                        if (context.SecurityToken is JwtSecurityToken token)
                        {
                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("access_token", token.RawData));
                            }
                        }

                        return Task.CompletedTask;
                    }
                };

            });


            // 于此处注册Auth0的基于scope的授权策略
            // AddScopeRequirementPolicy(services, "scope_name");
            // AddScopeRequirementPolicy(services, "scope_name", "policy_name");
            // ...

            // 注册授权管理者
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            // 添加所有控制器
            services.AddControllers();
        }

        /// <summary>
        /// 注册身份认证策略，使用<paramref name="scope"/>作为策略的名称
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="scope">完整的scope名称，应与Auth0内注册的scope名称完全一致</param>
        private void AddScopeRequirementPolicy(IServiceCollection services, string scope)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(scope, policy => policy.Requirements.Add(new HasScopeRequirement(scope, Domain)));
            });
        }

        /// <summary>
        /// 注册身份认证策略
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="scope">完整的scope名称，应与Auth0内注册的scope名称完全一致</param>
        /// <param name="policyName">策略名称</param>
        private void AddScopeRequirementPolicy(IServiceCollection services, string scope, string policyName)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(policyName, policy => policy.Requirements.Add(new HasScopeRequirement(scope, Domain)));
            });
        }

        /// <summary>
        /// 注册数据库上下文
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

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
