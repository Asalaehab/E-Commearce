using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class serviceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository basketRepository,UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProdutService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProdutService.Value;


        private readonly Lazy<IBasketService> LazyBasketService = new Lazy<IBasketService>(()=>new BasketService(basketRepository,_mapper));
        public IBasketService BasketService => LazyBasketService.Value;


        private readonly Lazy<IAuthentaction> LazyAuthentaction = new Lazy<IAuthentaction>(() => new Authentaction(_userManager,_configuration,_mapper));
        public IAuthentaction Authentaction => LazyAuthentaction.Value;

        private readonly Lazy<IOrderService> LazyorderService = new Lazy<IOrderService>(() => new OrderService(_mapper,basketRepository, _unitOfWork));

        public IOrderService OrderService => LazyorderService.Value;
    }
}
