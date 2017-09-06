using System.Collections.Generic;

namespace Nop.Services.Cms
{
    /// <summary>
    /// Widget服务接口
    /// </summary>
    public partial interface IWidgetService
    {
        /// <summary>
        /// 加载活动小部件
        /// </summary>
        /// <param name="storeId">仅在指定的存储中加载记录; 0加载所有记录</param>
        /// <returns>Widgets</returns>
        IList<IWidgetPlugin> LoadActiveWidgets(int storeId = 0);

        /// <summary>
        /// 加载活动小部件
        /// </summary>
        /// <param name="widgetZone">Widget zone</param>
        /// <param name="storeId">仅在指定的存储中加载记录; 0加载所有记录</param>
        /// <returns>Widgets</returns>
        IList<IWidgetPlugin> LoadActiveWidgetsByWidgetZone(string widgetZone, int storeId = 0);

        /// <summary>
        /// 按系统名称加载小部件
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found widget</returns>
        IWidgetPlugin LoadWidgetBySystemName(string systemName);

        /// <summary>
        /// 加载所有小部件
        /// </summary>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Widgets</returns>
        IList<IWidgetPlugin> LoadAllWidgets(int storeId = 0);
    }
}
