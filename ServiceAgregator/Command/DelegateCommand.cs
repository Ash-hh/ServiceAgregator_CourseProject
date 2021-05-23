using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServiceAgregator.Models;
using System.Windows.Data;

namespace ServiceAgregator.Command
{
    public class DelegateCommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod((T)parameter);
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
    

    public static class Extensions
    {
        public static ObservableCollection<object> FromListToObservableCollection(this ObservableCollection<object> observ, List<object> list)
        {
            foreach(object f in list)
            {
                observ.Add(f);
            }
            return observ;
        }

        public static ObservableCollection<Services> FromListToObservableCollection(this ObservableCollection<Services> observ, List<Services> list)
        {
            foreach (Services f in list)
            {
                observ.Add(f);
            }
            return observ;
        }

        public static ObservableCollection<Orders> FromListToObservableCollection(this ObservableCollection<Orders> observ, ICollection<Orders> list)
        {            
            foreach (Orders f in list)
            {
                observ.Add(f);
            }
            return observ;
        }

        public static ObservableCollection<Orders> FromListToObservableCollection(this ObservableCollection<Orders> observ, List<Orders> list)
        {
            foreach (Orders f in list)
            {
                observ.Add(f);
            }
            return observ;
        }


    }
        
}
