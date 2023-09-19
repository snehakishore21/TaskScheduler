namespace MeetingScheduler.TaskModels
{
    public class OneTimeTask : ScheduledTask
    {
        private readonly long executionTime;

        public OneTimeTask(ExecutionContext context, long executionTime) : base(context)
        {
            this.executionTime = executionTime;
        }

        public override long GetNextExecutionTime()
        {
            return executionTime;
        }

        public override bool IsRecurring()
        {
            return false;
        }

        public override ScheduledTask? NextScheduledTask()
        {
            return null;
        }
    }
}
