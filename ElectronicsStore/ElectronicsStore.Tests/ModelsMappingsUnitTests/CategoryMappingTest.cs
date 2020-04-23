using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.Mappings;
using ElectronicsStore.Tests.Mocks.DataModelMocks;
using ElectronicsStore.Tests.Mocks.InputModelMock;
using NUnit.Framework;

namespace ElectronicsStore.Tests.ModelsMappingsUnitTests
{
    [TestFixture]
    public class CategoryMappingTest
    {
        private CategoryInputModel _categoryInputModel;
        private Category _categoryDataModel;

        [OneTimeSetUp]
        public void SetUp()
        {
            _categoryInputModel = (CategoryInputModel)CategoryInputModelMock.categoryInputModel.Clone();
            _categoryDataModel = (Category)CategoryDataModelMock.categoryDataModel.Clone();
        }
        
        [Test]
        public void ShouldMapInputToDataModel()
        {
            var actual = CategoryMapping.ToDataModel(_categoryInputModel);
            Assert.IsTrue(_categoryDataModel.Equals(actual));
        }
    }
}
