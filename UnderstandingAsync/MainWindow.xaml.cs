using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UnderstandingAsync
{
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer = new System.Timers.Timer(10);

        public MainWindow()
        {
            InitializeComponent();

            TextBox_SleepTime.Text = TaskExamples.SleepTime.ToString();

            initializeTimer();          
        }

        private void initializeTimer()
        {
            timer = new System.Timers.Timer(10);

            timer.Elapsed += (s, e) =>
            {
                DateTime now = DateTime.Now;
                Application.Current.Dispatcher.Invoke(() => TextBlock_Timer.Text = $"{now.Second}.{now.Millisecond}");
            };

            Loaded += (s, e) => timer.Start();
            Closing += (s, e) => timer.Stop();
        }

        private async void buttonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if ((CheckBox_DisableButton.IsChecked).GetValueOrDefault())
                button.IsEnabled = false;

            var result = string.Empty;

            switch (button.Name)
            {
                case nameof(StringTaskButton):
                    result = await TaskExamples.StringTask();
                    break;

                case nameof(AsyncStringTaskButton):
                    result = await TaskExamples.AsyncStringTask();
                    break;

                case nameof(TaskDotRunButton):
                    result = await TaskExamples.StringTaskDotRunAsync();
                    break;

                default:
                    result = "No Result Found";
                    break;

            }

            ListBoxResults.Items.Add(result);

            button.IsEnabled = true;
        }    

        private async void RunAll_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (CheckBox_DisableButton.IsChecked.GetValueOrDefault())
                button.IsEnabled = false;

            var tasks = new List<Task<string>>()
            {
                TaskExamples.StringTask(),
                TaskExamples.StringTaskDotRunAsync(),
                TaskExamples.AsyncStringTask()
            };

            //Comment out here and foreach and uncomment tasks.ForEach.  The lambda expression gets returned to each call immediately and the ForEach becomes asynchronous.
            //The reason is there is no overload of ForEach that will return task such as ForEachAsync; that can be awaited.
            await Task.WhenAll(tasks);

            foreach (var task in tasks)
                ListBoxResults.Items.Add(task.Result);

            /* The ForEach lambda expression required it's own async / await as shown below.
            * If you uncomment the line below and comment out the foreach (var task...) above then you'll notice the button
            * is re-enabled before all tasks are complete.  Therefore we can see that the ForEach lambda expression is started and returned to this task
            * before it completes.  Both work but the button is re-enabled before completion. */

            //tasks.ForEach(async task => ListBoxResults.Items.Add(await task));

            button.IsEnabled = true;
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
            => ListBoxResults.Items.Clear();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBox_SleepTime.Text, out int sleepTime))
                TaskExamples.SleepTime = Convert.ToInt32(TextBox_SleepTime.Text);
            else
                TextBox_SleepTime.Text = TaskExamples.DefaultSleepTime.ToString();
        }
    }
}
