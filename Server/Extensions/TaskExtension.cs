using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraAcademy.AgoraEgo.Server.Extensions
{
    /// <summary>
    /// 关于<see cref="Task{TResult}"/>对象的扩展方法
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// 类似<see cref="System.Linq"/>内的Select扩展方法，起到转换作用
        /// </summary>
        /// <typeparam name="TResult"><paramref name="task"/>原本的返回值类型</typeparam>
        /// <typeparam name="TNewResult">通过<paramref name="selector"/>转换后的返回值类型</typeparam>
        /// <param name="task">返回<typeparamref name="TResult"/>类型对象的<see cref="Task{TResult}"/>对象</param>
        /// <param name="selector">从<typeparamref name="TResult"/>对象到<typeparamref name="TNewResult"/>对象的映射方法</param>
        /// <returns>新<see cref="Task{TResult}"/>对象，在<paramref name="task"/>结束后开始执行，将<paramref name="task"/>的返回值用<paramref name="selector"/>转换为<typeparamref name="TNewResult"/>类型对象并返回</returns>
        public static Task<TNewResult> Select<TResult, TNewResult>(this Task<TResult> task, [NotNull] Func<TResult, TNewResult> selector)
        {
            return task.ContinueWith<TNewResult>(continuationFunction: (t) => selector.Invoke(t.Result));
        }
    }
}
