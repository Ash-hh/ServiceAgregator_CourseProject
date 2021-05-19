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
            ServiceDB = new DataBase.ServicesQuery();
            Tags = ServiceDB.GetTags();
            Services = ServiceDB.GetAllServices();           
            ServiceDetails = new DelegateCommand<object>(Details, CanExecute);
            UserProfile = new DelegateCommand<object>(ViewProfile);
            FilterByTag = new DelegateCommand<object>(TagFilter);
            FilterClear = new DelegateCommand<object>(ClearFilter);
        }

        //TODO:Refactor
        public ServicesViewModel(int UserId)
        {
            ServiceDB = new DataBase.ServicesQuery();
            Tags = ServiceDB.GetTags();
            Services = ServiceDB.GetUserServices(UserId);
            ServiceDetails = new DelegateCommand<object>(Details, CanExecute);
            UserProfile = new DelegateCommand<object>(ViewProfile);
            FilterClear = new DelegateCommand<object>(ClearFilter);
        }

        private DataBase.ServicesQuery ServiceDB;
        public DelegateCommand<object> ServiceDetails { set; get; }

        public DelegateCommand<object> UserProfile { set; get; }

        public DelegateCommand<object> FilterByTag { set; get; }

        public DelegateCommand<object> FilterClear { set; get; }

        private ObservableCollection<Services> _services;

        private Tags _tag;
        public Tags Tag { set { _tag = value; OnPropertyChanged("Tag"); } get { return _tag; } }
        public  List<Tags> Tags { set; get; }
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
                OnPropertyChanged("Services");
            }
        }

        private void ViewProfile(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new OtherUserProfileViewModel(SelectedService.User_ID);
        }
        

        private void Details(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new DetailsServiceViewModel(SelectedService);
        }

        private void TagFilter(object obj)
        {
            if(Tag!=null)
            {
                Services = ServiceDB.GetServicesByTag(Tag);
            }           
        }

        //TODO: Clear Combobox 
        private void ClearFilter(object obj)
        {
            Services = ServiceDB.GetAllServices();
        }

        public bool CanExecute(object obj)
        {
            return true;
        }
       
    }
}
