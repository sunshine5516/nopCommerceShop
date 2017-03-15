using System.Collections.Generic;
using System.Linq;

namespace Nop.Core.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// 通过ID获取实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        ///获取表
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 采用notracking方式获取表 
        /// 仅在仅对只读操作加载记录时使用它
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
