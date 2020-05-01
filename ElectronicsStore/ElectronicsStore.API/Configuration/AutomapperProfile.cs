using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using System;
using System.Globalization;

namespace ElectronicsStore.API.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProductInputModel, Product>();

            CreateMap<Product, ProductOutputModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category
                {
                    Name = src.Category.Name,
                    ParentCategory =
                new Category { Name = src.Category.ParentCategory.Name }
                }))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TradeMark, opt => opt.MapFrom(src => src.TradeMark));

            CreateMap<Category, CategoryOutputModel>()
            .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory.Name));

            CreateMap<CategoryWithNumber, CategoryWithNumberOutputModel>();

            CreateMap<ProductWithCity, ProductWithCityOutputModel>();

            CreateMap<PeriodInputModel, Period>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => DateTime.ParseExact(src.StartDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => DateTime.ParseExact(src.EndDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)));

            CreateMap<FilialWithIncome, FilialWithIncomeOutputModel>();

            CreateMap<IncomeByIsForeignCriteria, IncomeByIsForeignCriteriaOutputModel>();

            CreateMap<ProductSearchInputModel, ProductSearch>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category
                {
                    Id = src.Category.Id,
                    Name = src.Category.Name,
                    ParentCategory =
                new Category { Id = src.Category.ParentCategory.Id, Name = src.Category.ParentCategory.Name }
                }));

            CreateMap<OrderInputModel, Order>()
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateTime, "dd.mm.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Filial, opt => opt.MapFrom(src => new Filial
                {
                    Id = src.FilialId,
                    Name = null,
                    CountryName = null,
                    IsForeign = null
                }));
        }
    }
}
