using MeetingScheduler.Database;
using MeetingScheduler.TaskModels;

public class TaskRunner 
{
    private readonly ITaskStore<ScheduledTask> taskStore;
    private bool running;

    public TaskRunner(ITaskStore<ScheduledTask> taskStore)
    {
        this.taskStore = taskStore ?? throw new ArgumentNullException(nameof(taskStore));
    }

   
    public void Run()
    {
        running = true;
        while (running)
        {
           
            {
                try
                {
                    ScheduledTask scheduledTask = taskStore.Poll();
                    if (scheduledTask == null)
                    {
                        break;
                    }
                    else
                    {
                        scheduledTask.Execute();
                        if (scheduledTask.IsRecurring())
                        {
                            taskStore.Add(scheduledTask.NextScheduledTask());
                        }
                    }
                }
                finally
                {
                   
                }
            }

        }
    }

    public void Stop()
    {
        running = false;
    }
}
