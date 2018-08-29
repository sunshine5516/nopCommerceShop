using Autofac;
using Autofac.Core;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
    /// 名    称：DependencyRegistrar
    /// 功    能：服务注册
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：DependencyRegistrar.cs
    /// 创建时间：2017-08-08 03:07
    /// 修改时间：2017-08-09 03:43
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<WeiXinExternalAuthService>().As<IWeiXinExternalAuthService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static")).InstancePerLifetimeScope();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 1; }
        }
    }
}
