using shared.DataTransferObjects.OrderDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        //public int MyProperty { get; set; }
        Task<OrderToReturn> CreateOrder(OrderDto orderDto,string Email);
    }
}
