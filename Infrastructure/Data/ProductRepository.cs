using ApplicationService;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly MyDbContext _dbContext;

    public ProductRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddAsync(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IReadOnlyCollection<Product>> GetByUserIdAsync(int userId)
    {
        return await _dbContext.Products.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task DeleteAsync(int productId)
    {
        Product? product = await _dbContext.Products.FindAsync(productId);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Product existingProduct)
    {
        _dbContext.Entry(existingProduct).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}