using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.ViewModel.ControlsViewModel;
using ServiceAgregator.Command;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;
using ServiceAgregator.ViewModel;
using ServiceAgregator.ViewModel.Main;

namespace ServiceAgregator.ViewModel.ControlsViewModel
{
    public class UserServicesViewModel : BaseViewModel
    {
        public ObservableCollection<Services> Services { set; get; }
        public Services SelectedService { set; get; }
        public UserServicesViewModel()
        {
            ChangeViewModel = new DelegateCommand<object>(Details);
            Services = new ServicesQuery().GetUserServices(Changer.CurrentUser.User_ID);
        }

        public DelegateCommand<object> ChangeViewModel { set; get; }

        private void Details(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesControllerViewModel(SelectedService);
        }
    }
}
