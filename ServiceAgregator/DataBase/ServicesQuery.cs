using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;
using ServiceAgregator.Command;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Windows;

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
                //var bufflist = db.Services.Where(p=>p.Available==true).ToList();
                //var buffUsers = db.Users.ToList();

                //foreach(Services service in bufflist)
                //{
                //    var User = buffUsers.Where(p => p.User_ID == service.User_ID).FirstOrDefault();
                //    service.Users = User;                   
                //}
                var bufflist = db.Services.Include("Users").Where(p => p.Available == true).ToList(); 
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

        public void UpdateService(Services ServiceToUpdate)
        {
            using (DBContext db = new DBContext())
            {
                var Service = db.Services.Find(ServiceToUpdate.Service_ID);
                Service.Tag = ServiceToUpdate.Tag;
                Service.Description = ServiceToUpdate.Description;
                Service.Cost = ServiceToUpdate.Cost;

                db.SaveChanges();
            }
        }

        public void AddNewService(Services ServiceToAdd)
        {
            string ErrorMessage="";
            using (DBContext db = new DBContext())
            {
                db.Services.Attach(ServiceToAdd);
                db.Services.Add(ServiceToAdd);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (DbEntityValidationResult validationRes in e.EntityValidationErrors)
                    {
                        foreach (DbValidationError err in validationRes.ValidationErrors)
                        {
                            ErrorMessage += err.ErrorMessage;
                        }
                        MessageBox.Show(ErrorMessage);
                    }
                }
               
            }
        }        

        public void DeleteService(int ServiceId)
        {

            using (DBContext db = new DBContext())
            {
                var ServiceToDelete = db.Services.FirstOrDefault(p => p.Service_ID == ServiceId);              
                if (ServiceToDelete.Orders.Count != 0 && ServiceToDelete.Orders.FirstOrDefault().Status != "ThisServiceIsNotAvailible")
                {
                    foreach (Orders order in ServiceToDelete.Orders)
                    {
                        order.Status = "ThisServiceIsNotAvailible";
                    }
                    db.Services.FirstOrDefault(p => p.Service_ID == ServiceToDelete.Service_ID).Available = false;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (DbEntityValidationResult validationRes in e.EntityValidationErrors)
                        {
                            foreach (DbValidationError err in validationRes.ValidationErrors)
                            {
                                Console.WriteLine(err.ErrorMessage);
                            }
                        }
                    }

                }
                else
                {
                    db.Services.Attach(db.Services.FirstOrDefault(p => p.Service_ID == ServiceToDelete.Service_ID));
                    db.Entry(ServiceToDelete).State = System.Data.Entity.EntityState.Deleted;
                    db.Services.Remove(ServiceToDelete);
                    db.SaveChanges();
                }
            }
        }       

        //TODO: Fix
        public ObservableCollection<Services> GetUserServices(int UserId)
        {
            ObservableCollection<Services> buffservices = new ObservableCollection<Services>();
            using (DBContext db = new DBContext()) 
            {
                
                var Services = db.Services.  
                    Include("Users").
                    Include("Orders").
                    Include("Orders.Users").
                    Where(p => p.Available == true && p.User_ID == UserId).ToList();
                foreach(Services serv in Services)
                {
                    while(serv.Orders.FirstOrDefault(z=>z.Status=="DeletedByProducer")!=null)
                    {
                        serv.Orders.Remove(serv.Orders.FirstOrDefault(z => z.Status == "DeletedByProducer"));
                    }
                }

                return buffservices.FromListToObservableCollection(Services);
            }

        }

        public Services GetService(int id)
        {
            using (DBContext db = new DBContext())
            {
                return db.Services.Find(id);
            }

        }

        public List<Tags> GetTags()
        {
            using (DBContext db = new DBContext())
            {
                return db.Tags.Include("Services").ToList();
            }
        }

        public ObservableCollection<Services> GetServicesByTag(Tags tag)
        {
            ObservableCollection<Services> buffservices = new ObservableCollection<Services>();
            using (DBContext db = new DBContext())
            {
                var Services = db.Services.Include("Users").Where(p => p.Available == true && p.Tag == tag.Tag).ToList();
                return buffservices.FromListToObservableCollection(Services);
            }
        }

    }
}
