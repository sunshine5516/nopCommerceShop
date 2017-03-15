using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;

namespace Nop.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 获取、设置当前用户
        /// </summary>
        Customer CurrentCustomer { get; set; }
        /// <summary>
        /// 获取或设置原始客户（如果当前被盗用）
        /// </summary>
        Customer OriginalCustomerIfImpersonated { get; }
        /// <summary>
        /// 获取当前供应商（登录管理器）
        /// </summary>
        Vendor CurrentVendor { get; }

        /// <summary>
        ///获取或设置当前语言
        /// </summary>
        Language WorkingLanguage { get; set; }
        /// <summary>
        /// 获取或设置当前用户的使用货币
        /// </summary>
        Currency WorkingCurrency { get; set; }
        /// <summary>
        /// 获取或设置当前税率显示类型
        /// </summary>
        TaxDisplayType TaxDisplayType { get; set; }

        /// <summary>
        /// 是否在管理区域
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
