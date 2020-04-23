namespace ElectronicsStore.API.Models.OutputModels
{
    public class CategoryOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SubcategoryId { get; set; }
    }
}
