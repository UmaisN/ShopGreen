using System.ComponentModel.DataAnnotations;

namespace ShopGreen.Server.Models
{
    public class Products
    {
        [Key] public int ProductId { get; set; }
        [Required] public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public ICollection<Sales> Sales { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
