using AgoraAcademy.AgoraEgo.Server.Constants;
using AgoraAcademy.AgoraEgo.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Extensions
{
    /// <summary>
    /// 授权服务接口的自定义扩展方法类
    /// </summary>
    public static class AuthorizationServiceExtension
    {
        /// <summary>
        /// 检测用户是否是资源的所有者的授权动作
        /// </summary>
        /// <param name="service">授权服务接口</param>
        /// <param name="user">被检测的用户</param>
        /// <param name="resource">被检测的资源</param>
        /// <returns>授权结果</returns>
        public static async Task<AuthorizationResult> AuthorizeOwnershipAsync(this IAuthorizationService service, ClaimsPrincipal user, IOwnedResource resource)
        {
            return await service.AuthorizeAsync(user, resource, AuthorizationPolicyConstants.Owner);
        }

        /// <summary>
        /// 检测用户是否是资源的合作所有者的授权动作
        /// </summary>
        /// <param name="service">授权服务接口</param>
        /// <param name="user">被检测的用户</param>
        /// <param name="resource">被检测的资源</param>
        /// <returns>授权结果</returns>
        public static async Task<AuthorizationResult> AuthorizeCollaboratorAsync(this IAuthorizationService service, ClaimsPrincipal user, ICollaborateResource resource)
        {
            return await service.AuthorizeAsync(user, resource, AuthorizationPolicyConstants.Collaborator);
        }

        /// <summary>
        /// 检测用户是否是资源的指导导师的授权动作
        /// </summary>
        /// <param name="service">授权服务接口</param>
        /// <param name="user">被检测的用户</param>
        /// <param name="resource">被检测的资源</param>
        /// <returns>授权结果</returns>
        public static async Task<AuthorizationResult> AuthorizeGuidingMentorAsync(this IAuthorizationService service, ClaimsPrincipal user, IMentorGuidingResource resource)
        {
            return await service.AuthorizeAsync(user, resource, AuthorizationPolicyConstants.GuidingMentor);
        }
    }
}
