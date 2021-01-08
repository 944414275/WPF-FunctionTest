using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace WpfBindingTest1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string s1;
        public string S1
        {
            get => s1;
            set
            {
                s1 = value;
                RaisePropertyChanged("S1");
            }
        }

        private string s2;
        public string S2
        {
            get => s2;
            set
            {
                s2 = value;
                RaisePropertyChanged("S2");
            }
        }
        private RelayCommand<TextBox> btn1;
        public RelayCommand<TextBox> Btn1
        {
            get
            {
                if (btn1 == null)
                    btn1 = new RelayCommand<TextBox>(o=>Btn1Fun(o));
                return btn1;
            } 
        }
        void Btn1Fun(TextBox textBox)
        { 
            S2 = textBox.Text;
        } 
    }
}