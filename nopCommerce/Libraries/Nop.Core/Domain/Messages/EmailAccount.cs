using System;

namespace Nop.Core.Domain.Messages
{
    /// <summary>
    /// 电子邮件账号
    /// </summary>
    public partial class EmailAccount : BaseEntity
    {
        /// <summary>
        /// 电子邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 电子邮件主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 电子邮件端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 该值控制SmtpClient是否使用安全套接字层（SSL）来加密连接
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值控制应用程序的默认系统凭据是否与请求一起发送。
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets a friendly email account name
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.DisplayName))
                    return this.Email + " (" + this.DisplayName + ")";
                return this.Email;
            }
        }
    }
}
