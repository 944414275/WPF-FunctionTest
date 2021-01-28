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
using Refit;
using WpfRefit.Apis;

namespace WpfRefit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://www.944414275.top:44305/api/Get/Get
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        public async void Test()
        {
            var gitHubApi = RestService.For<IGetApi>("https://www.944414275.top:44305");
            var name = await gitHubApi.GetTwo();
        }
    }
}
