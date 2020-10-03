using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Constants
{
    /// <summary>
    /// 配置文件键值常量
    /// </summary>
    public static class ConfigurationKeyConstants
    {
        /// <summary>
        /// Auth0域
        /// </summary>
        public const string Auth0Domain = "Domain";

        /// <summary>
        /// Auth0管理API客户端ID
        /// </summary>
        public const string Auth0ManagementClientId = "ManagementClientId";

        /// <summary>
        /// Auth0管理API客户端密钥
        /// </summary>
        public const string Auth0ManagementClientSecret = "ManagementClientSecret";

        /// <summary>
        /// Auth0用户数据库连接ID
        /// </summary>
        public const string Auth0Connection = "Connection";

        /// <summary>
        /// Auth0登录API客户端ID
        /// </summary>
        public const string Auth0LoginApiId = "LoginApiId";

        /// <summary>
        /// Auth0登录API的ID
        /// </summary>
        public const string Auth0Audience = "Audience";

        /// <summary>
        /// 本地数据库连接
        /// </summary>
        public const string DbConnectionString = "DbConnectionString";
    }
}
