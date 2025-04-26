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
        public async Task DataSeedAsync()
        {
            // happen only time
            //you have to check All Migrations


            try
            {
                if ((await _dbcontext.Database.GetPendingMigrationsAsync()).Any())
                {
                  await   _dbcontext.Database.MigrateAsync();
                }


                //Check if the ProductBrands has any Data on it
                if (!_dbcontext.ProductBrands.Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Peristance\Data\DataSeed\brands.json");

                    var ProductBrandsList = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrandsList is not null && ProductBrandsList.Any())
                      await  _dbcontext.ProductBrands.AddRangeAsync(ProductBrandsList);
                  
                }



                if (!_dbcontext.ProductTypes.Any())
                {

                    var ProductTypesData = File.OpenRead(@"..\Infrastructure\Peristance\Data\DataSeed\types.json");

                    var ProductTypesList = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);

                    if (ProductTypesList is not null && ProductTypesList.Any())
                        await _dbcontext.ProductTypes.AddRangeAsync(ProductTypesList);

                }


                if (!_dbcontext.Products.Any())
                {
                    var ProductsData = File.OpenRead(@"..\Infrastructure\Peristance\Data\DataSeed\products.json");

                    var productsList =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);

                    if (productsList is not null && productsList.Any())
                       await _dbcontext.Products.AddRangeAsync(productsList);
                   

                }
              await   _dbcontext.SaveChangesAsync();

            }
            catch
            {
                //TO DO
            }


        }
    }
}
