using System.Text.Json.Serialization;

namespace HappenCodeECommerceAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        [JsonIgnore]
        public Customer Customer { get; set; } = null!;
        public List<CartItem> Items { get; set; } = new();
        
    }
}
