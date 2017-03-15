namespace Nop.Services.Tasks
{
    /// <summary>
    /// 该接口被每个 task继承
    /// 20170222 Jayson
    /// </summary>
    public partial interface ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        void Execute();
    }
}
