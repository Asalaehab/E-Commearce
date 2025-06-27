using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    //it will not have PK
    //It will be mapped as column
    //as subtotal
    public class OrderAddress
    {
        public string firstname { get; set; } = default!;
        public string lastname { get; set; } = default!;
        public string city { get; set; } = default!;
        public string country { get; set; } = default!;
        public string street { get; set; } = default!;
    }
}
