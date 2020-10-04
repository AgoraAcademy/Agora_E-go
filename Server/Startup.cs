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
    /// �����࣬��<see cref="Program.CreateHostBuilder(string[])"/>�б�ʹ��
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration">����ע��������ļ�</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // Auth0:DomainӦ��¼��secrets.json�У���Ӧ��Auth0��Ӧ�е�����ͬ
            Domain = $"https://{Configuration["Auth0:Domain"]}/";
        }

        /// <summary>
        /// ����ע��������ļ�
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ����Auth0����
        /// </summary>
        private string Domain { get; }

        /// <summary>
        /// ��������ʱ���ã������ڽ�������ӵ�������
        /// </summary>
        /// <param name="services">���񼯺�</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // �ڴ˴�ע�����ݿ����ӣ����ʱӦ��SQL Server������Դ����������ӱ�
            //AddDbContext<TContext>(services);
            // ...

            // ��������֤
            services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Domain;
                // Auth0:AudienceӦ��¼��secrets.json�У�ֵӦΪAuth0�ڵĵ�¼api��identifier��
                // Ӧ�ڸ�api��permissionsҳ��ע������scope������Auth0���ڵ�User & Roles/Rolesҳ�������û�������scope�б�
                options.Audience = Configuration["Auth0:Audience"];
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // ��access_token��Ϊclaim��¼��user����ʹ��Auth0��https://{Configuration["Auth0:Domain"]}/api/v2/userinfo����ȡ�û���Ϣ
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


            // �ڴ˴�ע��Auth0�Ļ���scope����Ȩ����
            // AddScopeRequirementPolicy(services, "scope_name");
            // AddScopeRequirementPolicy(services, "scope_name", "policy_name");
            // ...

            // ע����Ȩ������
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            // ������п�����
            services.AddControllers();
        }

        /// <summary>
        /// ע�������֤���ԣ�ʹ��<paramref name="scope"/>��Ϊ���Ե�����
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <param name="scope">������scope���ƣ�Ӧ��Auth0��ע���scope������ȫһ��</param>
        private void AddScopeRequirementPolicy(IServiceCollection services, string scope)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(scope, policy => policy.Requirements.Add(new HasScopeRequirement(scope, Domain)));
            });
        }

        /// <summary>
        /// ע�������֤����
        /// </summary>
        /// <param name="services">���񼯺�</param>
        /// <param name="scope">������scope���ƣ�Ӧ��Auth0��ע���scope������ȫһ��</param>
        /// <param name="policyName">��������</param>
        private void AddScopeRequirementPolicy(IServiceCollection services, string scope, string policyName)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(policyName, policy => policy.Requirements.Add(new HasScopeRequirement(scope, Domain)));
            });
        }

        /// <summary>
        /// ע�����ݿ�������
        /// </summary>
        /// <typeparam name="TContext">���ݿ�����������</typeparam>
        /// <param name="services">���񼯺�</param>
        private void AddDbContext<TContext>(IServiceCollection services)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>((context) =>
            {
                // �������ݿ⣬Ӧ��Visual Studio��Server��Ŀ��Connected Servicesҳ�����ӱ���SQL Server��
                // Ȼ��ѡ�����ӹؼ��ּ�¼��secrets.json�У�
                // ����appsettings.json�е�DbConnectionString��Ӧ��ֵ��Ϊ���ӹؼ���
                context.UseSqlServer(Configuration.GetConnectionString(Configuration["DbConnectionString"]));
            });
        }

        /// <summary>
        /// ��������ʱ���ã�����������HTTP����ܵ���
        /// </summary>
        /// <param name="app">Ӧ�ý�����</param>
        /// <param name="env">�йܻ���</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // �����Ҫ��Ӳ�����Ӧ��ʵ����������ϸ�ڼ� https://aka.ms/aspnetcore-hsts.
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
