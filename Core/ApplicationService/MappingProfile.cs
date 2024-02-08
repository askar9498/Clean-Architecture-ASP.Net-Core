using AutoMapper;
using Contract.Command;
using Contract.Query;
using Domain;

namespace ApplicationService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>()
            .ForMember(x=>x.Id,opt=>opt.Ignore());
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(x=>x.Id,opt=>opt.Ignore());
    }
}