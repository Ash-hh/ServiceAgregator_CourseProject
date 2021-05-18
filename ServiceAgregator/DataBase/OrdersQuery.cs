using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;
using ServiceAgregator.Command;

namespace ServiceAgregator.DataBase
{
    class OrdersQuery
    {

        public void AddNewOrder(Models.Orders Order)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    if (Order != null)
                    {
                        db.Orders.Attach(Order);
                        db.Orders.Add(Order);
                        db.SaveChanges();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Object: " + validationError.Entry.Entity.ToString());
                        Console.WriteLine("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Console.WriteLine(err.ErrorMessage + "");
                        }
                    }
                }
               
            }
        }
      

        public void OrderUpdate(Orders Order)
        {
            using (DBContext db = new DBContext())
            {
                var OrderToUpdate = db.Orders.Where(p => p.Order_ID == Order.Order_ID).FirstOrDefault();
                OrderToUpdate.Status = Order.Status;
                OrderToUpdate.Service_ID = Order.Service_ID;
                OrderToUpdate.User_ID = Order.User_ID;
                db.SaveChanges();
            }
                
        }

        public ObservableCollection<Orders> GetUserOrders()
        {
            ObservableCollection<Orders> UserOrders = new ObservableCollection<Orders>();
            
            using (DBContext db = new DBContext())
            {
                var Orders = db.Orders.Include("Services.Users").Where(p => p.User_ID == Changer.CurrentUserID && p.Status!= "DeletedByCustomer").ToList();               
                return UserOrders.FromListToObservableCollection(Orders);
            }
        }

        public void DeleteOrder(Orders order)
        {            
                if (order.Status == "DeletedByCustomer" && order.Services.User_ID == Changer.CurrentUserID)
                {
                    Delete();
                }
                else if (order.Status == "DeletedByProducer" && order.Services.User_ID != Changer.CurrentUserID)
                {
                    Delete();
                }
                else if (order.Status == "ThisServiceIsNotAvailible" && order.Services.User_ID != Changer.CurrentUserID)
                {
                    using (DBContext db = new DBContext())
                    {
                        if (db.Orders.Where(p => p.Service_ID == order.Service_ID).Count() == 1)
                        {                           
                            Delete();
                            new ServicesQuery().DeleteService(order.Service_ID);
                        }
                        else
                        {
                            Delete();
                        }

                    }
                }
                else
                {
                    OrderUpdate(order);
                }

            void Delete()
            {
                using (DBContext db = new DBContext())
                {
                    db.Orders.Attach(order);
                    db.Entry(order).State = System.Data.Entity.EntityState.Deleted;
                    db.Orders.Remove(order);
                    db.SaveChanges();

                }
            }
            
        }        
        
    }
}
