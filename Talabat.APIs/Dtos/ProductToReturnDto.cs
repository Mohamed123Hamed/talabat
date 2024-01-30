using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int ProductBrandId { get; set; } 
        // Navigational Property
        public string ProductBrand { get; set; }  

        public int ProductTypeId { get; set; }  
        // Navigational Property
        public string ProductType { get; set; } // contain name of type

    }
}
