using AutoMapper;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Drugs;
using Pharmacy.WebApi.ViewModels.Orders;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserCreateModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<IQueryable<User>, List<UserViewModel>>().ReverseMap();

            CreateMap<Drug, DrugCreateModel>().ReverseMap();
            CreateMap<Drug, DrugViewModel>().ReverseMap();
            CreateMap<IQueryable<Drug>, List<DrugViewModel>>().ReverseMap();

            CreateMap<Order, OrderCreateModel>().ReverseMap();
            //CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<IQueryable<Order>, List<OrderViewModel>>().ReverseMap();
            CreateMap<Order, OrderViewModel>()
                .ForMember(dto => dto.UserFullName,
                    expression => expression.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
                .ForMember(dto => dto.DrugName,
                    expression => expression.MapFrom(entity => entity.Drug.Name));
        }
    }
}
