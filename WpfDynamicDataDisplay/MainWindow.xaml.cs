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

namespace WpfDynamicDataDisplay
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //VerticalAxis axis = new VerticalAxis();
            //axis.SetConversion(0, 100, 100, 0);

            //plotter.Children.Add(axis);
            //// this is only an example of visible rectange. Use here rect you actually need.
            //plotter.Viewport.Visible = new Rect(0, 0, 1, 100);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //#region 画点
            //int N = 100;
            //Random r = new Random();

            //List<Double> pointXs = new List<Double>(N);
            //List<Double> pointYs = new List<Double>(N);
            //for (int i = 0; i < N; i++)
            //{
            //    pointXs.Add(r.NextDouble() * 100 - 1);
            //    pointYs.Add(r.NextDouble() * 100 - 1);
            //}
            //circles.PlotXY(pointXs, pointYs);
            //#endregion
        }
    }
}
