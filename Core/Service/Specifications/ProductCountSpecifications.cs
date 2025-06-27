using DomainLayer.Models.ProductModels;
using shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) :  base(p => (!queryParams.BrandId.HasValue || queryParams.BrandId == p.BrandId)
            &&(!queryParams.TypeId.HasValue  || queryParams.TypeId == p.BrandId)
            &&(string.IsNullOrWhiteSpace(queryParams.search)||p.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            
        }
    }
}
