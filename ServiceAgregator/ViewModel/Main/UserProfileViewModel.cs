using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.ViewModel;
using ServiceAgregator.Models;

namespace ServiceAgregator.ViewModel.Main
{
    class UserProfileViewModel : BaseViewModel
    {

        public UserProfileViewModel() { }

        public UserProfileViewModel(Users currentUser)
        {
            _currentUser = currentUser;
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




    }
}
