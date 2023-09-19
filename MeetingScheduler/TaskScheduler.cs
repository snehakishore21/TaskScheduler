using MeetingScheduler.Database;
using MeetingScheduler.TaskModels;
using System.Diagnostics.Metrics;

namespace MeetingScheduler
{
    class MyCounter
    {
        public static Mutex MuTexLock = new Mutex();
    }

    public class TaskScheduler
    {
        private List<Thread> threads;

        private ITaskStore<ScheduledTask> taskStore;

        //So this ensures that once a thread has locked a piece of code then no other
        //thread can execute the same region until it is unlocked by the thread who locked it.
        public TaskScheduler(ExecutorConfig executorConfig, ITaskStore<ScheduledTask> taskStoreInput)
        {
            this.threads = new ();
            this.taskStore = taskStoreInput;
            for (int i = 0; i < executorConfig.NumThreads; i++)
            {
                MyCounter.MuTexLock.WaitOne();
                Thread thread = new Thread(new TaskRunner(taskStore).Run);
                thread.Start();
                threads.Add(thread);

                MyCounter.MuTexLock.ReleaseMutex();
            }
        }

        public void stop()
        {
            threads.ForEach(t=> t.Abort());
        }
    }
}
