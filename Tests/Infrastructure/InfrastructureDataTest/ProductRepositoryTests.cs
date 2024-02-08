using Domain;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureDataTest;

public class ProductRepositoryTests
{
    private ProductRepository _testClass;
    private MyDbContext _dbContext;
    public ProductRepositoryTests()
    {
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb1")
            .Options;
        _dbContext = new MyDbContext(options);
        _testClass = new ProductRepository(_dbContext);
    }

    [Fact]
    public async Task AddAsync_ValidProduct_ProductAddedSuccessfully()
    {
        // Arrange
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb22")
            .Options;
        _dbContext = new MyDbContext(options);
        _testClass = new ProductRepository(_dbContext);
        Product product = new(545094031, "TestValue1766709633", false, "a@a.com", "+989179979498", DateTime.UtcNow);

        // Act
        int result = await _testClass.AddAsync(product);

        // Assert
        result.Should().NotBe(null);
        result.Should().Be(1);
    }


    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts()
    {
        // Arrange
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb3")
            .Options;
        _dbContext = new MyDbContext(options);
        _testClass = new ProductRepository(_dbContext);

        await _dbContext.Products.AddRangeAsync(
            new Product(11, "Product1", true, "email1@test.com", "1234567890", DateTime.UtcNow),
            new Product(11, "Product2", false, "email2@test.com", "0987654321", DateTime.UtcNow),
            new Product(22, "Product3", true, "email3@test.com", "1357902468", DateTime.UtcNow)
        );
        await _dbContext.SaveChangesAsync();

        // Act
        IReadOnlyCollection<Product> products = await _testClass.GetAllAsync();

        // Assert
        products.Should().NotBeNull();
        products.Count.Should().Be(3);
    }

    [Fact]
    public async Task GetByUserIdAsync_ReturnsProductsByUserId()
    {
        // Arrange
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb4")
            .Options;
        _dbContext = new MyDbContext(options);
        _testClass = new ProductRepository(_dbContext);

        await _dbContext.Products.AddRangeAsync(
            new Product(1, "Product1", true, "email1@test.com", "1234567890", DateTime.UtcNow),
            new Product(1, "Product2", false, "email2@test.com", "0987654321", DateTime.UtcNow),
            new Product(2, "Product3", true, "email3@test.com", "1357902468", DateTime.UtcNow)
        );
        await _dbContext.SaveChangesAsync();

        // Act
        IReadOnlyCollection<Product> productsForUser1 = await _testClass.GetByUserIdAsync(1);
        IReadOnlyCollection<Product> productsForUser2 = await _testClass.GetByUserIdAsync(2);

        // Assert
        productsForUser1.Should().NotBeNull();
        productsForUser1.Count.Should().Be(2);
        productsForUser2.Should().NotBeNull();
        productsForUser2.Count.Should().Be(1);
    }

    [Fact]
    public async Task GetByIdAsync_ValidProductId_ReturnsProduct()
    {
        // Arrange
        DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb2")
            .Options;
        _dbContext = new MyDbContext(options);
        _testClass = new ProductRepository(_dbContext);
        Product addedProduct = new(545094031, "TestValue1766709633", false, "a@a.com", "+989179979498", DateTime.UtcNow);

        // Act
        await _testClass.AddAsync(addedProduct);
        int productId = await _dbContext.Products
            .Select(p => p.Id)
            .FirstOrDefaultAsync();

        // Act
        Product? product = await _testClass.GetByIdAsync(productId);

        // Assert
        product.Should().NotBeNull();
        product!.Id.Should().Be(productId);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidProductId_ReturnsNull()
    {
        // Arrange - Ensure no product with this ID exists
        int invalidProductId = -1;

        // Act
        Product? product = await _testClass.GetByIdAsync(invalidProductId);

        // Assert
        product.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ValidProductId_ProductDeletedSuccessfully()
    {
        // Arrange
        Product product = new(545094031, "TestValue1766709633", false, "a@a.com", "+989179979498", DateTime.UtcNow);
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        // Act
        await _testClass.DeleteAsync(product.Id);

        // Assert
        Product? deletedProduct = await _dbContext.Products.FindAsync(product.Id);
        deletedProduct.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ValidProduct_ProductUpdatedSuccessfully()
    {
        // Arrange
        Product product = new(545094031, "TestValue1766709633", false, "a@a.com", "+989179979498", DateTime.UtcNow);
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        // Modify product details
        product.Update(new Product(product.UserId, product.Name, false, "UpdatedEmail@test.com", product.ManufacturePhone, DateTime.Now.AddDays(-10)));

        // Act
        await _testClass.UpdateAsync(product);

        // Assert
        Product? updatedProduct = await _dbContext.Products.FindAsync(product.Id);
        updatedProduct.Should().NotBeNull();
        updatedProduct!.Name.Should().Be(product.Name);
        updatedProduct.IsAvailable.Should().BeFalse();
        updatedProduct.ManufactureEmail.Should().Be(product.ManufactureEmail);
    }
}