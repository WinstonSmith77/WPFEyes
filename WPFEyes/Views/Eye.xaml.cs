using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPFEyes.Views
{
    /// <summary>
    /// Interaction logic for Eye.xaml
    /// </summary>
    public partial class Eye : UserControl
    {
        private const int _maxOffset = 1000;
        private readonly double _extension;
        private readonly double _extensionInnerEye;
        private readonly double _extensionBlue;

        private static readonly DispatcherTimer _timer;

        static Eye()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20), IsEnabled = true };
        }


        public Eye()
        {
            this.InitializeComponent();
            this.DataContext = this;

            this.InitTimer();

            this._extension = (double)this.FindResource("Extension");
            this.Center = new Point(this._extension / 2, this._extension / 2);

            this._extensionInnerEye = (double)this.FindResource("ExtensionInnerEye");
            this.OffsetXInnerEye = this.Center.X - this._extensionInnerEye / 2;
            this.OffsetYInnerEye = this.Center.Y - this._extensionInnerEye / 2;

            this._extensionBlue = (double)this.FindResource("ExtensionBlue");
            this.OffsetBlue = (this._extensionInnerEye - this._extensionBlue) / 2;
        }

        private void InitTimer()
        {
            _timer.Tick += this.timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var mouseOnScreen = Win32.GetMousePositionOnScreen();
            var centerOnScreen = this.PointToScreen(this.Center);

            var offset = mouseOnScreen;
            offset.Offset(-centerOnScreen.X, -centerOnScreen.Y);

            var angle = Math.Atan2(offset.Y, offset.X);
            var length = Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y);

            //  length = Math.Min(Math.Abs(length), _maxOffset) * Math.Sign(length);

            length *= this._extension / _maxOffset / 2;

            offset = this.CalcPos(angle, length, this._extensionInnerEye, x => this.OffsetXInnerEye = x, y => this.OffsetYInnerEye = y);

            this.Info = offset.ToString();
        }

        private Point CalcPos(double angle, double length, double extension, Action<double> setX, Action<double> setY)
        {
            length = Math.Min(Math.Abs(length), (this._extension / 2 - extension)) * Math.Sign(length);

            var offset = new Point(Math.Cos(angle) * length, Math.Sin(angle) * length);

            setX(this.Center.X + offset.X - extension / 2);
            setY(this.Center.Y + offset.Y - extension / 2);
            return offset;
        }


        public Point Center
        {
            get { return (Point)this.GetValue(CenterProperty); }
            set { this.SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(Eye), new PropertyMetadata(new Point()));



        public double OffsetBlue
        {
            get { return (double)this.GetValue(OffsetBlueProperty); }
            set { this.SetValue(OffsetBlueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetBlue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetBlueProperty =
            DependencyProperty.Register("OffsetBlue", typeof(double), typeof(Eye), new PropertyMetadata(0.0));




        public double OffsetXInnerEye
        {
            get { return (double)this.GetValue(OffsetXInnerEyeProperty); }
            set { this.SetValue(OffsetXInnerEyeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetXInnerEye.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXInnerEyeProperty =
            DependencyProperty.Register("OffsetXInnerEye", typeof(double), typeof(Eye), new PropertyMetadata(0.0));

        public double OffsetYInnerEye
        {
            get { return (double)this.GetValue(OffsetYInnerEyeProperty); }
            set { this.SetValue(OffsetYInnerEyeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OffsetXInnerEye.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYInnerEyeProperty =
            DependencyProperty.Register("OffsetYInnerEye", typeof(double), typeof(Eye), new PropertyMetadata(0.0));





        public string Info
        {
            get { return (string)this.GetValue(InfoProperty); }
            set { this.SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(Eye), new PropertyMetadata(String.Empty));


    }
}
