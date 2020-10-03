using AgoraAcademy.AgoraEgo.Server.Interfaces;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Services
{
    /// <summary>
    /// <see cref="IAvailableRoleGetterService"/>的实现类
    /// </summary>
    public class AvailableRoleGetterService : IAvailableRoleGetterService
    {
        /// <summary>
        /// Auth0管理API Client
        /// </summary>
        private readonly ManagementApiClient managementClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="managementClient">Auth0管理API Client</param>
        public AvailableRoleGetterService(ManagementApiClient managementClient)
        {
            this.managementClient = managementClient;
        }

        /// <summary>
        /// 获取可用用户身份
        /// </summary>
        /// <returns>可用用户身份</returns>
        public async Task<Role[]> GetAvailableRoles()
        {
            // 发送获取请求并将返回值转化为数组
            return (await managementClient.Roles.GetAllAsync(new GetRolesRequest())).ToArray();
        }
    }
}
