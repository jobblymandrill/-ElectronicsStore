using System;
using System.Collections.Generic;

namespace ElecronicsStore.DB.Models
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public Filial Filial { get; set; }
        public List<Order_Product_Amount> Products { get; set; }
    }
}
