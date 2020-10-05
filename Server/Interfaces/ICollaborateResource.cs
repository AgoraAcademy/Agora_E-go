using AgoraAcademy.AgoraEgo.Server.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Interfaces
{
    /// <summary>
    /// 合作使用的资源的接口
    /// </summary>
    public interface ICollaborateResource
    {
        /// <summary>
        /// 合作者ID
        /// </summary>
        int[] CollaboratorIDs { get; }
    }
}
