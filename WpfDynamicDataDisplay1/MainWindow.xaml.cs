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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.ComponentModel;

namespace WpfDynamicDataDisplay1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Point point = new Point();
        Pen pen = new Pen(Brushes.Red, 1);
        private ObservableDataSource<Point> observableDataSource = new ObservableDataSource<Point>();


        public Pen PenProp
        {
            get { return pen; }
            set
            {
                pen = value;
                this.OnPropertyChanged("PenProp");
            }
        }
        public ObservableDataSource<Point> LineDataSource
        {
            get
            {
                if (observableDataSource == null)
                    observableDataSource = new ObservableDataSource<Point>();
                return observableDataSource;
            }
            set
            {
                observableDataSource = value;
                this.OnPropertyChanged("LineDataSource");
            }
        }
        private Slider zoomSlider = new Slider
        {
            Margin = new Thickness(20),
            Orientation = Orientation.Vertical,
            Height = 200,
            Minimum = 1,
            Maximum = 3
        };

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            //zoomSlider.ValueChanged += zoomSlider_ValueChanged;
            //plotter.MainCanvas.Children.Add(zoomSlider);

            InitPoint();
        }

        //void zoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    DataRect visible = new DataRect(0, 0, 1, 1).ZoomInToCenter(e.NewValue);
        //    plotter.Visible = visible;
        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DataRect visible = new DataRect(0, 0, 1, 1).ZoomInToCenter(1);
        //    plotter.Visible = visible;
        //}

        List<System.Windows.Point> pppp = new List<System.Windows.Point>() { new System.Windows.Point(0,114), new System.Windows.Point(134, 113)
            ,new System.Windows.Point(135,112),new System.Windows.Point(136,110),new System.Windows.Point(137,105)};
        void InitPoint()
        {
            for (int i = 0; i < 5; i++)
            {

                LineDataSource.Collection.Add(pppp[i]);

                //point.X = i;
                //point.Y = i;
                //LineDataSource.Collection.Add(point);
            }
        }

        #region INotifyPropertyChanged members 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
