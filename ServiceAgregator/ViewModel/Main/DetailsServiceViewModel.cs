﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.Models;
using ServiceAgregator.Command;
using ServiceAgregator.DataBase;
using System.Windows;

namespace ServiceAgregator.ViewModel.Main
{
    class DetailsServiceViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        public BaseViewModel MakeOrder
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged("MakeOrder"); }
        }


        private Services _services;

        public Users Provider { set; get; }

        public DelegateCommand<object> BackToService { set; get; }

        public DelegateCommand<object> StartOrder { set; get; }
        public Services Service
        {
            get
            {
                return _services;
            }
            private set
            {
                _services = value;
                OnPropertyChanged("Service");
            }
        }

        public DetailsServiceViewModel()
        {

        }

        public DetailsServiceViewModel(Services service)
        {
            StartOrder = new DelegateCommand<object>(ShowOrderWin, CanExecute);
            BackToService = new DelegateCommand<object>(Back_toService, CanExecute);
            Service = service;
            Provider = new DataBase.UserQuery().GetUserById(Service.User_ID);
        }

        private void Back_toService(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServicesViewModel();
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void ShowOrderWin(object obj)
        {
            if(new OrdersQuery().GetUserOrders().FirstOrDefault(p=>p.Service_ID == Service.Service_ID)==null)
            {
                MakeOrder = new ControlsViewModel.MakeOrderViewModel(Service);
            }
            else
            {
                MessageBox.Show("You already order this Service");
            }
            
        }
    }
}
