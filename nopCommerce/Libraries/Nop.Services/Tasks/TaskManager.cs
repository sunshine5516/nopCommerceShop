using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Nop.Core.Infrastructure;

namespace Nop.Services.Tasks
{
    /// <summary>
    /// Represents task manager任务管理类
    /// 主要负责任务的初始化，添加到线程列表，任务的开始和停止
    /// </summary>
    public partial class TaskManager
    {
        private static readonly TaskManager _taskManager = new TaskManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();
        
        private const int _notRunTasksInterval = 60 * 30; //30 minutes

        private TaskManager()
        {
        }
        
        /// <summary>
        /// 初始化线程管理
        /// </summary>
        public void Initialize()
        {
            this._taskThreads.Clear();

            //var typeFinder = _containerManager.Resolve<ITypeFinder>();
            //var taskService = typeFinder.FindClassesOfType<IScheduleTaskService>();

            //taskService.

            var taskService = EngineContext.Current.Resolve<IScheduleTaskService>();
            var scheduleTasks = taskService
                .GetAllTasks()
                .OrderBy(x => x.Seconds)
                .ToList();

            //线程分组
            foreach (var scheduleTaskGrouped in scheduleTasks.GroupBy(x => x.Seconds))
            {
                //create a thread
                var taskThread = new TaskThread
                {
                    Seconds = scheduleTaskGrouped.Key
                };
                foreach (var scheduleTask in scheduleTaskGrouped)
                {
                    var task = new Task(scheduleTask);
                    taskThread.AddTask(task);
                }
                this._taskThreads.Add(taskThread);
            }

            //有时一个线程周期可以设置为几个小时（甚至几天）。
            //在这种情况下，它将运行的概率相当小（应用程序可以重新启动）
            //我们应该手动运行那些没有运行很长时间的任务
            var notRunTasks = scheduleTasks
                //找到“运行期”超过30分钟的任务
                .Where(x => x.Seconds >= _notRunTasksInterval)
                .Where(x => !x.LastStartUtc.HasValue || x.LastStartUtc.Value.AddSeconds(x.Seconds) < DateTime.UtcNow)
                .ToList();
            //为没有运行很长时间的任务创建一个线程
            if (notRunTasks.Any())
            {
                var taskThread = new TaskThread
                {
                    RunOnlyOnce = true,
                    Seconds = 60 * 5 //在应用程序启动后5分钟内运行这样的任务
                };
                foreach (var scheduleTask in notRunTasks)
                {
                    var task = new Task(scheduleTask);
                    taskThread.AddTask(task);
                }
                this._taskThreads.Add(taskThread);
            }
        }

        /// <summary>
        /// 启动任务管理器
        /// </summary>
        public void Start()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        /// <summary>
        /// 停止任务管理器
        /// </summary>
        public void Stop()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.Dispose();
            }
        }

        /// <summary>
        /// 获得一个任务管理的实例
        /// </summary>
        public static TaskManager Instance
        {
            get
            {
                return _taskManager;
            }
        }

        /// <summary>
        /// 获取此任务管理器的任务线程的列表
        /// </summary>
        public IList<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(this._taskThreads);
            }
        }
    }
}
