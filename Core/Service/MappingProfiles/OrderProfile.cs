using AutoMapper;
using DomainLayer.Models.OrderModels;
using shared.DataTransferObjects.IdentityDTO_S;
using shared.DataTransferObjects.OrderDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, AddressDto>();

            CreateMap<Order, OrderToReturn>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());



        }
    }
}
