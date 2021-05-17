using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.DataBase;
using ServiceAgregator.Models;
using ServiceAgregator.ViewModel;
using ServiceAgregator.Command;

namespace ServiceAgregator.ViewModel.Main
{
    class OtherUserProfileViewModel : BaseViewModel
    {
        public Users UserProfile { set; get; }
        public OtherUserProfileViewModel(int userId)
        {
            UserProfile = new UserQuery().GetUserById(userId);
            UserServices = new DelegateCommand<object>(Services);
        }

        public DelegateCommand<object> UserServices { set; get; }

        private void Services(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesViewModel(UserProfile.User_ID);
        }
    }
}
