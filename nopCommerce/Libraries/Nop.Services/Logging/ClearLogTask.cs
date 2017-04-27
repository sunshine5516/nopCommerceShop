using Nop.Services.Tasks;

namespace Nop.Services.Logging
{
    /// <summary>
    /// 清除日志Task
    /// </summary>
    public partial class ClearLogTask : ITask
    {
        private readonly ILogger _logger;

        public ClearLogTask(ILogger logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute()
        {
            _logger.ClearLog();
        }
    }
}
