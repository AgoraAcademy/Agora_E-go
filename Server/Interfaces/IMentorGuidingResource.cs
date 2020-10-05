using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Interfaces
{
    /// <summary>
    /// 有指导导师的资源接口
    /// </summary>
    public interface IMentorGuidingResource
    {
        /// <summary>
        /// 导师ID
        /// </summary>
        int[] MentorID { get; }
    }
}
