using ElecronicsStore.DB.Models;

namespace ElectronicsStore.Tests.Mocks.DataModelMocks
{
    public class CategoryDataModelMock
    {
        public static Category categoryDataModel = new Category()
        {
            Id = 2,
            Name = "Жопа",
            SubcategoryId = 1
        };
    }
}
