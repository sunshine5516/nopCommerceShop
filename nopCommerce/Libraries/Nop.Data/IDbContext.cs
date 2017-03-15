using System.Collections.Generic;
using System.Data.Entity;
using Nop.Core;

namespace Nop.Data
{
    /// <summary>
    /// 数据库上下文接口，定义了针对数据库最基本的一些操作，比如设置实体，保存实体，执行sql或存储过程等等。
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 获取DbSet
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>DbSet</returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 执行存储过程并在结束时加载实体列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">参数</param>
        /// <returns>Entities</returns>
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
            where TEntity : BaseEntity, new();

        /// <summary>
        /// sql查询 Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        /// <summary>
        ///执行一个指定 DDL/DML 命令
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="doNotEnsureTransaction">false - 事物未创建; true - 事务创建.</param>
        /// <param name="timeout"></param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 分离实体
        /// </summary>
        /// <param name="entity">Entity</param>
        void Detach(object entity);

        /// <summary>
        /// 获取或设置一个值，指示是否启用代理创建设置（在EF中使用）
        /// </summary>
        bool ProxyCreationEnabled { get; set; }

        /// <summary>
        /// 获取或设置一个值，指示是否启用自动检测更改设置（在EF中使用）
        /// </summary>
        bool AutoDetectChangesEnabled { get; set; }
    }
}
