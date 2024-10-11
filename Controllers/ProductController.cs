using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using App.Models;
using System.Text.Json;

namespace App.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/products")] 
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Detail(int? id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] Product model)
        {


            var product = new Product
            {
                Name = model.Name,
                Price = model.Price
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            
            var auditTrail = new AuditTrail
            {
                userid = User.Identity.Name, 
                operationtype = "CREATE",
                datetime = DateTime.UtcNow,
                description = JsonSerializer.Serialize((product.)), 
                primarykey = product.Id.ToString() 
            };

            _context.AuditTrails.Add(auditTrail);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] Product model)
        {
            var product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            if (product == null)
            {
                return NotFound(); 
            }

            product.Name = model.Name;
            product.Price = model.Price;
            await _context.SaveChangesAsync();

           
            var auditTrail = new AuditTrail
            {
                userid = User.Identity.Name,
                operationtype = "UPDATE",
                datetime = DateTime.UtcNow,
                description = JsonSerializer.Serialize(product),
                primarykey = product.Id.ToString()
            };

            _context.AuditTrails.Add(auditTrail);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound(); 
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

           
            var auditTrail = new AuditTrail
            {
                userid = User.Identity.Name,
                operationtype = "DELETE",
                datetime = DateTime.UtcNow,
                description = JsonSerializer.Serialize(product), 
                primarykey = product.Id.ToString()
            };

            _context.AuditTrails.Add(auditTrail);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}









/*
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using App.Models;

namespace App.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

       
        [HttpGet("api/products")]
        [Authorize(Roles = "ADMIN")]

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("api/products/{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            return Ok(product);
        }

        [HttpPost("api/products")]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            var product = new Product();
            product.Id = model.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut("api/products/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product model)
        {
            var product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            product.Name = model.Name;
            product.Price = model.Price;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("api/products/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

*/