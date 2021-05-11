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

        public DelegateCommand<string> ChangeModel { set; get; }

        public ServicesControllerViewModel()
        {
            ChangeModel = new DelegateCommand<string>(ChangeViewModel);

            SelectedViewModel = new UserServicesViewModel();
            
        }

        public ServicesControllerViewModel(Services serv)
        {
            SelectedViewModel = new UserServiceDetailsViewModel(serv);
        }

        public void ChangeViewModel(string newmodel)
        {
            switch(newmodel)
            {
                case "AllServices":
                    SelectedViewModel = new UserServicesViewModel();
                    break;
                case "AddNewService":
                    SelectedViewModel = new AddServiceViewModel();
                    break;
                //case "ServiceDetails":
                //    SelectedViewModel = new UserServiceDetailsViewModel((SelectedViewModel as UserServicesViewModel).SelectedService);
                //    break;
            }
        }
    }
}
