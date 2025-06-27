using E_Commearce.web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using shared.DataTransferObjects.OrderDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : APIBaseController
    {
        //Create Order
        
        [HttpPost]
        public async Task<ActionResult<OrderToReturn>> createOrder(OrderDto orderDto)
        {
            //var Email=User.FindFirstValue(ClaimTypes.Email);//get email from token

            var Order = await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());

            return Ok(Order);
        }


        //Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDto>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }
        //Get All Order By Email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturn>>> GetAllOrders()
        {
            var Order = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Order);
        }
        //Get Order By Id
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturn>> GetOrderById(Guid id)
        {
            var Order =await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }


    }
}
