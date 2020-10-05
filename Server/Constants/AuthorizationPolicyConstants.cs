using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Constants
{
    /// <summary>
    /// 代表授权策略的常量字符串
    /// </summary>
    public static class AuthorizationPolicyConstants
    {
        /// <summary>
        /// 代表检测用户是否是资源所有者的授权策略
        /// </summary>
        public const string Owner = nameof(Owner);

        /// <summary>
        /// 代表检测用户是否是指导导师的授权策略
        /// </summary>
        public const string GuidingMentor = nameof(GuidingMentor);

        /// <summary>
        /// 代表检测用户是否是合作者的授权策略
        /// </summary>
        public const string Collaborator = nameof(Collaborator);
    }
}
