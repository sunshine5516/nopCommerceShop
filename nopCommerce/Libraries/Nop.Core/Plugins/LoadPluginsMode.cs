namespace Nop.Core.Plugins
{
    /// <summary>
    /// Represents a mode to load plugins
    /// </summary>
    public enum LoadPluginsMode
    {
        /// <summary>
        /// 加载所有
        /// </summary>
        All = 0,
        /// <summary>
        /// 只加载安装的
        /// </summary>
        InstalledOnly = 10,
        /// <summary>
        /// 只加载未安装的
        /// </summary>
        NotInstalledOnly = 20
    }
}