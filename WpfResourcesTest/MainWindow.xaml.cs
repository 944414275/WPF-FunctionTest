using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfResourcesTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //新建资源
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 1);

            brush.GradientStops.Add(new GradientStop(Color.FromRgb(240, 248, 255), 0));
            //gs没有绑定方法
            GradientStop gs = new GradientStop(Color.FromRgb(0, 100, 0), 0.1);
            /* 
             * 如果xaml中是使用style调用资源的情况下
             * 必须要在gs被add进入brush之前绑定，否则绑定不上。
             * 应该是使用style的时候，style会占用该资源，使其变成只读，导致无法绑定。
             * 如果不是用style绑定而是直接调用background属性的时候，则可以随时绑定
             */
            Binding bd = new Binding();
            bd.Source = slider;
            bd.Path = new PropertyPath("Value");
            BindingOperations.SetBinding(gs, GradientStop.OffsetProperty, bd);

            /*
             * 方法二 这里采用绑定slider的方法，因为GradientStop没有SetBinding方法
            Binding bd = new Binding();
            bd.Source = gs;
            bd.Path = new PropertyPath("Offset");
            slider.SetBinding(Slider.ValueProperty, bd);
             */
            brush.GradientStops.Add(gs);
            brush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 140, 0), 1));
            this.Resources["innerLgbResource"] = brush;
        }
    }
}
