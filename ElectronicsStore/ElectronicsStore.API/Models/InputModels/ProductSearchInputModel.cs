namespace ElectronicsStore.API.Models.InputModels
{
    public class ProductSearchInputModel
    {
        public long? Id { get; set; }
        public int? IdOperation { get; set; }
        public string Name { get; set; }
        public int? NameOperation { get; set; }
        public decimal? Price { get; set; }
        public int? PriceOperation { get; set; }
        public string TradeMark { get; set; }
        public int? TradeMarkOperation { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryIdOperation { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? ParentCategoryIdOperation { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryNameOperation { get; set; }
        public string ParentCategoryName { get; set; }
        public int? ParentCategoryNameOperation { get; set; }
    }
}
