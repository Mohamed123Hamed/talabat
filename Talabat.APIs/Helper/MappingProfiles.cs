using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                    .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                    .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                    .ForMember(d=>d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
