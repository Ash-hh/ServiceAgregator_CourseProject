using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using ServiceAgregator.Command;

namespace ServiceAgregator.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            _Login = new Models.Login();
            LogInCommand = new DelegateCommand<Window>(OnLogin, CanLogin);
            RegisterCommand = new DelegateCommand<Window>(OnRegister, CanRegister);
        }

        private Models.Login _Login;

        // public Models.Login login { get { return _Login; } set { _Login = value; OnPropertyChanged(nameof(login)); } }

        public string Login
        {
            get { return _Login._Login; }
            set { _Login._Login = value; LogInCommand.RaiseCanExecuteChanged(); }
        }

        public string Password
        {
            get { return _Login._Password; }
            set { _Login._Password = value; LogInCommand.RaiseCanExecuteChanged(); }
        }





        public DelegateCommand<Window> LogInCommand { set; get; }

        public DelegateCommand<Window> RegisterCommand { set; get; }

        private void OnLogin(Window win)
        {
            DataBase.LoginQuery login = new DataBase.LoginQuery(_Login);


            
           
            if(login.Login())
            {
                MainWindow main = new MainWindow(login.ReturnCurrenUser());
                main.Show();
                if (win != null)
                {
                    win.Close();
                }
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        private void OnRegister(Window win)
        {
            if(win != null)
            {
                View.RegisterWin register = new View.RegisterWin();
                register.Show();
                win.Close();
            }
        }

        private bool CanLogin(Window win)
        {
            //if (Login == null || Password == null)
            //{
            //    return false;
            //}
            return true;
        }

        private bool CanRegister(Window win)
        {
            return true;
        }

        








    }
}
