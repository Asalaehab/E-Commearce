using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using shared;
using shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands=await Repo.GetAllAsync();
            var BrandDtos=_mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);

            return BrandDtos;

        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWityhBrandSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(specifications);
            var ProductDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var productsCont = Products.Count();
            var TotalCount =await Repo.CountAsync(new ProductCountSpecifications(queryParams));
            return new PaginatedResult<ProductDto>(queryParams.PageIndex,productsCont, TotalCount, ProductDto);
        }

        public async Task<IEnumerable<TypeDto>> GetAlltypesAsync()
        {
            var Types=await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto=_mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productWithBrand = new ProductWityhBrandSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(productWithBrand);
            if (product is null)
            {
                throw new ProductNotFoundException(id);
            }


            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;

        }
    }
}
