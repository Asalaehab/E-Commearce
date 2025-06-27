using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
     class OrderWithSpecifications : BaseSpecifications<Order,Guid>
    {
        public OrderWithSpecifications(string paymentIntentId): base(O=>O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
