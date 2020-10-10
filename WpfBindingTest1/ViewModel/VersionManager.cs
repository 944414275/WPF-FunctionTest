using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfBindingTest1.ViewModel
{
    public class VersionManager : DependencyObject
    {
        public static readonly DependencyProperty FilterStringProperty =
            DependencyProperty.Register("FilterString", typeof(string),
            typeof(VersionManager), new UIPropertyMetadata("no version!"));
        
        public string FilterString
        {
            get { return (string)GetValue(FilterStringProperty); }
            set { SetValue(FilterStringProperty, value); }
        }

        public static VersionManager Instance { get; private set; }

        static VersionManager()
        {
            Instance = new VersionManager();
        }
    }
}
