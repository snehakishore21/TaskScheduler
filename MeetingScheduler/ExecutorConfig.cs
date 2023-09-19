using System.Xml.Linq;

namespace MeetingScheduler
{
    public class ExecutorConfig
    {
        private int numThreads;

        public ExecutorConfig(int numThreads)
        {
            this.NumThreads = numThreads;
        }

        public int NumThreads 
        {
            get { return numThreads; }  
            set { numThreads = value; }
        }
      
    }
}
