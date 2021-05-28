using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using ServiceAgregator.Command;
using ServiceAgregator.DataBase;
using System.Windows;

namespace ServiceAgregator.ViewModel.Main
{
    class AdminAllUsersViewModel : BaseViewModel
    {

        public AdminAllUsersViewModel()
        {
            var users = new UserQuery().GetAllUsers();
            Users = new ObservableCollection<Stat>();
            foreach(Users p in users)
            {              
                Users.Add(new Stat(p));
            }
            ChangeUserActivity = new DelegateCommand<object>(UserActivity);
            ChangeUserRights = new DelegateCommand<object>(AdminRights);
        }
        public ObservableCollection<Stat> Users{set;get;}

        private Stat _selecteduser;
        public Stat SelectedUser
        {
            set { _selecteduser = value; OnPropertyChanged("SelectedUser"); }
            get { return _selecteduser; }
        }


        public DelegateCommand<object> ChangeUserActivity { set; get; }

        public DelegateCommand<object> ChangeUserRights { set; get; }

        private void UserActivity(object obj)
        {
            SelectedUser.User.Active = SelectedUser.User.Active.Value ? false : true;
            new UserQuery().UserUpdate(SelectedUser.User);
        }        

        //TODO: Fix
        private void AdminRights(object obj)
        {
            if(SelectedUser.User.User_Type == 0)
            {
                SelectedUser.User.User_Type = 1;
            }
            else
            {
                SelectedUser.User.User_Type = 0;
            }
            new UserQuery().UserUpdate(SelectedUser.User);
        }

        public class Stat
        {
            public Users User { set; get; }
            public int PostedService { set; get; }
            public int ServiceOrders { set; get; }            
            public int ServiceOrdersDone { set; get; }
            
            public int UserOrders { set; get; }            
            
            public Stat(Users user)
            {
                User = user;

                PostedService = user.Services.Where(z=>z.Available==true).Count();

                ServiceOrders = user.Services.Sum(z => z.Orders.Where(p => p.Status != "DeletedByCustomer" && p.Status!= "DeletedByProducer").Count());

                ServiceOrdersDone = user.Services.Sum(z => z.Orders.Where(p => p.Status == "Done").Count());

                UserOrders = user.Orders.Where(z=>z.Status!= "DeletedByCustomer" && z.Status!= "DeletedByProducer").Count();               
            }
           
        }
    }
}
