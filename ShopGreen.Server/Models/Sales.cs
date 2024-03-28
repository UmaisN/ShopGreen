using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopGreen.Server.Models
{
    public class Sales
    {
        [Key] public int SaleId { get; set; }
        public int quantitySold { get; set; }
        public DateTime saleDate { get; set; }
        public decimal totalSaleAmount { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
