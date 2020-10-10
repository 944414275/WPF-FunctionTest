using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfBindingTest1.ViewModel
{
    public class VM1:ViewModelBase
    {
        private string s1 = "111";
        public string S1
        {
            get => s1;
            set
            {
                s1 = value;
                RaisePropertyChanged("S1");
            }
        }

        private RelayCommand btn1;
        public RelayCommand Btn1
        {
            get
            {
                if (btn1 == null) 
                 btn1 = new RelayCommand(Btn1Fun);
                return btn1;
            }

        }
        void Btn1Fun()
        {

        }
    }
}
