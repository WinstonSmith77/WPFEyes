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
            ReadSettings();
        }

        private void ReadSettings()
        {
            var settings = Properties.Settings.Default;
            Left = settings.Left;
            Top = settings.Top;
            Width = settings.Width;
            Height = settings.Height;
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

            SaveSettings();
        }


        private void MainWindow_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            var settings = Properties.Settings.Default;
            var saveIt = false;

            if (settings.Left != Left)
            {
                settings.Left = Left;
                saveIt = true;
            }

            if (settings.Top != Top)
            {
                settings.Top = Top;
                saveIt = true;
            }

            if (settings.Width != Width)
            {
                settings.Width = Width;
                saveIt = true;
            }

            if (settings.Height != Height)
            {
                settings.Height = Height;
                saveIt = true;
            }

            if (saveIt)
            {
                settings.Save();
            }
        }
    }


}
