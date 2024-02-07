using ApplicationService.Command.CreateProduct;
using Domain;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    public Task<int> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }
}