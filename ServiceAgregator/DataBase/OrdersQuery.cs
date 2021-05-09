using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgregator.DataBase
{
    class OrdersQuery
    {

        public void AddNewOrder(Models.Orders Order)
        {
            using (DBContext db = new DBContext())
            {
                if(Order!=null)
                {
                    db.Orders.Add(Order);
                    db.SaveChanges();
                }
            }
        }
    }
}
