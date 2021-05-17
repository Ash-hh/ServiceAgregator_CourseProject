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
    class MakeOrderViewModel : BaseViewModel
    {
        public Orders Order {set;get;}        
        public DateTime Today { set; get; }
        public DelegateCommand<object> MakeOrder { set; get; }
    
        public MakeOrderViewModel(Services service)
        {           
            MakeOrder = new DelegateCommand<object>(Checkout, CanExecute);
            Today = DateTime.Today;
            Order = new Orders();       
            Order.Service_ID = service.Service_ID;
            Order.Services = service;
            Order.User_ID = Changer.CurrentUser.User_ID;
            Order.Status = "Waiting";
        }

        public void Checkout(object obj)
        {
            if(new View.DialogWins.Default(Order).ShowDialog() == true)
            {
                new OrdersQuery().AddNewOrder(Order);                
                MessageBox.Show("Your Order Confirmed");
                Changer.getInstance(null).MainViewModel.SelectedViewModel = new Main.ServicesViewModel();
            }            
        }

        public bool CanExecute(object obj)
        {
            return true;
        }
    }
}
