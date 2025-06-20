using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.DataTransferObjects.OrderDTO_S
{
    public class DeliveryMethodsDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string DeliveryTime { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
