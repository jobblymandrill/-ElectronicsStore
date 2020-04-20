
namespace ElectronicsStore.API.Models.OutputModels
{
    public class ProductOutputModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public CategoryOutputModel Category { get; set; }
    }
}
