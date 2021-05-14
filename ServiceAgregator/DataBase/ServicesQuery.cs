using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;
using ServiceAgregator.Command;

namespace ServiceAgregator.DataBase
{
    class ServicesQuery
    {
        public ServicesQuery()
        {

        }

        public ObservableCollection<Services> GetAllServices()
        {
            using (DBContext db = new DBContext())
            {
                ObservableCollection<Services> services = new ObservableCollection<Services>();
                var bufflist = db.Services.Where(p=>p.Available==true).ToList();
                var buffUsers = db.Users.ToList();
             
                foreach(Services service in bufflist)
                {
                    var User = buffUsers.Where(p => p.User_ID == service.User_ID).FirstOrDefault();
                    service.Users = User;                   
                }

                return services.FromListToObservableCollection(bufflist);              
            }
            
        }

        public ObservableCollection<Services> GetOrderServices()
        {
            using (DBContext db = new DBContext())
            {
                ObservableCollection<Services> services = new ObservableCollection<Services>();
                var bufflist = db.Services.ToList();
                var buffUsers = db.Users.ToList();

                foreach (Services service in bufflist)
                {
                    var User = buffUsers.Where(p => p.User_ID == service.User_ID).FirstOrDefault();
                    service.Users = User;
                }

                return services.FromListToObservableCollection(bufflist);
            }
        }

        public void AddNewService(Services ServiceToAdd)
        {
            using (DBContext db = new DBContext())
            {
                db.Services.Add(ServiceToAdd);
                db.SaveChanges();
            }
        }

        // TODO: Update DeleteService; Delete by ID, Not By object;
        public void DeleteService(Services Service)
        {
            
            using (DBContext db = new DBContext())
            {
                var ServiceToDelete = db.Services.FirstOrDefault(p => p.Service_ID == Service.Service_ID);
                var Orders = db.Orders.Where(p => p.Service_ID == ServiceToDelete.Service_ID);
                if(Orders.FirstOrDefault() != null && Orders.FirstOrDefault().Status != "ThisServiceIsNotAvailible")
                {
                    foreach (Orders order in Orders)
                    {
                        order.Status = "ThisServiceIsNotAvailible";
                    }
                    db.Services.FirstOrDefault(p => p.Service_ID == ServiceToDelete.Service_ID).Available = false;
                    db.SaveChanges();                    
                }
                else
                {                    
                    db.Services.Attach(db.Services.FirstOrDefault(p=>p.Service_ID == ServiceToDelete.Service_ID));
                    db.Entry(ServiceToDelete).State = System.Data.Entity.EntityState.Deleted;
                    db.Services.Remove(ServiceToDelete);
                    db.SaveChanges();
                }
            }
        }

        public ObservableCollection<Services> GetUserServices()
        {
            ObservableCollection<Services> buffservices = new ObservableCollection<Services>();
            using (DBContext db = new DBContext())
            {
                var Services = db.Services.Where(z => z.User_ID == Changer.CurrentUser.User_ID && z.Available == true).GroupJoin(db.Orders,
                    p => p.Service_ID,
                    t => t.Service_ID,
                    (serv, order) => new
                    {
                        Service_ID = serv.Service_ID,
                        User_ID = serv.User_ID,
                        Tag = serv.Tag,
                        Description = serv.Description,
                        Cost = serv.Cost,
                        Date_OfAdd = serv.Date_OfAdd,
                        Orders = order.ToList(),
                    }).ToList();

                foreach (var f in Services)
                {
                    buffservices.Add(new Services
                    {
                        Service_ID = f.Service_ID,
                        User_ID = f.User_ID,
                        Tag = f.Tag,
                        Description = f.Description,
                        Cost = f.Cost,
                        Date_OfAdd = f.Date_OfAdd,
                        Orders = f.Orders
                    });
                }
                //var Services = db.Services.Where(p => p.Available == true).ToList();
                //TODO: Fix Binding with uppermethod
                return buffservices;
            }

        }

    }
}
