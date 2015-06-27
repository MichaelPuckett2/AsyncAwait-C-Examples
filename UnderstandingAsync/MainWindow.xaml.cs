using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnderstandingAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Elapsed += timer_Elapsed;
        }

        System.Timers.Timer timer = new System.Timers.Timer(10);

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            Application.Current.Dispatcher.Invoke(() =>
                {
                    this.TextBlock_Timer.Text = String.Format("{0}.{1}", now.Second, now.Millisecond);
                });
        }

        private async void String_WithAsynLabel_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = (Boolean)!CheckBox_DisableButton.IsChecked;
            this.ListBoxResults.Items.Add(await TaskExamples.String_WithAsynLabel());
            ((Button)sender).IsEnabled = true;
        }

        private async void String_WithTaskRun_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = (Boolean)!CheckBox_DisableButton.IsChecked;
            this.ListBoxResults.Items.Add(await TaskExamples.String_WithTaskRun());
            ((Button)sender).IsEnabled = true;

        }

        private async void String_WithTaskFromResultAtReturn_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = (Boolean)!CheckBox_DisableButton.IsChecked;

            this.ListBoxResults.Items.Add(await TaskExamples.String_WithTaskFromResultAtReturn());
            ((Button)sender).IsEnabled = true;

        }

        private async void String_WithTaskFromResultAsWhole_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = (Boolean)!CheckBox_DisableButton.IsChecked;

            this.ListBoxResults.Items.Add(await TaskExamples.String_WithTaskFromResultAsWhole());
            ((Button)sender).IsEnabled = true;

        }

        private void RunAll_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = (Boolean)!CheckBox_DisableButton.IsChecked;

            List<Task<String>> tasks = new List<Task<string>>()
            {
                TaskExamples.String_WithAsynLabel(),
                TaskExamples.String_WithTaskRun(),
                TaskExamples.String_WithTaskFromResultAtReturn(),
                TaskExamples.String_WithTaskFromResultAsWhole()
            };

            tasks.ForEach(async tsk => this.ListBoxResults.Items.Add(await tsk));

            ((Button)sender).IsEnabled = true;

        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            this.ListBoxResults.Items.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer.Stop();
            System.Threading.Thread.Sleep(200);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32 d = 1000;

            try
            {
                d = Convert.ToInt32(TextBox_SleepTime.Text);
            }
            catch (Exception)
            {
                this.TextBox_SleepTime.Text = "1000";
            }

            TaskExamples.SleepTime = d;
        }




    }
}
