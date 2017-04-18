using System.Collections.Generic;
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Common
{
    public class CommonSettings : ISettings
    {
        public CommonSettings()
        {
            IgnoreLogWordlist = new List<string>();
        }

        public bool SubjectFieldOnContactUsForm { get; set; }
        public bool UseSystemEmailForContactUsForm { get; set; }

        public bool UseStoredProceduresIfSupported { get; set; }
        
        public bool SitemapEnabled { get; set; }
        public bool SitemapIncludeCategories { get; set; }
        public bool SitemapIncludeManufacturers { get; set; }
        public bool SitemapIncludeProducts { get; set; }

        /// <summary>
        /// 是否在禁用javascropt脚本时显示警告
        /// </summary>
        public bool DisplayJavaScriptDisabledWarning { get; set; }

        /// <summary>
        /// 是否支持全文搜索模式
        /// </summary>
        public bool UseFullTextSearch { get; set; }

        /// <summary>
        /// 获取或设置全文搜索模式
        /// </summary>
        public FulltextSearchMode FullTextMode { get; set; }

        /// <summary>
        /// 404错误是否要被记录
        /// </summary>
        public bool Log404Errors { get; set; }

        /// <summary>
        /// 获取或设置网站上使用的面包屑分隔符
        /// </summary>
        public string BreadcrumbDelimiter { get; set; }

        /// <summary>
        /// 是否呈现 <meta http-equiv="X-UA-Compatible" content="IE=edge"/> tag
        /// </summary>
        public bool RenderXuaCompatible { get; set; }

        /// <summary>
        /// 获取或设置“X-UA兼容”META标签的值
        /// </summary>
        public string XuaCompatibleValue { get; set; }

        /// <summary>
        /// 获取或设置忽略记录错误/消息时忽略的单词（短语）
        /// </summary>
        public List<string> IgnoreLogWordlist { get; set; }
    }
}