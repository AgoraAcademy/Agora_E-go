using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Authorization
{
    /// <summary>
    /// 授权需求类，用户需是资源所有者，具体实现见<see cref="ResourceOwnershipAuthorizationHandler"/>
    /// </summary>
    public class OwnershipRequirement : IAuthorizationRequirement
    {

    }
}
