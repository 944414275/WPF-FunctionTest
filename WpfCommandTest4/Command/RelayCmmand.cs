using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCommandTest4.Command
{
    public class RelayCmmand : ICommand
    {
        #region 字段
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;



        #endregion

        #region Constructors
        public RelayCmmand(Action<object> execute) : this(execute, null) { }

        //通过构造函数对委托进行实例化，方便后面进行委托调用相关函数进行操作
        public RelayCmmand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Member
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion

    }
}
