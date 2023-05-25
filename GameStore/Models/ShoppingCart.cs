using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser identityUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public Product product { get; set; }

        public int Quantity { get; set; }

    }
}
