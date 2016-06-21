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
using System.Windows.Shapes;
using System.ComponentModel;


namespace TestWpfApp
{
    // This class implements INotifyPropertyChanged
    // to support one-way and two-way bindings
    // (such that the UI element updates when the source
    // has been changed dynamically)
    public class User : INotifyPropertyChanged
    {
        private string name;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;
        public User()
        {
        }
        public User(string value)
        {
            this.name = value;
        }
        public string UserName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("UserName");
            }
        }
        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>   
    public partial class Window1 : System.Windows.Window
    {
        public static Window1 w1;
        static System.Net.WebClient wc;
        int left = 100;
        public static bool shouldShutdown = true;
        public Window1()
        {
            InitializeComponent();
            w1 = this;

            // Bring in the web classes which we are going to use later. 
            // This is so that we can clearly see in Process Viewer if we leak or not. 
            // Note that you do not have to do that in your application. 
            // Make sure you have internet connection. 
            //try
            //{
            //    wc = new System.Net.WebClient();
            //    wc.DownloadString("http://www.microsoft.com/en/us/default.aspx");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "\nAre you connected to the internet ?", this.Title);
            //}

            this.Unloaded += new RoutedEventHandler(Window1_Unloaded);
         }
         private void OnBtn1Click(object sender, RoutedEventArgs e)
        {
            Window2 w2 = new Window2();
            w2.Left = left; // open each window slightly to the right. 
            left += 20;
            if (left > 1000)
                left = 0;
            w2.Show();
        }

        private void OnBtn2Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void Window1_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Window1.shouldShutdown)
            {
                Application.Current.Shutdown();
            }
            this.Unloaded -= new RoutedEventHandler(Window1_Unloaded);  // clear event
        }
    }
}