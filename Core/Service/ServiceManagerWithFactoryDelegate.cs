using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> productfactory,
        Func<IOrderService> OrderFactor,
        Func<IBasketService> BasketFactory,
        Func<IAuthentaction> AuthentactionFactory) : IServiceManager
    {
        public IProductService ProductService => productfactory.Invoke();
        public IBasketService BasketService => BasketFactory.Invoke();

        public IAuthentaction Authentaction => AuthentactionFactory.Invoke();

        public IOrderService OrderService => OrderFactor.Invoke();
    }
}
