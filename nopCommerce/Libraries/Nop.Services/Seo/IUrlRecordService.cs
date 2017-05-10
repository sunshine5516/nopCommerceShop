using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Seo;

namespace Nop.Services.Seo
{
    /// <summary>
    /// 提供有关URL记录的信息
    /// </summary>
    public partial interface  IUrlRecordService
    {
        /// <summary>
        /// 删除一个URL记录
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void DeleteUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 删除URL记录
        /// </summary>
        /// <param name="urlRecords">URL records</param>
        void DeleteUrlRecords(IList<UrlRecord> urlRecords);

        /// <summary>
        /// 获取URL记录
        /// </summary>
        /// <param name="urlRecordId">URL recordId</param>
        /// <returns>URL record</returns>
        UrlRecord GetUrlRecordById(int urlRecordId);

        /// <summary>
        /// 获取URL记录
        /// </summary>
        /// <param name="urlRecordIds">urlRecordIds</param>
        /// <returns>URL record</returns>
        IList<UrlRecord> GetUrlRecordsByIds(int [] urlRecordIds);

        /// <summary>
        /// 插入一个URL记录
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void InsertUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 更新一个URL记录
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void UpdateUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// 查找URL记录
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecord GetBySlug(string slug);

        /// <summary>
        /// 查找URL记录（缓存版本）。
        /// 此方法的工作方式与“GetBySlug”一样，但缓存结果。
        /// 因此，它仅用于框架中的性能优化
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        UrlRecordService.UrlRecordForCaching GetBySlugCached(string slug);

        /// <summary>
        /// 获取所有URL记录
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>URL records</returns>
        IPagedList<UrlRecord> GetAllUrlRecords(string slug = "", int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        string GetActiveSlug(int entityId, string entityName, int languageId);

        /// <summary>
        /// Save slug
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        /// <param name="languageId">Language ID</param>
        void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported;
    }
}