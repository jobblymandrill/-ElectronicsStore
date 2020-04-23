using ElectronicsStore.API.Models.InputModels;

namespace ElectronicsStore.Tests.Mocks.InputModelMock
{
    public static class CategoryInputModelMock
    {
        public static CategoryInputModel categoryInputModel = new CategoryInputModel()
        {
            Id = 2,
            Name = "Жопа",
            SubcategoryId = 1
        };
    }
}
