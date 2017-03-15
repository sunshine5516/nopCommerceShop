namespace Nop.Core.Plugins
{
    /// <summary>
    /// 插件的操作接口
    /// Interface denoting plug-in attributes that are displayed throughout 
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 设置插件的属性信息
        /// </summary>
        PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// 安装插件接口
        /// </summary>
        void Install();

        /// <summary>
        /// 卸载插件接口
        /// </summary>
        void Uninstall();
    }
}
