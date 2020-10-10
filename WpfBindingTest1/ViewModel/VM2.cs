using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace WpfBindingTest1.ViewModel
{
    public class VM2:DependencyObject,INotifyPropertyChanged
    {
        private string s2 = "222";
        public string S2
        {
            get => s2;
            set
            {
                s2 = value;
                //RaisePropertyChanged("S2");
            }
        }
        private RelayCommand btn2;
        public RelayCommand Btn2
        {
            get
            {
                if (btn2 == null)
                    btn2 = new RelayCommand(Btn2Fun);
                return btn2;
            }

        }
        
        void Btn2Fun()
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 1);

            brush.GradientStops.Add(new GradientStop(Color.FromRgb(240, 248, 255), 0));


            WinStyles winStyles = new WinStyles();
            winStyles.StyleName = "11";
            winStyles.BackgroundBrush = brush;


            //Application.Current.TryFindResource("innerLgbResource")= winStyles.BackgroundBrush;
            //this.Resources["innerLgbResource"] = brush;
            Application.Current.Resources["innerLgbResource"] = brush;
            //Application.Current.Resources["innerLgbResource"] = brush;
        }

        public static readonly DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString", typeof(string),
            typeof(VM2), new UIPropertyMetadata("version2"));

        public event PropertyChangedEventHandler PropertyChanged;

        public string FilterString
        {
            get { return (string)GetValue(FilterStringProperty); }
            set
            {
                SetValue(FilterStringProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(FilterString));
            }
        }

        public static VM2 Instance { get; private set; }

        static VM2()
        {
            Instance = new VM2();
        }
    }

    public class WinStyles
    {
        public string StyleName { get; set; }
        public LinearGradientBrush BackgroundBrush { get; set; }
        //public SolidColorBrush Fontcolor { get; set; }
    }
}
