using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Authorization
{
    /// <summary>
    /// 授权需求类，用户需是指导导师，具体实现见<see cref="ResourceGuidingMentorAuthorizationHandler"/>
    /// </summary>
    public class GuidingMentorRequirement : IAuthorizationRequirement
    {

    }
}
