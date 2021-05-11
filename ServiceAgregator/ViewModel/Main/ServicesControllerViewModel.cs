using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.ViewModel.ControlsViewModel;
using ServiceAgregator.Command;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;


namespace ServiceAgregator.ViewModel.Main
{
    public class ServicesControllerViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public ServicesControllerViewModel()
        {           
            SelectedViewModel = new UserServicesViewModel();
        }
    }
}
