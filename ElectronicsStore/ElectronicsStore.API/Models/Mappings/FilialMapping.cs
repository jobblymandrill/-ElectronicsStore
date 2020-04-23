using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.OutputModels;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class FilialMapping
    {
        public static List<FilialOutputModel> ToOutputModels(List<Filial> dataModels)
        {
            List<FilialOutputModel> result = new List<FilialOutputModel>();
            foreach (var item in dataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }

        public static FilialOutputModel ToOutputModel(Filial dataModel)
        {
            return new FilialOutputModel
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Country = dataModel.Country,
                IsForeign = dataModel.IsForeign
            };
        }
    }
}
