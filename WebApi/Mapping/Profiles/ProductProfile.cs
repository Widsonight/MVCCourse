using AutoMapper;
using CoreBusiness;
using WebApi.ViewModel;

namespace WebApi.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductView>().ReverseMap();
            CreateMap<Category, CategoryView>().ReverseMap();
            CreateMap<Transaction, TransactionsViewModel>().ReverseMap();
        }

    }
}
