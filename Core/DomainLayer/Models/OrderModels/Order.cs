using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod,  decimal subtotal, ICollection<OrderItem> items)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            Items = items;
        }
        
        public string UserEmail { get; set; } = default!;

        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public OrderStatus OrderStatus { get; set; }
        public decimal Subtotal { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; }//FK
        //[NotMapped]
        //public decimal Total { get=> SubTotal }
        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
    }

}
