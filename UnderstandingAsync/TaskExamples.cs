using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingAsync
{
    static public class TaskExamples
    {

        static Int32 TaskCounter = 0;

        static Int32 sleepTime = 1000;
        static public Int32 SleepTime
        {
            get { return sleepTime; }
            set { sleepTime = value; }
        }
        

        static public async Task<String> String_WithAsynLabel()
        {
            return SubTaskForTaskFromResult("String_WithAsynLabel");
        }

        static async public Task<String> String_WithTaskFromResultAsWhole()
        {
            return await Task.FromResult<String>(SubTaskForTaskFromResult("String_WithTaskFromResultAsWhole"));
        }

        
        static public Task<String> String_WithTaskRun() //This is the only task that appears to work as it should.
        {
            return Task.Run<String>(() =>
            {
                return SubTaskForTaskFromResult("String_WithTaskRun");            
            });
        }

        static public Task<String> String_WithTaskFromResultAtReturn()
        {
            System.Threading.Thread.Sleep(sleepTime);

            return Task.FromResult<String>(SubTaskForTaskFromResult("String_WithTaskFromResultAtReturn"));
        }

      
        static private String SubTaskForTaskFromResult(String taskName)
        {
            System.Threading.Thread.Sleep(sleepTime);

            return String.Format("Task {0}:  {1}",
                ++TaskCounter,
                taskName);
        }

    }
}
