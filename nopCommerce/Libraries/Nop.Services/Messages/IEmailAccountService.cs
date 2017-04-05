using System.Collections.Generic;
using Nop.Core.Domain.Messages;

namespace Nop.Services.Messages
{
    public partial interface IEmailAccountService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        void InsertEmailAccount(EmailAccount emailAccount);

        /// <summary>
        /// 更新电子邮件
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        void UpdateEmailAccount(EmailAccount emailAccount);

        /// <summary>
        /// 删除电子邮件
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        void DeleteEmailAccount(EmailAccount emailAccount);
        /// <summary>
        /// 批量删除电子邮件账号
        /// </summary>
        /// <param name="products">Products</param>
        void DeleteEmailAccounts(IList<EmailAccount> emailAccounts);

        /// <summary>
        /// 根据id获取电子邮件
        /// </summary>
        /// <param name="emailAccountId">The email account identifier</param>
        /// <returns>Email account</returns>
        EmailAccount GetEmailAccountById(int emailAccountId);
        /// <summary>
        /// 根据获取所有的商品
        /// </summary>
        /// <param name="emailAccountIds"></param>
        /// <returns></returns>
        IList<EmailAccount> GetEmailAccountsByIds(int[] emailAccountIds);
        /// <summary>
        /// 获取所有电子邮件账号
        /// </summary>
        /// <returns>Email accounts list</returns>
        IList<EmailAccount> GetAllEmailAccounts();
    }
}
