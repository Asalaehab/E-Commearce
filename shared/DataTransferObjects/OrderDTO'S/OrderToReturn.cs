using shared.DataTransferObjects.IdentityDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.DataTransferObjects.OrderDTO_S
{
    public class OrderToReturn
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;

        public DateTimeOffset OrderDate { get; set; }

        public AddressDto shipToAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;

        public decimal deliveryCost { get; set; }
        public string status { get; set; } = default!;

        public ICollection<OrderItemDTO> Items { get; set; } = [];

        public decimal Subtotal { get; set; }

        //[NotMapped]
        //public decimal Total { get=> SubTotal }
        public decimal Total {  get; set; }
    }
}
