using System;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure.DependencyManagement;

namespace Nop.Core.Infrastructure
{
    /// <summary>
    /// 实现此接口的类可以充当组成Nop引擎的各种服务的门户。编辑功能，模块和实现通过此接口访问大多数Nop功能
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 容器管理
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// 初始化组件和插件。
        /// </summary>
        /// <param name="config">Config</param>
        void Initialize(NopConfig config);

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T Resolve<T>() where T : class;

        /// <summary>
        ///  解析依赖
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 解析所有的依赖关系
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        T[] ResolveAll<T>();
    }
}
