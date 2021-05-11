using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.DataBase;
using ServiceAgregator.Models;
using ServiceAgregator.Command;
using System.Windows;

namespace ServiceAgregator.ViewModel.ControlsViewModel
{
    class UserServiceDetailsViewModel : BaseViewModel
    {
        public UserServiceDetailsViewModel(Services service)
        {
            Service = service;
        }

      

        private Services _service;

        public Services Service
        {
            set { _service = value; OnPropertyChanged("Service"); }
            get { return _service; }
        }


    }
}
