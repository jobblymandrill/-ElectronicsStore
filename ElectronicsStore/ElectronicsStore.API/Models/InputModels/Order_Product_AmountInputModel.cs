namespace ElectronicsStore.API.Models.InputModels
{
    public class Order_Product_AmountInputModel
    {
        public long? Id { get; set; }
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Amount { get; set; }
    }
}
