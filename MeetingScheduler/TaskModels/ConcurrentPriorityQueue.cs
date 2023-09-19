using MeetingScheduler.TaskModels;
using System.Collections.Concurrent;
public class ConcurrentPriorityQueue<T>
{
    private PriorityQueue<ScheduledTask, long> queue = new PriorityQueue<ScheduledTask, long>();
    private readonly IComparer<T> comparer;
    private readonly Mutex mutexlock;
    private readonly int maxQueueSize;

    public ConcurrentPriorityQueue(IComparer<T> comparer, int maxQueueSize)
    {
        this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        this.maxQueueSize = maxQueueSize;
        this.mutexlock = new Mutex();
    }

    public void Enqueue(ScheduledTask item)
    {
        //Semaphores are used to implement critical sections, which are regions of code that must be executed by
        //only one process at a time. By using semaphores processes can coordinate access to shared resources, such as shared memory or I/O devices.
        //This is also known as a mutex lock.
        mutexlock.WaitOne();

        try
        {
                if (queue.Count == maxQueueSize)
                {
                    throw new InvalidOperationException("The queue is full.");
                }
                queue.Enqueue(item, item.GetNextExecutionTime());
        }
        catch { throw; }
        finally
        {
            mutexlock.ReleaseMutex();
        }
    }

    public bool TryDequeue(out ScheduledTask result)
    {
        return TryDequeue(Int32.MaxValue, out result);
    }

    public bool TryDequeue(int timeout, out ScheduledTask result)
    {
        result = default;
        if (!mutexlock.WaitOne())
        {
            return false;
        }

        try
        {
            if (queue.Count != 0)
            {
                result = queue.Dequeue();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch { throw; }
        finally
        {
            mutexlock.ReleaseMutex();
        }
    }

    public ScheduledTask Peek()
    {
        if (queue.Count != 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        return queue.Peek();
    }

    

    public bool IsEmpty => queue.Count == 0;
}
