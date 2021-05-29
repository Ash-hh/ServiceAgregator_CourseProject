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
    class UserOrdersViewModel : BaseViewModel
    {
        public UserOrdersViewModel()
        {

            Orders = new OrdersQuery().GetUserOrders();

            Orders.CollectionChanged += Orders_CollectionChanged;
 
            UpdateStatus = new DelegateCommand<string>(Update_Status);
            DeleteOrder = new DelegateCommand<object>(Delete);
            UserProfile = new DelegateCommand<object>(ViewUserProfile);
        }

        private void Orders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("");
        }

        public DelegateCommand<object> UserProfile { set; get; }
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

        //TODO: Trigers
        //TODO: Fix        
        private void Update_Status(string status)
        {
            if(SelectedOrder.Status=="Waiting")
            {
                SelectedOrder.Status = status; 
                new OrdersQuery().OrderUpdate(SelectedOrder);
                Orders = new OrdersQuery().GetUserOrders();
            }
            else
            {
                MessageBox.Show($"You cant change status. Current status: {SelectedOrder.Status}");   
            }
        }      
        
        private void ViewUserProfile(object obj)
        {
            Changer.getInstance(null).MainViewModel.SelectedViewModel = new OtherUserProfileViewModel(SelectedOrder.Services.User_ID);
        }

        private void Delete(object obj)
        {
            if(SelectedOrder.Status != "ThisServiceIsNotAvailible")
            {
                SelectedOrder.Status = "DeletedByCustomer";
            }            
            new OrdersQuery().DeleteOrder(SelectedOrder);
            Orders.Remove(SelectedOrder);

        }
        public bool CanExecute(object obj)
        {
            return true;
        }

    }
}
