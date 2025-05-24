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
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get Basket
        [HttpGet]//Get BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string id) 
        {
            var Basket =await _serviceManager.BasketService.GetBasketDto(id);
            return Ok(Basket);
            
        }

        //Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        //Delete
        [HttpDelete("{Key}")]//DELETE
        public async Task<ActionResult<bool>>DeleteBasket(string id)
        {
            var Result =await _serviceManager.BasketService.DeleteBasketAsync(id);
            return Ok(Result);
        }

    }
}
