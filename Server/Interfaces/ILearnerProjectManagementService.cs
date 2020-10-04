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
    /// 学习者项目管理接口
    /// </summary>
    public interface ILearnerProjectManagementService
    {
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="id">需要获取信息的项目ID</param>
        /// <returns>项目数据</returns>
        Task<LearnerProject> GetLearnerProjectAsync(int id);

        /// <summary>
        /// 获取所有项目信息
        /// </summary>
        /// <returns>所有项目信息</returns>
        Task<LearnerProject[]> GetAllLearnerProjectAsync();

        /// <summary>
        /// 创建新项目
        /// </summary>
        /// <param name="data">项目数据对象</param>
        /// <returns>新建项目的ID</returns>
        Task<int> CreateLearnerProjectAsync([NotNull] PureLearnerProject data);

        /// <summary>
        /// 更新项目信息
        /// </summary>
        /// <param name="update">项目更新数据对象</param>
        /// <returns>状态码</returns>
        Task<int> UpdateLearnerProjectAsync([NotNull] LearnerProjectUpdate update);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id">需要删除的项目的ID</param>
        /// <returns>状态码</returns>
        Task<int> DeleteLearnerProject(int id);
    }
}
