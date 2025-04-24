using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Peristance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peristance
{
    public class DataSeeding(StoreDbContext _dbcontext) : IDataSeeding
    {
        public void DataSeed()
        {
            // happen only time
            //you have to check All Migrations


            try
            {
                if (_dbcontext.Database.GetPendingMigrations().Any())
                {
                    _dbcontext.Database.Migrate();
                }


                //Check if the ProductBrands has any Data on it
                if (!_dbcontext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Peristance\Data\DataSeed\brands.json");

                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrands is not null && ProductBrands.Any())
                        _dbcontext.ProductBrands.AddRange(ProductBrands);

                }



                if (!_dbcontext.ProductTypes.Any())
                {

                    var ProductTypesData = File.ReadAllText(@"..\Infrastructure\Peristance\Data\DataSeed\types.json");

                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);

                    if (ProductTypes is not null && ProductTypes.Any())
                        _dbcontext.ProductTypes.AddRange(ProductTypes);
                }


                if (!_dbcontext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Infrastructure\Peristance\Data\DataSeed\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (products is not null && products.Any())
                        _dbcontext.Products.AddRange(products);

                }
                _dbcontext.SaveChanges();
            }
            catch
            {
                //TO DO
            }


        }
    }
}
