using AgoraAcademy.AgoraEgo.Server.Extensions;
using AgoraAcademy.AgoraEgo.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Authorization
{
    /// <summary>
    /// 有指导导师的资源的操作授权处理者类
    /// </summary>
    public class ResourceGuidingMentorAuthorizationHandler : AuthorizationHandler<GuidingMentorRequirement, IMentorGuidingResource>
    {
        /// <summary>
        /// 依赖注入的用户管理服务
        /// </summary>
        private readonly IUserManagementService managementService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="managementService">依赖注入的用户管理服务</param>
        public ResourceGuidingMentorAuthorizationHandler(IUserManagementService managementService)
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
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GuidingMentorRequirement requirement, IMentorGuidingResource resource)
        {
            // 获取数字ID，未找到用户则为-1
            int ID = await managementService
                .RetrieveUserWithClaimPrincipal(context.User)
                .Select((data) => data?.ID ?? -1);
            if (resource.MentorID.Contains(ID))
            {
                context.Succeed(requirement);
            }
        }
    }
}
