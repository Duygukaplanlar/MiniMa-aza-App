namespace MiniMagaza.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Bir kategoride birden fazla ürün olabilir
        public List<Product> Products { get; set; }
    }
}
