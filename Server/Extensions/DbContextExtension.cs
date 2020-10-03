using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Extensions
{
    /// <summary>
    /// 和数据库上下文有关的扩展方法
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// 附带异常处理的保存扩展方法
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文类</typeparam>
        /// <param name="context">数据库上下文对象</param>
        /// <returns>代表任务的<see cref="Task"/>对象</returns>
        public static async Task SaveWithExceptionHandlingAsync<TDbContext>(this TDbContext context)
            where TDbContext : DbContext
        {
            if (context == null)
            {
                return;
            }

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    // 尝试保存
                    await context.SaveChangesAsync();
                }
                // 捕捉更新失败异常
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    // 将原始值设为当前值以规避异常
                    foreach (EntityEntry entry in ex.Entries)
                    {
                        entry.OriginalValues.SetValues(entry.CurrentValues);
                    }
                }
            } while (saveFailed);
        }
    }
}
