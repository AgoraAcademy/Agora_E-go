using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Interfaces
{
    /// <summary>
    /// 有拥有者的资源接口
    /// </summary>
    public interface IOwnedResource
    {
        /// <summary>
        /// 拥有者用户ID
        /// </summary>
        int OwnerID { get; }
    }
}
