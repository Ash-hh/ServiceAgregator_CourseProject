using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ServiceAgregator.ViewModel.Main;
using ServiceAgregator.Command;
using ServiceAgregator.Models;
using System.Windows;
using ServiceAgregator.View;

namespace ServiceAgregator.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public MainViewModel()
        {          
            UpdateViewCommand = new DelegateCommand<string>(OnUpdateViewCommand, CanUpdateViewCommand);
        }        

        public MainViewModel(Models.Users user)
        {
            Changer.getInstance(this);
            Changer.CurrentUserID = user.User_ID;
            SelectedViewModel = new ServicesViewModel();
            CurrentUser = user;
            UpdateViewCommand = new DelegateCommand<string>(OnUpdateViewCommand, CanUpdateViewCommand);
            AppExit = new DelegateCommand<Window>(Exit);
            LogOut = new DelegateCommand<Window>(UserLogOut);
        }

        private Users _currentUser;
        public Users CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
       
        public DelegateCommand<string> UpdateViewCommand { get; set; }

        public DelegateCommand<Window> AppExit { set; get; } 

        public DelegateCommand<Window> LogOut { set; get; }

        private void OnUpdateViewCommand(string parameter)
        {
            switch (parameter)
            {
                case "YourProfile":
                    Changer.getInstance(null).MainViewModel.SelectedViewModel = new UserProfileViewModel(_currentUser);
                    break;
                case "Services":
                    Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesViewModel();
                    break;
                case "MyOrders":
                    Changer.getInstance(null).MainViewModel.SelectedViewModel = new UserOrdersViewModel();
                    break;
                case "MyServices":
                    Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesControllerViewModel();
                    break;
                case "AllUsers":
                    if(Changer.IsAdmin)
                    {
                        Changer.getInstance(null).MainViewModel.SelectedViewModel = new AdminAllUsersViewModel();
                    } break;
            }
        }

        private bool CanUpdateViewCommand(string parameter)
        {
            return true;
        }

        private void UserLogOut(Window win)
        {
           
            new LoginWin().Show();
            win.Close();
        }

        public void Exit(Window win)
        {
            win.Close();
        }

    }
}
