using System.Threading.Tasks;

namespace UnderstandingAsync
{
    static public class TaskExamples
    {

        static int TaskCounter = 0;

        static int sleepTime = 1000;
        static public int SleepTime
        {
            get { return sleepTime; }
            set { sleepTime = value; }
        }
        

        static public Task<string> String_WithAsynLabel() 
            => SubTaskForTaskFromResult(nameof(String_WithAsynLabel));

        static async public Task<string> String_WithTaskFromResultAsWhole()
            => await Task.FromResult(await SubTaskForTaskFromResult(nameof(String_WithTaskFromResultAsWhole)));

        
        static public Task<string> String_WithTaskRun() //This is the only task that appears to work as it should.
            //This method actually runs on a background thread where the others are continuations of the main thread. !!Important
            => Task.Run(async () => await SubTaskForTaskFromResult(nameof(String_WithTaskRun)));

     
        static private async Task<string> SubTaskForTaskFromResult(string taskName)
        {
            //System.Threading.Thread.Sleep(sleepTime);  //When sleeping here the only time it works is if we use the background thread. ... Task.Run(..

            await Task.Delay(sleepTime);  //When sleeping here all methods continue because this sleeps on a background thread I'm assuming.

            return $"Task {++TaskCounter}:  {taskName}";
        }

        /* NOTE:  In the previous push of this repo I had all Task methods marked with async and they were awaiting the task SubTaskForTaskFromResult.
         * This didn't work out and the only task that was truly awaited was String_WithTaskRun which returns Task.Run(...
         * After updating the code to simply await the Task I now understand that the Task is executed on await and processed seperately than the original call.
         * I can see this in the MainWindow code behind where I am calling each method via await for button press.  The UI timer will continue to run for all tasks
         * although before it was only running for String_WithTask.
         * Look at the second commit in this repo to see the difference.
         * */

    }
}
