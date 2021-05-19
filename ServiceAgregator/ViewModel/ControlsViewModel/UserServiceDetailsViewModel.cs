using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceAgregator.DataBase;
using ServiceAgregator.Models;
using ServiceAgregator.Command;
using System.Windows;
using System.Collections.Generic;

namespace ServiceAgregator.ViewModel.ControlsViewModel
{
    class UserServiceDetailsViewModel : BaseViewModel
    {
        public UserServiceDetailsViewModel(Services service)
        {
            Service = service;
            Tags = new ServicesQuery().GetTags();
            DeleteService = new DelegateCommand<object>(ServiceDelete);
            CancelCom = new DelegateCommand<string>(Cancel);
            DeleteOrder = new DelegateCommand<object>(Delete);
            Orders = new ObservableCollection<Orders>();
            Orders.FromListToObservableCollection(Service.Orders);
            EditService = new DelegateCommand<string>(Edit);            
        }
        //TODO: CanExecute() !!
        public DelegateCommand<object> DeleteService { set; get; }
        public DelegateCommand<string> CancelCom { set; get; }
        public DelegateCommand<string> EditService { set; get; }
        public DelegateCommand<object> DeleteOrder { set; get; }

        private Services _service;

        public List<Tags> Tags { set; get; }

        public int OrderID { set; get; }

        private Orders _order;
        public Orders Order 
        {   
            set { _order = value;OnPropertyChanged("Orders"); }
            get { return _order; }
        }

        private ObservableCollection<Orders> _orders;

        public ObservableCollection<Orders> Orders
        {
            set { _orders = value; OnPropertyChanged("Orders"); }
            get { return _orders; }
        }
        public Services Service
        {
            set { _service = value; OnPropertyChanged("Service"); }
            get { return _service; }
        }

       

        public void ServiceDelete(object obj)
        {
            new ServicesQuery().DeleteService(Service.Service_ID);
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new ServiceAgregator.ViewModel.Main.ServicesControllerViewModel();
        }

        
      //TODO: Update Delete Fucntion
        public void Cancel(string status)
        {            
            if(Order.Status != "DeletedByCustomer")
            {
                Order.Status = "CanceledByProducer";
                new OrdersQuery().OrderUpdate(Order);                
            }
        }

        
        public void Delete(object obj)
        {
            if(Order.Status == "DeletedByCustomer")
            {
                Order.Services = Service;
                new OrdersQuery().DeleteOrder(Order);
                Orders.Remove(Order);
                          
            }
            else
            {
                Order.Services = Service;
                Order.Status = "DeletedByProducer";
                Orders.FirstOrDefault(p=>p.Order_ID == Order.Order_ID).Status = "DeletedByProducer";                
                new OrdersQuery().OrderUpdate(Order);
                Orders.Remove(Order);
            }
        }

        private void Edit(string param)
        {
            if(param == "EDIT")
            {
                new ServicesQuery().UpdateService(Service);
            }
        }
    }
}
