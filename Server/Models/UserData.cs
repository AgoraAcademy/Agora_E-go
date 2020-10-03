using AgoraAcademy.AgoraEgo.Server.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Models
{
    /// <summary>
    /// 代表用户信息的数据模型
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// 本应用内的用户ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Auth0使用的字符串用户ID
        /// </summary>
        [NotNull]
        public string StringUserID { get; set; }

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
        public string Role { get; set; }

        /// <summary>
        /// 转换为<see cref="PureUserData"/>类
        /// </summary>
        /// <returns><see cref="PureUserData"/>实例</returns>
        public PureUserData ToPureUserData()
        {
            return new PureUserData
            {
                Email = Email,
                PhoneNumber = PhoneNumber,
                Name = Name,
                NickName = NickName,
                UserName = UserName,
                BirthDate = BirthDate,
                Role = Role
            };
        }

        /// <summary>
        /// 转换为除Auth0ID以外的数据集合
        /// </summary>
        /// <returns>排除Auth0ID后的数据集合</returns>
        public (int ID, string Email, string PhoneNumber, string Name, string NickName, string UserName, DateTime BirthDate, string Role) ExcludeAuth0ID()
        {
            return (ID, Email, PhoneNumber, Name, NickName, UserName, BirthDate, Role);
        }

        /// <summary>
        /// 应用输入的数据更新
        /// </summary>
        /// <param name="update">代表数据更新的对象</param>
        public void ApplyUpdate([NotNull] UserDataUpdate update)
        {
            Email = update.Email ?? Email;
            PhoneNumber = update.PhoneNumber ?? PhoneNumber;
            Name = update.Name ?? Name;
            NickName = update.NickName ?? NickName;
            UserName = update.UserName ?? UserName;
            BirthDate = update.BirthDate ?? BirthDate;
            Role = update.Role ?? Role;
        }
    }
}
