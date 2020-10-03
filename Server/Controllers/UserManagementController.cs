using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgoraAcademy.AgoraEgo.Server.Interfaces;
using AgoraAcademy.AgoraEgo.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraAcademy.AgoraEgo.Server.Controllers
{
    /// <summary>
    /// 用户管理API控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        /// <summary>
        /// 依赖注入的逻辑层接口
        /// </summary>
        private readonly IUserManagementService managementService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="managementService">依赖注入的逻辑层接口</param>
        public UserManagementController(IUserManagementService managementService)
        {
            this.managementService = managementService;
        }

        /// <summary>
        /// 获取用户信息，将被开放至GET /api/usermanagement/{id}
        /// </summary>
        /// <param name="id">用户id，于请求主体内查找</param>
        /// <returns>用户信息或错误码</returns>
        [Authorize("read:user")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserData data = await managementService.GetUserDataAsync(id);
            if (data != null)
            {
                return Ok(data.ToPureUserData());
            }
            return Problem(statusCode: StatusCodes.Status404NotFound);

        }

        /// <summary>
        /// 创建用户，将被开放至POST /api/create
        /// </summary>
        /// <param name="data">用户数据</param>
        /// <returns>用户ID或错误码</returns>
        [Authorize("create:user")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PureUserData data)
        {
            int id = await managementService.CreateUserAsync(data);

            if (id == -1)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
            return Ok(new { id });
        }

        /// <summary>
        /// 更新用户信息，将被开放至PUT /api/edit
        /// </summary>
        /// <param name="update">代表用户数据更新的对象</param>
        /// <returns>代表执行情况的状态码</returns>
        [Authorize("edit:user")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] UserDataUpdate update)
        {
            return new StatusCodeResult(await managementService.UpdateUserDataAsync(update));
        }

        /// <summary>
        /// 删除用户，将被开放至 DELETE /api/delete/{id}
        /// </summary>
        /// <param name="id">需要被删除的用户ID</param>
        /// <returns></returns>
        [Authorize("delete:user")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return new StatusCodeResult(await managementService.DeleteUserAsync(id));
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>所有用户信息或404状态码，代表请求失败</returns>
        [HttpGet("get")]
        [Authorize("read:user")]
        public async Task<IActionResult> GetAll()
        {
            UserData[] datas = await managementService.GetAllUserDataAsync();

            if (datas != null)
            {
                return Ok(datas);
            }
            return Problem(statusCode: StatusCodes.Status404NotFound);
        }
    }
}
