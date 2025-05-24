using shared.DataTransferObjects.BasketDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketDto(string Key);

        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);

        Task<bool> DeleteBasketAsync(string Key);
    }
}
