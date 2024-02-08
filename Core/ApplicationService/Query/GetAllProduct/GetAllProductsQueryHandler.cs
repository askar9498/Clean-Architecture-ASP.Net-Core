using AutoMapper;
using Contract.Query;
using Domain;

namespace ApplicationService.Query.GetAllProduct;

public class GetAllProductsQueryHandler : IGetAllProductsQueryHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        IReadOnlyCollection<Product> products = await _productRepository.GetAllAsync();

        List<ProductDto> productDtos = products.Select(MapToDto).ToList();

        return productDtos;
    }

    private ProductDto MapToDto(Product product)
    {
        return _mapper.Map<ProductDto>(product);
    }
}