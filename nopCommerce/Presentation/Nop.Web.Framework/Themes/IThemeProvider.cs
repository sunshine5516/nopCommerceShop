using System.Collections.Generic;

namespace Nop.Web.Framework.Themes
{
    public partial interface IThemeProvider
    {
        //获取指定主题配置信息
        ThemeConfiguration GetThemeConfiguration(string themeName);
        //获取全部主题的配置信息集合
        IList<ThemeConfiguration> GetThemeConfigurations();
        //判断主题是否存在
        bool ThemeConfigurationExists(string themeName);
    }
}
