using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Extensions
{
    /// <summary>
    /// <see cref="IConfiguration"/>接口的扩展方法
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 相当于<code>GetSection("Auth0")?[name]</code>
        /// </summary>
        /// <param name="configuration">配置文件对象</param>
        /// <param name="name">配置项名称</param>
        /// <returns>配置项内容</returns>
        public static string GetAuth0Config(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("Auth0")?[name];
        }
    }
}
