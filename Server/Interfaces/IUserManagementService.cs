using AgoraAcademy.AgoraEgo.Server.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Interfaces
{
    /// <summary>
    /// 关于用户管理的接口，内部应使用<see cref="IAuth0UserManagementService"/>来在Auth0同步信息
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户信息</returns>
        Task<UserData> GetUserDataAsync(int id);

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>所有用户信息数组</returns>
        Task<UserData[]> GetAllUserDataAsync();

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="searchMethod">搜索的函数</param>
        /// <returns>搜索结果</returns>
        Task<UserData[]> GetUserDatasAsync([NotNull] Func<IEnumerable<UserData>, UserData[]> searchMethod);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="update">代表更新的对象</param>
        /// <returns>状态码，<see cref="StatusCodes.Status500InternalServerError"/>代表出错，<see cref="StatusCodes.Status200OK"/>代表成功</returns>
        Task<int> UpdateUserDataAsync([NotNull] UserDataUpdate update);

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns>新用户ID</returns>
        Task<int> CreateUserAsync([NotNull] PureUserData data);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">将删除的用户的ID</param>
        /// <returns>状态码，<see cref="StatusCodes.Status204NoContent"/>代表删除成功，<see cref="StatusCodes.Status404NotFound"/>代表未找到</returns>
        Task<int> DeleteUserAsync(int id);
    }
}
