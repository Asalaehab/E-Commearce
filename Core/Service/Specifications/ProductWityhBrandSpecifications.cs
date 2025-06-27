using DomainLayer.Models.ProductModels;
using shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    class ProductWityhBrandSpecifications :BaseSpecifications<Product,int>
    {
        //Get All Products with Types and Brands
        public ProductWityhBrandSpecifications(ProductQueryParams queryParams) :
            base(p => (!queryParams.BrandId.HasValue || queryParams.BrandId == p.BrandId)
            &&(!queryParams.TypeId.HasValue  || queryParams.TypeId == p.BrandId)
            &&(string.IsNullOrWhiteSpace(queryParams.search)||p.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            AddInclude(p=>p.productType);
            AddInclude(p => p.productBrand);

            switch (queryParams.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc
                    :
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.pageNumber);
        }

        public ProductWityhBrandSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.productType);
            AddInclude(p => p.productBrand);

        }
    }
}
