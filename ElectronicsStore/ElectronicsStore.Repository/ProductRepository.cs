
using ElecronicsStore.DB.Models;
using ElecronicsStore.DB.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IProductStorage _productStorage;
        public ProductRepository(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }
        public void AddOneMillionProducts()
        {
            _productStorage.AddMillionProducts();
        }
    }
}
