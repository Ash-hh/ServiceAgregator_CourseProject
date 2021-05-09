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

        public static Models.Users CurrentUser { set; get; }
        private Changer(MainViewModel mainView)
        {           
            MainViewModel = mainView;
        }

        public void SetCurrentUser(Models.Users user)
        {
            CurrentUser = user;
        }
       
        public static Changer getInstance(MainViewModel mainViewModel = null)
        {
            return instance ?? (instance = new Changer(mainViewModel));
        }
       
    }
}
