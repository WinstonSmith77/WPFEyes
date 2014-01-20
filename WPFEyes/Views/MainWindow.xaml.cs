using System;
using System.Windows;
using System.Windows.Input;

namespace WPFEyes.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void MainWindow_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Control)
            {
                return;
            }

            var aspect = Width / Height;

            var defaultHeight = (double)this.FindResource("DefaultHeight") / 10;

            int delta = e.Delta / 120;

            const int increment = 10;

            Height = Math.Max(defaultHeight, Height + delta * increment);
            Width = aspect * Height;
        }


    }


}
