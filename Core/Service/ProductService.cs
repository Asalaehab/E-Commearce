using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
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
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            
            var productDto = _mapper.Map<Product, ProductDto>(product);
            return productDto;

        }
    }
}
