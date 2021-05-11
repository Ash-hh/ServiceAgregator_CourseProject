using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.ViewModel.ControlsViewModel;
using ServiceAgregator.Command;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;

namespace ServiceAgregator.ViewModel.ControlsViewModel
{
    public class UserServicesViewModel : BaseViewModel
    {
        public ObservableCollection<object> Services { set; get; }
        public Services SelectedService { set; get; }
        public UserServicesViewModel()
        {
            Services = new ServicesQuery().GetUserServices();
        }
    }
}
