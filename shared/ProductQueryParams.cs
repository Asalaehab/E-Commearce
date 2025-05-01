using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }

        public int? BrandId { get; set; }

        public ProductSortingOptions sortingOptions { get; set; }

        public string? SearchValue { get; set; }
    }
}
