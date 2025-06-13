using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.DataTransferObjects.IdentityDTO_S
{
    public class AddressDto
    {
        public string City { get; set; } = default!;
        public string street { get; set; }=default!;
        public string Country { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
