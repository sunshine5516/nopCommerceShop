namespace Nop.Core.Domain.Common
{
    /// <summary>
    /// 表示全文搜索模式
    /// </summary>
    public enum FulltextSearchMode
    {
        /// <summary>
        /// 完全匹配（使用CONTAINS with prefix_term）
        /// </summary>
        ExactMatch = 0,
        /// <summary>
        /// 使用CONTAINS和OR与prefix_term
        /// </summary>
        Or = 5,
        /// <summary>
        ///使用CONTAINS和AND与prefix_term
        /// </summary>
        And = 10
    }
}
