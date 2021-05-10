using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;

namespace ServiceAgregator.ViewModel.Main
{
    class UserOrdersViewModel : BaseViewModel
    {
        public UserOrdersViewModel()
        {
            Orders = new OrdersQuery().GetUserOrders();
        }

        public ObservableCollection<Orders> Orders { set; get; }


    }
}
