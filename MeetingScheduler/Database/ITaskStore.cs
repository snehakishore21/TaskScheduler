using MeetingScheduler.TaskModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Database
{
    public interface ITaskStore<T> where T : ScheduledTask
    {

        public T Peek();

        public T Poll();

        public void Add(T task);

        public bool Remove(T task);

        public Task<bool> IsEmpty();
    }
}
