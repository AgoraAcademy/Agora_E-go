using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Helpers
{
    /// <summary>
    /// Auth0用户管理API access token帮助类
    /// </summary>
    public static class ManagementApiAccessTokenHelper
    {
        /// <summary>
        /// 储存token的expire时间的静态变量
        /// </summary>
        private static DateTime expireAt = DateTime.MinValue;

        /// <summary>
        /// 储存新申请的token的静态变量
        /// </summary>
        private static string token = null;

        /// <summary>
        /// <see cref="GetAccessToken(IConfiguration)"/>的内部实现
        /// </summary>
        /// <param name="configuration">应用配置文件，将会读取其中的<code>"Auth0:Domain"</code>，<code>"Auth0:ManagementClientId"</code>与<code>"Auth0:ManagementClientSecret"</code></param>
        /// <returns>新申请的字符串格式的Auth0管理API的access token</returns>
        private static string InternalGetAccessToken(IConfiguration configuration)
        {
            // 创建token申请请求并发往Auth0
            RestClient client = new RestClient($"https://{configuration["Auth0:Domain"]}/oauth/token");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string json = JsonConvert.SerializeObject(new
            {
                grant_type = "client_credentials",
                client_id = configuration["Auth0:ManagementClientId"],
                client_secret = configuration["Auth0:ManagementClientSecret"],
                audience = $"https://{configuration["Auth0:Domain"]}/api/v2/"
            });
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            // 解析返回的response，保存token和expire时间至静态变量，然后返回token
            ResponseObject result = JsonConvert.DeserializeObject<ResponseObject>(response.Content);
            expireAt = DateTime.Now.AddSeconds(result.ExpireIn);
            token = result.AccessToken;
            return token;
        }

        /// <summary>
        /// 用于解析access token获取请求返回JSON的类
        /// </summary>
        private class ResponseObject
        {
            /// <summary>
            /// 返回的access token
            /// </summary>
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            /// <summary>
            /// token的可用时长
            /// </summary>
            [JsonProperty("expires_in")]
            public int ExpireIn { get; set; }

            /// <summary>
            /// token的有效scope列表
            /// </summary>
            [JsonProperty("scope")]
            public string Scope { get; set; }

            /// <summary>
            /// 返回token的类型
            /// </summary>
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        /// <summary>
        /// 获取Auth0用户管理API的access token，token将被重复使用直至距离过期只有1小时，那时将重新申请token
        /// </summary>
        /// <param name="configuration">应用配置文件，将会读取其中的<code>"Auth0:Domain"</code>，<code>"Auth0:ManagementClientId"</code>与<code>"Auth0:ManagementClientSecret"</code></param>
        /// <returns>字符串格式的Auth0管理API的access token</returns>
        public static string GetAccessToken(IConfiguration configuration)
        {
            if (token == null || DateTime.Now >= expireAt.AddHours(-1))
            {
                return InternalGetAccessToken(configuration);
            }
            return token;
        }
    }
}
