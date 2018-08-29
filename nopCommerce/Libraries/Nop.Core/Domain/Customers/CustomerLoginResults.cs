namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// Represents the customer login result enumeration
    /// </summary>
    public enum CustomerLoginResults
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        Successful = 1,
        /// <summary>
        /// 客户不存在（电子邮件或用户名）
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// 帐户尚未激活
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
    }
}
