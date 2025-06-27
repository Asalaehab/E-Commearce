using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderModuleSpecifications : BaseSpecifications<Order, Guid>
    {
        //Get All Orders By Email
        public OrderModuleSpecifications(string Email) : base(O => O.BuyerEmail == Email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderByDescending(O => O.OrderDate);
        }


        public OrderModuleSpecifications(Guid id):base(O=>O.Id == id) 
        {
            AddInclude(O => O.DeliveryMethodId);
            AddInclude(O => O.Items);
        }
    }
}
