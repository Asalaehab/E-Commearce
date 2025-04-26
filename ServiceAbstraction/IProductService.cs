using shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        //GetAll Product
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
       
        //get product by Id
        Task<ProductDto> GetProductByIdAsync(int productId);

        //get all types
        Task<IEnumerable<TypeDto>> GetAlltypesAsync();

        //get all brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

    }
}
