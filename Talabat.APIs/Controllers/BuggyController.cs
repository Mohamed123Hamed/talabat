using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext context)
        {
            _dbcontext = context;
        }

        /////// 1.. Not Found
        [HttpGet("notFound")]  // Get : BaseUrl/api/Buggy/notFound    // Not Found
        public ActionResult GetNotFoundRequest()
        { 
         var Product = _dbcontext.Products.Find(100);
           if (Product == null) return NotFound(new ApiResponse(404));
           return Ok(Product);
        }

        ///////// 2.. Server Error
        [HttpGet("serverError")] // Get : BaseUrl/api/Buggy/serverError
        public ActionResult GetServerError() 
        {
            var Producct = _dbcontext.Products.Find(100);
            var ProductToReturn = Producct.ToString(); //will Throw Exception[will Reference Exeception]
            return Ok(ProductToReturn);
        }
       
        ////// 3.. Bad Request
        [HttpGet("badRequest")]        // Get :  /api/Buggy/badRequest
        public ActionResult GetBadRequest() 
        {
            return BadRequest(new ApiResponse(400));
        }

        ////// 4.. Validation Error
        [HttpGet("badRequest/{id}")]      // Get :  /api/Buggy/badRequest / 1
        public ActionResult GetBadRequest(int id) 
        {
            return Ok();
        }
    }
}
