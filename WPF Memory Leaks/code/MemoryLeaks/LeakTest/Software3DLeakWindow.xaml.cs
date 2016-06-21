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
using System.Windows.Media.Media3D;

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for Software3DLeakWindow.xaml
    /// </summary>
    public partial class Software3DLeakWindow : Window
    {
        private bool shouldLeak = false;

        public Software3DLeakWindow(bool shouldLeak)
        {
            InitializeComponent();

            this.shouldLeak = shouldLeak;
            this.Loaded += new RoutedEventHandler(Software3DLeakWindow_Loaded);
        }

        private void Software3DLeakWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.shouldLeak)
            {
                this.theViewport.Children.Clear();

                ModelVisual3D vis = (ModelVisual3D)FindResource("leakyModel");
                this.theViewport.Children.Add(vis);
            }
        }
    }
}