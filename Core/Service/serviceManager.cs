using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class serviceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProdutService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProdutService.Value;


        private readonly Lazy<IBasketService> LazyBasketService = new Lazy<IBasketService>(()=>new BasketService(basketRepository,_mapper));
        public IBasketService BasketService => LazyBasketService.Value;
    }
}
