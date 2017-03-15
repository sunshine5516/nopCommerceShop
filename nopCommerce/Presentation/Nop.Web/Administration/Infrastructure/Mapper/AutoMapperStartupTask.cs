using Nop.Core.Infrastructure;

namespace Nop.Admin.Infrastructure.Mapper
{
    /// <summary>
    /// 启动自动映射器类
    /// </summary>
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            AutoMapperConfiguration.Init();
        }
        
        public int Order
        {
            get { return 0; }
        }
    }
}