using System;

namespace CS.Utils
{
    /// <summary>
    /// Task助手
    /// </summary>
    public class TaskHelper
    {
        /// <summary>  
        /// 将一个方法function异步运行，在执行完毕时执行回调callback  
        /// </summary>  
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是void</param>  
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void</param>  
        public static async void RunAsync(Action function, Action callback = null)
        {
            Func<System.Threading.Tasks.Task> taskFunc = () => System.Threading.Tasks.Task.Run(function);
            await taskFunc();
            callback?.Invoke();
        }

        /// <summary>  
        /// 将一个方法function异步运行，在执行完毕时执行回调callback  
        /// </summary>  
        /// <typeparam name="TResult">异步方法的返回类型</typeparam>  
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是TResult</param>  
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法参数为TResult，返回类型必须是void</param>  
        public static async void RunAsync<TResult>(Func<TResult> function, Action<TResult> callback = null)
        {
            Func<System.Threading.Tasks.Task<TResult>> taskFunc = () => System.Threading.Tasks.Task.Run(function);
            var rlt = await taskFunc();
            callback?.Invoke(rlt);
        }
    }
}