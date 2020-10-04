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
        }

        /// <summary>
        /// ����ע��������ļ�
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ��������ʱ���ã������ڽ�������ӵ�������
        /// </summary>
        /// <param name="services">���񼯺�</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // �ڴ˴�������ݿ����ӣ����ʱӦ��SQL Server������Դ����������ӱ�
            //AddDbContext<TContext>(services);
            // ...

            // ������п�����
            services.AddControllers();
        }

        /// <summary>
        /// ������ݿ�������
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
