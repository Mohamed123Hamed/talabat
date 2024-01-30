using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any()) // send dataseeding only time
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                if (Brands?.Count > 0) // if(brands is not null && count >0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            //////////////////////////////
            if (!dbcontext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0) // if(types is not null && count >0)
                {
                    foreach (var type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(type);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            ////////////////
            if (!dbcontext.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0) // if(brands is not null && count >0)
                {
                    foreach (var product in products)
                    {
                        await dbcontext.Set<Product>().AddAsync(product);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
           
        }
    }
}
