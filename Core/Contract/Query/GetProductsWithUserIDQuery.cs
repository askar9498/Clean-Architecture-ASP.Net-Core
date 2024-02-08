using MediatR;

namespace Contract.Query;

public class GetProductsWithUserIdQuery : IRequest<IReadOnlyCollection<ProductDto>>
{
    public int UserId { get; set; }
}