using AgoraAcademy.AgoraEgo.Server.Authorization;
using AgoraAcademy.AgoraEgo.Server.Data.DbContexts;
using AgoraAcademy.AgoraEgo.Server.Extensions;
using AgoraAcademy.AgoraEgo.Server.Interfaces;
using AgoraAcademy.AgoraEgo.Server.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Clients;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Services
{
    /// <summary>
    /// <see cref="IUserManagementService"/>的实现类
    /// </summary>
    public class UserManagementService : IUserManagementService
    {
        /// <summary>
        /// Auth0 用户管理API Client实例
        /// </summary>
        private readonly UsersClient userClient;

        /// <summary>
        /// 用户数据的数据库上下文
        /// </summary>
        private readonly UserDataContext dataContext;

        /// <summary>
        /// 程序配置文件
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// 可用用户身份获取服务接口
        /// </summary>
        private readonly IAvailableRoleGetterService roleGetter;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="managementClient">Auth0 用户管理API Client实例</param>
        /// <param name="dataContext">用户数据的数据库上下文</param>
        /// <param name="configuration">程序配置文件</param>
        /// <param name="roleGetter">可用用户身份获取服务接口</param>
        public UserManagementService(ManagementApiClient managementClient, UserDataContext dataContext, IConfiguration configuration, IAvailableRoleGetterService roleGetter)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
            this.roleGetter = roleGetter;
            userClient = managementClient.Users;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="update">代表更新的对象</param>
        /// <returns>状态码，<see cref="StatusCodes.Status500InternalServerError"/>代表出错，<see cref="StatusCodes.Status200OK"/>代表成功</returns>
        public async Task<int> UpdateUserDataAsync([NotNull] UserDataUpdate update)
        {
            // 尝试获取用户数据
            UserData data = await dataContext.FindAsync<UserData>(update.ID);
            if (data == null)
            {
                return StatusCodes.Status404NotFound;
            }
            // 如果有邮箱或电话号码更新，发送请求
            if (update.Email != data.Email || update.PhoneNumber != data.PhoneNumber)
            {
                UserUpdateRequest request = new UserUpdateRequest();
                if (update.Email != null)
                {
                    request.Email = update.Email;
                }
                if (update.PhoneNumber != null)
                {
                    request.PhoneNumber = update.PhoneNumber;
                }

                request.Connection = configuration["Auth0:Connection"];

                if (null == await userClient.UpdateAsync(data.StringUserID, request))
                {
                    return StatusCodes.Status500InternalServerError;
                }
            }
            // 如果有身份变化
            if (update.Role != null)
            {
                // 获取可用身份列表
                Role[] availableRoles = await roleGetter.GetAvailableRoles();
                // 去除重复项与不在身份列表内的项
                string[] UpdateRoles = update.Role.Split(' ').Distinct().Where((name) => availableRoles.Any((role) => role.Name == name)).ToArray();
                string[] currentRoles = data.Role.Split(' ');
                update.Role = string.Join(' ', UpdateRoles);

                // 获取新增的身份
                string[] assignRoles = (from name in UpdateRoles
                                        where !currentRoles.Contains(name)
                                        // 确认包含于可用身份列表内
                                        let roleObject = availableRoles.FirstOrDefault((role) => role.Name == name)
                                        where roleObject != null
                                        // 选中ID
                                        select roleObject.Id).ToArray();

                // 获取移除的身份
                string[] removeRoles = (from name in currentRoles
                                        where !UpdateRoles.Contains(name)
                                        // 确认包含于可用身份列表内
                                        let roleObject = availableRoles.FirstOrDefault((role) => role.Name == name)
                                        // 选中ID
                                        where roleObject != null
                                        select roleObject.Id).ToArray();

                // 如果有需要添加的身份
                if (assignRoles.Length > 0)
                {
                    // 添加身份
                    await userClient.AssignRolesAsync(data.StringUserID,
                        new AssignRolesRequest { Roles = assignRoles });
                }

                // 如果有需要移除的身份
                if (removeRoles.Length > 0)
                {
                    // 移除身份
                    await userClient.RemoveRolesAsync(data.StringUserID,
                        new AssignRolesRequest { Roles = removeRoles });
                }
            }

            // 应用变化然后保存
            data.ApplyUpdate(update);

            await dataContext.SaveWithExceptionHandlingAsync();

            return StatusCodes.Status200OK;
        }

        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns>新用户ID</returns>
        public async Task<int> CreateUserAsync([NotNull] PureUserData data)
        {
            UserCreateRequest request = new UserCreateRequest
            {
                Connection = configuration["Auth0:Connection"],
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            User user = await userClient.CreateAsync(request);
            if (user == null)
            {
                return -1;
            }

            int nextId = dataContext.UserDatas.Select((data) => data.ID).Max() + 1;

            // 获取可用身份列表
            Role[] availableRoles = await roleGetter.GetAvailableRoles();

            // 去除重复项与不在身份列表内的项
            string[] roles = data.Role.Split(' ').Distinct()
                .Where((name) => availableRoles.Any((role) => role.Name == name)).ToArray();

            await userClient.AssignRolesAsync(user.UserId, new AssignRolesRequest { Roles = roles });

            data.Role = roles.Aggregate((r1, r2) => r1 + ' ' + r2);

            dataContext.Add<UserData>(data.ToUserData(nextId, user.UserId));
            dataContext.SaveChanges();
            return nextId;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">将删除的用户的ID</param>
        /// <returns>状态码，<see cref="StatusCodes.Status204NoContent"/>代表删除成功，<see cref="StatusCodes.Status404NotFound"/>代表未找到</returns>
        public async Task<int> DeleteUserAsync(int id)
        {
            // 获取用户
            UserData data = await dataContext.FindAsync<UserData>(id);
            if (data == null)
            {
                return StatusCodes.Status404NotFound;
            }

            do
            {
                // 在Auth0上删除
                await userClient.DeleteAsync(data.StringUserID);
            } while (null != await userClient.GetAsync(data.StringUserID));

            // 在本地数据库中删除
            dataContext.Remove<UserData>(data);
            dataContext.SaveChanges();
            return StatusCodes.Status204NoContent;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>所有用户信息数组</returns>
        public Task<UserData[]> GetAllUserDataAsync()
        {
            // 获取所有用户
            return dataContext.UserDatas.ToArrayAsync();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户信息</returns>
        public Task<UserData> GetUserDataAsync(int id)
        {
            // 根据ID查找用户
            return dataContext.FindAsync<UserData>(id).AsTask();
        }

        /// <summary>
        /// 搜索用户信息
        /// </summary>
        /// <param name="searchMethod">搜索的函数</param>
        /// <returns>搜索结果</returns>
        public Task<UserData[]> GetUserDatasAsync(Func<IEnumerable<UserData>, UserData[]> searchMethod)
        {
            // 运行回调函数
            return Task.Run<UserData[]>(() => searchMethod?.Invoke(dataContext.UserDatas));
        }
    }
}
