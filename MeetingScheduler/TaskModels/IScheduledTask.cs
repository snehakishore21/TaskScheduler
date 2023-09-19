namespace MeetingScheduler.TaskModels
{
    public interface ExecutionContext
    {
        public void execute();
    }

    public  class ExecutionContextTask : ExecutionContext
    {
        private string name;
        public ExecutionContextTask(string nameInput)
        {
            name = nameInput;
        }
        public void execute()
        {
            Console.WriteLine("Executing task " + name);
        }
    }

    public abstract class ScheduledTask
    {
        public readonly ExecutionContext context;

        public ScheduledTask(ExecutionContext context)
        {
            this.context = context;
        }

        public abstract bool IsRecurring();

        public void Execute()
        {
            context.execute();
        }

        public abstract ScheduledTask NextScheduledTask();

        public abstract long GetNextExecutionTime();
    }
}