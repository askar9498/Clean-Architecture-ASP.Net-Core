namespace Contract.Query;

public interface IGetProductsWithUserIdQueryHandler
{
    Task<IReadOnlyCollection<ProductDto>> Handle(GetProductsWithUserIdQuery request, CancellationToken cancellationToken);
}