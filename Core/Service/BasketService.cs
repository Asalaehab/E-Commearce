using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModels;
using ServiceAbstraction;
using shared.DataTransferObjects.BasketDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basketDto);

            var IsCreatedOrUpdated=_basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if(IsCreatedOrUpdated is not null)
            {
                return await GetBasketDto(basketDto.Id);
            }
            else
            {
                throw new Exception("Can Not Update Or Create Basket Now,Try Again");
            }

        }

        public Task<bool> DeleteBasketAsync(string Key)=>
           _basketRepository.DeleteBasketAsync(Key);


        public async Task<BasketDto> GetBasketDto(string Key)
        {
            var Basket=await _basketRepository.GetBasketAsync(Key);
            if(Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
            {
                throw new  BasketNotFoundException(Key);
            }

        }
    }
}
