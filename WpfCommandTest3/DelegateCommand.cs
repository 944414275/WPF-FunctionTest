using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCommandTest3
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteCommand = null;
        public Func<object, bool> CanExecuteCommand = null;

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else{ return true; } 
        }

        public void Execute(object parameter)
        {
            if (ExecuteCommand != null) this.ExecuteCommand(parameter); 
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}
