using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopGreen.Server.Data;
using ShopGreen.Server.Models;

namespace ShopGreen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProductsController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        //Get all products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _appDbContext.Products.ToListAsync();

            return Ok(products);
        }

        //Create a new product
        [HttpPost]
        public async Task<IActionResult> AddProduct(Products product)
        {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        // Get a single product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // Update a product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductByID(int id, [FromBody] Products product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _appDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDbContext.Products.Any(e => e.ProductId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductByID(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
