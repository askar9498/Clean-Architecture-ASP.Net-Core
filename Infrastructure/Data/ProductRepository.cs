using ApplicationService;
using Domain;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    public Task<int> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Product>> GetByUserIdAsync(int requestUserId)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(int requestProductId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int requestProductId)
    {
        throw new NotImplementedException();
    }
}