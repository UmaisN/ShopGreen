using System.ComponentModel.DataAnnotations;

namespace ShopGreen.Server.Models
{
    public class Purchases
    {
        [Key] public int PurchaseId { get; set; }
        public int quantityPurchased { get; set; }
        public DateTime purchaseDate { get; set; }
        public decimal totalPurchaseAmount { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
