using MediatR;

namespace Contract.Query.GetAllProduct;

public class GetAllProductsQuery : IRequest<IReadOnlyCollection<ProductDto>>
{
}