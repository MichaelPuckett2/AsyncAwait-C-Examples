using System.Threading;
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

        //Task
        //This continues on the main thread.
        static public Task<string> StringTask() => SubTaskForTask(nameof(StringTask)); //Blocking

        //async Task
        static async public Task<string> AsyncStringTask() => await SubTaskForTask(nameof(AsyncStringTask)); //Blocking

        //Task.Run
        //This method actually runs on a background thread where the others are continuations of the main thread.
        static public Task<string> StringTaskDotRun() => Task.Run(() => SubTaskForTask(nameof(StringTaskDotRun))); //Non-Blocking

       /*This task is broken on purpose.
        * Notice that when you call the task it will continue processing on the main thread until for the 1st second.
        * Then it will lock the thread for another second.
        * This is because the task is running on the same thread that it is called on regardless of async await.
        * The only way to get this task to operate without blocking the calling thread (most like the UI / Main thread) is to use Task.Run
        * There are 3 wrapper methods above that call this task in various ways to illustrate the difference.
        */
        static private async Task<string> SubTaskForTask(string taskName)
        {
            await Task.Delay(sleepTime / 2);

            Thread.Sleep(sleepTime / 2);

            return $"Task {++TaskCounter}:  {taskName}";
        }
    }
}
