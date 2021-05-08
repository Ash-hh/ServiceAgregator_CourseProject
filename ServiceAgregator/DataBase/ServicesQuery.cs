using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceAgregator.Models;

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
                var bufflist = db.Services.ToList<Services>();
                foreach (Services buff in bufflist)
                {
                    services.Add(buff);
                }
                return services;
            }
            
        }

        public ObservableCollection<Users> GetAllUsers()
        {
            using (DBContext db = new DBContext())
            {
                ObservableCollection<Users> users = new ObservableCollection<Users>();
                var bufflist = db.Users.ToList<Users>();
                foreach (Users buff in bufflist)
                {
                    users.Add(buff);
                }
                return users;
            }
                
        }




    }
}
