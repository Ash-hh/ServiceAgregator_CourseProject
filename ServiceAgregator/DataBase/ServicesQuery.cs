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

    }
}
