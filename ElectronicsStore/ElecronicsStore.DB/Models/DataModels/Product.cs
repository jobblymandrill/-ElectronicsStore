
namespace ElecronicsStore.DB.Models.DataModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
