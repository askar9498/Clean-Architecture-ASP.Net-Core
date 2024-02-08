using AutoMapper;
using Contract.Query;
using Domain;

namespace ApplicationService.Query.GetAllProduct;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}