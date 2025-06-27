using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModels;
using Service.Specifications;
using ServiceAbstraction;
using shared.DataTransferObjects.IdentityDTO_S;
using shared.DataTransferObjects.OrderDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturn> CreateOrder(OrderDto orderDto, string Email)
        {
            //Mapping Address to Order Address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.shipToAddress);

            //Get basket
            var Basket =await _basketRepository.GetBasketAsync(orderDto.BasketId)?? throw new BasketNotFoundException(Email);

            ArgumentNullException.ThrowIfNull(Basket.paymentIntentId);
            var orderSpec = new OrderWithSpecifications(Basket.paymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var ExisitingOrder =await OrderRepo.GetByIdAsync(orderSpec);
            if (ExisitingOrder is not null) OrderRepo.Delete(ExisitingOrder);
            //Create OrderItem List
            List<OrderItem> orderItems = [];
            
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach(var item in Basket.Items)
            {
                //orderItems
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }

            //Get Delivery Method
            var DeliveryMethod=await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
            ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            //calculate sub Total
            var SubTotal=orderItems.Sum(I=>I.Quantity*I.Price);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, SubTotal, orderItems,Basket.paymentIntentId);

            //Add Order To Order Tbl

           await OrderRepo.AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturn>(Order);

        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModels.BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder() { ProductId = product.Id, PictureUrl = product.PictureUrl, ProductName = product.Name },
                Price = product.Price,
                Quantity = item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodsDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodsDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string Email)
        {
            var spec=new OrderModuleSpecifications(Email);
            var Orders=await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(Orders);
        }

        public async Task<OrderToReturn> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderModuleSpecifications(id);
            var Order =await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(id);
            return _mapper.Map<OrderToReturn>(Order);   
        }
    }
}
