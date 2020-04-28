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
            CreateMap<Product, ProductOutputModel>();
            CreateMap<Category, CategoryOutputModel>()
            .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory.Name));
            CreateMap<CategoryWithNumber, CategoryWithNumberOutputModel>();
            CreateMap<ProductWithCity, ProductWithCityOutputModel>();
            CreateMap<PeriodInputModel, Period>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => DateTime.ParseExact(src.StartDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => DateTime.ParseExact(src.EndDate, "dd.mm.yyyy", CultureInfo.InvariantCulture)));
            CreateMap<FilialWithIncome, FilialWithIncomeOutputModel>();
            CreateMap<IncomeByIsForeignCriteria, IncomeByIsForeignCriteriaOutputModel>();

            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
            //.ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Category.ParentName));
            //.ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToString(@"dd.MM.yyyy")))
            //.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString(@"dd.MM.yyyy")))
            //.ForMember(dest => dest.LastUpdateDate, opt => opt.MapFrom(src => src.LastUpdateDate.ToString(@"dd.MM.yyyy")));
            //CreateMap<Lead, LeadOutputModel>()
            //    .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
            //    .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToString(@"dd.MM.yyyy")))
            //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToString(@"dd.MM.yyyy")))
            //    .ForMember(dest => dest.LastUpdateDate, opt => opt.MapFrom(src => src.LastUpdateDate.ToString(@"dd.MM.yyyy")));
            //CreateMap<LeadInputModel, Lead>()
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => new City { Id = src.CityId }))
            //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture)));
            //CreateMap<LeadSearchInputModel, LeadSearchModel>()
            //    .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate == null ? null : (DateTime?)Convert.ToDateTime(src.BirthDate)))
            //    .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate == null ? null : (DateTime?)Convert.ToDateTime(src.RegistrationDate)));
        }
    }
}
