
namespace ElectronicsStore.API.Models.InputModels
{
    public class ProductInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public CategoryInputModel Category { get; set; }
    }
}
