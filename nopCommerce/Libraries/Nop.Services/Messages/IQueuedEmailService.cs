using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Messages;

namespace Nop.Services.Messages
{
    /// <summary>
    /// 邮件队列接口
    /// </summary>
    public partial interface IQueuedEmailService
    {
        /// <summary>
        /// 插入电子邮件队列
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void InsertQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// 更新电子邮件队列
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void UpdateQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// 删除电子邮件队列
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void DeleteQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// 删除电子邮件队列
        /// </summary>
        /// <param name="queuedEmails">Queued emails</param>
        void DeleteQueuedEmails(IList<QueuedEmail> queuedEmails);

        /// <summary>
        /// 根据ID获取电子邮件队列
        /// </summary>
        /// <param name="queuedEmailId">QueuedEmailId</param>
        /// <returns>Queued email</returns>
        QueuedEmail GetQueuedEmailById(int queuedEmailId);

        /// <summary>
        /// 根据ID获取邮件
        /// </summary>
        /// <param name="queuedEmailIds">queued email identifiers</param>
        /// <returns>Queued emails</returns>
        IList<QueuedEmail> GetQueuedEmailsByIds(int[] queuedEmailIds);

        /// <summary>
        /// 搜索排队的电子邮件
        /// </summary>
        /// <param name="fromEmail">发送方</param>
        /// <param name="toEmail">收件方</param>
        /// <param name="createdFromUtc">创建时间; null加载所有</param>
        /// <param name="createdToUtc">创建时间; null加载所有</param>
        /// <param name="loadNotSentItemsOnly">是否只加载未发送的邮件</param>
        /// <param name="loadOnlyItemsToBeSent">是否只加载发送的</param>
        /// <param name="maxSendTries">最大尝试次数</param>
        /// <param name="loadNewest">是否排序排队的电子邮件降序; 否则上升。</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Queued emails</returns>
        IPagedList<QueuedEmail> SearchEmails(string fromEmail,
            string toEmail, DateTime? createdFromUtc, DateTime? createdToUtc, 
            bool loadNotSentItemsOnly, bool loadOnlyItemsToBeSent, int maxSendTries,
            bool loadNewest, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 删除所有排队的电子邮件
        /// </summary>
        void DeleteAllEmails();
    }
}
