using System;

namespace Nop.Core.Domain.Messages
{
    /// <summary>
    /// 发送的队列邮件
    /// </summary>
    public partial class QueuedEmail : BaseEntity
    {
        /// <summary>
        /// 优先等级
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        ///发件方(电子邮件地址)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 发件方姓名
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// 收件方(电子邮件地址)
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 收件方姓名
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the ReplyTo property (email address)
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the ReplyToName property
        /// </summary>
        public string ReplyToName { get; set; }

        /// <summary>
        /// 转发邮件
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 私密发送邮件  
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件文件路径
        /// </summary>
        public string AttachmentFilePath { get; set; }

        /// <summary>
        /// 附件文件名。 如果指定，则该文件名将被发送给收件人。 否则，将使用“AttachmentFilePath”名称。
        /// </summary>
        public string AttachmentFileName { get; set; }

        /// <summary>
        /// 附件文件的下载Id
        /// </summary>
        public int AttachedDownloadId { get; set; }

        /// <summary>
        /// 添加时间（UTC）
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 不应发送此电子邮件的日期和时间
        /// </summary>
        public DateTime? DontSendBeforeDateUtc { get; set; }

        /// <summary>
        /// 发送次数
        /// </summary>
        public int SentTries { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SentOnUtc { get; set; }

        /// <summary>
        /// EmailAccountId
        /// </summary>
        public int EmailAccountId { get; set; }

        /// <summary>
        /// 电子邮件账号
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; }


        /// <summary>
        /// 获取或设置优先级
        /// </summary>
        public QueuedEmailPriority Priority
        {
            get
            {
                return (QueuedEmailPriority)this.PriorityId;
            }
            set
            {
                this.PriorityId = (int)value;
            }
        }

    }
}
