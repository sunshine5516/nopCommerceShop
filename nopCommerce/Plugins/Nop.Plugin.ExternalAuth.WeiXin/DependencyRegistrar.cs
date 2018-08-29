using Autofac;
using Nop.Plugin.ExternalAuth.WeiXin.Core;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.ExternalAuth.WeiXin
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<WeiXinProviderAuthorizer>().As<IOAuthProviderWeiXinAuthorizer>().InstancePerLifetimeScope();
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<WeiXinProviderAuthorizer>().As<IOAuthProviderWeiXinAuthorizer>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
