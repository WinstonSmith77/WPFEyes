using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPFEyes.UI
{
    /// <summary>
    /// Interaction logic for Eye.xaml
    /// </summary>
    public partial class Eye : UserControl
    {
        private const int _maxOffset = 1000;
        private readonly double _extension;
        private readonly double _extensionInnerEye;

        public Eye()
        {
            InitializeComponent();
            DataContext = this;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50), IsEnabled = true };
            timer.Tick += timer_Tick;


            _extension = (double)FindResource("Extension");
            Center = new Point(_extension / 2, _extension / 2);

            _extensionInnerEye = (double)FindResource("ExtensionInnerEye");
            OffsetXInnerEye = Center.X - _extensionInnerEye / 2;
            OffsetYInnerEye = Center.Y - _extensionInnerEye / 2;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var mouseOnScreen = Win32.GetMousePositionOnScreen();
            var centerOnScreen = PointToScreen(Center);

            var offset = mouseOnScreen;
            offset.Offset(-centerOnScreen.X, -centerOnScreen.Y);

            var angle = Math.Atan2(offset.Y, offset.X);
            var length = Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y);

            length = Math.Min(Math.Abs(length), _maxOffset) * Math.Sign(length);

            length *= _extension / _maxOffset;
           // length = 0;

            offset = new Point(Math.Cos(angle) * length, Math.Sin(angle) * length);

            OffsetXInnerEye = Center.X + offset.X - _extensionInnerEye / 2;
            OffsetYInnerEye = Center.Y + offset.Y - _extensionInnerEye / 2;

            Info = offset.ToString();
        }




        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(Eye), new PropertyMetadata(new Point()));




        public double OffsetXInnerEye
        {
            get { return (double)GetValue(OffsetXInnerEyeProperty); }
            set { SetValue(OffsetXInnerEyeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetXInnerEye.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXInnerEyeProperty =
            DependencyProperty.Register("OffsetXInnerEye", typeof(double), typeof(Eye), new PropertyMetadata(0.0));

        public double OffsetYInnerEye
        {
            get { return (double)GetValue(OffsetYInnerEyeProperty); }
            set { SetValue(OffsetYInnerEyeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetXInnerEye.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYInnerEyeProperty =
            DependencyProperty.Register("OffsetYInnerEye", typeof(double), typeof(Eye), new PropertyMetadata(0.0));





        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(Eye), new PropertyMetadata(String.Empty));


    }
}
