// See https://aka.ms/new-console-template for more information


/*Design location feed for ubereats.
Restaurant data needs to be feed in the database along with its location.
Write API to get the list of nearby restaurant feeds with pagination based on current location.
More focus was on designing the location database for update and fetching the list of restaurant based on current location.
I have tried using the quad tree.But somehow interviewer was not convinced.*/
//Design social network network graph, and quering system.
//Design group chat feature of whatsapp.
//Design question (an API that picks up billing amounts from various cloud platforms, aggregates and presents it to the User. Event driven approach to be used. )

//Design a logging application like Logger. Did this round very badly. I wasn't entirely sure how to approach it and I came up with a sub-optimal design.
//Machine Coding Round: Design a multithreaded job scheduler. A job can have multiple tasks, and each task can have a pre-requisite task as well. Jobs should run in as much parallelization as possible.
//Design google auto compelete system in very detail. From user search to Trie update.Database to aggregator, ML algorithms to mapreduce. You name it. If your system design sill is rusty, you will be toasted througoutly.
//LLD of Kafka (Message Queue) (Discussed and implemented Publisher, Subscriber and Topic classes using Observer Design Pattern, but wasn't able to implement offset logic for multiple subscribers of a topic.) 

//1.Meeting room 2
//2.Verify Alien dictionary
//3.User1 Blgr COMPLETED timestamp4
//  The input stream is not ordered based on timestamp, as in an event occuring later can come first. You have to put this in a data structure to support timestamp based queries, like Give list of RIDE_REQUESTED for bglr city from [timestamp1,.. timestamp5]

using MeetingScheduler.Database;
using MeetingScheduler.TaskModels;
using System.Numerics;

namespace MeetingScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecutorConfig executorConfig = new ExecutorConfig(3);
            var comparer = Comparer<ScheduledTask>.Create((x, y) => x.GetNextExecutionTime().CompareTo(y.GetNextExecutionTime()));
            ITaskStore<ScheduledTask> taskStore = new PriorityBlockingQueueTaskStore(comparer, 9);
            
            ExecutionContextTask executionContextTask = new ExecutionContextTask("Task1");
            ScheduledTask task = new OneTimeTask(executionContextTask, 9000);
            taskStore.Add(task);

            ExecutionContextTask executionContextTask2 = new ExecutionContextTask("Task2");
            ScheduledTask task2 = new OneTimeTask(executionContextTask2, 2000);
            taskStore.Add(task2);

            ExecutionContextTask executionContextTask4 = new ExecutionContextTask("Task4");
            ScheduledTask task4 = new OneTimeTask(executionContextTask4, 2000);
            taskStore.Add(task4);

            ExecutionContextTask executionContextTask3 = new ExecutionContextTask("Task3");
            ScheduledTask task3 = new RecurringTask(executionContextTask3, 3000, 10000);
            taskStore.Add(task3);

            TaskScheduler t=  new TaskScheduler(executorConfig, taskStore);
            
        }
    }
}