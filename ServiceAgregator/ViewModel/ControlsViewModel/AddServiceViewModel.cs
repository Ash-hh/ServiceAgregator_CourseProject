using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;
using ServiceAgregator.Command;

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
            if(Service!=null)
            {
                Service.User_ID = Changer.CurrentUserID;
                Service.Date_OfAdd = DateTime.Now.Date;
                Service.Available = true;
                new ServicesQuery().AddNewService(Service);
            }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        
    }
}
