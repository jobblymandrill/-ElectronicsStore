using ElectronicsStore.API.Models.OutputModels.Components;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class FilialOutputModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public List<ProductsWithAmountOutputModel> AvailiableProducts { get; set; }
    }
}
