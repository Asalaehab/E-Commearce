using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All Products
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        //Get Product by Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product= await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types =await _serviceManager.ProductService.GetAlltypesAsync();
            return Ok(types);
        }

        //Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands =await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }


    }
}
