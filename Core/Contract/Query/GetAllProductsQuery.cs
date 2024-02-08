using MediatR;

namespace Contract.Query;

public class GetAllProductsQuery : IRequest<IReadOnlyCollection<ProductDto>>
{
}