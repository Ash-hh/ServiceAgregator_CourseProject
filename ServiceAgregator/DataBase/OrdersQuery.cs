﻿using System;
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

        public int GenerateUniqueId()
        {
            using (DBContext db = new DBContext())
            {
                var id = db.Orders.OrderByDescending(p => p.Order_ID).Select(p => p.Order_ID).Take(1);
                foreach (int f in id)
                {
                    return f + 1;
                }
                return 1;
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
                var Orders = db.Orders.Where(p => p.User_ID == Changer.CurrentUser.User_ID).ToList();
                var buffServices = new ServicesQuery().GetAllServices();
                foreach(Models.Orders order in Orders)
                {
                    var Service = buffServices.Where(p => p.Service_ID == order.Service_ID).FirstOrDefault();
                    order.Services = Service;
                }
                return UserOrders.FromListToObservableCollection(Orders);
            }
        }

        
    }
}
