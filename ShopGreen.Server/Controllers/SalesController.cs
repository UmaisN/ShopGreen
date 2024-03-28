using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopGreen.Server.Data;
using ShopGreen.Server.Models;

namespace ShopGreen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public SalesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Get all sales
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _appDbContext.Sales.ToListAsync();

            return Ok(sales);
        }

        //Create a new sale
        [HttpPost]
        public async Task<IActionResult> CreateSale(Sales sale)
        {
            _appDbContext.Sales.Add(sale);
            await _appDbContext.SaveChangesAsync();

            return Ok(sale);
        }

        // Get a single sale by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleByID(int id)
        {
            var sale = await _appDbContext.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

        // Update a sale
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaleByID(int id, [FromBody] Sales sale)
        {
            if (id != sale.SaleId)
            {
                return BadRequest();
            }

            _appDbContext.Entry(sale).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDbContext.Sales.Any(e => e.SaleId == id))
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

        // Delete a sale
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleByID(int id)
        {
            var sale = await _appDbContext.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _appDbContext.Sales.Remove(sale);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
