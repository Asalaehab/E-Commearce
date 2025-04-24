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

                    var ProductBrandsList = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrandsList is not null && ProductBrandsList.Any())
                        _dbcontext.ProductBrands.AddRange(ProductBrandsList);
                  
                }



                if (!_dbcontext.ProductTypes.Any())
                {

                    var ProductTypesData = File.ReadAllText(@"..\Infrastructure\Peristance\Data\DataSeed\types.json");

                    var ProductTypesList = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);

                    if (ProductTypesList is not null && ProductTypesList.Any())
                        _dbcontext.ProductTypes.AddRange(ProductTypesList);
                  
                }


                if (!_dbcontext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Infrastructure\Peristance\Data\DataSeed\products.json");

                    var productsList = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (productsList is not null && productsList.Any())
                        _dbcontext.Products.AddRange(productsList);
                   

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
