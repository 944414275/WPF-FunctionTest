using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using DynamicDataDisplay;


namespace WpfChartTest1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableDataSource<Point> source1 = null;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
            e.Cancel = false;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            source1 = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            source1.SetXYMapping(p => p);

            // Add the graph. Colors are not specified and chosen random
            //plotter.AddLineGraph(source1, 2, "Data row");

            // Force everyting to fit in view
            plotter.Viewport.FitToView();

            // Start computation process in second thread
            Thread simThread = new Thread(new ThreadStart(Simulation));
            simThread.IsBackground = true;
            simThread.Start();
        }

        private void Simulation()
        {
            int i = 0;
            while (true)
            {
                Point p1 = new Point(i, 0.3 * Math.Sin(i));
                source1.AppendAsync(Dispatcher, p1);

                i++;
                Thread.Sleep(10);

            }
        }

    }
}
