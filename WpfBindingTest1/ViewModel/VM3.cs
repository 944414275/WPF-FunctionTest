using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfBindingTest1.ViewModel
{
    public class VM3:ViewModelBase
    {
        private string s3 = "333";
        public string S3
        {
            get => s3;
            set
            {
                s3 = value;
                RaisePropertyChanged("S3");
            }
        }

        private RelayCommand btn3;
        public RelayCommand Btn3
        {
            get
            {
                if (btn3 == null)
                    btn3 = new RelayCommand(Btn3Fun);
                return btn3;
            }

        }
        void Btn3Fun()
        {

        }
    }
}
