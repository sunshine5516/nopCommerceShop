using System.Collections.Generic;

namespace Nop.Services.Events
{
    /// <summary>
    /// 事件订阅服务
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Event consumers</returns>
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}
