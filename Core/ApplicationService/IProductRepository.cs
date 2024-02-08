using Domain;

namespace ApplicationService;

public interface IProductRepository
{
    Task<int> AddAsync(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
}