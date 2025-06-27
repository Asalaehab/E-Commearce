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


        //Get Delivery Methods
        Task<IEnumerable<DeliveryMethodsDto>> GetDeliveryMethodsAsync();

        //Get All Orders => IEnumberable
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string Email);

        //Get Order By Id
        Task<OrderToReturn> GetOrderByIdAsync(Guid id);


    }
}
