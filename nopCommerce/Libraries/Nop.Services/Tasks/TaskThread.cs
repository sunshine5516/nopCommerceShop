using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Nop.Services.Tasks
{
    /// <summary>
    /// Represents task thread
    /// 任务线程管理类
    /// 此线程管理主要负责判断任务的执行状态，线程执行间隔时间及调用任务执行的主方法Execute，通过Timer定时器实现定时自动运行。
    /// </summary>
    public partial class TaskThread : IDisposable
    {
        private Timer _timer;
        private bool _disposed;
        private readonly Dictionary<string, Task> _tasks;

        internal TaskThread()
        {
            this._tasks = new Dictionary<string, Task>();
            this.Seconds = 10 * 60;
        }

        /// <summary>
        /// 从列表中读取任务并执行
        /// </summary>
        private void Run()
        {
            if (Seconds <= 0)
                return;

            this.StartedUtc = DateTime.UtcNow;
            this.IsRunning = true;
            foreach (Task task in this._tasks.Values)
            {
                task.Execute();
            }
            this.IsRunning = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void TimerHandler(object state)
        {
            this._timer.Change(-1, -1);
            ///Run方法会执行当前任务类中的Execute（）方法
            this.Run();
            if (this.RunOnlyOnce)
            {
                this.Dispose();
            }
            else
            {
                this._timer.Change(this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            if ((this._timer != null) && !this._disposed)
            {
                lock (this)
                {
                    this._timer.Dispose();
                    this._timer = null;
                    this._disposed = true;
                }
            }
        }

        /// <summary>
        /// 初始化定时器，创建了Timer对象，在定时的时间内会执行TimerHandler()方法体中内容
        /// </summary>
        public void InitTimer()
        {
            if (this._timer == null)
            {
                this._timer = new Timer(new TimerCallback(this.TimerHandler), null, this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// 添加任务到线程
        /// </summary>
        /// <param name="task">The task to be added</param>
        public void AddTask(Task task)
        {
            if (!this._tasks.ContainsKey(task.Name))
            {
                this._tasks.Add(task.Name, task);
            }
        }


        /// <summary>
        /// 运行任务的间隔（以秒为单位）
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 线程启动时间
        /// </summary>
        public DateTime StartedUtc { get; private set; }

        /// <summary>
        /// 线程是否运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Get a list of tasks
        /// </summary>
        public IList<Task> Tasks
        {
            get
            {
                var list = new List<Task>();
                foreach (var task in this._tasks.Values)
                {
                    list.Add(task);
                }
                return new ReadOnlyCollection<Task>(list);
            }
        }

        /// <summary>
        /// 获取运行任务的时间间隔
        /// </summary>
        public int Interval
        {
            get
            {
                return this.Seconds * 1000;
            }
        }

        /// <summary>
        /// 线程是否只运行一次（每个应用程序启动）
        /// </summary>
        public bool RunOnlyOnce { get; set; }
    }
}
