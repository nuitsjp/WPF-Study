using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonBrockingProcessingStudy
{
    public class AsyncTaskService
    {
        private IList<Task> Tasks { get; } = new SynchronizedCollection<Task>();

        public Task Run(Action action)
        {
            var task = Task.Run(action);
            task.GetAwaiter().OnCompleted(() => Tasks.Remove(task));
            Tasks.Add(task);
            return task;
        }

        public void WaitAll()
        {
            Task.WaitAll(Tasks.ToArray());
        }
    }
}