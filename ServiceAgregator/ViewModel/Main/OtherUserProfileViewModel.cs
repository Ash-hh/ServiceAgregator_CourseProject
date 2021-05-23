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


        private bool[] _modeArray = new bool[] { false, false, false, false, false };
        public bool[] ModeArray
        {   
            set { _modeArray = value; OnPropertyChanged("ModeArray"); }
            get { return _modeArray; }
        }
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
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
        //TODO: Add Rating Options
        private void ReviewSend(object obj)
        {
            if(ReviewText==null || ReviewText.Length<=0)
            {                
                MessageBox.Show("You can't leave empty review");
            }
            else
            {
                if(SelectedMode == -1)
                {
                    MessageBox.Show("You didnt set rating");
                }
                else
                {
                    if (UserProfile.Services.FirstOrDefault(p => p.Orders.FirstOrDefault(z => z.User_ID == Changer.CurrentUserID) != null) != null)
                    {

                        Reviews review = new Reviews
                        {
                            Text = ReviewText,
                            Mark = SelectedMode + 1,
                            Review_Date = DateTime.Today,
                            Sender_Id = Changer.CurrentUserID,
                            Recipient_Id = UserProfile.User_ID
                        };
                        new UserQuery().SendReview(review);

                        UserProfile.Rating = (5 + UserProfile.ReviewsRecepient.Sum(e => e.Mark) + SelectedMode + 1) / (UserProfile.ReviewsRecepient.Count + 2);

                        new UserQuery().UserUpdate(UserProfile);

                        ReviewText = "";
                        ModeArray[SelectedMode] = false;
                        MessageBox.Show("Your review succesfully sended");
                        Changer.getInstance(null).MainViewModel.SelectedViewModel = new OtherUserProfileViewModel(UserProfile.User_ID);
                    }
                    else
                    {
                        MessageBox.Show("You have no orders for this user to leave a review");
                    }
                }                
            }
            
        }
    }
}
