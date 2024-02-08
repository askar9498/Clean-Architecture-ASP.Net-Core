namespace Contract.Query;
public interface IGetAllProductsQueryHandler
{
    Task<IReadOnlyCollection<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken);
}