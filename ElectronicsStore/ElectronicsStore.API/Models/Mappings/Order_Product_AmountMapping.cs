using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class Order_Product_AmountMapping
    {
        public static List<Order_Product_Amount> ToDataModels (List<Order_Product_AmountInputModel> inputModels)
        {
            List<Order_Product_Amount> result = new List<Order_Product_Amount>();
            foreach(var item in inputModels)
            {
                result.Add(ToDataModel(item));
            }
            return result;
        }

        public static Order_Product_Amount ToDataModel (Order_Product_AmountInputModel inputModel)
        {
            return new Order_Product_Amount
            {
                Id = (long)inputModel.Id,
                Product = new Product { Id = (int)inputModel.Id },
                Order = new Order { Id = (long)inputModel.Id},
                Amount = inputModel.Amount
            };
        }

        public static Order_Product_AmountOutputModel ToOutputModel (Order_Product_Amount dataModel)
        {
            return new Order_Product_AmountOutputModel
            {
                Id = dataModel.Id,
                Product = ProductMapping.ToOutputModel(dataModel.Product),
                Order = OrderMapping.ToOutputModel(dataModel.Order),
                Amount = dataModel.Amount
            };
        }

        public static List<Order_Product_AmountOutputModel> ToOutputModels (List <Order_Product_Amount> dataModels)
        {
            List<Order_Product_AmountOutputModel> result = new List<Order_Product_AmountOutputModel>();
            foreach(var item in dataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }
    }
}
