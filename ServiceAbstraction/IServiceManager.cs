using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }

        public IBasketService BasketService { get;  }
        public IAuthentaction Authentaction { get; }

        public IOrderService OrderService { get; }

        public IPaymentService paymentService { get;  }
    }
}
