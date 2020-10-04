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

        // ��������ʱ���ã������ڽ�������ӵ�������
        public void ConfigureServices(IServiceCollection services)
        {
            // �ڴ˴�������ݿ����ӣ����ʱӦ��SQL Server������Դ����������ӱ�
            //AddDbContext<TContext>(services);
            // ...

            // ������п�����
            services.AddControllers();
        }
        // ������ݿ�������
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

        // ��������ʱ���ã�����������HTTP����ܵ���
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
