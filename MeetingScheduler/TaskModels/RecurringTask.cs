using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MeetingScheduler.TaskModels.ScheduledTask;

namespace MeetingScheduler.TaskModels
{
    internal class RecurringTask : ScheduledTask
    {
        private long executionTime;
        private long interval;
        public RecurringTask(ExecutionContext context, long executionTime, long interval) : base(context)
        {
            this.executionTime = executionTime;
            this.interval = interval;
        }

        public override long GetNextExecutionTime()
        {
            return executionTime;
        }

        public override bool IsRecurring()
        {
            return true;
        }

        public override ScheduledTask NextScheduledTask()
        {
            return new RecurringTask(context, executionTime + interval, interval);
        }
    }
}
