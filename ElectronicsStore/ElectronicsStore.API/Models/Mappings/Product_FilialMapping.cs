using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.Mappings
{
    public class Product_FilialMapping
    {
        public static Product_Filial ToDataModel (Product_FilialInputModel inputModel)
        {
            return new Product_Filial
            {
                Id = inputModel.Id,
                Filial = new Filial { Id = inputModel.FilialId},
                Product = new Product { Id = inputModel.ProductId},
                Amount = inputModel.Amount
            };
        }

        public static List<Product_Filial> ToDataModels (List<Product_FilialInputModel> inputModels)
        {
            List<Product_Filial> result = new List<Product_Filial>();
            foreach(var item in inputModels)
            {
                result.Add(ToDataModel(item));
            }
            return result;
        }

        public static Product_FilialOutputModel ToOutputModel (Product_Filial dataModel)
        {
            return new Product_FilialOutputModel
            {
                Id = dataModel.Id,
                Filial = FilialMapping.ToOutputModel(dataModel.Filial),
                Product = ProductMapping.ToOutputModel(dataModel.Product),
                Amount = dataModel.Amount
            };
        }

        public static List<Product_FilialOutputModel> ToOutputModels (List<Product_Filial> dataModels)
        {
            List<Product_FilialOutputModel> result = new List<Product_FilialOutputModel>();
            foreach(var item in dataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }
    }
}
