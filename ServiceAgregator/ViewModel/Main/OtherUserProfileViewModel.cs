using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.DataBase;
using ServiceAgregator.Models;
using ServiceAgregator.ViewModel;
using ServiceAgregator.Command;
using System.Windows;

namespace ServiceAgregator.ViewModel.Main
{
    class OtherUserProfileViewModel : BaseViewModel
    {
        public Users UserProfile { set; get; }     
        
        private string _reviewtext;
        public string ReviewText 
        {   set { _reviewtext = value; OnPropertyChanged("ReviewText"); } 
            get { return _reviewtext; }
        }
        public OtherUserProfileViewModel(int userId)
        {
            UserProfile = new UserQuery().GetUserById(userId);
            UserServices = new DelegateCommand<object>(Services);
            SendReview = new DelegateCommand<object>(ReviewSend);
        }

        public DelegateCommand<object> UserServices { set; get; }

        public DelegateCommand<object> SendReview { set; get; }

        private void Services(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesViewModel(UserProfile.User_ID);
        }

        //TODO: TextBox Clear After Review       
        //TODO: DataGrid Binding Update
        //TODO: Add Rating Options
        private void ReviewSend(object obj)
        {
            if(UserProfile.Services.FirstOrDefault(p=>p.Orders.FirstOrDefault(z=>z.User_ID==Changer.CurrentUserID)!=null)!=null)
            {
                Reviews review = new Reviews {
                    Text = ReviewText,
                    Review_Date = DateTime.Today,
                    Sender_Id = Changer.CurrentUserID,
                    Recipient_Id = UserProfile.User_ID
                };
                new UserQuery().SendReview(review);
                ReviewText = "";         
                MessageBox.Show("Your review succesfully sended");

            }
            else
            {
                MessageBox.Show("You have no orders for this user to leave a review");
            }
        }
    }
}
