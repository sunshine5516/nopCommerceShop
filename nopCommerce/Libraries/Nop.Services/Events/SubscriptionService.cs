using System.Collections.Generic;
using Nop.Core.Infrastructure;

namespace Nop.Services.Events
{
    /// <summary>
    /// 事件订阅服务
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Event consumers</returns>
        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            return EngineContext.Current.ResolveAll<IConsumer<T>>();
        }
    }
}
