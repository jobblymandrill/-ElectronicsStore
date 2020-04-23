using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class ProductMapping
    {
        public static List<Product> ToDataModels (List<ProductInputModel> inputModels)
        {
            List<Product> result = new List<Product>();
            foreach(var item in inputModels)
            {
                result.Add(ToDataModel(item));
            }
            return result;

        }

        public static Product ToDataModel (ProductInputModel inputModel)
        {
            return new Product
            {
                Id = (long)inputModel.Id,
                Name = inputModel.Name,
                Price = inputModel.Price,
                TradeMark = inputModel.TradeMark,
                Category = CategoryMapping.ToDataModel(inputModel.Category)
            };
        }

        public static List<ProductOutputModel> ToOutputModels (List<Product> dataModels)
        {
            List<ProductOutputModel> result = new List<ProductOutputModel>();
            foreach(var item in dataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }

        public static ProductOutputModel ToOutputModel (Product dataModel)
        {
            return new ProductOutputModel
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Price = dataModel.Price,
                TradeMark = dataModel.TradeMark,
                Category = CategoryMapping.ToOutputModel(dataModel.Category)
            };
        }
    }
}
