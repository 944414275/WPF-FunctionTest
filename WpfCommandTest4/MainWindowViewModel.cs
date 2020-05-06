using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfCommandTest4.Command;



namespace WpfCommandTest4
{
    public class MainWindowViewModel
    {

        #region RelayCmmand
        private RelayCmmand _btnClick = null;
         
        public ICommand btnClick
        {
            get
            {
                if (_btnClick == null)
                {
                    _btnClick = new RelayCmmand((p) => UpdateBtn(p), (p) => DeleteBtn(p));
                }
                return _btnClick;
            }
        }


        public void UpdateBtn(Object P)
        {
            System.Windows.MessageBox.Show("111");
        }

        public bool DeleteBtn(Object P)
        {
            return true;
        }
        #endregion

        #region DelegateCommand
        public DelegateCommand txtDelegateCommand { get; set; }
        public DelegateCommand lableDelegateCommand { get; set; }

        public MainWindowViewModel()
        {
            txtDelegateCommand = new DelegateCommand();
            txtDelegateCommand.ExecuteAction = new Action<object>(ShowTextBlock);

            lableDelegateCommand = new DelegateCommand();
            lableDelegateCommand.ExecuteAction = new Action<object>(ShowLabel);
        }

        private void ShowTextBlock(object parameter)
        {
            System.Windows.MessageBox.Show("这是TextBook");
        }

        private void ShowLabel(object parameter)
        {
            System.Windows.MessageBox.Show("这是Lable");
        } 
        #endregion

        public ICommand command1
        {
            get
            {
                return new RoutedCommand();
            }
        }
    }
}
