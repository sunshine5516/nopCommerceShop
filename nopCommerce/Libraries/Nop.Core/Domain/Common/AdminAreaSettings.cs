
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Common
{
    public class AdminAreaSettings : ISettings
    {
        /// <summary>
        /// 默认网格page size
        /// </summary>
        public int DefaultGridPageSize { get; set; }
        /// <summary>
        /// 弹出式网页页面大小（弹出式页面）
        /// </summary>
        public int PopupGridPageSize { get; set; }
        /// <summary>
        /// 逗号分隔的可用网格页面大小的列表
        /// </summary>
        public string GridPageSizes { get; set; }
        /// <summary>
        /// 富文本编辑器的附加设置
        /// </summary>
        public string RichEditorAdditionalSettings { get; set; }
        /// <summary>
        ///富文本编辑器是否支持javascript
        /// </summary>
        public bool RichEditorAllowJavaScript { get; set; }
        /// <summary>
        /// 是否应隐藏广告（新闻）
        /// </summary>
        public bool HideAdvertisementsOnAdminArea { get; set; }
        /// <summary>
        /// 最新消息的标题（管理区）
        /// </summary>
        public string LastNewsTitleAdminArea { get; set; }
    }
}