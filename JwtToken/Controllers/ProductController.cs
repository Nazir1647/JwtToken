using JwtToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly CloneDbContext _context;
        public ProductController(CloneDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
       // [Authorize(Roles = "user")]
        [Authorize]
        public IActionResult Get()
        {
            var data = _context.Products.ToList();
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveProduct")]
        //[Authorize(Roles = "user")]
        public IActionResult SaveProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Data save successfully");
        }
    }
}
