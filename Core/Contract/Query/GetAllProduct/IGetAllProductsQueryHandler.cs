namespace Contract.Query.GetAllProduct;
public interface IGetAllProductsQueryHandler
{
    Task<IReadOnlyCollection<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken);
}