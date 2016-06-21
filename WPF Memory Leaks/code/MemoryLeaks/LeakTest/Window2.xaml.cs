using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Windows.Interop;


namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>

    public partial class Window2 : System.Windows.Window
    {
        public byte[] myMemory = new byte[50*1024*1024];    //allocate 50MB
        static BitmapImage bi1 = new BitmapImage(new Uri("Bitmap1.bmp", UriKind.RelativeOrAbsolute));
        static BitmapImage bi2 = new BitmapImage(new Uri("Bitmap2.bmp", UriKind.RelativeOrAbsolute));

        private static AutoResetEvent s_event = new AutoResetEvent(false);

        delegate void DispatcherDelegate();
        CommandBinding myCmdBinding;
        RoutedCommand command;
        Binding myDataBinding;
        Image m_Image1, m_Image2;

        public Window2()
        {
            InitializeComponent();

            UserNameLabel.Content = Window1.w1.TextBox1.Text;
            switch (Window1.w1.lb.SelectedIndex)
            {
                case 0: //use Event Handler. This leaks memory if event not cleared
                    CheckBox1.Visibility = Visibility.Visible;
                    Title = "Event Handler Test";
                    label2.Visibility = Visibility.Visible;
                    UserNameLabel.Visibility = Visibility.Visible;
                    //set event handler
                    Window1.w1.TextBox1.TextChanged += new TextChangedEventHandler(this.TextBox1_TextChanged);
                    break;
                case 1: //use data-binding. This leak memory
                    Title = "Data Binding Test";
                    CheckBox2.Visibility = Visibility.Visible;
                    MyTextBlock.Visibility = Visibility.Visible;
                    //Do below in code 
                    //   <TextBlock Name="MyTextBlock" Text="{Binding ElementName=myGrid, Path=Children.Count}" />
                    myDataBinding = new Binding("Children.Count");
                    myDataBinding.Source = myGrid;
                    myDataBinding.Mode = BindingMode.OneWay;
                    MyTextBlock.SetBinding(TextBlock.TextProperty, myDataBinding);

                    break;
                case 2: //use Command Binding. This leaks memory if not cleared
                    Title = "Command Binding Test";
                    labelF5.Visibility = Visibility.Visible;
                    UserNameLabel.Visibility = Visibility.Visible;
                    CheckBox3.Visibility = Visibility.Visible;
                    command = new RoutedCommand("ClearBox", this.GetType());
                    command.InputGestures.Add(new KeyGesture(Key.F5));
                    myCmdBinding = new CommandBinding(command, F5CommandExecute);
                    Window1.w1.CommandBindings.Add(myCmdBinding);    //add binding on Main window, so F5 will work when focus is in main window
                    break;

                case 3: //use static Event Handler. This leaks memory if event not cleared
                    Title = "Static Event Handler Test";
                    CheckBox4.Visibility = Visibility.Visible;
                    //set static event handler
                    Application.Current.Activated += Current_Activated;
                break;
            }         
        }

        private void Current_Activated(object sender, EventArgs e)
        {
           
        }

        Random r = new Random();
        int[] a = new int[900];

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var w = new WriteableBitmap(30, 30, 96, 96, PixelFormats.Bgr32, null);

            byte color = (byte)r.Next(255);
            for (int i = 0; i < 900; i++)
            {
                a[i] = color;
            }

            w.WritePixels(new Int32Rect(0, 0, 30, 30), a, 120, 0, 0);
            var im = new Image() { Height = 100, Width = 100, Source = w };
            this.Content = im;
        }

        private void OnCreateNewWindow1Click(object sender, RoutedEventArgs e)
        {
            Window1 win = new Window1();
            win.Show();
        }

      

        void w_Loaded(object sender, RoutedEventArgs e)
        {
            var source = HwndSource.FromHwnd(new WindowInteropHelper(sender as Window).Handle);
            var target = source.CompositionTarget as HwndTarget;
            target.RenderMode = RenderMode.SoftwareOnly;
        }

       

        private void Window_ThreadStart()
        {
            var win = new TimedWindow();
            win.Show();
            win.RunDispatcher();
        }

        private void F5CommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            // This will work as it is using the dispatcher
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, (DispatcherDelegate)delegate()
            {
                UserNameLabel.Content = Window1.w1.TextBox1.Text;
            });
        }
        private void TextBox1_TextChanged(object sender, RoutedEventArgs e)
        {
            //change text block on event
            UserNameLabel.Content  = ((TextBox)(e.OriginalSource)).Text;
        }

        
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {

            if (CheckBox1.IsChecked == true) // clear event ?
            {
                //if we don't clear this event we will leak memory!
                Window1.w1.TextBox1.TextChanged -= new TextChangedEventHandler(TextBox1_TextChanged);
            }

            if (CheckBox2.IsChecked == true) // clear data binding ?
            {
                if (myDataBinding != null)
                    //if we don't clear this binding, we will leak memory!
                    BindingOperations.ClearBinding(MyTextBlock, TextBlock.TextProperty);
            }

            if (CheckBox3.IsChecked == true) // clear command binding ?
            {
                if (myCmdBinding != null)
                    //if we don't clear this command binding, we will leak memory!
                    Window1.w1.CommandBindings.Remove(myCmdBinding); //remove command-binding if one exists
            }

            if (CheckBox4.IsChecked == true) // clear static event ?
            {
                //if we don't clear this event, we will leak memory!
                Application.Current.Activated -= new EventHandler(Current_Activated);
            }

            CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
        }

    }
}