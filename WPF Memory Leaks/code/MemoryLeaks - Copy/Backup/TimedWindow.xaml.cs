using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for TimedWindow.xaml
    /// </summary>
    public partial class TimedWindow : Window
    {
        private delegate void NoArgDelegate();

        public TimedWindow()
        {
            InitializeComponent();

            this.Closed += new EventHandler(Window1_Closed);
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new NoArgDelegate(CloseWindow));
        }

        private void CloseWindow()
        {
            Thread.Sleep(10000);
            this.Close();
        }

        void Window1_Closed(object sender, EventArgs e)
        {
            this.Dispatcher.InvokeShutdown();

            this.Closed -= new EventHandler(Window1_Closed);
            this.Loaded -= new RoutedEventHandler(Window1_Loaded);
        }

        public void RunDispatcher()
        {
            if (!Dispatcher.CurrentDispatcher.HasShutdownStarted)
            {
                Dispatcher.Run();
            }
        }
    }
}
