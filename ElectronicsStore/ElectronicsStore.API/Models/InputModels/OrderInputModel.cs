using System.Collections.Generic;

namespace ElectronicsStore.API.Models.InputModels
{
    public class OrderInputModel
    {
        public long? Id { get; set; }
        public string DateTime { get; set; }
        public int FilialId { get; set; }
        public List<Order_Product_AmountInputModel> Products { get; set; }
    }
}
