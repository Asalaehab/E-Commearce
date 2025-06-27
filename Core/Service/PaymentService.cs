using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using shared.DataTransferObjects.BasketDto_s;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModels.Product;
namespace Service
{
    internal class PaymentService(IConfiguration _configuration,
        IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            //Configure Strip : Install Package Strip.Net
            StripeConfiguration.ApiKey =_configuration["StripeSettings:SecretKey"];
            //Get Basket By BasketId 
            var BasKet = await _basketRepository.GetBasketAsync(BasketId)?? throw new BasketNotFoundException(BasketId);
            //To can get Amount -Get Product +Delivery Method Cost
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();


            foreach (var item in BasKet.Items)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id)??throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;//To Make Sure that All is Equals
            }
            ArgumentNullException.ThrowIfNull(BasKet.deliveryMethodId);//throw Exception if DeliveryMethodId id Null
            var DeliveryMethod=await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(BasKet.deliveryMethodId.Value)??
                throw new DeliveryMethodNotFoundException(BasKet.deliveryMethodId.Value);

            BasKet.shippingPrice = DeliveryMethod.Cost;

            var BasketAmount =(long)(BasKet.Items.Sum(item => item.Quantity * item.Price) * DeliveryMethod.Cost)*100;


            //Create Payment Intent[Create-Update)
            var PaymentService = new PaymentIntentService();
            if(BasKet.paymentIntentId is null)//create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount=BasketAmount,
                    Currency="USD",
                    PaymentMethodTypes = ["card"]
                };
               var PaymentIntent= await PaymentService.CreateAsync(options);
               BasKet.paymentIntentId=PaymentIntent.Id;
               BasKet.clientSecret=PaymentIntent.ClientSecret;
            }
            else //update
            {
                var Options = new PaymentIntentUpdateOptions() 
                { 
                    Amount = BasketAmount
                };
                await PaymentService.UpdateAsync(BasKet.paymentIntentId,Options);
            }

            await _basketRepository.CreateOrUpdateBasketAsync(BasKet);

            return _mapper.Map<BasketDto>(BasKet);

        }
    }
}
