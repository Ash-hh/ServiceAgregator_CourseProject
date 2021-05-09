using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ServiceAgregator.Models;
using ServiceAgregator.Command;


namespace ServiceAgregator.ViewModel.Main
{
    class ServicesViewModel : BaseViewModel
    {


        public ServicesViewModel()
        {
            Services = new DataBase.ServicesQuery().GetAllServices();
            User = new DataBase.UserQuery().GetAllUsers();
            ServiceDetails = new DelegateCommand<object>(Details, CanExecute);
        }

        public DelegateCommand<object> ServiceDetails { set; get; }

        private ObservableCollection<Services> _services;
        private ObservableCollection<Users> User { set; get; }

        public Services SelectedService { set; get; }
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

        private void Details(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ChoosenServiceViewModel(SelectedService);
        }

        public bool CanExecute(object obj)
        {
            return true;
        }







    }
}
