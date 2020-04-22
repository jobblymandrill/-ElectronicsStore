
using ElecronicsStore.DB.Models.DataModels;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class OrderMapping
    {
        public static Order FromInputToDataModel (OrderInputModel inputModel)
        {
            return new Order();
        }
    }
}
