using DomainLayer.Models;
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
        public ProductWityhBrandSpecifications(int? BrandId,int? TypeId):
            base(p => (!BrandId.HasValue || BrandId == p.BrandId)
            &&(!TypeId.HasValue  || TypeId==p.BrandId))
        {
            AddInclude(p=>p.productType);
            AddInclude(p => p.productBrand);
        }

        public ProductWityhBrandSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.productType);
            AddInclude(p => p.productBrand);

        }
    }
}
