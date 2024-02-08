using AutoMapper;
using Contract.Query;
using Domain;

namespace ApplicationService.Query.GetProductsWithUserId;

public class GetProductsWithUserIdQueryHandler : IGetProductsWithUserIdQueryHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductsWithUserIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetProductsWithUserIdQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Product> products = await _productRepository.GetByUserIdAsync(request.UserId);
        List<ProductDto> productDtos = products.Select(MapToDto).ToList();

        return productDtos;
    }

    private ProductDto MapToDto(Product product)
    {
        return _mapper.Map<ProductDto>(product);
    }
}