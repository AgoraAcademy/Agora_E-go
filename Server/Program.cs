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
    /// 程序主入口类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序主入口方法
        /// </summary>
        /// <param name="args">外部参数</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创造托管环境建造者
        /// </summary>
        /// <param name="args">外部参数</param>
        /// <returns>托管环境建造者</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
