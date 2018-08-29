namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// 注册类型枚举类
    /// </summary>
    public enum UserRegistrationType
    {
        /// <summary>
        /// 标准帐户创建
        /// </summary>
        Standard = 1,
        /// <summary>
        /// 注册后需要电子邮件验证
        /// </summary>
        EmailValidation = 2,
        /// <summary>
        /// 用户应由管理员批准
        /// </summary>
        AdminApproval = 3,
        /// <summary>
        /// 注册被禁用
        /// </summary>
        Disabled = 4,
    }
}
