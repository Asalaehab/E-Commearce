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
    [ApiController]
    [Route("api/[Controller]")]
    public class OrdersController(IServiceManager _serviceManager) : APIBaseController
    {
        //Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturn>>createOrder(OrderDto orderDto)
        {
            //var Email=User.FindFirstValue(ClaimTypes.Email);//get email from token

            var Order =await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken());

            return Ok(Order);
        }


        //Get Delivery Methods
        //[HttpGet("DeliveryMethod")]
        //public Task<ActionResult<IEnumerable<DeliveryMethodsDto>>>


        //Get All Order By Email

        //Get Order By Id



    }
}
