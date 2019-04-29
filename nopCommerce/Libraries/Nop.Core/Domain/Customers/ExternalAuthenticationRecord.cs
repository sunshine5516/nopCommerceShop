namespace Nop.Core.Domain.Customers
{
    /// <summary>
    /// 外部身份验证记录
    /// </summary>
    public partial class ExternalAuthenticationRecord : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 外部身份验证
        /// </summary>
        public string ExternalIdentifier { get; set; }

        /// <summary>
        /// 外部身份验证名称
        /// </summary>
        public string ExternalDisplayIdentifier { get; set; }

        /// <summary>
        /// OAuthToken
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        /// OAuthAccessToken
        /// </summary>
        public string OAuthAccessToken { get; set; }

        /// <summary>
        /// provider名称
        /// </summary>
        public string ProviderSystemName { get; set; }
        
        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual Customer Customer { get; set; }
    }

}
