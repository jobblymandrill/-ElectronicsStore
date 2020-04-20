using ElectronicsStore.API.Models.InputModels.Components;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.InputModels
{
    public class OrderInputModel
    {
        public List<ProductsWithAmountInputModel> Products { get; set; }
        public CustomerInputModel Customer { get; set; } 
        public string DateTime { get; set; }
        public string FilialName { get; set; }
    }
}
