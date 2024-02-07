using Domain;

namespace ApplicationService.Command.CreateProduct;

public interface IProductRepository
{
    Task<int> AddAsync(Product product);
}