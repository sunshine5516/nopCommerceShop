using System.Configuration;
using System.Runtime.CompilerServices;
using Nop.Core.Configuration;

namespace Nop.Core.Infrastructure
{
    /// <summary>
    /// 创建一个单例的NOPEngine
    /// </summary>
    public class EngineContext
    {
        #region 方法

        /// <summary>
        /// 初始化时会创建一个新的NopEngine，
        /// 参数false指定当NopEngine不为空时是否重新生成一个新的NopEngine。
        /// </summary>
        /// <param name="forceRecreate">是否创建新的工厂实例</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            /// NopEngine 使用单例模式，在整个程序运行期间存在一个实例，
            /// 代码首先会判断NopEngine是否为空，为空的话则根据web.config中配置的 NopConfig节点信息创建一个新的NopEngine实例，然后对该实例进行初始化操作
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new NopEngine();

                var config = ConfigurationManager.GetSection("NopConfig") as NopConfig;
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        ///替换单例中的实例对象
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取IEngine单例实例对象
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
