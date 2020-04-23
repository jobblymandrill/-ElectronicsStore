﻿using ElecronicsStore.DB.Models;

namespace ElectronicsStore.API.Models.OutputModels
{
    public class ProductOutputModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TradeMark { get; set; }
        public CategoryOutputModel Category { get; set; }
    }
}
