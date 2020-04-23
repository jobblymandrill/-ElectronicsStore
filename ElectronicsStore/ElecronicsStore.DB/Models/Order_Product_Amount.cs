namespace ElecronicsStore.DB.Models
{
    public class Order_Product_Amount
    {
        public long Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set;  }
        public int Amount { get; set; }
    }
}
