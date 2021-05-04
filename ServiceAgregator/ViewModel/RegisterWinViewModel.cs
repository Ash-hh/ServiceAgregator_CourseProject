using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Command;
using System.Windows;

namespace ServiceAgregator.ViewModel
{
    class RegisterWinViewModel : BaseViewModel
    {

        public RegisterWinViewModel()
        {
            _User = new Models.Users();

            RegisterCommand = new DelegateCommand<object>(Register, CanExecute);

            LoadImage = new DelegateCommand<object>(ImageLoad, CanExecute);
            
        }

        private Models.Users _User;

        public Models.Users User { get { return _User;  } set { _User = value; OnPropertyChanged(nameof(User)); } }

        public DelegateCommand<object> RegisterCommand { set; get; }

        public DelegateCommand<object> LoadImage { set; get; }

        private void ImageLoad(object obj)
        {

        }

        private void Register(object obj)
        {
            var ff = new DataBase.RegisterQuery(_User);

           // MessageBox.Show($"{_User.User_Name}  {_User.Login}");   
        }

        private bool CanExecute(object obj)
        {
            return true;
        }


    }
    


}
