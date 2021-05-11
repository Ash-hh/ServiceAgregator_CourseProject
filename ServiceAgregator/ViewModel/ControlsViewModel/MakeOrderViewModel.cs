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

        public DelegateCommand<object> MakeOrder { set; get; }

        public MakeOrderViewModel(Services service)
        {
            MakeOrder = new DelegateCommand<object>(Checkout, CanExecute);

            Order = new Orders();
            Order.Order_ID = new OrdersQuery().GenerateUniqueId();
            Order.Service_ID = service.Service_ID;
            Order.User_ID = Changer.CurrentUser.User_ID;
            Order.Status = "Waiting";
        }

        public void Checkout(object obj)
        {
            new OrdersQuery().AddNewOrder(Order);
            MessageBox.Show("Your Order Confirmed");
        }

        public bool CanExecute(object obj)
        {
            return true;
        }
    }
}
