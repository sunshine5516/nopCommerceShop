using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Services.Events;

namespace Nop.Services.Messages
{
    public partial class EmailAccountService : IEmailAccountService
    {
        private readonly IRepository<EmailAccount> _emailAccountRepository;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="emailAccountRepository">Email account repository</param>
        /// <param name="eventPublisher">Event published</param>
        public EmailAccountService(IRepository<EmailAccount> emailAccountRepository,
            IEventPublisher eventPublisher)
        {
            this._emailAccountRepository = emailAccountRepository;
            this._eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Inserts an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void InsertEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            emailAccount.Email = CommonHelper.EnsureNotNull(emailAccount.Email);
            emailAccount.DisplayName = CommonHelper.EnsureNotNull(emailAccount.DisplayName);
            emailAccount.Host = CommonHelper.EnsureNotNull(emailAccount.Host);
            emailAccount.Username = CommonHelper.EnsureNotNull(emailAccount.Username);
            emailAccount.Password = CommonHelper.EnsureNotNull(emailAccount.Password);

            emailAccount.Email = emailAccount.Email.Trim();
            emailAccount.DisplayName = emailAccount.DisplayName.Trim();
            emailAccount.Host = emailAccount.Host.Trim();
            emailAccount.Username = emailAccount.Username.Trim();
            emailAccount.Password = emailAccount.Password.Trim();

            emailAccount.Email = CommonHelper.EnsureMaximumLength(emailAccount.Email, 255);
            emailAccount.DisplayName = CommonHelper.EnsureMaximumLength(emailAccount.DisplayName, 255);
            emailAccount.Host = CommonHelper.EnsureMaximumLength(emailAccount.Host, 255);
            emailAccount.Username = CommonHelper.EnsureMaximumLength(emailAccount.Username, 255);
            emailAccount.Password = CommonHelper.EnsureMaximumLength(emailAccount.Password, 255);

            _emailAccountRepository.Insert(emailAccount);

            //event notification
            _eventPublisher.EntityInserted(emailAccount);
        }

        /// <summary>
        /// Updates an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void UpdateEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            emailAccount.Email = CommonHelper.EnsureNotNull(emailAccount.Email);
            emailAccount.DisplayName = CommonHelper.EnsureNotNull(emailAccount.DisplayName);
            emailAccount.Host = CommonHelper.EnsureNotNull(emailAccount.Host);
            emailAccount.Username = CommonHelper.EnsureNotNull(emailAccount.Username);
            emailAccount.Password = CommonHelper.EnsureNotNull(emailAccount.Password);

            emailAccount.Email = emailAccount.Email.Trim();
            emailAccount.DisplayName = emailAccount.DisplayName.Trim();
            emailAccount.Host = emailAccount.Host.Trim();
            emailAccount.Username = emailAccount.Username.Trim();
            emailAccount.Password = emailAccount.Password.Trim();

            emailAccount.Email = CommonHelper.EnsureMaximumLength(emailAccount.Email, 255);
            emailAccount.DisplayName = CommonHelper.EnsureMaximumLength(emailAccount.DisplayName, 255);
            emailAccount.Host = CommonHelper.EnsureMaximumLength(emailAccount.Host, 255);
            emailAccount.Username = CommonHelper.EnsureMaximumLength(emailAccount.Username, 255);
            emailAccount.Password = CommonHelper.EnsureMaximumLength(emailAccount.Password, 255);

            _emailAccountRepository.Update(emailAccount);

            //event notification
            _eventPublisher.EntityUpdated(emailAccount);
        }

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public virtual void DeleteEmailAccount(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            if (GetAllEmailAccounts().Count == 1)
                throw new NopException("You cannot delete this email account. At least one account is required.");

            _emailAccountRepository.Delete(emailAccount);

            //event notification
            _eventPublisher.EntityDeleted(emailAccount);
        }

        /// <summary>
        /// 根据ID获取账号
        /// </summary>
        /// <param name="emailAccountId">The email account identifier</param>
        /// <returns>Email account</returns>
        public virtual EmailAccount GetEmailAccountById(int emailAccountId)
        {
            if (emailAccountId == 0)
                return null;

            return _emailAccountRepository.GetById(emailAccountId);
        }

        /// <summary>
        /// 获取所有电子邮件账号
        /// </summary>
        /// <returns>Email accounts list</returns>
        public virtual IList<EmailAccount> GetAllEmailAccounts()
        {
            var query = from ea in _emailAccountRepository.Table
                        orderby ea.Id
                        select ea;
            var emailAccounts = query.ToList();
            return emailAccounts;
        }

        public virtual IList<EmailAccount> GetEmailAccountsByIds(int[] emailAccountIds)
        {
            if (emailAccountIds == null || emailAccountIds.Length == 0)
                return new List<EmailAccount>();
            var query = from e in _emailAccountRepository.Table
                        where emailAccountIds.Contains(e.Id)
                        select e;
            var emails = query.ToList();
            var sortEmails = new List<EmailAccount>();
            foreach (int id in emailAccountIds)
            {
                var emailAccount = emails.Find(x => x.Id == id);
                if (emailAccount != null)
                {
                    sortEmails.Add(emailAccount);
                }
            }
            return sortEmails;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="emailAccounts"></param>
        public virtual void DeleteEmailAccounts(IList<EmailAccount> emailAccounts)
        {
            if (emailAccounts == null)
            {
                throw new ArgumentNullException("emailAccounts");
            }
            _emailAccountRepository.Delete(emailAccounts);
            foreach (var account in emailAccounts)
            {
                //event notification
                _eventPublisher.EntityDeleted(account);
            }
            //throw new NotImplementedException();
        }

        public IPagedList<EmailAccount> GetAllPagedList(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _emailAccountRepository.Table.OrderBy(ea => ea.Id);
            var emailAccount = new PagedList<EmailAccount>(query, pageIndex, pageSize);
            return emailAccount;
        }
        //public virtual void UpdateProducts(IList<EmailAccount> emailAccounts)
        //{
        //    if (emailAccounts == null)
        //        throw new ArgumentNullException("emailAccounts");
        //    _emailAccountRepository.Update(emailAccounts);
        //    //event notification
        //    foreach (var account in emailAccounts)
        //    {
        //        _eventPublisher.EntityUpdated(account);
        //    }
        //}
    }
}
