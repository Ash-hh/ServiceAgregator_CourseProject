using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;
using ServiceAgregator.DataBase;
using ServiceAgregator.Command;
using System.Windows;
using System.ComponentModel;


namespace ServiceAgregator.ViewModel.Main
{
    class UserOrdersViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public UserOrdersViewModel()
        {

            Orders = new OrdersQuery().GetUserOrders();
 
            UpdateStatus = new DelegateCommand<string>(Update_Status);
            DeleteOrder = new DelegateCommand<object>(Delete);
        }

        public DelegateCommand<string> UpdateStatus { set; get; }      
        public DelegateCommand<object> DeleteOrder { set; get; }

        private ObservableCollection<Orders> _orders;

        public ObservableCollection<Orders> Orders
        {
            set { _orders = value; OnPropertyChanged("Orders"); }
            get { return _orders; }
        }

        public int SelectedIndex { set; get; } 

        private Orders _selectedOrder;

        public Orders SelectedOrder 
        { 
            set 
            { 
                _selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
                OnPropertyChanged("Orders");              
            } 
            get { return _selectedOrder; }
        }

        //--Если вы читаете это сообщение, то снизу огромный костыль
        private void Update_Status(string status)
        {
            if(SelectedOrder.Status!=" ")
            {
                //SelectedOrder.Status = status;
                //OnPropertyChanged("SelectedOrder");
                //OnPropertyChanged("Orders");

                Orders buff = new Orders();
                buff = SelectedOrder;
                buff.Status = status;
                int id = SelectedIndex;
                Orders.RemoveAt(SelectedIndex);
                Orders.Insert(id, buff);
                new OrdersQuery().OrderUpdate(Orders.ElementAt(id));

                //new OrdersQuery().OrderUpdate(SelectedOrder);
            }
            else
            {
                MessageBox.Show($"You cant change status. Current status: {SelectedOrder.Status}");   
            }
        }      
        
        private void Delete(object obj)
        {
            Orders.Remove(SelectedOrder);
        }
        public bool CanExecute(object obj)
        {
            return true;
        }

    }
}
