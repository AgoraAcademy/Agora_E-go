using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgoraAcademy.AgoraEgo.Server
{
    /// <summary>
    /// �����������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ��������ڷ���
        /// </summary>
        /// <param name="args">�ⲿ����</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// �����йܻ���������
        /// </summary>
        /// <param name="args">�ⲿ����</param>
        /// <returns>�йܻ���������</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
