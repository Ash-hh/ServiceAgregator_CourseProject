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
                var bufflist = db.Services.ToList();
                var buffUsers = db.Users.ToList();
             
                foreach(Services service in bufflist)
                {
                    var User = buffUsers.Where(p => p.User_ID == service.User_ID).FirstOrDefault();
                    service.Users = User;                   
                }

                return services.FromListToObservableCollection(bufflist);              
            }
            
        }

        public ObservableCollection<object> GetUserServices()
        {
            ObservableCollection<object> buffservices = new ObservableCollection<object>();
            using (DBContext db = new DBContext())
            {


                //var Services = (from services in db.Services
                //               join orders in db.Orders on services equals orders.Services
                //               where services.User_ID == Changer.CurrentUser.User_ID
                //               select services).ToList();

                var Services = db.Services.GroupJoin(db.Orders,
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
                        Orders = order
                    }).ToList();

                foreach(var f in Services)
                {
                    Console.WriteLine(f.ToString());
                    
                    
                }
              
                
                return buffservices;
            }

        }

    }
}
