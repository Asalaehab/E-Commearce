using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Peristance.Data;
using Peristance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Peristance
{
    public class DataSeeding(StoreDbContext _dbcontext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _context) : IDataSeeding
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
                if (!_dbcontext.Set<DeliveryMethod>().Any())
                {
                    var ProductsData = File.OpenRead(@"..\Infrastructure\Peristance\Data\DataSeed\delivery.json");

                    var DeliveryMethodsList = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(ProductsData);

                    if (DeliveryMethodsList is not null && DeliveryMethodsList.Any())
                        await _dbcontext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethodsList);


                }
                await   _dbcontext.SaveChangesAsync();

            }
            catch
            {
                //TO DO
            }


        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "01149114891",
                        UserName = "MohamedTarek"
                    };

                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Aly",
                        PhoneNumber = "01124926497",
                        UserName = "SalmaAly"
                    };

                    await _userManager.CreateAsync(User01, "Pa$sw0rd");
                    await _userManager.CreateAsync(User02, "Pa$sw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
                _context.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
