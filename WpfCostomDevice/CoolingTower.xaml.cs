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

namespace WpfCostomDevice
{
    /// <summary>
    /// CoolingTower.xaml 的交互逻辑
    /// </summary>
    public partial class CoolingTower : UserControl
    {
        public RunningState RunningState
        {
            get { return (RunningState)GetValue(RunningStateProperty); }
            set { SetValue(RunningStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RunningState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RunningStateProperty =
            DependencyProperty.Register("RunningState", typeof(RunningState), typeof(CoolingTower), new PropertyMetadata(default(RunningState), RunningStateChangedCallback));

        private static void RunningStateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RunningState value = (RunningState)e.NewValue;
            VisualStateManager.GoToState(d as CoolingTower, value == RunningState.Normal ? "normalState" : "errorState", false);
        }

        public CoolingTower()
        {
            InitializeComponent();
        }
    }

    public enum RunningState
    {
        Running,
        Normal,
        Error
    }
}
