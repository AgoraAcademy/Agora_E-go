using AgoraAcademy.AgoraEgo.Server.Interfaces;
using AgoraAcademy.AgoraEgo.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Extensions
{
    /// <summary>
    /// <see cref="IUserManagementService"/>接口的扩展方法
    /// </summary>
    public static class UserManagementServiceExtension
    {
        /// <summary>
        /// 查找用户数据的异步扩展方法
        /// </summary>
        /// <param name="managementService">用户管理服务接口实现者</param>
        /// <param name="predicator">关于<see cref="UserData"/>类的一阶谓词</param>
        /// <returns>所有符合条件的对象</returns>
        public static async Task<UserData[]> RetrieveUserDataAsync(this IUserManagementService managementService, Func<UserData, bool> predicator)
        {
            return await managementService.GetUserDatasAsync((datas) => datas.Where(predicator).ToArray());
        }

        /// <summary>
        /// 查找用户数据的异步扩展方法，返回第一个符合条件的对象
        /// </summary>
        /// <param name="managementService">用户管理服务接口实现者</param>
        /// <param name="predicator">关于<see cref="UserData"/>类的一阶谓词</param>
        /// <returns>第一个符合条件的对象，全部不符合条件则返回<see cref="null"/></returns>
        public static async Task<UserData> RetrieveFirstOrDefaultAsync(this IUserManagementService managementService, Func<UserData, bool> predicator)
        {
            return await managementService.RetrieveUserDataAsync(predicator).Select(datas => datas.FirstOrDefault());
        }

        /// <summary>
        /// 根据<see cref="UserData.StringUserID"/>查找用户
        /// </summary>
        /// <param name="managementService">用户管理服务接口实现者</param>
        /// <param name="StringUserID">用户的字符串ID</param>
        /// <returns><see cref="UserData.StringUserID"/>与<paramref name="StringUserID"/>相符的用户，未找到时返回<see cref="null"/></returns>
        public static async Task<UserData> RetrieveUserWithStringID(this IUserManagementService managementService, string StringUserID)
        {
            return await managementService.RetrieveFirstOrDefaultAsync((data) => data.StringUserID == StringUserID);
        }

        /// <summary>
        /// 根据<paramref name="user"/>查找用户
        /// </summary>
        /// <param name="managementService">用户管理服务接口实现者</param>
        /// <param name="user">asp.net core中的用户对象</param>
        /// <returns><see cref="UserData.StringUserID"/>与<paramref name="user"/>内的<c>"UserID"</c>声明的值相符的用户，未找到时返回<see cref="null"/></returns>
        public static async Task<UserData> RetrieveUserWithClaimPrincipal(this IUserManagementService managementService, ClaimsPrincipal user)
        {
            return await managementService.RetrieveUserWithStringID(user.FindFirst("user_id").Value);
        }


    }
}
