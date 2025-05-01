using AutoMapper;
using DomainLayer.Contracts;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? Brandid, int? typeid,ProductSortingOptions sortingOptions)
        {
            var specifications = new ProductWityhBrandSpecifications(Brandid,typeid, sortingOptions);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            var ProductDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return ProductDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAlltypesAsync()
        {
            var Types=await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto=_mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var productWithBrand=new ProductWityhBrandSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(productWithBrand);
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;

        }
    }
}
