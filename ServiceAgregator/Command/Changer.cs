using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.ViewModel;

namespace ServiceAgregator.Command
{
    class Changer
    {
        private static Changer instance;
        public MainViewModel MainViewModel { get; set; }
        public static bool IsAdmin { set; get; }
        public static int CurrentUserID { set; get; }
        private Changer(MainViewModel mainView)
        {           
            MainViewModel = mainView;
        }

        public void SetCurrentUser(Models.Users user)
        {
            
            CurrentUserID = user.User_ID;
        }
       
        public static Changer getInstance(MainViewModel mainViewModel = null)
        {
            return instance ?? (instance = new Changer(mainViewModel));
        }
       
    }
}
