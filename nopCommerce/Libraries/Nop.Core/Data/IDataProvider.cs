
using System.Data.Common;

namespace Nop.Core.Data
{
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// 初始化连接工厂
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// 设置数据库初始化程序
        /// </summary>
        void SetDatabaseInitializer();

        /// <summary>
        /// 数据库初始化
        /// </summary>
        void InitDatabase();

        /// <summary>
        /// 是否支持存储过程
        /// </summary>
        bool StoredProceduredSupported { get; }

        /// <summary>
        /// 是否支持备份
        /// </summary>
        bool BackupSupported { get; }

        /// <summary>
        /// 获取支持数据库参数对象（由存储过程使用）
        /// </summary>
        /// <returns>Parameter</returns>
        DbParameter GetParameter();

        /// <summary>
        /// HASHBYTES功能的最大数据长度
        /// 0表示不支持
        /// </summary>
        /// <returns>Length of the data for HASHBYTES functions</returns>
        int SupportedLengthOfBinaryHash();
    }
}
