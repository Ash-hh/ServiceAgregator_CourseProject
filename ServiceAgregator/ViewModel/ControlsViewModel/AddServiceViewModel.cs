using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;
using ServiceAgregator.Command;
using ServiceAgregator.ViewModel.Main;
using System.Windows.Controls;
using System.Windows;

namespace ServiceAgregator.ViewModel.ControlsViewModel
{
    class AddServiceViewModel : BaseViewModel
    {

        private Services _service;

        public List<Tags> Tags {set;get;}

        public Services Service
        {
            set { _service = value; OnPropertyChanged("Service"); }
            get { return _service; }
        }
        public AddServiceViewModel()
        {
            AddNewService = new DelegateCommand<object>(ServiceAdd, CanExecute);
            Service = new Services();
            Tags = new ServicesQuery().GetTags();
        }

        public DelegateCommand<object> AddNewService { set; get; }


        private void ServiceAdd(object obj)
        {
            string ErrorMessage;
            if(new Command.Validation<Services>().IsValid(Service, out ErrorMessage))
            {
                Service.User_ID = Changer.CurrentUserID;
                Service.Date_OfAdd = DateTime.Now.Date;
                Service.Available = true;
                new ServicesQuery().AddNewService(Service);
                Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesControllerViewModel();
            }
            else
            {
                MessageBox.Show(ErrorMessage);
            }
        }

        private void validationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        
    }
}
