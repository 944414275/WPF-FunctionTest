using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCommandTest4.Command
{
    public class DelegateCommand : ICommand
    {
        #region Filed 两个委托，用于方法的传递
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; } 
        #endregion

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunc == null)
                return true;
            return this.CanExecuteFunc(parameter); 
        }

        //设置委托
        public void Execute(object parameter)
        {
            if (ExecuteAction == null)
                return;
            this.ExecuteAction(parameter);
        }
    }
}
