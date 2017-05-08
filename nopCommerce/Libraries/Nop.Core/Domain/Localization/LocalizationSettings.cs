using Nop.Core.Configuration;

namespace Nop.Core.Domain.Localization
{
    public class LocalizationSettings : ISettings
    {
        /// <summary>
        /// 默认管理区域语言Id
        /// </summary>
        public int DefaultAdminLanguageId { get; set; }

        /// <summary>
        ///是否使用图像进行语言选择
        /// </summary>
        public bool UseImagesForLanguageSelection { get; set; }

        /// <summary>
        /// 是否启用具有多种语言的SEO友好URL的值
        /// </summary>
        public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

        /// <summary>
        /// 是否应该检测客户区域的当前语言（浏览器设置）
        /// </summary>
        public bool AutomaticallyDetectLanguage { get; set; }

        /// <summary>
        /// 是否在应用程序启动时加载所有LocaleStringResource记录
        /// </summary>
        public bool LoadAllLocaleRecordsOnStartup { get; set; }

        /// <summary>
        /// 是否在应用程序启动时加载所有LocalizedProperty记录的值
        /// </summary>
        public bool LoadAllLocalizedPropertiesOnStartup { get; set; }

        /// <summary>
        /// 是否在应用程序启动时加载所有搜索引擎友好名称（slugs）
        /// </summary>
        public bool LoadAllUrlRecordsOnStartup { get; set; }

        /// <summary>
        ///是否应忽略管理区域的RTL语言属性的值。
        /// It's useful for store owners with RTL languages for public store but preferring LTR for admin area
        /// </summary>
        public bool IgnoreRtlPropertyForAdminArea { get; set; }
    }
}