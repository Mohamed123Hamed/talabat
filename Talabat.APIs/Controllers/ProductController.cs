using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo,
                                 IGenericRepository<ProductBrand> BrandRepo,
                                 IGenericRepository<ProductType> TypeRepo,IMapper mapper)
        {
            _productRepo = ProductRepo;
            _brandRepo = BrandRepo;
            _typeRepo = TypeRepo;
            _mapper = mapper;
        }

        // basUrl/api/product        (Get)
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> Getsproducts(string? Sort, int? brandId , int? typeId)
        { 
         var spec = new ProductWithBrandAndTypeSpecifications(Sort, brandId, typeId); // refactor and using specifications
            spec.Includes.Add(p=>p.ProductBrand);
            spec.Includes.Add(p => p.ProductType);
            var Products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products));
        }
     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
          var spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (Product is null) return NotFound(new ApiResponse(404)); // Not Found
            return Ok( _mapper.Map<Product,ProductToReturnDto>(Product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands() 
        {
            var Brands =await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        { 
        var types = await _typeRepo.GetAllAsync();
            return Ok(types);
        }
    }
}