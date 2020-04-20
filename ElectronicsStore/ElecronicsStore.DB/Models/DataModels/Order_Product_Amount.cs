
namespace ElecronicsStore.DB.Models.DataModels
{
    public class Order_Product_Amount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}
