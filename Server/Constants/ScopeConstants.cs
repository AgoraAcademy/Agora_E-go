using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Constants
{
    /// <summary>
    /// 基于scope的授权方案的scope常量字符串
    /// </summary>
    public static class ScopeConstants
    {
        #region 用户管理

        /// <summary>
        /// 读取用户信息
        /// </summary>
        public const string ReadUser = "read:user";
        
        /// <summary>
        /// 创建用户
        /// </summary>
        public const string CreateUser = "create:user";
        
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        public const string EditUser = "edit:user";

        /// <summary>
        /// 删除用户
        /// </summary>
        public const string DeleteUser = "delete:user";

        #endregion
    }
}
