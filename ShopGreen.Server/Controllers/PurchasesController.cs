using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopGreen.Server.Data;
using ShopGreen.Server.Models;

namespace ShopGreen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public PurchasesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //Get all purchases
        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _appDbContext.Purchases.ToListAsync();

            return Ok(purchases);
        }

        //Create a new purchase
        [HttpPost]
        public async Task<IActionResult> CreatePurchase(Purchases purchase)
        {
            _appDbContext.Purchases.Add(purchase);
            await _appDbContext.SaveChangesAsync();

            return Ok(purchase);
        }

        // Get a single purchase by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseByID(int id)
        {
            var purchase = await _appDbContext.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }

        // Update a purchase
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseByID(int id, [FromBody] Purchases purchase)
        {
            if (id != purchase.PurchaseId)
            {
                return BadRequest();
            }

            _appDbContext.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDbContext.Purchases.Any(e => e.PurchaseId == id))
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

        // Delete a purchase
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseByID(int id)
        {
            var purchase = await _appDbContext.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _appDbContext.Purchases.Remove(purchase);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
