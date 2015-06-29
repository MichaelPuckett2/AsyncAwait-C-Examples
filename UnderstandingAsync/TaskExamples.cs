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
            return await SubTaskForTaskFromResult("String_WithAsynLabel");
        }

        static async public Task<String> String_WithTaskFromResultAsWhole()
        {
            return await Task.FromResult<String>(await SubTaskForTaskFromResult("String_WithTaskFromResultAsWhole"));
        }

        
        static public async Task<String> String_WithTaskRun() //This is the only task that appears to work as it should.
        {
            //This method actually runs on a background thread where the others are continuations of the main thread. !!Important
            return await Task.Run<String>(async () =>
            {
                return await SubTaskForTaskFromResult("String_WithTaskRun");            
            });
        }

        static public async Task<String> String_WithTaskFromResultAtReturn()
        {
            return await Task.FromResult<String>(await SubTaskForTaskFromResult("String_WithTaskFromResultAtReturn"));
        }

      
        static private async Task<String> SubTaskForTaskFromResult(String taskName)
        {
            //System.Threading.Thread.Sleep(sleepTime);  //When sleeping here the only time it works is if we use the background thread. ... Task.Run(..

            await Task.Delay(sleepTime);  //When sleeping here all methods continue because this sleeps on a background thread I'm assuming.

            return String.Format("Task {0}:  {1}",
                ++TaskCounter,
                taskName);
        }

    }
}
