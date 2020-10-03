using AgoraAcademy.AgoraEgo.Server.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Models
{
    /// <summary>
    /// 代表用户数据更新的数据结构
    /// </summary>
    public class UserDataUpdate
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }

#nullable enable

        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 用户电话号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 昵称，应当是在社区内（无论现实或是线上）被以此称呼均能认出是在叫自己且愿意回应的称呼
        /// 将会显示在用户名下方
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 用户身份，多于一个时以空格分隔
        /// </summary>
        public string? Role { get; set; }

#nullable disable
    }
}
