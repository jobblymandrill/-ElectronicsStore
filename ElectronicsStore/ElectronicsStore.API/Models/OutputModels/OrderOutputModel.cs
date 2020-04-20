using ElectronicsStore.API.Models.OutputModels.Components;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class OrderOutputModel
    {
        public long Id { get; set; }
        public decimal TotalCost { get; set; }
        public List<ProductsWithAmountOutputModel> Products { get; set; }
        public CustomerOutputModel Customer { get; set; }
        public string DateTime { get; set; }
        public ShortFilialOutputModel Filial { get; set; }
    }
}
