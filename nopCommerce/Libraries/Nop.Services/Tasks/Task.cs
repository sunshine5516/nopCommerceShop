using System;
using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Domain.Tasks;
using Nop.Core.Infrastructure;
using Nop.Services.Infrastructure;
using Nop.Services.Logging;

namespace Nop.Services.Tasks
{
    /// <summary>
    /// Task处理任务的执行过程及执行过程类的结果处理
    /// </summary>
    public partial class Task
    {
        #region 构造函数

        /// <summary>
        /// Ctor for Task
        /// </summary>
        private Task()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        public Task(ScheduleTask task)
        {
            this.Type = task.Type;
            this.Enabled = task.Enabled;
            this.StopOnError = task.StopOnError;
            this.Name = task.Name;
        }

        #endregion

        #region Utilities实用程序
        /// <summary>
        /// 创建对应的task实例
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        private ITask CreateTask(ILifetimeScope scope)
        {
            ITask task = null;
            if (this.Enabled)
            {
                var type2 = System.Type.GetType(this.Type);
                if (type2 != null)
                {
                    object instance;
                    if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                    {
                        //not resolved
                        instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                    }
                    task = instance as ITask;
                }
            }
            return task;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="throwException">如果发生某些错误，是否应抛出异常</param>
        /// <param name="dispose">在任务运行后是否应该处理所有实例的值</param>
        /// <param name="ensureRunOnOneWebFarmInstance"></param>
        public void Execute(bool throwException = false, bool dispose = true, bool ensureRunOnOneWebFarmInstance = true)
        {
            //background tasks has an issue with Autofac
            //because scope is generated each time it's requested
            //that's why we get one single scope here
            //this way we can also dispose resources once a task is completed
            var scope = EngineContext.Current.ContainerManager.Scope();
            var scheduleTaskService = EngineContext.Current.ContainerManager.Resolve<IScheduleTaskService>("", scope);
            var scheduleTask = scheduleTaskService.GetTaskByType(this.Type);

            try
            {
                //task is run on one farm node at a time?
                if (ensureRunOnOneWebFarmInstance)
                {
                    //is web farm enabled (multiple instances)?
                    var nopConfig = EngineContext.Current.ContainerManager.Resolve<NopConfig>("", scope);
                    if (nopConfig.MultipleInstancesEnabled)
                    {
                        var machineNameProvider = EngineContext.Current.ContainerManager.Resolve<IMachineNameProvider>("", scope);
                        var machineName = machineNameProvider.GetMachineName();
                        if (String.IsNullOrEmpty(machineName))
                        {
                            throw new Exception("Machine name cannot be detected. You cannot run in web farm.");
                            //actually in this case we can generate some unique string (e.g. Guid) and store it in some "static" (!!!) variable
                            //then it can be used as a machine name
                        }

                        //lease can't be aquired only if for a different machine and it has not expired
                        if (scheduleTask.LeasedUntilUtc.HasValue &&
                            scheduleTask.LeasedUntilUtc.Value >= DateTime.UtcNow &&
                            scheduleTask.LeasedByMachineName != machineName)
                            return;

                        //lease the task. so it's run on one farm node at a time
                        scheduleTask.LeasedByMachineName = machineName;
                        scheduleTask.LeasedUntilUtc = DateTime.UtcNow.AddMinutes(30);
                        scheduleTaskService.UpdateTask(scheduleTask);
                    }
                }

                //initialize and execute
                var task = this.CreateTask(scope);
                if (task != null)
                {
                    this.LastStartUtc = DateTime.UtcNow;
                    if (scheduleTask != null)
                    {
                        //update appropriate datetime properties
                        scheduleTask.LastStartUtc = this.LastStartUtc;
                        scheduleTaskService.UpdateTask(scheduleTask);
                    }
                    task.Execute();
                    this.LastEndUtc = this.LastSuccessUtc = DateTime.UtcNow;
                }
            }
            catch (Exception exc)
            {
                this.Enabled = !this.StopOnError;
                this.LastEndUtc = DateTime.UtcNow;

                //错误日志
                var logger = EngineContext.Current.ContainerManager.Resolve<ILogger>("", scope);
                logger.Error(string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
                if (throwException)
                    throw;
            }

            if (scheduleTask != null)
            {
                //update appropriate datetime properties
                scheduleTask.LastEndUtc = this.LastEndUtc;
                scheduleTask.LastSuccessUtc = this.LastSuccessUtc;
                scheduleTaskService.UpdateTask(scheduleTask);
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 最后一个开始的日期
        /// </summary>
        public DateTime? LastStartUtc { get; private set; }

        /// <summary>
        /// 最后一个的日期
        /// </summary>
        public DateTime? LastEndUtc { get; private set; }

        /// <summary>
        /// 最后成功的日期
        /// </summary>
        public DateTime? LastSuccessUtc { get; private set; }

        /// <summary>
        /// task类型
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// A value indicating whether to stop task on error
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }

        #endregion
    }
}
