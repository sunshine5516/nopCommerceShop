using System;
using System.Linq;
using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using Nop.Services.Logging;

namespace Nop.Services.Events
{
    /// <summary>
    /// 事件发布，实现IEventPublisher接口
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly ISubscriptionService _subscriptionService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="subscriptionService"></param>
        public EventPublisher(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// 发布事件，通知订阅者
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="x">事件监听者</param>
        /// <param name="eventMessage">Event message</param>
        protected virtual void PublishToConsumer<T>(IConsumer<T> x, T eventMessage)
        {
            //Ignore not installed plugins
            var plugin = FindPlugin(x.GetType());
            if (plugin != null && !plugin.Installed)
                return;

            try
            {
                x.HandleEvent(eventMessage);
            }
            catch (Exception exc)
            {
                //错误日志
                var logger = EngineContext.Current.Resolve<ILogger>();
                try
                {
                    logger.Error(exc.Message, exc);
                }
                catch (Exception)
                {
                   
                }
            }
        }

        /// <summary>
        /// 根据位于其程序集中的某个类型找到一个插件描述符
        /// </summary>
        /// <param name="providerType">程序类型</param>
        /// <returns>Plugin descriptor</returns>
        protected virtual PluginDescriptor FindPlugin(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException("providerType");

            if (PluginManager.ReferencedPlugins == null)
                return null;

            foreach (var plugin in PluginManager.ReferencedPlugins)
            {
                if (plugin.ReferencedAssembly == null)
                    continue;

                if (plugin.ReferencedAssembly.FullName == providerType.Assembly.FullName)
                    return plugin;
            }

            return null;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="eventMessage">Event message</param>
        public virtual void Publish<T>(T eventMessage)
        {
            var subscriptions = _subscriptionService.GetSubscriptions<T>();
            subscriptions.ToList().ForEach(x => PublishToConsumer(x, eventMessage));
        }

    }
}
