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
            // 通过字符串ID获取数字ID，缺损则为-1
            int ID = (await managementService.GetUserDatasAsync((datas) => datas.Where((data) => context.User.HasClaim("UserID", data.StringUserID)).ToArray())).FirstOrDefault()?.ID ?? -1;
            if (resource.MentorID.Contains(ID))
            {
                context.Succeed(requirement);
            }
        }
    }
}
