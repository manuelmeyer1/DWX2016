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
                    Application.Current.Activated += new EventHandler(App_Activated);
                    break;

                case 4: //Use Image Source . This will leak memory if an un-frozen BitmapImage is used
                    Title = "Image Source Test";
                    if (bi2.CanFreeze)
                        bi2.Freeze();

                    CheckBox5.Visibility = Visibility.Visible;
                    CheckBox6.Visibility = Visibility.Visible;
                    break;

                case 5: // Use Image Source . 
                    // This sequence leaks memory since bi1 is static and it continues to have reference to m_Image1
                    // This issue was introduced with .Net 3.5. We plan to fix in the comming .net 3.5 Sp1
                    Title = "Image Source Test2";
                    if (bi2.CanFreeze)
                        bi2.Freeze();

                    m_Image1 = new Image();
                    m_Image1.Height = 50;
                    m_Image1.Width = 50;
                    m_Image1.Source = bi1;      // use un-frozen bitmap.
                    m_Image1.Source = bi2;      // use frozen bitmap.
                    MyStackPanel.Children.Add(m_Image1);
                    break;
                
                case 6: // Use downloadable image as the Image Source. Avoid using this to prevent leak                 
                    Title = "Image Source Test3";

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(@"http://blogs.msdn.com/blogfiles/jgoldb/WindowsLiveWriter/FindingMemoryLeaksinWPFbasedapplications_C681/image_14.png", UriKind.RelativeOrAbsolute);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.EndInit();
                    
                    m_Image1 = new Image();
                    m_Image1.Source = image;    
                    MyStackPanel.Children.Add(m_Image1);
                    break;
                case 7: // HostVisual leak
                    Window1.shouldShutdown = false;
                    Window1.w1.Close();
                    Window1.w1 = null;

                    // Make sure Window1 gets garbage collected.
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    var rect = new Rectangle() { Fill = Brushes.Red, Width = 100, Height = 100 };

                    DoubleAnimation anim = new DoubleAnimation()
                    {
                        Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                        From = 0.5,
                        To = 1.0,
                        RepeatBehavior = RepeatBehavior.Forever,
                        AutoReverse = true,
                    };

                    rect.BeginAnimation(Rectangle.OpacityProperty, anim);
                    
                    var b = new Button();
                    b.Content = "Go back to menu...";
                    b.Click += new RoutedEventHandler(OnCreateNewWindow1Click);
                    MyStackPanel.Children.Add(b);
                    MyStackPanel.Children.Add(rect);
                    break;
                case 8:
                    for (int i = 0; i < 100; i++)
                    {
                        OpenTimedWindow();
                        Thread.Sleep(25);
                    }

                    MyStackPanel.Children.Add(new TextBlock() { Text = "Finished launching threads.." });

                    break;
                case 9:
                    CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
                    break;
                case 10:
                    
                    StackPanel soft3dStack = new StackPanel();
                    soft3dStack.Orientation = Orientation.Horizontal;

                    CheckBox soft3dCheck = new CheckBox();
                    soft3dCheck.IsChecked = false;
                    TextBlock soft3dTB = new TextBlock() { Text = "Check here to use SolidColorBrush to avoid memory leak." };
                    soft3dStack.Children.Add(soft3dCheck);
                    soft3dStack.Children.Add(soft3dTB);

                    Button soft3dButton = new Button();
                    soft3dButton.Content = "Launch Viewport3D Window...";
                    soft3dButton.Tag = soft3dCheck;
                    soft3dButton.Click += new RoutedEventHandler(soft3dButton_Click);

                    MyStackPanel.Children.Add(soft3dStack);
                    MyStackPanel.Children.Add(soft3dButton);
                    
                    break;
            }         
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

        private void soft3dButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (sender as Button);
            CheckBox cb = (b.Tag as CheckBox);

            Software3DLeakWindow w = new Software3DLeakWindow(!cb.IsChecked.Value);
            w.Loaded += new RoutedEventHandler(w_Loaded);
            w.Show();
        }

        void w_Loaded(object sender, RoutedEventArgs e)
        {
            var source = HwndSource.FromHwnd(new WindowInteropHelper(sender as Window).Handle);
            var target = source.CompositionTarget as HwndTarget;
            target.RenderMode = RenderMode.SoftwareOnly;
        }

        private void OpenTimedWindow()
        {
            Thread t = new Thread(new ThreadStart(Window_ThreadStart));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void Window_ThreadStart()
        {
            var win = new TimedWindow();
            win.Show();
            win.RunDispatcher();
        }

        // Add image - this will leak. 
        // Since BitmapImage is static and it keeps a reference to m_Image1.
        // As a result, Window2 is kept alive even when you think you closed it
        private void OnCheckBox5Click(object sender, RoutedEventArgs e)
        {
            m_Image1 = new Image();
            m_Image1.Height = 50;
            m_Image1.Width = 50;

            m_Image1.Source = bi1;
            MyStackPanel.Children.Add(m_Image1);
            CheckBox5.IsEnabled = false;
            CheckBox6.IsEnabled = false;
        }        
        // This will not leak, since we use a Freezable image
        private void OnCheckBox6Click(object sender, RoutedEventArgs e)
        {
            m_Image2 = new Image();
            m_Image2.Height = 50;
            m_Image2.Width = 50;

            m_Image2.Source = bi2;  // bi2 is frozen to this wil not leake

            MyStackPanel.Children.Add(m_Image2);
            CheckBox5.IsEnabled = false;
            CheckBox6.IsEnabled = false;
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
                Application.Current.Activated -= new EventHandler(App_Activated);
            }

            CompositionTarget.Rendering -= new EventHandler(CompositionTarget_Rendering);
        }

        private void App_Activated(object sender, EventArgs e)
        {
        }



    }
}