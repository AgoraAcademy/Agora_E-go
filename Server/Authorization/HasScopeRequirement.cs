using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Authorization
{
    /// <summary>
    /// 授权需求对象，用于Auth0
    /// </summary>
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 授权提供者域名
        /// </summary>
        public string Issuer { get; }
        
        /// <summary>
        /// 授权范围
        /// </summary>
        public string Scope { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">授权范围</param>
        /// <param name="issuer">授权提供者域名</param>
        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
