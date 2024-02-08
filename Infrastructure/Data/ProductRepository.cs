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
}