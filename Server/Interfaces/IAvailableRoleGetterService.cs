using Auth0.ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Interfaces
{
    /// <summary>
    /// 在Auth0 内的登录用API的所有可用用户身份获取
    /// </summary>
    public interface IAvailableRoleGetterService
    {
        /// <summary>
        /// 获取可用用户身份
        /// </summary>
        /// <returns>可用用户身份</returns>
        Task<Role[]> GetAvailableRoles();
    }
}
