
namespace ElectronicsStore.API.Models.InputModels
{
    public class CategoryInputModel
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }
        public SubcategoryInputModel Subcategory { get; set; }
    }
}
