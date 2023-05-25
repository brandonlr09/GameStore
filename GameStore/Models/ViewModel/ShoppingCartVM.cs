namespace GameStore.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
