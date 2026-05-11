namespace MiniMagaza.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Quantity { get; set; }
        // Toplam fiyatı otomatik hesaplayan özellik
        public decimal TotalPrice => Price * Quantity;
    }
}