using System;

namespace ElecronicsStore.DB.Models.DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FilialId { get; set; }
        public DateTime Time { get; set; }
    }
}
