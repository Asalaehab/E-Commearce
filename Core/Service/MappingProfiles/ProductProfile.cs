using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Options;
using shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(dist=>dist.BrandName,Options=>Options.MapFrom(Src=>Src.productBrand.Name))
                .ForMember(dist=>dist.TypeName,Options=>Options.MapFrom(src=>src.productType.Name))
                .ForMember(dist=>dist.PictureUrl,Options=>Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
