using System;
using System.Runtime.CompilerServices;

namespace EventsAndDelegates
{
    //public delegate void TaskProcessingHandler(string taskName);

    public class TaskProcessor
    {
        //public event TaskProcessingHandler TaskProcessing;
        public event EventHandler<string> TaskProcessing;
        //public event EventHandler TaskCompleted;


        private EventHandler _taskCompleted;

        public event EventHandler TaskCompleted
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _taskCompleted = (EventHandler)Delegate.Combine(_taskCompleted, value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _taskCompleted = (EventHandler)Delegate.Remove(_taskCompleted, value);
            }
        } 

        public void ProcessTask(string taskName, int count)
        {
            for (int i = 0; i < count; i++)
            {
                OnTaskProcessing(taskName);
            }

            OnTaskCompleted();
        }

        protected virtual void OnTaskProcessing(string taskName)
        {
            if (TaskProcessing != null)
            {
                TaskProcessing(this, taskName);
            }
        }

        protected virtual void OnTaskCompleted()
        {
            if (TaskCompleted != null)
            {
                TaskCompleted(this, EventArgs.Empty);
            }
        }
    }

    public class TaskEventArgs : EventArgs
    {
        public TaskEventArgs(string taskName)
        {
            TaskName = taskName;
        }

        public string TaskName { get; set; }
    }
}
