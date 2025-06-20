using shared.DataTransferObjects.IdentityDTO_S;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.DataTransferObjects.OrderDTO_S
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;

        public int DeliveryMethodId { get; set; }

        public AddressDto Address { get; set; }=default!;


    }
}
