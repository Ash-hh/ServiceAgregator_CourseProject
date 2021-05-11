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
            Changer.CurrentUser = user;
            SelectedViewModel = new ServicesViewModel();
            CurrentUser = user;
            UpdateViewCommand = new DelegateCommand<string>(OnUpdateViewCommand, CanUpdateViewCommand);
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
            }
        }

        private bool CanUpdateViewCommand(string parameter)
        {
            return true;
        }

    }
}
