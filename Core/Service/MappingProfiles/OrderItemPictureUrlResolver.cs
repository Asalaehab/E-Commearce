using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using shared.DataTransferObjects.OrderDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Product.PictureUrl))
                return string.Empty;

            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}/{source.Product.PictureUrl}";
                return Url;
            }
        }
    }

}
