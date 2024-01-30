using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        // This constructor is used for get All Products
        public ProductWithBrandAndTypeSpecifications(string? Sort, int? brandId, int? TypeId)
            :base(p => 
            (!brandId.HasValue || p.ProductBrandId == brandId) &&
            (!TypeId.HasValue || p.ProductTypeId == TypeId)
            )
        {
            if (!string.IsNullOrEmpty(Sort))
            { 
              switch (Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
            Includes.Add(p=>p.ProductBrand);
            Includes.Add(T =>T.ProductType);
        }
        // this constructor is used to for get a specific product by id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(T => T.ProductType);
        }
    }
}
