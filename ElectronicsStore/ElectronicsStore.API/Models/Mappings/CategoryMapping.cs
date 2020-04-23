using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System.Collections.Generic;

namespace ElectronicsStore.API.Models.Mappings
{
    public static class CategoryMapping
    {
        public static List<Category> ToDataModels(List<CategoryInputModel> inputModels)
        {
            List<Category> result = new List<Category>();
            foreach(var item in inputModels)
            {
                result.Add(ToDataModel(item));
            }
            return result;
        }

        public static Category ToDataModel(CategoryInputModel inputModel)
        {
            return new Category
            {
                Id = (int)inputModel.Id,
                Name = inputModel.Name,
                SubcategoryId = inputModel.SubcategoryId
            };
        }

        public static List<CategoryOutputModel> ToOutputModel(List<Category> dataModels)
        {
            List<CategoryOutputModel> result = new List<CategoryOutputModel>();
            foreach(var item in dataModels)
            {
                result.Add(ToOutputModel(item));
            }
            return result;
        }

        public static CategoryOutputModel ToOutputModel(Category dataModel)
        {
            return new CategoryOutputModel
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                SubcategoryId = dataModel.SubcategoryId
            };
        }
    }
}
