using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VirtusFitApi.DTO;
using VirtusFitApi.Models;

namespace VirtusFitApi.MapperProfiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CreateProductAction, ProductAction>();
            CreateMap<ProductAction, ProductActionDto>();
            CreateMap<CreateDietPlanAction, DietPlanAction>();
            CreateMap<DietPlanAction, DietPlanActionDto>();
            CreateMap<CreateProductInPlanAction, ProductInPlanAction>();
            CreateMap<ProductInPlanAction, ProductInPlanActionDto>();
            CreateMap<CreateSearchStringAction, SearchStringAction>();
            CreateMap<SearchStringAction, SearchStringActionDto>();
            CreateMap<CreateSearchValueAction, SearchValueAction>();
            CreateMap<SearchValueAction, SearchValueActionDto>();
            CreateMap<CreateBmiAction, BmiAction>();
            CreateMap<BmiAction, BmiActionDto>();
            CreateMap<CreateUserAccountAction, UserAccountAction>();
            CreateMap<UserAccountAction, UserAccountActionDto>();
        }
    }
}
