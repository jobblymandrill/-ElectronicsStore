using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class OrderMapping
    {
        public static List<Order> ToDataModels (List<OrderInputModel> inputModels)
        {
            List<Order> result = new List<Order>();
            foreach(var item in inputModels)
            {
                result.Add(ToDataModel(item));
            }
            return result;
        }

        public static Order ToDataModel (OrderInputModel inputModel)
        {
            return new Order
            {
                Id = (long)inputModel.Id,
                DateTime = DateTime.ParseExact(inputModel.DateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Filial = new Filial { Id = inputModel.FilialId },
                Products = Order_Product_AmountMapping.ToDataModels(inputModel.Products)
            };
        }

        public static List<OrderOutputModel> ToOutputModels (List<Order> fullDataModels)
        {
            List<OrderOutputModel> result = new List<OrderOutputModel>();
            foreach(var item in fullDataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }

        public static OrderOutputModel ToOutputModel (Order dataModel)
        {
            return new OrderOutputModel
            {
                Id = dataModel.Id,
                DateTime = Convert.ToDateTime(dataModel.DateTime).ToString(@"dd.MM.yyyy"),
                Filial = FilialMapping.ToOutputModel(dataModel.Filial),
                Products = Order_Product_AmountMapping.ToOutputModels(dataModel.Products)
            };
        }
    }
}
