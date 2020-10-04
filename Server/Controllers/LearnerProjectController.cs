using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraAcademy.AgoraEgo.Server;
using AgoraAcademy.AgoraEgo.Server.Interfaces;
using AgoraAcademy.AgoraEgo.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraAcademy.AgoraEgo.Server.Controllers
{
    /// <summary>
    /// 学习者项目管理控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LearnerProjectController : Controller
    {
        /// <summary>
        /// 依赖注入的内部服务接口
        /// </summary>
        private readonly ILearnerProjectManagementService projectService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectService">依赖注入的内部服务接口</param>
        public LearnerProjectController(ILearnerProjectManagementService projectService)
        {
            this.projectService = projectService;
        }

        /// <summary>
        /// 获取项目信息，将被开放至GET /api/learnerproject/get/{id}
        /// </summary>
        /// <param name="id">学习者项目ID</param>
        /// <returns>项目信息或</returns>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            LearnerProject project = await projectService.GetLearnerProjectAsync(id);
            if (project != null)
            {
                return Ok(project);
            }
            return Problem(statusCode: StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// 获取所有项目信息，将被开放至 GET /api/learnerproject/get
        /// </summary>
        /// <returns>所有项目信息或错误码</returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            LearnerProject[] project = await projectService.GetAllLearnerProjectAsync();
            if (project != null)
            {
                return Ok(project);
            }
            return Problem(statusCode: StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// 更新项目信息，将被开放至POST /api/learnerproject/update
        /// </summary>
        /// <param name="update">项目更新对象</param>
        /// <returns>状态码</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] LearnerProjectUpdate update)
        {
            return new StatusCodeResult(await projectService.UpdateLearnerProjectAsync(update));
        }

        /// <summary>
        /// 创建新项目，将被开放至POST /api/learnerproject/create
        /// </summary>
        /// <param name="proj">新项目项目信息</param>
        /// <returns>新项目的项目ID</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PureLearnerProject proj)
        {
            int id = await projectService.CreateLearnerProjectAsync(proj);
            return Ok(new { id });

        }

        /// <summary>
        /// 删除项目，将被开放至DELETE /api/learnerproject/delete/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>状态码</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return new StatusCodeResult(await projectService.DeleteLearnerProject(id));

        }
    }
}
