namespace Nop.Web.Framework.Themes
{
    /// <summary>
    /// 主题上下文用于调用其它主题相关类，它是主题的入口，也是中心。
    /// </summary>
    public interface IThemeContext
    {
        /// <summary>
        /// Get or set current theme system name
        /// </summary>
        string WorkingThemeName { get; set; }
    }
}
