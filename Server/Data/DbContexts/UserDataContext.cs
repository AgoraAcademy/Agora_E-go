using AgoraAcademy.AgoraEgo.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Data.DbContexts
{
    /// <summary>
    /// 用户数据库上下文
    /// </summary>
    public class UserDataContext : DbContext
    {
        /// <summary>
        /// 用户数据集合
        /// </summary>
        public DbSet<UserData> UserDatas { get; set; }

        /// <summary>
        /// 接受Options对象的构造函数
        /// </summary>
        /// <param name="options">数据库上下文构建属性</param>
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {

        }
    }
}
