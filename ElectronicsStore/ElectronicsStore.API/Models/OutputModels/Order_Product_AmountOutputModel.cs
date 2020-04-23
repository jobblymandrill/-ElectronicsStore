namespace ElectronicsStore.API.Models.OutputModels
{
    public class Order_Product_AmountOutputModel
    {
        public long Id { get; set; }
        public ProductOutputModel Product { get; set; }
        public OrderOutputModel Order { get; set; }
        public int Amount { get; set; }
    }
}
