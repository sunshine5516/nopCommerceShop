using System.Collections.Generic;
using Nop.Core.Domain.Messages;

namespace Nop.Services.Messages
{
    /// <summary>
    /// 邮件发送接口
    /// </summary>
    public partial interface IEmailSender
    {
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="emailAccount">邮件账号</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="fromAddress">发件人地址</param>
        /// <param name="fromName">发件人姓名</param>
        /// <param name="toAddress">收件人地址</param>
        /// <param name="toName">收件人姓名</param>
        /// <param name="replyToAddress"></param>
        /// <param name="replyToName"></param>
        /// <param name="bcc">私密发送邮件</param>
        /// <param name="cc">抄送</param>
        /// <param name="attachmentFilePath">附件路径</param>
        /// <param name="attachmentFileName">附件名称</param>
        /// <param name="attachedDownloadId">附件ID</param>
        /// <param name="headers"></param>
        void SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
             string replyToAddress = null, string replyToName = null,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null,
            int attachedDownloadId = 0, IDictionary<string, string> headers = null);
    }
}
