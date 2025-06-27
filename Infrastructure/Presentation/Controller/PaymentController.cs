using E_Commearce.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using shared.DataTransferObjects.BasketDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class PaymentControlle(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
              
        {
         var basket=  await _serviceManager.paymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(basket);
        }
    }
}
