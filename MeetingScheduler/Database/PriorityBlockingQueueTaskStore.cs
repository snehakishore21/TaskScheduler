using MeetingScheduler.TaskModels;

namespace MeetingScheduler.Database
{
    public class PriorityBlockingQueueTaskStore : ITaskStore<ScheduledTask>
    {

        private ConcurrentPriorityQueue<ScheduledTask> taskQueue;
        private HashSet<ScheduledTask> tasks;

        public PriorityBlockingQueueTaskStore(Comparer<ScheduledTask> comparator, int queueSize)
        {
            this.taskQueue = new ConcurrentPriorityQueue<ScheduledTask>(comparator, queueSize);
            this.tasks = new HashSet<ScheduledTask>();
        }


        public void Add(ScheduledTask task)
        {
            taskQueue.Enqueue(task);
        }


        public ScheduledTask Poll()
        {
            ScheduledTask t;
            taskQueue.TryDequeue(out t);
            return t;
        }


        public ScheduledTask Peek()
        {
            return taskQueue.Peek();
        }


        public bool Remove(ScheduledTask task)
        {
            if (tasks.Contains(task))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Task<bool> IsEmpty()
        {
            return Task.FromResult(taskQueue.IsEmpty);
        }
    }
}
