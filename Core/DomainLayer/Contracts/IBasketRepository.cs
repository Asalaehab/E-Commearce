using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);


        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan? timeLive=null);

        Task<bool> DeleteBasketAsync(string id);

    }
}
