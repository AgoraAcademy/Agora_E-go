using AgoraAcademy.AgoraEgo.Server.Authorization;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AgoraAcademy.AgoraEgo.Server.Models
{
    /// <summary>
    /// 不包含用户ID与Auth0用户ID的用户数据
    /// </summary>
    public class PureUserData
    {
        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        [NotNull]
        public string Email { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        [NotNull]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// 昵称，应当是在社区内（无论现实或是线上）被以此称呼均能认出是在叫自己且愿意回应的称呼
        /// 将会显示在用户名下方
        /// </summary>
        [NotNull]
        public string NickName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [NotNull]
        public string UserName { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 用户身份，多于一个时以空格分隔
        /// </summary>
        [NotNull]
        public string Role { get; set; }

        /// <summary>
        /// 转换为<see cref="UserData"/>类
        /// </summary>
        /// <param name="id">本应用内的用户ID</param>
        /// <param name="stringUserID">在Auth0的字符串用户ID</param>
        /// <returns><see cref="UserData"/>实例</returns>
        public UserData ToUserData(int id, string stringUserID)
        {
            return new UserData
            {
                ID = id,
                StringUserID = stringUserID,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Name = Name,
                NickName = NickName,
                UserName = UserName,
                BirthDate = BirthDate,
                Role = Role
            };
        }
    }
}
