using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PictureBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch sw = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = TimeSpan.FromSeconds(1)
            };
            rot.BeginAnimation(RotateTransform.AngleProperty, da);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sw.Reset();
            sw.Start();
            string path = Environment.CurrentDirectory + @"\imgs";
            var files = Directory.GetFiles(path, "*.jpg");
            ObservableCollection<string> filePaths = new ObservableCollection<string>();

            int itemCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                foreach (var file in files)
                {
                    filePaths.Add(file);
                }
                itemCount += files.Length;
            }

            this.countLbl.Content = itemCount;
            this.Dispatcher.BeginInvoke(new Action(StopStopwatch), DispatcherPriority.ApplicationIdle);
            lb.ItemsSource = filePaths;
        }

        private void StopStopwatch()
        {
            sw.Stop();
            this.elapsedLbl.Content = sw.Elapsed;
        }

        
    }
}
