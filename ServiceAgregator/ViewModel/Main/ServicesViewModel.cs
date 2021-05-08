using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ServiceAgregator.Models;

namespace ServiceAgregator.ViewModel.Main
{
    class ServicesViewModel : BaseViewModel
    {


        public ServicesViewModel()
        {
            Services = new DataBase.ServicesQuery().GetAllServices();
            User = new DataBase.ServicesQuery().GetAllUsers();

        }

        private ObservableCollection<Services> _services;

        private ObservableCollection<Users> User { set; get; }
        public ObservableCollection<Services> Services
        {
            get
            {
                return _services;
            }
            set
            {
                _services = value;
            }
        }

        private void LoadServices()
        {

        }
        






    }
}
