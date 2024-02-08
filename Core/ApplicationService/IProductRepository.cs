using Domain;

namespace ApplicationService;

public interface IProductRepository
{
    Task<int> AddAsync(Product product);
    Task<IReadOnlyCollection<Product>> GetAllAsync();
    Task<IReadOnlyCollection<Product>> GetByUserIdAsync(int requestUserId);
    Task<Product?> GetByIdAsync(int requestProductId);
    Task DeleteAsync(int requestProductId);
    Task UpdateAsync(Product existProduct);
}