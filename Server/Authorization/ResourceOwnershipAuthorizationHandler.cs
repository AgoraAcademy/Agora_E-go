using AgoraAcademy.AgoraEgo.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Authorization
{
    /// <summary>
    /// 有拥有者的资源的操作授权处理者类
    /// </summary>
    public class ResourceOwnershipAuthorizationHandler : AuthorizationHandler<OwnershipRequirement, IOwnedResource>
    {
        /// <summary>
        /// 依赖注入的用户管理服务
        /// </summary>
        private readonly IUserManagementService managementService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="managementService">依赖注入的用户管理服务</param>
        public ResourceOwnershipAuthorizationHandler(IUserManagementService managementService)
        {
            this.managementService = managementService;
        }

        /// <summary>
        /// 重写的授权程序需求处理方法
        /// </summary>
        /// <param name="context">授权处理上下文</param>
        /// <param name="requirement">被审核的需求</param>
        /// <param name="resource">被审核的资源</param>
        /// <returns>用于等待的<see cref="Task"/>对象</returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnershipRequirement requirement, IOwnedResource resource)
        {
            string userID = (await managementService.GetUserDataAsync(resource.OwnerID))?.StringUserID;
            if (userID != null && context.User.HasClaim("UserID", userID))
            {
                context.Succeed(requirement);
            }
        }
    }
}